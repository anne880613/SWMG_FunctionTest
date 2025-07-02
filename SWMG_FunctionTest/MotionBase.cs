
using SSCApiCLR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWMG_FunctionTest
{
    public abstract class MotionBase
    {
        public enum MotionMode
        {
            Position,
            Torque,
            None,
        }
        public enum TorqueParameter
        {
            TargetTorque,
            ActualTorque,
            SpeedLimit,
            TorqueLimit,
        }
        public string AxisName = string.Empty;
        public string InitialErrorMessage{ protected set; get; } = string.Empty;
        public int AxisIndex { protected set; get; }
        public bool IsReverse { protected set; get; }
        public bool IsValid { protected set; get; }
        public bool SoftLimitEnabled { protected set; get; }
        public HomeState HomeMode { protected set; get; }
        public bool HomeDirection { protected set; get; }
        public double HomeCreepingSpeed { protected set; get; }
        public double HomeOffsetDistance { protected set; get; }
        public abstract void Stop();
        public abstract void SetToZero();
        public void SetIndex(int index) { AxisIndex = index; }
        public abstract bool IsHomeAttained();
        public abstract void EmergencyStop();
        public abstract void Home(double speed, double acc, double dec);
        public abstract void AbsMove(double position, double speed, double acc, double dec);
        public abstract void RelMove(double distance, double speed, double acc, double dec);
        public abstract void JogMove(bool direction, double speed, double acc, double dec);
        public abstract bool IsStopped();
        public abstract bool IsAlarm();
        public abstract void ServoOn();
        public abstract void ServoOff();
        public abstract ushort GetDriverErrorCode();
        public abstract void SetSoftLimit(double nagetiveValue, double positiveValue);
        public abstract void GetSoftLimit(out double nagetiveValue, out double positiveValue);
        public abstract void EnableDirectionReverse(bool value);
        public abstract void EnableSoftLimit(bool value);
        public abstract double GetPosition();
        public abstract bool GetSignal(CoreMotionAxisStatus axisStatus);
        public abstract void SetPPU(uint ppu);
        public abstract uint GetPPU();
        public abstract void SetPPUDenominator(uint PPUDenominator);
        public abstract uint GetPPUDenominator();
        public abstract void SetBacklash(double backlash);
        public abstract double GetBacklash();
        public void SetHomeMode(HomeState mode, double creepingSpeed, double offsetDiatance, bool direction)
        {
            HomeMode = mode;
            HomeCreepingSpeed = creepingSpeed;
            HomeOffsetDistance = offsetDiatance;
            HomeDirection = direction;
        }

        public abstract CoreMotionAxisStatus GetState();
        public abstract void ErrorReset();
        public abstract IntPtr GetAxisHandle();
        public abstract IntPtr GetDeviceHandle();

        public abstract void SetMode(MotionMode mode);
        public abstract MotionMode GetMode();
        public abstract void SetTorqueModeParameter(TorqueParameter parameterm, int value);
        public abstract int GetTorqueModeParameter(TorqueParameter parameter);
        public abstract void TorqueMove();
        public abstract void TorqueStop();
    }
}
