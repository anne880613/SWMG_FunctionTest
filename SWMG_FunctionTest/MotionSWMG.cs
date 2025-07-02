using SSCApiCLR;
using System.Text;

namespace SWMG_FunctionTest
{
    public class MotionSWMG : MotionBase
    {
        private IntPtr AxisHandle = IntPtr.Zero;
        private IntPtr _DeviceHandle = IntPtr.Zero;
        private static CoreMotion _device_cm;
        private static CoreMotionStatus _CmStatus;

        public MotionSWMG(int axisIndex)
        {
            try
            {
                InitialErrorMessage = string.Empty;
                IsValid = false;

                AxisIndex = axisIndex-1;
                if (!SWMG.IsOpened)
                    SWMG.OpenDevice();

                _device_cm = SWMG.GetCoreMotion();
                _CmStatus = SWMG.GetCoreMotionStatus();

                InitialErrorMessage = SWMG.InitialErrorMessage;
                if (InitialErrorMessage == string.Empty)
                    IsValid = true;
                //SWMG.GetDeviceHandle(ref _DeviceHandle);//沒東西
                //SWMG.OpenAxisAndReturnAxisHandle((ushort)axisIndex, ref AxisHandle);
            }
            catch
            {
                IsValid = false;
            }
        }
        public static CoreMotion GetCoreMotion()
        {
            return _device_cm;
        }

        public static CoreMotionStatus GetCoreMotionStatus()
        {
            return _CmStatus;
        }
        public override IntPtr GetAxisHandle()
        {
            return AxisHandle;//SWMG不用?
        }
        public override IntPtr GetDeviceHandle()
        {
            return _DeviceHandle;//SWMG不用?
        }
        public override void EmergencyStop()
        {
            _device_cm.Motion.ExecQuickStop(0);
        }


        public override void Stop()
        {
            //要怎麼只指定停止一軸 //如果每一軸獨立創一個CoreMotion他們就會是個別的第0軸??
            _device_cm.Motion.Stop(0);
        }

        public override void SetToZero()
        {
            uint result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set command position failed wih error code: [0x" + Convert.ToString(result, 16) + "]");
            result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set actual position failed wih error code: [0x" + Convert.ToString(result, 16) + "]");
        }

        public override bool IsHomeAttained()
        {
            uint statusWord = 0;
            uint result;
            result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Get PAR_AxStatusWord Failed With Error Code: [0x" + Convert.ToString(result, 16) + "]");
            if ((statusWord & (0x01 << 12)) > 0 && (statusWord & (0x01 << 10)) > 0)
                return true;
            else
                return false;
        }
        public override void Home(double speed, double acc, double dec)
        {
            uint result;
            result = 0;
            //if (IsReverse)
            //    offsetDistance *= -1;

            //result = Advantech.Motion.Motion.mAcm_SetF64Property(AxisHandle, (uint)PropertyID.PAR_AxHomeVelLow, HomeCreepingSpeed);
            //if (result != (uint)ErrorCode.None)
            //    goto Error;
            //result = Advantech.Motion.Motion.mAcm_SetF64Property(AxisHandle, (uint)PropertyID.PAR_AxHomeVelHigh, speed);
            //if (result != (uint)ErrorCode.None)
            //    goto Error;
            //result = Advantech.Motion.Motion.mAcm_SetF64Property(AxisHandle, (uint)PropertyID.PAR_AxHomeAcc, acc);
            //if (result != (uint)ErrorCode.None)
            //    goto Error;
            //result = Advantech.Motion.Motion.mAcm_SetF64Property(AxisHandle, (uint)PropertyID.PAR_AxHomeDec, dec);
            //if (result != (uint)ErrorCode.None)
            //    goto Error;
            //result = Advantech.Motion.Motion.mAcm_SetF64Property(AxisHandle, (uint)PropertyID.PAR_AxHomeCrossDistance, 1);
            //if (result != (uint)ErrorCode.None)
            //    goto Error;
            //result = Advantech.Motion.Motion.mAcm_SetF64Property(AxisHandle, (uint)PropertyID.CFG_AxHomeOffsetDistance, HomeOffsetDistance);
            //if (result != (uint)ErrorCode.None)
            //    goto Error;
            //result = Advantech.Motion.Motion.mAcm_SetF64Property(AxisHandle, (uint)PropertyID.CFG_AxHomeOffsetVel, speed);
            //if (result != (uint)ErrorCode.None)
            //    goto Error;
            //result = Advantech.Motion.Motion.mAcm_AxMoveHome(AxisHandle, (uint)HomeMode, HomeDirection ? (uint)0 : 1);
            //if (result != (uint)ErrorCode.None)
                goto Error;
            Error:
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set Home Speed Failed With Error Code: [0x" + Convert.ToString(result, 16) + "]");
        }

