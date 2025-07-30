using SSCApiCLR;
using System;
using System.Text;
using static SSCApiCLR.Config;
using static SWMG_FunctionTest.MotionManager;
using static SWMG_FunctionTest.MotionSWMG;
using static System.Windows.Forms.LinkLabel;

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
                //_cmAxis = SWMG.GetCoreMotionAxisStatus(AxisIndex);//各軸獨立

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
            _device_cm.Motion.ExecQuickStop(this.AxisIndex);
        }

        public override void SetToZero()//沒有用@@
        { 
            _device_cm.GetStatus(ref _CmStatus);
            CoreMotionAxisStatus cmAxis = _CmStatus.AxesStatus[this.AxisIndex];
            cmAxis.ActualPos = 0;
        }

        public override bool IsHomeAttained()
        {
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
        public static void DirectAbsMove(AxisSelection axisGroup, double speed, double acc, double dec, bool useSCurve, double[] positions)
        {
            Motion.LengthAndEndCircularIntplCommand cir = new();
            for (int i = 0; i < axisGroup.AxisCount; i++)
            {
                cir.Axis[i] = axisGroup.Axis[i];
                cir.EndPos[i] = positions[i];
            }
            cir.Profile.Type = useSCurve ? SSCApiCLR.ProfileType.SCurve : SSCApiCLR.ProfileType.Trapezoidal;
            cir.Profile.Velocity = speed;
            cir.Profile.Acc = acc;
            cir.Profile.Dec = dec;

            int result;
            result = _device_cm.Motion.StartCircularIntplPos(cir);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Group Move DirectAbs failed with error :" + SSCApi.ErrorToString(result));

        }
        public static void LinearAbsMove(AxisSelection axisGroup, double speed, double acc, double dec, bool useSCurve, double[] positions)
        {
            int result;
            Motion.LinearIntplCommand lin = new();
            lin.AxisCount = (uint)axisGroup.AxisCount;

            for(int i = 0; i < axisGroup.AxisCount; i++)
            {
                lin.Axis[i] = axisGroup.Axis[i];
                lin.Target[i] = positions[i];
            }
            lin.Profile.Type = useSCurve ? SSCApiCLR.ProfileType.SCurve : SSCApiCLR.ProfileType.Trapezoidal;
            lin.Profile.Velocity = speed;
            lin.Profile.Acc = acc;
            lin.Profile.Dec = dec;

            result = _device_cm.Motion.StartLinearIntplPos(lin);

            if (result != (uint)ErrorCode.None)
                throw new Exception("Group Move DirectAbs failed with error code: [0x" + SSCApi.ErrorToString(result) + "]");
        }

        public static void DirectRelMove(AxisSelection axisGroup, double speed, double acc, double dec, bool useSCurve, double[] positions)
        {
            Motion.LengthAndEndCircularIntplCommand cir = new();
            for (int i = 0; i < axisGroup.AxisCount; i++)
            {
                cir.Axis[i] = axisGroup.Axis[i];
                cir.EndPos[i] = positions[i];
            }
            cir.Profile.Type = useSCurve ? SSCApiCLR.ProfileType.SCurve : SSCApiCLR.ProfileType.Trapezoidal;
            cir.Profile.Velocity = speed;
            cir.Profile.Acc = acc;
            cir.Profile.Dec = dec;

            int result;
            result = _device_cm.Motion.StartCircularIntplMov(cir);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Group Move DirectAbs failed with error :" + SSCApi.ErrorToString(result));

        }

        public static void LinearRelMove(AxisSelection axisGroup, double speed, double acc, double dec, bool useSCurve, double[] positions)
        {
            int result;
            Motion.LinearIntplCommand lin = new();
            lin.AxisCount = (uint)axisGroup.AxisCount;

            for (int i = 0; i < axisGroup.AxisCount; i++)
            {
                lin.Axis[i] = axisGroup.Axis[i];
                lin.Target[i] = positions[i];
            }
            lin.Profile.Type = useSCurve ? SSCApiCLR.ProfileType.SCurve : SSCApiCLR.ProfileType.Trapezoidal;
            lin.Profile.Velocity = speed;
            lin.Profile.Acc = acc;
            lin.Profile.Dec = dec;

            result = _device_cm.Motion.StartLinearIntplMov(lin);

            if (result != (uint)ErrorCode.None)
                throw new Exception("Group Move DirectAbs failed with error code:" + SSCApi.ErrorToString(result));
        }


        public override void RelMove(double distance, double speed, double acc, double dec)
        {
            CoreMotionAxisStatus cmAxis = _CmStatus.AxesStatus[this.AxisIndex];
            if (cmAxis == null)
                throw new InvalidOperationException("cmAxis 尚未初始化，請確認建構子已正確執行且軸狀態已取得。");

            Motion.PosCommand posCommand = new Motion.PosCommand();
            posCommand.Profile.Type = SSCApiCLR.ProfileType.Trapezoidal;
            posCommand.Axis = this.AxisIndex;
            posCommand.Target = distance;
            posCommand.Profile.Velocity = speed;
            posCommand.Profile.Acc = acc;
            posCommand.Profile.Dec = acc;
            int result = _device_cm.Motion.StartMov(posCommand);
            if (result != ErrorCode.None)
                throw new Exception("RelMove failed with error:" + SSCApi.ErrorToString(result));
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

            int result = _device_cm.Motion.StartJog(jogCommand);
            if (result != ErrorCode.None)
                throw new Exception("Jog failed with error:" + SSCApi.ErrorToString(result));
           
        }

        public override bool IsStopped()//OK
        {
            //判斷是否為Idle
            int result = _device_cm.GetStatus(ref _CmStatus);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Get current position Failed With Error:" + SSCApi.ErrorToString(result));

            CoreMotionAxisStatus status = _CmStatus.AxesStatus[this.AxisIndex];
            OperationState currentState = status.OpState;
            if (currentState == OperationState.Idle)
                return true;
            else
                return false;
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

            AlarmParam alarmParam = new();
            int result = _device_cm.Config.GetAlarmParam(this.AxisIndex, ref alarmParam);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Get AlarmParam Failed With Error:" + SSCApi.ErrorToString(result));

            _device_cm.GetStatus(ref _CmStatus);
            CoreMotionAxisStatus cmAxis = _CmStatus.AxesStatus[this.AxisIndex];
            if(cmAxis.AmpAlarm || cmAxis.ServoOffline || cmAxis.FollowingErrorAlarm)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public override double GetPosition()
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
        public override void ServoOn()
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
        public override void ServoOff()
        {
            if (!IsValid) return;
            _device_cm.AxisControl.SetServoOn(this.AxisIndex, 0);
        }
        public override void ErrorReset()
        {
            _device_cm.AxisControl.ClearAxisAlarm(this.AxisIndex);
            _device_cm.AxisControl.ClearAmpAlarm(this.AxisIndex);
            _device_cm.GetStatus(ref _CmStatus);
            CoreMotionAxisStatus cmAxis = _CmStatus.AxesStatus[this.AxisIndex];//要抓對軸
            if (cmAxis.AmpAlarm || cmAxis.FollowingErrorAlarm)
            {
                ErrorReset();
            }
        }

        public void SetSpeedParameter(double speed, double acc, double dec)
        {
            //SWMG RelMove JogMove AbsMove 都可以直接設定速度 加速度 減速度
        }

        public void SetJogPatrmeter(double speed, double acc, double dec)
        {
            //SWMG 在JOG中可以直接設定速度 加速度
        }
        public override void EnableSoftLimit(bool value)
        {
            int result;
            if (value)
            {
                CoreMotionAxisStatus cmAxis = _CmStatus.AxesStatus[this.AxisIndex];//要抓對軸
                cmAxis.NegativeSoftLimit = value;
                cmAxis.PositiveSoftLimit = value;
            }
            else
            {
                CoreMotionAxisStatus cmAxis = _CmStatus.AxesStatus[this.AxisIndex];//要抓對軸
                cmAxis.NegativeSoftLimit = value;
                cmAxis.PositiveSoftLimit = value;
            }
        }
        public override void SetSoftLimit(double nagetiveValue, double positiveValue)
        {
            int result;
            positiveValue = Math.Round(positiveValue);
            nagetiveValue = Math.Round(nagetiveValue);
            LimitParam Lparam = new();
            _device_cm.Config.GetLimitParam(this.AxisIndex, ref Lparam);
            Lparam.SoftLimitNegativePos = nagetiveValue;
            Lparam.SoftLimitPositivePos = positiveValue;
        }

        public override void GetSoftLimit(out double nagetiveValue, out double positiveValue)
        {
            LimitParam Lparam = new();
            _device_cm.Config.GetLimitParam(this.AxisIndex, ref Lparam);
            
            double nag = Lparam.SoftLimitNegativePos;
            double pos = Lparam.SoftLimitPositivePos;
            nagetiveValue = nag;
            positiveValue = pos;
        }

        public override void SetPPU(uint ppu)//沒有ppu:
        {
            //輸入驅動器內設定的電子齒輪比 x pulses/rev
            int result = 0;
            //result = Advantech.Motion.Motion.mAcm_SetU32Property(AxisHandle, (uint)PropertyID.CFG_AxPPU, ppu);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set ppu failed with error:" + SSCApi.ErrorToString(result));

            /*
             //此座標轉換必須在引擎啟動後執行，取代原點復歸動作。若要執行座標還原，需使用SetCommandPos函數。Home Done訊號也必須使用SetHomeDone函數手動設定。

             //以下代碼是設定軸座標。軸座標的原點位置上的絕對編碼器位置為10000脈衝。

            CoreMotionStatus st;
            double offset, factor, gearRatioNumerator, gearRatioDenominator;

            //取得齒輪比分子與分母 
            err = ssclib_cm.config->GetGearRatio(0, &gearRatioNumerator, &gearRatioDenominator);
            if (err != ErrorCode::None) {
                ssclib_cm.ErrorToString(err, errString, sizeof(errString));
                printf("Failed to get gear ratio. Error=%d (%s)\n", err, errString);
                goto exit;
            }

            //係數為齒輪比 
            factor = gearRatioNumerator / gearRatioDenominator;

            //偏置為原點位置的絕對編碼器位置 
            offset = 10000;

            //取得目前的絕對編碼器位置 
            err = ssclib_cm.GetStatus(&st);
            if (err != ErrorCode::None) {
                ssclib_cm.ErrorToString(err, errString, sizeof(errString));
                printf("Failed to get absolute encoder position. Error=%d (%s)\n", err, errString);
                goto exit;
            }

            //設定軸的座標 
            err = ssclib_cm.home->SetCommandPos(0, (st.axesStatus[0].encoderCommand - offset)/factor);
            if (err != ErrorCode::None) {
                ssclib_cm.ErrorToString(err, errString, sizeof(errString));
                printf("Failed to set axis coordinates. Error=%d (%s)\n", err, errString);
                goto exit;
            }

            //設定Home Done狀態 
            err = ssclib_cm.home->SetHomeDone(0, 1);
            if (err != ErrorCode::None) {
                ssclib_cm.ErrorToString(err, errString, sizeof(errString));
                printf("Failed to set home done. Error=%d (%s)\n", err, errString);
                goto exit;
            }
             */
        }
        public override uint GetPPU()//隨便啦不知道
        {
            int result = 0;
            double Numberator = 0;
            double Denominator = 0;
            result = _device_cm.Config.GetGearRatio(this.AxisIndex, ref Numberator, ref Denominator);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set ppu denominator failed with error:"+ SSCApi.ErrorToString(result));
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
                throw new Exception("Set ppu denominator failed with error:" + SSCApi.ErrorToString(result));
            return;
        }

        public override uint GetPPUDenominator()//隨便啦不知道
        {
            int result = 0;
            double Numberator = 0;
            double Denominator = 0;
            result = _device_cm.Config.GetGearRatio(this.AxisIndex, ref Numberator, ref Denominator);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Set ppu denominator failed with error:" + SSCApi.ErrorToString(result));
            return (uint)Denominator;
        }
        public override void SetBacklash(double backlash)//?
        {
            CoreMotionAxisStatus cmAxis = _CmStatus.AxesStatus[this.AxisIndex];//要抓對軸
            AxisCompensation axisCompensation = cmAxis.Compensation;
  
            if (backlash > 0)
            {
                axisCompensation.BacklashCompensation = backlash; //    throw new Exception("Set CFG_AxBacklashPulses failed with error code: [0x" + Convert.ToString(result, 16) + "]");
            }
            else
            {
                axisCompensation.BacklashCompensation = backlash;
            }

        }
        public override double GetBacklash()
        {
            CoreMotionAxisStatus cmAxis = _CmStatus.AxesStatus[this.AxisIndex];//要抓對軸
            AxisCompensation axisCompensation = cmAxis.Compensation;

            double backLash = axisCompensation.BacklashCompensation;
            
            return backLash;
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
                        throw new Exception("Set Positon MotionMode failed with error: " + SSCApi.ErrorToString(result));
                    break;
                case MotionMode.Velocity:
                    AxisCommandMode motionModeVelocity = (AxisCommandMode)1;
                    result = _device_cm.AxisControl.SetAxisCommandMode((int)this.AxisIndex, motionModeVelocity);
                    if (result != (uint)ErrorCode.None)
                        throw new Exception("Set Velocity MotionMode failed with error: " + SSCApi.ErrorToString(result));
                    break;
                case MotionMode.Torque:
                    AxisCommandMode motionModeTorque = (AxisCommandMode)2;
                    result = _device_cm.AxisControl.SetAxisCommandMode((int)this.AxisIndex, motionModeTorque);
                    if (result != (uint)ErrorCode.None)
                        throw new Exception("Set Torque MotionMode failed with error:: " + SSCApi.ErrorToString(result));
                    break;
            }
        }
        public override MotionMode GetMode()
        {
            AxisCommandMode motionMode = 0;
            int result = _device_cm.AxisControl.GetAxisCommandMode(this.AxisIndex, ref motionMode);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Get MotionMode failed with error :" + SSCApi.ErrorToString(result));
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
            CoreMotionAxisStatus cmAxis = _CmStatus.AxesStatus[this.AxisIndex];//要抓對軸
            switch (parameter)
            {
                case TorqueParameter.ActualTorque:
                    break;
                case TorqueParameter.SpeedLimit:
                    cmAxis.VelocityCmd = value;

                    break;
                case TorqueParameter.TargetTorque:
                    cmAxis.TorqueCmd = value;
                    
                    break;
                case TorqueParameter.TorqueLimit:
                    int result = _device_cm.Torque.SetMaxTrqLimit(this.AxisIndex, value);
                    if (result != (uint)ErrorCode.None)
                        throw new Exception("Set MaxTrqLimit failed with error :" + SSCApi.ErrorToString(result));
                    break;
            }
        }
        public override int GetTorqueModeParameter(TorqueParameter parameter)
        {
            CoreMotionAxisStatus cmAxis = _CmStatus.AxesStatus[this.AxisIndex];//要抓對軸
            
            switch (parameter)
            {
                case TorqueParameter.ActualTorque:
                    double actualTorque = cmAxis.ActualTorque;
                    int actTorque = (int)Math.Round(actualTorque);
                    return actTorque;
                case TorqueParameter.SpeedLimit:
                    break;
                case TorqueParameter.TargetTorque:
                    double targetTorque = cmAxis.TorqueCmd;
                    int target = (int)Math.Round(targetTorque);
                    return target;
                case TorqueParameter.TorqueLimit:
                    double maxTorque = 0;
                    _device_cm.Torque.GetMaxTrqLimit(this.AxisIndex, ref maxTorque);
                    int torqueLimit = (int)Math.Round(maxTorque);
                    return torqueLimit;
                    
            }
            throw new Exception("GetTorqueModeParameter illega parameter.");
        }
        public override void TorqueMove()
        {
            Torque.TrqCommand trq= new();
            int result = _device_cm.AxisControl.SetAxisCommandMode(this.AxisIndex, AxisCommandMode.Torque);
            if (result != (int)ErrorCode.None)
                throw new Exception("Set TorqueMode failed with error :" + SSCApi.ErrorToString(result));
            result = _device_cm.Torque.StartTrq(trq);
            if (result != (int)ErrorCode.None)
                throw new Exception("Start Torque failed with error :" + SSCApi.ErrorToString(result));
        }
        public override void TorqueStop()
        {
            int result = _device_cm.Torque.StopTrq(this.AxisIndex);
            if (result != (uint)ErrorCode.None)
                throw new Exception("Stop Torque failed with error :" + SSCApi.ErrorToString(result));
        }

        public override OperationState GetState()//回傳Ready, Homing ...等等的狀態
        {
            _device_cm.GetStatus(ref _CmStatus);
            CoreMotionAxisStatus status = _CmStatus.AxesStatus[this.AxisIndex];
            OperationState currentState = status.OpState;

            /*
             public enum OperationState
            {
                Idle = 0,//軸為怠速狀態，為在執行指令。
                Pos = 1,//軸正在執行位置指令
                Jog = 2,//軸正在執行步進指令
                Home = 3,//軸正在執行原點復歸動作
                Sync = 4,//軸正在與其他軸同步
                GantryHome = 5,//軸正在執行龍門原點復歸動作
                Stop = 6,//軸正在執行停止指令
                Intpl = 7,//軸執行插補指令
                Velocity = 8,//軸正在執行速度指令
                ConstLinearVelocity = 9,//軸正在執行恆定線速度指令
                Trq = 10,//軸正在執行轉矩指令
                DirectControl = 11,//軸正在以直接指令模式動作
                PVT = 12,//軸正在執行PVT指令
                ECAM = 13,//軸正在由ECAM指令與其他軸同步
                SyncCatchUp = 14,//軸正在執行追趕其他軸的動作
                DancerControl = 15//軸正在執行變位控制指令
            }
             */

            return currentState;
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