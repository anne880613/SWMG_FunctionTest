

namespace SWMG_FunctionTest
{
    public class IOManager
    {
        public enum Module
        {
            SWMG
        }
        public struct IOInfo
        {
            public Module Module;
            public int Port;
            public int Number;
            public string Name;
            public Enum IO;
            public bool Pass;
        }
        public enum DI 
        { 
            testDI1,
            testDI2
        }
        public enum DO
        {
            testDO1,
            testDO2,
            testDO3,
        }
        public enum AI { }
        public enum AO { }
        public static bool IsValid { private set; get; }

        private static IniFile? _Ini;
        private static long _DIClickTempResetTimer = 1000;
        private readonly static IOBase[] _IOModule = new IOBase[Enum.GetNames(typeof(Module)).Length]; 
        private readonly static bool[] _DITempArray_Click = new bool[Enum.GetNames(typeof(DI)).Length];
        private readonly static IOInfo[] _DIArray = new IOInfo[Enum.GetNames(typeof(DI)).Length];
        private readonly static IOInfo[] _DOArray = new IOInfo[Enum.GetNames(typeof(DO)).Length];
        private readonly static IOInfo[] _AIArray = new IOInfo[Enum.GetNames(typeof(AI)).Length];
        private readonly static IOInfo[] _AOArray = new IOInfo[Enum.GetNames(typeof(AO)).Length];