        public override void AbsMove(double position, double speed, double acc, double dec)
        {
            if (!IsStopped())
                return;
            //-----------------------------------------------------------------
            // Create a command value.
            //-----------------------------------------------------------------
            Motion.PosCommand posCommand = new Motion.PosCommand();
            posCommand.Profile.Type = SSCApiCLR.ProfileType.Trapezoidal;
            posCommand.Axis = this.AxisIndex;
            posCommand.Target = position;
            posCommand.Profile.Velocity = speed;
            posCommand.Profile.Acc = acc;
            posCommand.Profile.Dec = acc;

            _device_cm.Motion.StartPos(posCommand);
        }
        public static nint DirectAbsMove(nint groupHandle, double speed, double acc, double dec, bool useSCurve, double[] positions)
        {
            uint result;
            //SetGroupSpeedParameter(groupHandle, speed, acc, dec, useSCurve);
            result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Group Move DirectAbs failed with error code: [0x" + Convert.ToString(result, 16) + "]");

            return groupHandle;
        }
        public static nint LinearAbsMove(nint groupHandle, double speed, double acc, double dec, bool useSCurve, double[] positions)
        {
            uint result;
            //nint groupHandle = SetGroup(axesHandle);
            //SetGroupSpeedParameter(groupHandle, speed, acc, dec, useSCurve);
            result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Group Move DirectAbs failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            return groupHandle;
        }

        public static nint DirectRelMove(nint groupHandle, double speed, double acc, double dec, bool useSCurve, double[] positions)
        {
            uint result;
            //SetGroupSpeedParameter(groupHandle, speed, acc, dec, useSCurve);
            result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Group Move DirectAbs failed with error code: [0x" + Convert.ToString(result, 16) + "]");

            return groupHandle;
        }

        public static nint LinearRelMove(nint groupHandle, double speed, double acc, double dec, bool useSCurve, double[] positions)
        {
            uint result;
            //SetGroupSpeedParameter(groupHandle, speed, acc, dec, useSCurve);
            result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Group Move DirectAbs failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            return groupHandle;
        }


        public override void RelMove(double distance, double speed, double acc, double dec)
        {
            //if (!IsStopped())
            //    return;
            //SetSpeedParameter(speed, acc, dec);
            //uint result = Advantech.Motion.Motion.mAcm_AxMoveRel(AxisHandle, distance);
            //if (result != (uint)ErrorCode.None)
            //    throw new Exception("Relative Move Failed With Error Code: [0x" + Convert.ToString(result, 16) + "]");

        }

        public override void JogMove(bool direction, double speed, double acc, double dec)
        {
            //-----------------------------------------------------------------
            // Create a command value.
            //-----------------------------------------------------------------
            Motion.JogCommand jogCommand = new();

            jogCommand.Profile.Type = SSCApiCLR.ProfileType.Trapezoidal;
            jogCommand.Axis = 0;

            if(direction)
                jogCommand.Profile.Velocity = speed;
            else
                jogCommand.Profile.Velocity = speed*(-1);

            jogCommand.Profile.Acc = acc;
            jogCommand.Profile.Dec = dec;

            _device_cm.Motion.StartJog(jogCommand);
        }

