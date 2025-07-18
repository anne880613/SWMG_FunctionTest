
using SSCApiCLR;
using System.ComponentModel;
using static SWMG_FunctionTest.MotionManager;
using static SWMG_FunctionTest.MotionSWMG;

namespace SWMG_FunctionTest
{
    public class MotionManager
    {
        public struct Speed
        {
            public double StartSpeed;
            public double MaxSpeed;
            public double EndSpeed;
            public double Acceleration;
            public double Deceleration;
        }

        enum Module
        {
            SWMG
        }
        public enum Axis
        {
            SampleX,
            SampleY, 
            SampleZ
        }

        private static IniFile? _MotionIni;
        private static readonly MotionBase[] _Axes = new MotionBase[Enum.GetNames(typeof(Axis)).Length];
        private static readonly Speed[] _Speeds = new Speed[Enum.GetNames(typeof(Axis)).Length];
        public static bool IsValid { private set; get; }
        public static int MachineSpeedPercentage {  get; private set; }

        public static void Initial(IniFile ini)
        {
        retry:
            try
            {
                IsValid = true;
                _MotionIni = ini;
                ReadMotionInfo();
            }
            catch (Exception ex)
            {
                IsValid = false;
                if (MessageBox.Show(ex.Message, "MotionManager", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    goto retry;
            }
        }

        public static Speed GetSpeed(Axis axis)
        {
            Speed speed = new()
            {
                StartSpeed = _Speeds[(int)axis].StartSpeed,
                MaxSpeed = _Speeds[(int)axis].MaxSpeed,
                EndSpeed = _Speeds[(int)axis].EndSpeed,
                Acceleration = _Speeds[(int)axis].Acceleration,
                Deceleration = _Speeds[(int)axis].Deceleration,
            };
            return speed;
        }
        private static Speed ChangeSpeedByPercentage(Speed speed, int specificPercentage)
        {
            speed.StartSpeed *= MachineSpeedPercentage / 100D * specificPercentage / 100D;
            speed.MaxSpeed *= MachineSpeedPercentage / 100D * specificPercentage / 100D;
            speed.EndSpeed *= MachineSpeedPercentage / 100D * specificPercentage / 100D;
            speed.Acceleration *= MachineSpeedPercentage / 100D * specificPercentage / 100D;
            speed.Deceleration *= MachineSpeedPercentage / 100D * specificPercentage / 100D;
            return speed;
        }

        public static void SetMachineSpeedPercentage(int percentage)
        {
            MachineSpeedPercentage = percentage;
        }

        public static MotionBase GetMotionBase(int axis)
        {
            return _Axes[(int)axis];
        }

        public static void ReadMotionInfo()
        {
            for (int i = 0; i < Enum.GetNames(typeof(Axis)).Length; i++)
            {
                string axisName = ((Axis)i).ToString();
                
                string moduleString = _MotionIni.ReadValue(axisName, "Module", typeof(string), (Module.SWMG).ToString());
                Module module = (Module)Enum.Parse(typeof(Module), moduleString);

                int index = _MotionIni.ReadValue(axisName, "Index", typeof(int), -1);
            retry:
                if (module == Module.SWMG)
                    _Axes[i] = new MotionSWMG(index);
                if (!_Axes[i].IsValid)
                {
                    IsValid = false;
                }

                string name = _MotionIni.ReadValue(axisName, "Name", typeof(string), "");
                SetName((Axis)i, name);

                bool directionReverseEnabled = _MotionIni.ReadValue(axisName, "DirectionReverseEnabled", typeof(bool), false);
                EnableDirectionReverse((Axis)i, directionReverseEnabled);

                bool softLimitEnabled = _MotionIni.ReadValue(axisName, "SoftLimitEnabled", typeof(bool), false);
                EnableSoftLimit((Axis)i, softLimitEnabled);

                int ppu = _MotionIni.ReadValue(axisName, "PPU", typeof(int), 1);
                SetPPU((Axis)i, (uint)ppu);

                int ppuDenominator = _MotionIni.ReadValue(axisName, "PPUDenominator", typeof(int), 1);
                SetPPUDenominator((Axis)i, (uint)ppuDenominator);

                double backlash = _MotionIni.ReadValue(axisName, "Backlash", typeof(double), 0);
                SetBacklash((Axis)i, backlash);

                string homeMode = _MotionIni.ReadValue(axisName, "HomeMode", typeof(string), Config.HomeType.HS);
                if (!Enum.GetNames(typeof(Config.HomeType)).Contains(homeMode.ToString()))
                    homeMode = HomeState.ZPulseSearch.ToString();
                double creepingSpeed = _MotionIni.ReadValue(axisName, "CreepingSpeed", typeof(double), 0.1);
                double offsetDistance = _MotionIni.ReadValue(axisName, "OffsetDistance", typeof(double), 0);
                bool homeDirection = _MotionIni.ReadValue(axisName, "HomeDirection", typeof(bool), true);
                SetHomeMode((Axis)i, (Config.HomeType)Enum.Parse(typeof(Config.HomeType), homeMode), creepingSpeed, offsetDistance, homeDirection);

                Speed speed = new()
                {
                    StartSpeed = _MotionIni.ReadValue(axisName, "StartSpeed", typeof(double), 0),
                    MaxSpeed = _MotionIni.ReadValue(axisName, "MaxSpeed", typeof(double), 0),
                    EndSpeed = _MotionIni.ReadValue(axisName, "EndSpeed", typeof(double), 0),
                    Acceleration = _MotionIni.ReadValue(axisName, "Acceleration", typeof(double), 0),
                    Deceleration = _MotionIni.ReadValue(axisName, "Deceleration", typeof(double), 0)
                };
                SetSpeed((Axis)i, speed);
            }
        }


        public static void WriteMotionInfo()
        {
            for (int i = 0; i < Enum.GetNames(typeof(Axis)).Length; i++)
            {
                string axisName = ((Axis)i).ToString();
                _MotionIni.WriteValue(axisName, "DirectionReverseEnabled", DirectionReverseEnabled((Axis)i).ToString());
                _MotionIni.WriteValue(axisName, "SoftLimitEnabled", SoftLimitEnabled((Axis)i).ToString());
                _MotionIni.WriteValue(axisName, "HomeMode", GetHomeMode((Axis)i).ToString());

                GetSoftLimit((Axis)i, out double limN, out double limP);
                _MotionIni.WriteValue(axisName, "LimitNegative", limN.ToString());
                _MotionIni.WriteValue(axisName, "LimitPositive", limP.ToString());

                _MotionIni.WriteValue(axisName, "Name", GetName((Axis)i).ToString());
                _MotionIni.WriteValue(axisName, "Index", GetIndex((Axis)i).ToString());
                _MotionIni.WriteValue(axisName, "HomeDirection", GetHomeDirection((Axis)i).ToString());
                _MotionIni.WriteValue(axisName, "CreepingSpeed", GetHomeCreepingSpeed((Axis)i).ToString());
                _MotionIni.WriteValue(axisName, "OffsetDistance", GetHomeOffsetDistance((Axis)i).ToString());
                _MotionIni.WriteValue(axisName, "PPU", GetPPU((Axis)i).ToString());
                _MotionIni.WriteValue(axisName, "PPUDenominator", GetPPUDenominator((Axis)i).ToString());
                _MotionIni.WriteValue(axisName, "Backlash", GetBacklash((Axis)i).ToString());

                _MotionIni.WriteValue(axisName, "StartSpeed", GetSpeed((Axis)i).StartSpeed.ToString());
                _MotionIni.WriteValue(axisName, "MaxSpeed", GetSpeed((Axis)i).MaxSpeed.ToString());
                _MotionIni.WriteValue(axisName, "EndSpeed", GetSpeed((Axis)i).EndSpeed.ToString());
                _MotionIni.WriteValue(axisName, "Acceleration", GetSpeed((Axis)i).Acceleration.ToString());
                _MotionIni.WriteValue(axisName, "Deceleration", GetSpeed((Axis)i).Deceleration.ToString());
            }
        }

        public static void SetSpeed(Axis axis, Speed speed)
        {
            _Speeds[(int)axis] = speed;
        }
        public static void Stop(Axis axis)
        {
            _Axes[(int)axis].Stop();
        }
        public static void StopAll ()
        {
            //if (SIMULATE)
            //    return;
            for (int i = 0; i < _Axes.Length; i++)
                _Axes[i].Stop();
        }
        public static bool IsHomeAttained(Axis axis, bool defaultValue, out string error)
        {
            error = "Axis : " + axis.ToString() + (defaultValue ? " home attained" : " not home attained.");
            if (!_Axes[(int)axis].IsValid)
                return defaultValue;
            return _Axes[(int)axis].IsHomeAttained();
        }

        public static void Home(Axis axis, int specificPercentage = 100)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            if (!CheckMotionRule(axis))
                return;
            var speed = ChangeSpeedByPercentage(GetSpeed(axis), specificPercentage);
            _Axes[(int)axis].Home(speed.MaxSpeed, speed.Acceleration, speed.Deceleration);
        }
        public static void AbsMove(Axis axis, double position, int specificPercentage = 100)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            if (!CheckMotionRule(axis))
                throw new Exception("Iliegal Move.");

            var speed = ChangeSpeedByPercentage(GetSpeed(axis), specificPercentage);
            _Axes[(int)axis].AbsMove(position, speed.MaxSpeed, speed.Acceleration, speed.Deceleration);
        }

