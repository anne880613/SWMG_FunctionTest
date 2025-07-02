using SSCApiCLR;
using static SWMG_FunctionTest.MotionManager;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SWMG_FunctionTest
{
    public partial class Form1 : Form
    {
        byte currentDoValue1 = 0;
        byte currentDIValue = 0;
        public Form1()
        {
            InitializeComponent();
            label1.Text = "";

            IOManager.Initial(new IniFile(Path.Combine("C:\\Program Files\\MotionSoftware\\SWM-G\\", "IO.ini")));
            MotionManager.Initial(new IniFile(Path.Combine("C:\\Program Files\\MotionSoftware\\SWM-G\\", "Motor.ini")));
            MotionManager.SetMachineSpeedPercentage(100);
        }

        private void buttonOpenDevice_Click(object sender, EventArgs e)
        {
            if (!SWMG.IsOpened)
                SWMG.OpenDevice();

            //檢查目前IO狀態
            IOSWMG.GetIo().GetOutBit(0x00, 0x00, ref currentDoValue1);
            if (currentDoValue1 == 1)
                checkBoxDo1.Checked = true;
            else checkBoxDo1.Checked = false;
        }

        private void buttonCloseDevice_Click(object sender, EventArgs e)
        {
            if (SWMG.IsOpened)
                SWMG.CloseDevice();

            checkBoxDo1.Checked = false;
        }

        private void buttonStartServo_Click(object sender, EventArgs e)
        {
            if (SWMG.IsOpened)
            {
                for(int i= 0; i < Enum.GetNames(typeof(Axis)).Length; i++)
                    MotionManager.ServoOn((Axis)i);
            }
        }

        private void buttonJOGNegative_MouseDown(object sender, MouseEventArgs e)
        {
            //-----------------------------------------------------------------
            // Create a command value.
            //-----------------------------------------------------------------
            //Motion.JogCommand jogCommand = new Motion.JogCommand();

            //jogCommand.Profile.Type = SSCApiCLR.ProfileType.Trapezoidal;
            //jogCommand.Axis = 0;
            //jogCommand.Profile.Velocity = -10000;
            //jogCommand.Profile.Acc = 1000;
            //jogCommand.Profile.Dec = 1000;

            //SWMG.GetCoreMotion().Motion.StartJog(jogCommand);
            MotionManager.StartJog(0, false, 100);
        }

        private void buttonJOGNegative_MouseUp(object sender, MouseEventArgs e)
        {
            //SWMG.GetCoreMotion().Motion.ExecQuickStop(0);
            MotionManager.Stop(0);
        }

        private void buttonJOGPositive_MouseDown(object sender, MouseEventArgs e)
        {
            //-----------------------------------------------------------------
            // Create a command value.
            //-----------------------------------------------------------------
            //Motion.JogCommand jogCommand = new Motion.JogCommand();
            //jogCommand.Profile.Type = SSCApiCLR.ProfileType.Trapezoidal;
            //jogCommand.Axis = 0;
            ////posCommand.Target = 1000000;
            //jogCommand.Profile.Velocity = 10000;
            //jogCommand.Profile.Acc = 1000;
            //jogCommand.Profile.Dec = 1000;

            //SWMG.GetCoreMotion().Motion.StartJog(jogCommand);
            MotionManager.StartJog(0, true, 100);
        }

        private void buttonJOGPositive_MouseUp(object sender, MouseEventArgs e)
        {
            //SWMG.GetCoreMotion().Motion.ExecQuickStop(0);
            MotionManager.Stop(0);
        }

        private void buttonStopServo_Click(object sender, EventArgs e)
        {
            if (SWMG.IsOpened)
            {
                //int err = SWMG.GetCoreMotion().AxisControl.SetServoOn(0, 0);
                //if (err != ErrorCode.None)
                //{
                //    MessageBox.Show("關閉Servo失敗，錯誤碼：" + err);
                //}
                MotionManager.ServoOff(0);
            }
        }

        private void buttonAbsMove_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------------------
            // Create a command value.
            //-----------------------------------------------------------------
            //Motion.PosCommand posCommand = new Motion.PosCommand();
            //posCommand.Profile.Type = SSCApiCLR.ProfileType.Trapezoidal;
            //posCommand.Axis = 0;
            //posCommand.Target = (double)numericUpDownTargetPos.Value;
            //posCommand.Profile.Velocity = 10000;
            //posCommand.Profile.Acc = 1000;
            //posCommand.Profile.Dec = 1000;

            //SWMG.GetCoreMotion().Motion.StartPos(posCommand);
            MotionManager.AbsMove(Axis.SampleZ, (double)numericUpDownTargetPos.Value, 100);
        }

        private void checkBoxDo1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDo1.Checked)
            {
                IOManager.SetDO(IOManager.DO.testDO1, true);
                if (!IOManager.GetDO(IOManager.DO.testDO1))
                    label1.Text = "Set true Fial";
                else label1.Text = "Success";
            }
            else
            {
                IOManager.SetDO(IOManager.DO.testDO1, false);
                if (IOManager.GetDO(IOManager.DO.testDO1))
                    label1.Text = "Set false fail";
                else label1.Text = "Success";
            }

        }

        private void checkBoxDI_Test_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDI_Test.Checked)
            {
                IOManager.SetDIPass(IOManager.DI.testDI2);
                string err;
                if (IOManager.GetDI(IOManager.DI.testDI2, true, out err))
                    checkBoxDI_Test.Text = "DI_Test GetTestDI2 = true";
            }
            else
            {
                string err;
                IOManager.ResetDIPass(IOManager.DI.testDI2);
                if (!IOManager.GetDI(IOManager.DI.testDI2, false, out err))
                    checkBoxDI_Test.Text = "DI_Test GetTestDI2= false";
            }
        }

        private void FormClose(object sender, EventArgs e)
        {
            IOManager.Close();

        }

        private void buttonChangeDeviceName_Click(object sender, EventArgs e)
        {
            SWMG.SetDeviceName(textBoxDeviceName.Text);
        }
    }
}