        public override bool IsStopped()
        {
            //判斷是否為Idle
            int result =_device_cm.GetStatus(ref _CmStatus);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Get current position Failed With Error Code: [" + result + "]");

            // Wait fot the 0 axis to run.
            Motion.WaitCondition waitCondition = new Motion.WaitCondition();
            waitCondition.AxisCount = 1;
            waitCondition.Axis[0] = this.AxisIndex;
            waitCondition.WaitConditionType = Motion.WaitConditionType.AxisIdle;
            _device_cm.Motion.Wait(waitCondition);
            return true;

        }
        public override bool IsAlarm()
        {
            //uint ioStatus = new();
            //uint result = Advantech.Motion.Motion.mAcm_AxGetMotionIO(AxisHandle, ref ioStatus);
            //if (result != (uint)ErrorCode.None)
            //    throw new Exception("Get AxGetMotionIO Failed With Error Code: [0x" + Convert.ToString(result, 16) + "]");
            //if ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_ALM) > 0)
            //    return true;
            //else
                return false;
        }
        public override ushort GetDriverErrorCode()
        {
            ushort errorCode = 0;
            //uint result = Advantech.Motion.Motion.mAcm_DevReadSDOData(GetDeviceHandle(), 0, (ushort)AxisIndex, 0x603f, 0, ref errorCode);
            //if (result != (uint)ErrorCode.None)
            //    throw new Exception("Get SDO:0x603F failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            return errorCode;
        }
        public override void EnableDirectionReverse(bool value)
        {
            IsReverse = value;
        }
        public override double GetPosition()
        {
            double position = new();
            uint result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Get current position Failed With Error Code: [0x" + Convert.ToString(result, 16) + "]");
            return position;
        }

        public override bool GetSignal(CoreMotionAxisStatus axisStatus)
        {
            
                return false;
        }
        public override void ServoOn()
        {
            _device_cm.AxisControl.SetServoOn(this.AxisIndex, 1);//要怎麼區分開哪個軸
        }
        public override void ServoOff()
        {
            _device_cm.AxisControl.SetServoOn(0, 0);
        }
        public override void ErrorReset()
        {
            

        }

        public void SetSpeedParameter(double speed, double acc, double dec)
        {
            
        }