        public static void Initial(IniFile ini)
        {
            try
            {
                _Ini = ini;
                IsValid = true;
                for (int i = 0; i < _DIArray.Length; i++) { _DIArray[i].IO = (DI)i; }
                for (int i = 0; i < _DOArray.Length; i++) { _DOArray[i].IO = (DO)i; }
                for (int i = 0; i < _AIArray.Length; i++) { _AIArray[i].IO = (AI)i; }
                for (int i = 0; i < _AOArray.Length; i++) { _AOArray[i].IO = (AO)i; }
                ReadIOInfo();
                for (int i = 0; i < Enum.GetNames(typeof(Module)).Length; i++)
                {

                retry:
                    if (_Ini.ReadValue("Module", ((Module)i).ToString(), typeof(bool), false))
                        _IOModule[i] = new IOSWMG();

                    if (_IOModule[i] == null)
                        continue;
                    if (!_IOModule[i].IsValid)
                    {
                        IsValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                IsValid = false;
                MessageBox.Show(ex.Message, "IOManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static IOInfo[] GetIOInfoArray(Type type)
        {
            if(type == typeof(DI))
                return (IOInfo[])_DIArray.Clone();
            else if (type == typeof(DO)) 
                return (IOInfo[])_DOArray.Clone();
            else if (type == typeof(AI)) 
                return (IOInfo[])_AIArray.Clone();
            else if (type == typeof(AO))
                return (IOInfo[])_AOArray.Clone();
            else
                throw new Exception("Wrong IO type.");
        }

        public static void SetIOInfo(DI io, IOInfo info){ _DIArray[(int)io] = info; }
        public static void SetIOInfo(DO io, IOInfo info){ _DOArray[(int)io] = info; }
        public static void SetIOInfo(AI io, IOInfo info){ _AIArray[(int)io] = info; }
        public static void SetIOInfo(AO io, IOInfo info){ _AOArray[(int)io] = info; }


        private static void ReadIOInfo()
        {
            string tempEnum = string.Empty;
            try
            {
                Type[] io_type = [typeof(DI), typeof(DO), typeof(AI), typeof(AO)];
                for (int i = 0; i < io_type.Length; i++)
                {
                    for (int j = 0; j < Enum.GetNames(io_type[i]).Length; j++)
                    {
                        IOInfo[] io_array = GetIOInfoArray(io_type[i]);
                        tempEnum = io_array[j].IO.ToString();
                        if (_Ini is null) throw new Exception("Empty ini file.");
                        string info = _Ini.ReadValue(io_type[i].Name, io_array[j].IO.ToString(), typeof(string), string.Empty);
                        if(info.Length > 0)
                        {
                            var info_seperate = info.Split(",");
                            IOInfo temp = new()
                            {
                                IO = io_array[j].IO,
                                Name = info_seperate[0],
                                Port = int.Parse(info_seperate[1]),
                                Number = int.Parse(info_seperate[2]),
                                Module = (Module)Enum.Parse(typeof(Module), info_seperate[3])
                            };
                            if (io_type[i] == typeof(DI))
                                SetIOInfo((DI)io_array[j].IO, temp);
                            else if (io_type[i] == typeof(DO))
                                SetIOInfo((DO)io_array[j].IO, temp);
                            else if (io_type[i] == typeof(AI))
                                SetIOInfo((AI)io_array[j].IO, temp);
                            else
                                SetIOInfo((AO)io_array[j].IO, temp);
                        }
                        
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(tempEnum + " : " + ex.Message);
            }
        }

        public static void WriteIOInfo()
        {
            Type[] io_type = [typeof(DI), typeof(DO), typeof(AI), typeof(AO)];
            for(int i = 0; i < io_type.Length; i++)
            {
                for(int j = 0; j < Enum.GetNames(io_type[i]).Length; j++)
                {
                    IOInfo[] io_array = GetIOInfoArray(io_type[i]);
                    string data = io_array[j].Name + "," + io_array[j].Port.ToString() + "," + io_array[j].Number.ToString() + "," + io_array[j].Module.ToString();
                    if (_Ini is null) throw new Exception("Empty ini file.");
                    _Ini.WriteValue(io_type[i].Name, io_array[j].IO.ToString(), data);
                }
            }
        }

        public static bool GetDI(DI digitalInput, bool defautValue, out string error)
        {
            error = $"{_DIArray[(int)digitalInput].Name}({_DIArray[(int)digitalInput].Port}X{_DIArray[(int)digitalInput].Number.ToString().PadLeft(2,'0')}):{(defautValue ? "ON" : "OFF")}";
            Module module = _DIArray[(int)digitalInput].Module;
            if (_DIArray[(int)digitalInput].Pass || !IsValid)
                return defautValue;
            int port = _DIArray[(int)digitalInput].Port;
            int number = _DIArray[(int)digitalInput].Number;
            var result = _IOModule[(int)module].GetDI(port, number);
            error = $"{_DIArray[(int)digitalInput].Name}({_DIArray[(int)digitalInput].Port}X{_DIArray[(int)digitalInput].Number.ToString().PadLeft(2, '0')}):{(result ? "ON" : "OFF")}";
            return result;
        }

        public static bool GetDIClick(DI digitalInput, bool defautValue)
        {
            //從第一次使用GetDIClick()後，0.3秒後訊號自動歸零
            if (Environment.TickCount - _DIClickTempResetTimer > 300)
            {
                _DITempArray_Click[(int)digitalInput] = false;
                _DIClickTempResetTimer = Environment.TickCount;
            }
            bool result = GetDI(digitalInput, defautValue, out string error);
            if (result)
                _DITempArray_Click[(int)digitalInput] = result;
            else
            {
                if (_DITempArray_Click[(int)digitalInput] != result)
                {
                    _DITempArray_Click[(int)digitalInput] = false;
                    return true;
                }
            }
            return false;
        }

        public static bool GetDO(DO digitalOutput)
        {
            Module module = _DOArray[(int)digitalOutput].Module;
            if (!IsValid)
                return false;
            int port = _DOArray[(int)digitalOutput].Port;
            int number = _DOArray[(int)digitalOutput].Number;
            var result = _IOModule[(int)module].GetDO(port, number);
            return result;
        }
        public static void SetDO(DO digitalOutput, bool onOff)
        {
            Module module = _DOArray[(int)digitalOutput].Module;
            if (!IsValid)
                return;
            int port = _DOArray[(int)digitalOutput].Port;
            int number = _DOArray[(int)digitalOutput].Number;
            _IOModule[(int)module].SetDO(port, number, onOff);
        }
        public static IOInfo[] GetDIPassList()
        {
            return _DIArray.Where(x => x.Pass).ToArray();
        }
        public static void SetDIPass(DI digitalInput)
        {
            _DIArray[(int)digitalInput].Pass = true;
        }
        public static void ResetDIPass(DI digitalInput)
        {
            _DIArray[(int)digitalInput].Pass = false;
        }
        public static void Close()
        {
            for (int i = 0; i < Enum.GetNames(typeof(Module)).Length; i++)
                _IOModule[i]?.Close();
        }
    }
}