        public static void RelMove(Axis axis, double distance, int specificPercentage = 100)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            if (!CheckMotionRule(axis))
                throw new Exception("Iliegal Move.");

            var speed = ChangeSpeedByPercentage(GetSpeed(axis), specificPercentage);
            _Axes[(int)axis].RelMove(distance, speed.MaxSpeed, speed.Acceleration, speed.Deceleration);
        }

        public static void StartJog(Axis axis, bool direction, int specificPercentage = 100)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            if (!CheckMotionRule(axis))
                return;

            var speed = ChangeSpeedByPercentage(GetSpeed(axis), specificPercentage);
            _Axes[(int)axis].JogMove(direction, speed.MaxSpeed, speed.Acceleration, speed.Deceleration);
        }

        //public static nint DirectAbsMove(int[] axes, double speed, double acc, double dec, double[] target, int speedPercentage = 100)
        //{
        //    nint groupHandle = CreatAndCloseNonUseGroup(axes);
        //    nint[] axesHandle = axes.Select(GetAxisHandle).ToArray();
        //    Array.Sort(OrderByAxisEnum(axes), target);
        //    speed *= MachineSpeedPercentage / 100D * speedPercentage / 100D;
        //    acc *= MachineSpeedPercentage / 100D * speedPercentage / 100D;
        //    dec *= MachineSpeedPercentage / 100D * speedPercentage / 100D;
        //    return MotionSWMG.DirectAbsMove(groupHandle, speed, acc, dec, false, target);
        //}

        //public static nint LinearAbsMove(int[] axes, double speed, double acc, double dec, double[] target, int speedPercentage = 100)
        //{
        //    nint groupHandle = CreatAndCloseNonUseGroup(axes);
        //    Array.Sort(OrderByAxisEnum(axes), target);
        //    speed *= MachineSpeedPercentage / 100D * speedPercentage / 100D;
        //    acc *= MachineSpeedPercentage / 100D * speedPercentage / 100D;
        //    dec *= MachineSpeedPercentage / 100D * speedPercentage / 100D;
        //    return MotionSWMG.LinearAbsMove(groupHandle, speed, acc, dec, false, target);
        //}

        //private static nint CreatAndCloseNonUseGroup(int[] axes)
        //{
        //    //nint[] temp = new nint[_AdvantechGroup.Count];
        //    //_AdvantechGroup.CopyTo(temp);
        //    //foreach (var item in temp)
        //    //{
        //    //    var state = MotionPCI1203.GetGroupState(item);
        //    //    if (state == GroupState.STA_Gp_Ready || state == GroupState.STA_Gp_Disable)
        //    //        MotionPCI1203.CloseGroup(item);
        //    //    _AdvantechGroup.Remove(item);
        //    //}
        //    //_AdvantechGroup.Add(MotionPCI1203.SetGroup(axes.Select(GetAxisHandle).ToArray()));
        //    //return _AdvantechGroup[^1];
        //    return 0;
        //}


        //private static int[] OrderByAxisEnum(int[] axes)
        //{
        //    int[] enumOrder = new int[axes.Length];
        //    for (int i = 0; i < axes.Length; i++)
        //        enumOrder[i] = (int)axes[i];
        //    return enumOrder;
        //}


        //public static void CloseGroup(nint groupHandle)
        //{
        //    //MotionPCI1203.CloseGroup(groupHandle);
        //}

        public static bool IsStopped(Axis axis, bool defalutValue, out string error)
        {
            error = string.Empty;
            if (!_Axes[(int)axis].IsValid)//是否有報錯
                return defalutValue;
            bool result = _Axes[(int)axis].IsStopped();
            error = "Axis : " + axis.ToString() + (result ? " stopped" : " not stopped.");
            return result;
        }
        public static bool IsAlarm(Axis axis)
        {
            if (!_Axes[(int)axis].IsValid)
                return true;
            return _Axes[(int)axis].IsAlarm();
        }
        public static void SetBacklash(Axis axis, double backlash)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            _Axes[(int)axis].SetBacklash(backlash);
        }
        public static double GetBacklash(Axis axis) 
        {
            if (!_Axes[(int)axis].IsValid)
                return 0;
            return _Axes[(int)axis].GetBacklash();
        }
        public static void SetToZero(Axis axis)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            _Axes[(int)axis].SetToZero();
        }
        public static double GetPosition(Axis axis)
        {
            if (!_Axes[(int)axis].IsValid)
                return 9999999;
            return _Axes[(int)axis].GetPosition();
        }
        public static void EnableDirectionReverse(Axis axis, bool value)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            _Axes[(int)axis].EnableDirectionReverse(value);
        }
        public static bool DirectionReverseEnabled(Axis axis)
        {
            if (!_Axes[(int)axis].IsValid)
                return false;
            return _Axes[(int)axis].IsReverse;
        }

        public static void EnableSoftLimit(Axis axis, bool value = false)
        {
            //if (SIMULATE )
            //    return;
            if (_Axes != null && _Axes[(int)axis] != null && _Axes[(int)axis].IsValid)
            {
                _Axes[(int)axis].EnableSoftLimit(value);
            }
        }
        public static bool SoftLimitEnabled(Axis axis)
        {
            if (!_Axes[(int)axis].IsValid)
                return false;
            return _Axes[(int)axis].SoftLimitEnabled;
        }
        public static void SetSoftLimit(Axis axis, double nagetiveValue, double positiveValue)
        {
            if (_Axes[(int)axis] == null || !_Axes[(int)axis].IsValid )
                return;
            _Axes[(int)axis].SetSoftLimit(nagetiveValue, positiveValue);
        }
        public static void GetSoftLimit(Axis axis, out double nagetiveValue, out double positiveValue)
        {
            if (!_Axes[(int)axis].IsValid)
            {
                nagetiveValue = positiveValue  = - 1;
                return;
            }
            _Axes[(int)axis].GetSoftLimit(out nagetiveValue, out positiveValue);
        }
        public static ushort GetDriverErrorCode(Axis axis)
        {
            if (!_Axes[(int)axis].IsValid)
                return 0;

            return _Axes[(int)axis].GetDriverErrorCode();
        }

        public static bool IsHoming(Axis axis, bool defaultValue, out string error)
        {
            error = "Axis : " + axis.ToString() + (defaultValue ? " homing" : " not homing.");
            if (!_Axes[(int)axis].IsValid)
                return defaultValue;
            bool result = false;
            error = "Axis : " + axis.ToString() + (result ? " homing" : " not homing.");
            return result;
        }

        public static bool IsReady(Axis axis, bool defaultValue, out string error)
        {
            error = "Axis : " + axis.ToString() + (defaultValue ? " ready" : " not ready.");
            if (!_Axes[(int)axis].IsValid)
                return defaultValue;
            bool result = false;
            error = "Axis : " + axis.ToString() + (result ? " ready" : " not ready.");
            return result;
        }
        public static void ErrorReset(Axis axis)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
                
            _Axes[(int)axis].ErrorReset();
        }
        public static void ServoOn(Axis axis)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            _Axes[(int)axis].ServoOn();
        }
        public static void ServoOff(Axis axis)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            _Axes[(int)axis].ServoOff();
        }
        public static void SetPPU(Axis axis, uint ppu)
        {//1000 PPU 表示移動 1 mm 需要輸入 1000 個脈波。
            if (!_Axes[(int)axis].IsValid)
                return;
            _Axes[(int)axis].SetPPU(ppu);
        }

        public static bool GetSignal(Axis axis, AxisStatus CmStatus, bool defaultValue, out string error)
        {
            error = "Axis : " + axis.ToString() + " , " + CmStatus.ToString() + " : " + (defaultValue ? "ON" : "OFF");
            if (!_Axes[(int)axis].IsValid)
                return defaultValue;
            bool result = _Axes[(int)axis].GetSignal(CmStatus);
            error = "Axis : " + axis.ToString() + " , " + CmStatus.ToString() + " : " + (result ? "ON" : "OFF");
            return result;
        }
        public static uint GetPPU(Axis axis)
        {
            if (!_Axes[(int)axis].IsValid)
                return 1;
            return _Axes[(int)axis].GetPPU();
        }
        public static void SetPPUDenominator(Axis axis, uint ppuDenominator)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            _Axes[(int)axis].SetPPUDenominator(ppuDenominator);
        }

        public static uint GetPPUDenominator(Axis axis)
        {
            if (!_Axes[(int)axis].IsValid)
                return 1;
            return _Axes[(int)axis].GetPPUDenominator();
        }

        public static void SetHomeMode(Axis axis, Config.HomeType mode, double creepingSpeed, double offsetDistance, bool direction)
        {
            _Axes[(int)axis].SetHomeMode(mode, creepingSpeed, offsetDistance, direction);
        }
        public static Config.HomeType GetHomeMode(Axis axis)
        {
            return _Axes[(int)axis].HomeMode;
        }
        public static double GetHomeCreepingSpeed(Axis axis)
        {
            return _Axes[(int)axis].HomeCreepingSpeed;
        }
        public static double GetHomeOffsetDistance(Axis axis)
        {
            return _Axes[(int)axis].HomeOffsetDistance;
        }

        public static bool GetHomeDirection(Axis axis)
        {
            return _Axes[(int)axis].HomeDirection;
        }

        public static int GetIndex(Axis axis)
        {
            return _Axes[(int)axis].AxisIndex;
        }

        public static string GetName(Axis axis)
        {
            return _Axes[(int)axis].AxisName;
        }

        public static void SetName(Axis axis, string name)
        {
            _Axes[(int)axis].AxisName = name;
        }

        public static void SetIndex(Axis axis, int index)
        {
            _Axes[(int)axis].SetIndex(index);
        }

        public static bool IsInPosition(Axis axis, double targetPosition, double tolerance, bool defaultValue, out string error)
        {
            error = string.Empty;
            if (!_Axes[(int)axis].IsValid)
                return defaultValue;
            if (tolerance < 0)
                throw new Exception("Tolerance < 0.");
            double currentPosition = GetPosition(axis);
            double offset = Math.Abs(currentPosition - targetPosition);
            error = "Axis : " + axis.ToString() + (offset <= tolerance ? " in position." : " not in position");
            if (offset <= tolerance)
                return true;
            else
                return false;
        }
        public static bool IsOverPosition(Axis axis, double targetPosition, bool defaultValue, out string error)
        {
            error = $"Axis : {axis} not over {targetPosition}";
            if (!_Axes[(int)axis].IsValid)
                return defaultValue;
            double currentPosition = GetPosition(axis);
            error = $"Axis : {axis} not over {targetPosition}";
            if (currentPosition > targetPosition)
                return true;
            return false;
        }
        public static bool IsBelowPosition(Axis axis, double targetPosition, bool defaultValue, out string error)
        {
            error = $"Axis : {axis} not below {targetPosition}";
            if (!_Axes[(int)axis].IsValid)
                return defaultValue;
            double currentPosition = GetPosition(axis);
            error = $"Axis : {axis} not below {targetPosition}";
            if (currentPosition < targetPosition)
                return true;
            return false;
        }

        public static nint GetAxisHandle(Axis axis)
        {
            return _Axes[(int)axis].GetAxisHandle();
        }

        public static nint GetDeviceHandle(Axis axis)
        {
            return _Axes[(int)axis].GetDeviceHandle();
        }
        public static void SetMode(Axis axis, MotionBase.MotionMode mode)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            _Axes[(int)axis].SetMode(mode);
        }
        public static MotionBase.MotionMode GetMode(Axis axis, MotionBase.MotionMode defalutValue, out string error)
        {
            error = "Axis : " + axis.ToString() + " , " + "Mode : " + defalutValue;
            if (!_Axes[(int)axis].IsValid)
                return defalutValue;
            MotionBase.MotionMode mode = _Axes[(int)axis].GetMode();
            error = "Axis : " + axis.ToString() + " , " + "Mode : " + mode;
            return mode;
        }
        public static void SetTorqueModeParameter(Axis axis, MotionBase.TorqueParameter parameterm, int value)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            _Axes[(int)axis].SetTorqueModeParameter(parameterm, value);
        }
        public static int GetTorqueModeParameter(Axis axis, MotionBase.TorqueParameter parameter, int defalutValue, out string error)
        {
            error = $"Axis : {axis} , {parameter} : {defalutValue}";
            if (!_Axes[(int)axis].IsValid)
                return defalutValue;
            int value = _Axes[(int)axis].GetTorqueModeParameter(parameter);
            error = $"Axis : {axis} , {parameter} : {value}";
            return value;
        }
        public static void TorqueMove(Axis axis)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            _Axes[(int)axis].TorqueMove();
        }
        public static void TorqueStop(Axis axis)
        {
            if (!_Axes[(int)axis].IsValid)
                return;
            _Axes[(int)axis].TorqueStop();
        }

        private static bool CheckMotionRule(Axis axis)
        {
            switch(axis)
            {
                //case Axis.MaskHolder:
                //    if (!GetSignal(Axis.Chuck_Z, Ax_Motion_IO.AX_MOTION_IO_ORG, false, out string error))
                //        return false;
                //    break;

            }
            return true;
        }
        public static void Close()
        {
            //Servo Off
            for (int i = 0; i < Enum.GetNames(typeof(Axis)).Length; i++)
                Stop((Axis)i);
            for (int i = 0; i < Enum.GetNames(typeof(Axis)).Length; i++)
                ServoOff((Axis)i);
        }

    }
}