        public void SetJogPatrmeter(double speed, double acc, double dec)
        {
            
        }
        public override void EnableSoftLimit(bool value)
        {
            uint result;
            if (value)
            {
                //result = Advantech.Motion.Motion.mAcm_SetU32Property(AxisHandle, (uint)PropertyID.CFG_AxSwMelEnable, 1);
                //if (result != (uint)ErrorCode.None)
                //    throw new Exception("Enable soft limit failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                //result = Advantech.Motion.Motion.mAcm_SetU32Property(AxisHandle, (uint)PropertyID.CFG_AxSwPelEnable, 1);
                //if (result != (uint)ErrorCode.None)
                //    throw new Exception("Enable soft limit failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            }
            else
            {
                //result = Advantech.Motion.Motion.mAcm_SetU32Property(AxisHandle, (uint)PropertyID.CFG_AxSwMelEnable, 0);
                //if (result != (uint)ErrorCode.None)
                //    throw new Exception("Disable soft limit failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                //result = Advantech.Motion.Motion.mAcm_SetU32Property(AxisHandle, (uint)PropertyID.CFG_AxSwPelEnable, 0);
                //if (result != (uint)ErrorCode.None)
                //    throw new Exception("Disable soft limit failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            }
        }
        public override void SetSoftLimit(double nagetiveValue, double positiveValue)
        {
            uint result;
            positiveValue = Math.Round(positiveValue);
            nagetiveValue = Math.Round(nagetiveValue);
            //result = Advantech.Motion.Motion.mAcm_SetF64Property(AxisHandle, (uint)PropertyID.CFG_AxSwPelValue, positiveValue);
            //if (result != (uint)ErrorCode.None)
            //    throw new Exception("Set soft limit value failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            //result = Advantech.Motion.Motion.mAcm_SetF64Property(AxisHandle, (uint)PropertyID.CFG_AxSwMelValue, nagetiveValue);
            //if (result != (uint)ErrorCode.None)
            //    throw new Exception("Set soft limit value failed with error code: [0x" + Convert.ToString(result, 16) + "]");
        }

        public override void GetSoftLimit(out double nagetiveValue, out double positiveValue)
        {
            uint result;
            double nag = new(), pos = new();
            //result = Advantech.Motion.Motion.mAcm_GetF64Property(AxisHandle, (uint)PropertyID.CFG_AxSwPelValue, ref pos);
            //if (result != (uint)ErrorCode.None)
            //    throw new Exception("Get soft limit value failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            //result = Advantech.Motion.Motion.mAcm_GetF64Property(AxisHandle, (uint)PropertyID.CFG_AxSwMelValue, ref nag);
            //if (result != (uint)ErrorCode.None)
            //    throw new Exception("Get soft limit value failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            nagetiveValue = nag;
            positiveValue = pos;
        }

        public override void SetPPU(uint ppu)
        {
            //輸入驅動器內設定的電子齒輪比 x pulses/rev
            uint result = 0;
            //result = Advantech.Motion.Motion.mAcm_SetU32Property(AxisHandle, (uint)PropertyID.CFG_AxPPU, ppu);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set ppu failed with error code: [0x" + Convert.ToString(result, 16) + "]");
        }
        public override uint GetPPU()
        {
            uint result = 0;
            uint v = 0;
            //result = Advantech.Motion.Motion.mAcm_GetU32Property(AxisHandle, (uint)PropertyID.CFG_AxPPU, ref v);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set ppu denominator failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            return v;
        }
        public override void SetPPUDenominator(uint ppuDenominator)
        {
            uint result = 0;
            //result = Advantech.Motion.Motion.mAcm_SetU32Property(AxisHandle, (uint)PropertyID.CFG_AxPPUDenominator, ppuDenominator);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set ppu denominator failed with error code: [0x" + Convert.ToString(result, 16) + "]");
        }

        public override uint GetPPUDenominator()
        {
            uint result = 0;
            uint v = 0;
            //result = Advantech.Motion.Motion.mAcm_GetU32Property(AxisHandle, (uint)PropertyID.CFG_AxPPUDenominator, ref v);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set ppu denominator failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            return v;
        }
        public override void SetBacklash(double backlash)
        {
            uint result;
            if (backlash > 0)
            {
                //result = Advantech.Motion.Motion.mAcm_SetU32Property(GetAxisHandle(), (uint)PropertyID.CFG_AxBacklashEnable, 1);
                //if (result != (uint)ErrorCode.None)
                //    throw new Exception("Set CFG_AxBacklashEnable failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                //uint pulses = (uint)(backlash / GetPPUDenominator() * GetPPU());
                //result = Advantech.Motion.Motion.mAcm_SetU32Property(GetAxisHandle(), (uint)PropertyID.CFG_AxBacklashPulses, pulses);
                //if (result != (uint)ErrorCode.None)
                //    throw new Exception("Set CFG_AxBacklashPulses failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            }
            else
            {
                //result = Advantech.Motion.Motion.mAcm_SetU32Property(GetAxisHandle(), (uint)PropertyID.CFG_AxBacklashEnable, 0);
                //if (result != (uint)ErrorCode.None)
                //    throw new Exception("Set CFG_AxBacklashEnable failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                //result = Advantech.Motion.Motion.mAcm_SetU32Property(GetAxisHandle(), (uint)PropertyID.CFG_AxBacklashPulses, 0);
                //if (result != (uint)ErrorCode.None)
                //    throw new Exception("Set CFG_AxBacklashPulses failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            }

        }
        public override double GetBacklash()
        {
            uint value = 0;
            uint result;
            //result = Advantech.Motion.Motion.mAcm_GetU32Property(GetAxisHandle(), (uint)PropertyID.CFG_AxBacklashEnable, ref value);
            //if (result != (uint)ErrorCode.None)
            //    throw new Exception("Get CFG_AxBacklashEnable failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            //if (value > 0)
            //{
            //    result = Advantech.Motion.Motion.mAcm_GetU32Property(GetAxisHandle(), (uint)PropertyID.CFG_AxBacklashPulses, ref value);
            //    if (result != (uint)ErrorCode.None)
            //        throw new Exception("Get CFG_AxBacklashPulses failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            //    double backlash = (double)value * GetPPUDenominator() / GetPPU();
            //    return backlash;
            //}
            //else
                return 0;
        }

        public override void SetMode(MotionMode mode)
        {
            switch (mode)
            {
                case MotionMode.Position:
                    //uint result = Advantech.Motion.Motion.mAcm_SetU32Property(AxisHandle, (uint)PropertyID.PAR_AxControlWord, 15);
                    //if (result != (uint)ErrorCode.None)
                    //    throw new Exception("Set torque controlWord failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                    //result = Advantech.Motion.Motion.mAcm_SetU32Property(AxisHandle, (uint)PropertyID.PAR_AxOperationMode, 0x08);
                    //if (result != (uint)ErrorCode.None)
                    //    throw new Exception("Set to position mode failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                    break;
                case MotionMode.Torque:
                    //result = Advantech.Motion.Motion.mAcm_SetU32Property(AxisHandle, (uint)PropertyID.PAR_AxControlWord, 271);
                    //if (result != (uint)ErrorCode.None)
                    //    throw new Exception("Set torque controlWord failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                    //result = Advantech.Motion.Motion.mAcm_SetU32Property(AxisHandle, (uint)PropertyID.PAR_AxOperationMode, 0x04);
                    //if (result != (uint)ErrorCode.None)
                    //    throw new Exception("Set to torque mode failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                    break;
            }
        }
        public override MotionMode GetMode()
        {
            uint motionMode = 0;
            uint result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Get PAR_AxOperationMode failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            if (motionMode == 0x04)
                return MotionMode.Torque;
            if (motionMode == 0x08)
                return MotionMode.Position;
            return MotionMode.None;
        }
        public override void SetTorqueModeParameter(TorqueParameter parameter, int value)
        {
            switch (parameter)
            {
                case TorqueParameter.ActualTorque:
                    break;
                case TorqueParameter.SpeedLimit:
                    uint result = 0;
                    if (result != (uint)ErrorCode.None)
                        throw new Exception("Set CFG_AxMaxMotorSpeed failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                    break;
                case TorqueParameter.TargetTorque:
                    result = 0;
                    if (result != (uint)ErrorCode.None)
                        throw new Exception("Set PAR_AxTargetTorque failed with error code: [0x" + Convert.ToString(result, 16) + "]");

                    break;
                case TorqueParameter.TorqueLimit:
                    result = 0;
                    if (result != (uint)ErrorCode.None)
                        throw new Exception("Set CFG_AxMaxTorque failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                    break;
            }
        }
        public override int GetTorqueModeParameter(TorqueParameter parameter)
        {
            switch (parameter)
            {
                case TorqueParameter.ActualTorque:
                    int actTorque = 0;
                    uint result = 0;
                    if (result != (uint)ErrorCode.None)
                        throw new Exception("Get actual torque failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                    return actTorque;
                case TorqueParameter.SpeedLimit:
                    break;
                case TorqueParameter.TargetTorque:
                    int target = 0;
                    result = 0;
                    if (result != (uint)ErrorCode.None)
                        throw new Exception("Set PAR_AxTargetTorque failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                    return target;
                case TorqueParameter.TorqueLimit:
                    int torqueLimit = 0;
                    result = 0;
                    if (result != (uint)ErrorCode.None)
                        throw new Exception("Get CFG_AxMaxTorque failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                    return torqueLimit;
            }
            throw new Exception("GetTorqueModeParameter illega parameter.");
        }
        public override void TorqueMove()
        {
            uint result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set PAR_AxControlWord failed with error code: [0x" + Convert.ToString(result, 16) + "]");
        }
        public override void TorqueStop()
        {
            uint result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set PAR_AxControlWord failed with error code: [0x" + Convert.ToString(result, 16) + "]");
        }

        public override CoreMotionAxisStatus GetState()
        {
            throw new NotImplementedException();
        }
    }
}
