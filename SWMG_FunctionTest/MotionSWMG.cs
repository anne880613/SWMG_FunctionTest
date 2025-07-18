using SSCApiCLR;
using System.Text;
using static SSCApiCLR.Config;
using static SWMG_FunctionTest.MotionManager;

namespace SWMG_FunctionTest
{
    public class MotionSWMG : MotionBase
    {
        private IntPtr AxisHandle = IntPtr.Zero;
        private IntPtr _DeviceHandle = IntPtr.Zero;
        private static CoreMotion _device_cm;
        private static CoreMotionStatus _CmStatus;
        private static CoreMotionAxisStatus _cmAxis;

        public MotionSWMG(int axisIndex)
        {
            try
            {
                InitialErrorMessage = string.Empty;
                IsValid = false;

                AxisIndex = axisIndex - 1;
                if (!SWMG.IsOpened)
                    SWMG.OpenDevice();

                _device_cm = SWMG.GetCoreMotion();
                _CmStatus = SWMG.GetCoreMotionStatus();
                _device_cm.GetStatus(ref _CmStatus);
                _cmAxis = SWMG.GetCoreMotionAxisStatus(AxisIndex);//各軸獨立

                InitialErrorMessage = SWMG.InitialErrorMessage;
                if (InitialErrorMessage == string.Empty)
                    IsValid = true;
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
            _device_cm.Motion.ExecQuickStop(this.AxisIndex);
        }

        public override void Stop()
        {
            _device_cm.Motion.Stop(this.AxisIndex);
        }

        public override void SetToZero()//??
        {
            
        }

        public override bool IsHomeAttained()
        {
            //int err = _device_cm.Home.SetHomeDone(this.AxisIndex, 1);
            //if (err != ErrorCode.None)
            //    return true;
            //else
            //    return false;
            _device_cm.GetStatus(ref _CmStatus);
            bool homeDone = _CmStatus.AxesStatus[this.AxisIndex].HomeDone;
            return homeDone;
        }
        public override void Home(double speed, double acc, double dec)
        {
            //參數要放在哪
            Config.HomeParam homeParam = new Config.HomeParam();
            _device_cm.Config.GetHomeParam(this.AxisIndex, ref homeParam);
            homeParam.HomeType = Config.HomeType.CurrentPos;
            _device_cm.Config.SetHomeParam(this.AxisIndex, homeParam);
            _device_cm.Home.StartHome(this.AxisIndex);
            _device_cm.Motion.Wait(this.AxisIndex);//等待軸復歸完成

            /*
            // Homing.
            Config.HomeParam homeParam = new Config.HomeParam();
            sscLib_cm.Config.GetHomeParam(0, ref homeParam);
            homeParam.HomeType = Config.HomeType.CurrentPos;
            sscLib_cm.Config.SetHomeParam(0, homeParam);
            sscLib_cm.Home.StartHome(0);
            sscLib_cm.Motion.Wait(0);
            */
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

            int err = _device_cm.Motion.StartPos(posCommand);
            if (err != ErrorCode.None)
                throw new Exception("AbsMove failed with error code: [" + err + "]");
        }
        public static nint DirectAbsMove(nint groupHandle, double speed, double acc, double dec, bool useSCurve, double[] positions)
        {
            //還沒
            int result;
            //SetGroupSpeedParameter(groupHandle, speed, acc, dec, useSCurve);
            result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Group Move DirectAbs failed with error code: [0x" + Convert.ToString(result, 16) + "]");

            return groupHandle;
        }
        public static nint LinearAbsMove(nint groupHandle, double speed, double acc, double dec, bool useSCurve, double[] positions)
        {
            //還沒
            int result;
            //nint groupHandle = SetGroup(axesHandle);
            //SetGroupSpeedParameter(groupHandle, speed, acc, dec, useSCurve);
            result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Group Move DirectAbs failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            return groupHandle;
        }

        public static nint DirectRelMove(nint groupHandle, double speed, double acc, double dec, bool useSCurve, double[] positions)
        {
            //還沒
            int result;
            //SetGroupSpeedParameter(groupHandle, speed, acc, dec, useSCurve);
            result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Group Move DirectAbs failed with error code: [0x" + Convert.ToString(result, 16) + "]");

            return groupHandle;
        }

        public static nint LinearRelMove(nint groupHandle, double speed, double acc, double dec, bool useSCurve, double[] positions)
        {
            //還沒
            int result;
            //SetGroupSpeedParameter(groupHandle, speed, acc, dec, useSCurve);
            result = 0;
            if (result != (uint)ErrorCode.None)
                throw new Exception("Group Move DirectAbs failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            return groupHandle;
        }


        public override void RelMove(double distance, double speed, double acc, double dec)
        {
            if (_cmAxis == null)
                throw new InvalidOperationException("_cmAxis 尚未初始化，請確認建構子已正確執行且軸狀態已取得。");

            Motion.PosCommand posCommand = new Motion.PosCommand();
            posCommand.Profile.Type = SSCApiCLR.ProfileType.Trapezoidal;
            posCommand.Axis = this.AxisIndex;
            posCommand.Target = distance;
            posCommand.Profile.Velocity = speed;
            posCommand.Profile.Acc = acc;
            posCommand.Profile.Dec = acc;
            int err = _device_cm.Motion.StartMov(posCommand);
            if (err != ErrorCode.None)
                throw new Exception("RelMove failed with error code: [" + err + "]");
        }

        public override void JogMove(bool direction, double speed, double acc, double dec)
        {
            //-----------------------------------------------------------------
            // Create a command value.
            //-----------------------------------------------------------------
            Motion.JogCommand jogCommand = new();

            jogCommand.Profile.Type = SSCApiCLR.ProfileType.Trapezoidal;
            jogCommand.Axis = this.AxisIndex;

            if (direction)
                jogCommand.Profile.Velocity = speed;
            else
                jogCommand.Profile.Velocity = speed * (-1);

            jogCommand.Profile.Acc = acc;
            jogCommand.Profile.Dec = dec;

            int err = _device_cm.Motion.StartJog(jogCommand);
            if (err != ErrorCode.None)
                throw new Exception("Jog failed with error code: [" + err + "]");
        }

        public override bool IsStopped()//OK
        {
            //不知道
            //判斷是否為Idle
            int result = _device_cm.GetStatus(ref _CmStatus);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Get current position Failed With Error Code: [" + result + "]");

            CoreMotionAxisStatus status = _CmStatus.AxesStatus[this.AxisIndex];
            OperationState currentState = status.OpState;
            if (currentState == OperationState.Idle)
                return true;
            else
                return false;
            //// Wait fot the 0 axis to run.
            //Motion.WaitCondition waitCondition = new Motion.WaitCondition();
            //waitCondition.AxisCount = 1;//等幾軸
            //waitCondition.Axis[0] = this.AxisIndex;
            //waitCondition.WaitConditionType = Motion.WaitConditionType.AxisIdle;

            //_device_cm.Motion.Wait(waitCondition);
            //return true;

        }
        public override bool IsAlarm()
        {
            //uint ioStatus = new();
            //int result = Advantech.Motion.Motion.mAcm_AxGetMotionIO(AxisHandle, ref ioStatus);
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
            //int result = Advantech.Motion.Motion.mAcm_DevReadSDOData(GetDeviceHandle(), 0, (ushort)AxisIndex, 0x603f, 0, ref errorCode);
            //if (result != (uint)ErrorCode.None)
            //    throw new Exception("Get SDO:0x603F failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            return errorCode;
        }
        public override void EnableDirectionReverse(bool value)
        {
            IsReverse = value;
        }
        public override double GetPosition()//OK
        {
            _device_cm.GetStatus(ref _CmStatus);
            CoreMotionAxisStatus cmAxis = _CmStatus.AxesStatus[this.AxisIndex];
            double position = cmAxis.ActualPos; //從CmStatus拿到實際位置
            return position;
        }

        public override bool GetSignal(AxisStatus axisStatus)
        {
            _device_cm.GetStatus(ref _CmStatus);
            CoreMotionAxisStatus cmAxis = _CmStatus.AxesStatus[this.AxisIndex];
            // 拿正負極限值、原點等訊號回傳
            // 回傳 true 或 false
            switch (axisStatus.ToString())//找對應的項目回傳值
            {
                case "PositiveLS":
                    return cmAxis.PositiveLS;
                case "NegativeLS":
                    return cmAxis.NegativeLS;
                case "NearPositiveLS":
                    return cmAxis.NearPositiveLS;
                case "NearNegativeLS":
                    return cmAxis.NearNegativeLS;
                case "ExternalPositiveLS":
                    return cmAxis.ExternalPositiveLS;
                case "ExternalNegativeLS":
                    return cmAxis.ExternalNegativeLS;
                case "PositiveSoftLimit":
                    return cmAxis.PositiveSoftLimit;
                case "NegativeSoftLimit":
                    return cmAxis.NegativeSoftLimit;
                case "HomeSwitch":
                    return cmAxis.HomeSwitch;
                case "HomePaused":
                    return cmAxis.HomePaused;
                case "HomeDone":
                    return cmAxis.HomeDone;
                case "CmdPosToFbPosFlag":
                    return cmAxis.CmdPosToFbPosFlag;
                case "ServoOn":
                    return cmAxis.ServoOn;
                case "servoOffline":
                    return cmAxis.ServoOffline;
                case "AmpAlarm":
                    return cmAxis.AmpAlarm;
                case "InPos":
                    return cmAxis.InPos;
                case "InPos2":
                    return cmAxis.InPos2;
                case "InPos3":
                    return cmAxis.InPos3;
                case "InPos4":
                    return cmAxis.InPos4;
                case "InPos5":
                    return cmAxis.InPos5;
                case "DelayedPosSet":
                    return cmAxis.DelayedPosSet;
                case "FollowingErrorAlarm":
                    return cmAxis.FollowingErrorAlarm;
                case "CommandReady":
                    return cmAxis.CommandReady;
                case "WaitingForTrigger":
                    return cmAxis.WaitingForTrigger;
                case "MotionPaused":
                    return cmAxis.MotionPaused;
                case "MotionComplete":
                    return cmAxis.MotionComplete;
                case "ExecSuperimposedMotion":
                    return cmAxis.ExecSuperimposedMotion;
                case "AccFlag":
                    return cmAxis.AccFlag;
                case "DecFlag":
                    return cmAxis.DecFlag;
                case "CmdDistributionEnd":
                    return cmAxis.CmdDistributionEnd;
                case "PosSet":
                    return cmAxis.PosSet;
                
                default:
                    return false;
            }
        }
        public override void ServoOn()//OK
        {
            _device_cm.AxisControl.SetServoOn(this.AxisIndex, 1);
            while (true)
            {
                _device_cm.GetStatus(ref _CmStatus);
                if (_CmStatus.AxesStatus[0].ServoOn)
                {
                    break;
                }//確定有成功開啟軸
                System.Threading.Thread.Sleep(100);
            }
        }
        public override void ServoOff()//OK
        {
            _device_cm.AxisControl.SetServoOn(this.AxisIndex, 0);
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
            int result;
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
            int result;
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
            int result;
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

        public override void SetPPU(uint ppu)//沒有ppu:
        {
            //輸入驅動器內設定的電子齒輪比 x pulses/rev
            int result = 0;
            //result = Advantech.Motion.Motion.mAcm_SetU32Property(AxisHandle, (uint)PropertyID.CFG_AxPPU, ppu);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set ppu failed with error code: [0x" + Convert.ToString(result, 16) + "]");
        }
        public override uint GetPPU()//隨便啦不知道
        {
            int result = 0;
            double Numberator = 0;
            double Denominator = 0;
            result = _device_cm.Config.GetGearRatio(this.AxisIndex, ref Numberator, ref Denominator);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set ppu denominator failed with error code: ["+ result.ToString() + "]");
            uint fakePPU = (uint)(Numberator / Denominator * 10000); //假設PPU是10000的倍數
            return fakePPU;
        }
        public override void SetPPUDenominator(uint ppuDenominator)//隨便啦不知道
        {
            int result = 0;
            double Numberator = 0;
            double Denominator = 0;
            _device_cm.Config.GetGearRatio(this.AxisIndex, ref Numberator, ref Denominator);
            result = _device_cm.Config.SetGearRatio(this.AxisIndex, Numberator, ppuDenominator);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set ppu denominator failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            return;
        }

        public override uint GetPPUDenominator()//隨便啦不知道
        {
            int result = 0;
            double Numberator = 0;
            double Denominator = 0;
            result = _device_cm.Config.GetGearRatio(this.AxisIndex, ref Numberator, ref Denominator);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set ppu denominator failed with error code: [" + result.ToString() + "]");
            return (uint)Denominator;
        }
        public override void SetBacklash(double backlash)//?
        {
            int result;
            if (backlash > 0)
            {
                AxisCompensation axisCompensation = new()
                {
                    PitchErrorCompensation = 0,
                    PitchErrorCompensation2D = 0,
                    BacklashCompensation = backlash,
                    TotalPosCompensation = 0
                };
                //axisCompensation.SetData(axisCompensation);
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
            int result;
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
            int result = 0;
            switch (mode)
            {
                case MotionMode.Position:
                    AxisCommandMode motionModePosition = (AxisCommandMode)0;
                    result = _device_cm.AxisControl.SetAxisCommandMode((int)this.AxisIndex, motionModePosition);
                    if (result != (uint)ErrorCode.None)
                        throw new Exception("Set Positon MotionMode failed with error code: " + result + "]");
                    break;
                case MotionMode.Velocity:
                    AxisCommandMode motionModeVelocity = (AxisCommandMode)1;
                    result = _device_cm.AxisControl.SetAxisCommandMode((int)this.AxisIndex, motionModeVelocity);
                    if (result != (uint)ErrorCode.None)
                        throw new Exception("Set Velocity MotionMode failed with error code: " + result + "]");
                    break;
                case MotionMode.Torque:
                    AxisCommandMode motionModeTorque = (AxisCommandMode)2;
                    result = _device_cm.AxisControl.SetAxisCommandMode((int)this.AxisIndex, motionModeTorque);
                    if (result != (uint)ErrorCode.None)
                        throw new Exception("Set Torque MotionMode failed with error code: " + result + "]");
                    break;
            }
        }
        public override MotionMode GetMode()
        {
            AxisCommandMode motionMode = 0;
            int result = _device_cm.AxisControl.GetAxisCommandMode(this.AxisIndex, ref motionMode);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Get MotionMode failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            if (motionMode == (AxisCommandMode)0)
                return MotionMode.Position;
            if (motionMode == (AxisCommandMode)1)
                return MotionMode.Velocity;
            if (motionMode == (AxisCommandMode)2)
                return MotionMode.Torque;
            return MotionMode.None;
        }
        public override void SetTorqueModeParameter(TorqueParameter parameter, int value)
        {
            switch (parameter)
            {
                case TorqueParameter.ActualTorque:
                    break;
                case TorqueParameter.SpeedLimit:
                    int result = 0;
                    if (result != (uint)ErrorCode.None)
                        throw new Exception("Set CFG_AxMaxMotorSpeed failed with error code: [0x" + Convert.ToString(result, 16) + "]");
                    break;
                case TorqueParameter.TargetTorque:
                    double maxTorque = 0;
                    result = _device_cm.Torque.GetMaxTrqLimit(this.AxisIndex, ref maxTorque); ;
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
            CoreMotionAxisStatus cmAxis = _CmStatus.AxesStatus[this.AxisIndex];//要抓對軸
            double actualTorque = cmAxis.ActualTorque;
            switch (parameter)
            {
                case TorqueParameter.ActualTorque:
                    int actTorque = 0;
                    int result = 0;
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
            Torque.TrqCommand trq= new();
            int result = _device_cm.AxisControl.SetAxisCommandMode(this.AxisIndex, AxisCommandMode.Torque);
            if (result != (int)ErrorCode.None)
                throw new Exception("Set TorqueMode failed with error code: " + result + "]");
            result = _device_cm.Torque.StartTrq(trq);
            if (result != (int)ErrorCode.None)
                throw new Exception("Start Torque failed with error code: " + result + "]");
        }
        public override void TorqueStop()
        {
            int result = _device_cm.Torque.StopTrq(this.AxisIndex);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Stop Torque failed with error code: [" + result + "]");
        }

        public override CoreMotionAxisStatus GetState()//不確定要回傳什麼
        {
            CoreMotionStatus axState = new CoreMotionStatus(); // Initialize the variable to fix CS0165
            int result = _device_cm.GetStatus(ref axState);
            if (result != (uint)ErrorCode.None)
                throw new Exception("GetState failed with error code: [" + result + "]");

            return axState.AxesStatus[this.AxisIndex];
        }

        public enum AxisStatus : int
        {
            ServoOn,
            DelayedPosSet,
            PosSet,
            CmdDistributionEnd,
            InPos5,
            InPos4,
            InPos3,
            InPos2,
            InPos,
            AccFlag,
            ExecSuperimposedMotion,
            MotionComplete,
            MotionPaused,
            WaitingForTrigger,
            CommandReady,
            DecFlag,
            FollowingErrorAlarm,
            PositiveLS,
            NearPositiveLS,
            CmdPosToFbPosFlag,
            HomePaused,
            HomeDone,
            NegativeLS,
            HomeSwitch,
            NegativeSoftLimit,
            PositiveSoftLimit,
            ExternalNegativeLS,//近接／外部極限開關
            ExternalPositiveLS,
            NearNegativeLS,
            AmpAlarm,
            ServoOffline
        }
    }
}