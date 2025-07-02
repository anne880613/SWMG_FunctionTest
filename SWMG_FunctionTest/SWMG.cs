using SSCApiCLR;
using SSCApiCLR.EcApiCLR;
using System;
using System.Security.AccessControl;
using System.Text;

namespace SWMG_FunctionTest
{
    public static class SWMG
    {
        private static SSCApi _device;//一個device可以有128軸
        static private IntPtr _DeviceHandle = IntPtr.Zero;


        private static string _deviceName = "Testdevice";
        public static bool IsOpened { get; private set; } = false;

        public static string InitialErrorMessage = string.Empty;
        private static int errNum;

        private static CoreMotion _device_cm;
        private static CoreMotionStatus _CmStatus;


        public static void OpenDevice()
        {
            IsOpened = false;
            InitialErrorMessage = string.Empty;
            try
            {
                _device = new SSCApi();
                // 建立裝置（可根據實際安裝位置調整路徑）
                errNum = _device.CreateDevice("C:\\Program Files\\MotionSoftware\\SWM-G\\",
                    DeviceType.DeviceTypeNormal,
                    0xFFFFFFFF);
                if (errNum != ErrorCode.None)
                {
                    string strTemp = "Create Device Numbers Failed With Error Code[" + errNum + "]";
                    InitialErrorMessage = strTemp + "\r\nError Message:" + SSCApi.ErrorToString(errNum);
                    MessageBox.Show(InitialErrorMessage);
                    return;
                }

                
                _deviceName = "Testdevice";
                _device.SetDeviceName(_deviceName);

                // Communicate the device
                errNum = _device.StartCommunication(5000);
                if (errNum != ErrorCode.None)
                {
                    string strTemp = "Create Device Numbers Failed With Error Code[" + errNum + "]";
                    InitialErrorMessage = strTemp + "\r\nError Message:" + SSCApi.ErrorToString(errNum);
                    return;
                }
                
                DevicesInfo devInfo = new DevicesInfo();
                _device.GetAllDevices(ref devInfo);

                if (devInfo.Count == 0)
                {
                    InitialErrorMessage = "未偵測到任何三菱 EtherCAT 裝置。";
                    return;
                }

                //初始化控制物件
                _device_cm = new CoreMotion(SWMG.GetDevice());
                _CmStatus = new CoreMotionStatus();

                IsOpened = true;
            }
            catch (Exception ex)
            {
                InitialErrorMessage = "開啟三菱裝置時發生例外：" + ex.ToString();
            }
        }

        public static SSCApi GetDevice()
        {
            return _device;
        }
        static public void GetDeviceHandle(ref IntPtr deviceHandle)
        {
            deviceHandle = _DeviceHandle;//沒東西
            //如果有很多個device就可以不用這個函示吧
        }
        static public void OpenAxisAndReturnAxisHandle(ushort slaveID, ref IntPtr axisHandle)
        {
            //分別SlaveID跟串接順序
          
        }
        public static CoreMotion GetCoreMotion()
        {
            return _device_cm;
        }

        public static CoreMotionStatus GetCoreMotionStatus()
        {
            return _CmStatus;
        }


        public static void SetDeviceName(string deviceName)
        {
            _device.SetDeviceName(deviceName);
        }

        public static void CloseDevice()
        {
            try
            {
                errNum = _device.StopCommunication();
                if (errNum != ErrorCode.None)
                {
                    throw new Exception($"Device : [{_device.GetDeviceID}] Stop Communucated Failed With Error Code: [0x" + SSCApi.ErrorToString(errNum) + "]");
                }


                errNum = _device.CloseDevice();
                if (errNum != ErrorCode.None)
                {
                    throw new Exception($"Device : [{_device.GetDeviceID}] Close Failed With Error Code: [0x" + SSCApi.ErrorToString(errNum) + "]");
                }

                _device.Dispose();
                IsOpened = false;
            }
            catch (Exception ex)
            {
                InitialErrorMessage = "關閉三菱裝置時發生錯誤：" + ex.Message;
            }
        }
    }
}
