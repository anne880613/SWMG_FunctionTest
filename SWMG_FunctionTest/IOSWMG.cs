using SSCApiCLR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SWMG_FunctionTest
{
    class IOSWMG : IOBase
    {
        private byte[] _DIPortStatus;
        private byte[] _DOPortStatus;
        private byte[] _UserSetDOPortStatus;
        private byte[] _AIPortStatus;
        private byte[] _AOPortStatus;
        private byte[] _UserSetAOPortStatus;
        private bool _Run;
        //digital unput, digital output 第0 port 為軸卡上的 local io
        //di port0 : 4 channels
        //do port0 : 8 channels
        private static Io _device_Io;
        private System.Timers.Timer _IoTimer;


        public IOSWMG()
        {
            try
            {
                InitialErrorMessage = string.Empty;
                IsValid = false;

                if(!SWMG.IsOpened)
                    SWMG.OpenDevice();

                _device_Io = new Io(SWMG.GetDevice());//確保拿的到sscApi 非NULL

                InitialIO();

                InitialErrorMessage = SWMG.InitialErrorMessage;
                if(InitialErrorMessage == string.Empty)
                {
                    IsValid = true;
                    _Run = true;

                    for (int port = 0; port < _DOPortStatus.Length; port++)
                    {
                        //讀取Output目前狀態，保持點位
                        if (_device_Io == null) return;
                        int result = _device_Io.GetOutByte(port, ref _UserSetDOPortStatus[port]);
                    }

                    //Thread thread = new(RefreshIO)
                    //{
                    //    Priority = ThreadPriority.Highest,
                    //    Name = "IO"
                    //};
                    // 啟動 Timer 取代 Thread
                    _IoTimer = new System.Timers.Timer(100); // 100ms 更新一次，可自行調整
                    _IoTimer.Elapsed += IoTimer_Elapsed;
                    _IoTimer.AutoReset = true;
                    _IoTimer.Start();
                }
            }
            catch 
            { 
                IsValid = false; 
            }
        }

        private void IoTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!_Run || _device_Io == null) return;

            try
            {
                lock (this) // 加入 lock，避免多執行緒同時進入
                {
                    RefreshIO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("IO Refresh Error: " + ex.Message);
            }
        }

        public void InitialIO()
        {
            int result;
            byte[] inData = new byte[Constants.MaxIoInSize];
            byte[] outData = new byte[Constants.MaxIoOutSize];
            int AnalogIN = 0;
            int AnalogOUT = 0;

            //初始化
            if (_device_Io == null) return;
            result = _device_Io.GetInBytes(0, Constants.MaxIoInSize, ref inData);//每一byte存一格
            if(result != ErrorCode.None)
                InitialErrorMessage = "Get Property Failed With Error Code: [" + result + "]" + "\r\nError Message:" + result.ToString();
            result = _device_Io.GetOutBytes(0, Constants.MaxIoOutSize, ref outData);
            if(result != ErrorCode.None)
                InitialErrorMessage = "Get Property Failed With Error Code: [" + result + "]" + "\r\nError Message:" + result.ToString();
            result = _device_Io.GetInAnalogDataInt(0x00, ref AnalogIN);
            if (result != ErrorCode.None)
                InitialErrorMessage = "Get Property Failed With Error Code: [" + result + "]" + "\r\nError Message:" + result.ToString();
            result = _device_Io.GetOutAnalogDataInt(0x00, ref AnalogOUT);
            if (result != ErrorCode.None)
                InitialErrorMessage = "Get Property Failed With Error Code: [" + result + "]" + "\r\nError Message:" + result.ToString();

            _DIPortStatus = new byte[8000];
            _DOPortStatus = new byte[8000];
            _UserSetDOPortStatus = new byte[8000];
            //_AIPortStatus = new byte[8000];
            //_AOPortStatus = new byte[8000];
            _UserSetAOPortStatus = new byte[8000];
        }
        public static Io GetIo()
        {
            return _device_Io;
        }

        public void RefreshIO()
        {
            if (!SWMG.IsOpened || _device_Io == null)
                return;

            while (_Run && IsValid)
            {
                if (_device_Io == null) return;
                int result;
                //refrsh Input
                result = _device_Io.GetInBytes(0, Constants.MaxIoInSize, ref _DIPortStatus);//每一byte存一格
                if (result != ErrorCode.None)
                    InitialErrorMessage = "Get Property Failed With Error Code: [" + result + "]" + "\r\nError Message:" + result.ToString();
                result = _device_Io.GetOutBytes(0, Constants.MaxIoOutSize, ref _DOPortStatus);
                if (result != ErrorCode.None)
                    InitialErrorMessage = "Get Property Failed With Error Code: [" + result + "]" + "\r\nError Message:" + result.ToString();
                    InitialErrorMessage = "Get Property Failed With Error Code: [" + result + "]" + "\r\nError Message:" + result.ToString();

                //refresh Output
                for (int port = 0; port < _DOPortStatus.Length; port++)
                {
                    if (_DOPortStatus[port] != _UserSetDOPortStatus[port])
                    {
                        result = _device_Io.SetOutBytes(0, 8000, _UserSetDOPortStatus);
                    }
                }
                //AO 先PASS

            }
        }

        public override void Close()
        {
            _Run = false;
            _IoTimer?.Stop();
            _IoTimer?.Dispose();
            _IoTimer = null;
            _device_Io?.Dispose();
            _device_Io = null; // Dispose 後設為 null
        }

        public override bool GetDI(int port, int number)
        {
            if (port > _DIPortStatus.Length - 1 || port < 0)
                return false;
            return (_DIPortStatus[port] & (0x01 << number)) > 0;
        }

        public override bool GetDO(int port, int number)
        {
            if (port > _DOPortStatus.Length - 1 || port < 0)
                return false;
            return (_DOPortStatus[port] & (0x01 << number)) > 0;
        }

        public override void SetDO(int port, int number, bool onOff)
        {
            if (port > _UserSetDOPortStatus.Length - 1 || port < 0)
                return;

            if (onOff)
                _UserSetDOPortStatus[port] |= (byte)(0x01 << number);
            else
            {
                var v = 0x01 << number;
                _UserSetDOPortStatus[port] &= (byte)(~v);
                //假設 number = 2：
                //v = 00000100 → ~v = 11111011
                //原值：_UserSetDOPortStatus[port] = 00000110
                //結果：00000110 & 11111011 = 00000010（第 2 bit 被清成 0）
            }
        }

        public override int GetAI(int module, int number)
        {
            throw new NotImplementedException();
        }

        public override void SetAO(int module, int number)
        {
            throw new NotImplementedException();
        }
    }
}
