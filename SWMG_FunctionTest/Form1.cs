using SSCApiCLR;
using static SWMG_FunctionTest.MotionManager;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SWMG_FunctionTest
{
    public partial class Form1 : Form
    {
        byte currentDoValue1 = 0;
        public Form1()
        {
            InitializeComponent();

        }

        private void buttonOpenDevice_Click(object sender, EventArgs e)
        {
            if (!SWMG.IsOpened)
                SWMG.OpenDevice();
            IOManager.Initial(new IniFile(Path.Combine("C:\\Program Files\\MotionSoftware\\SWM-G\\", "IO.ini")));
            MotionManager.Initial(new IniFile(Path.Combine("C:\\Program Files\\MotionSoftware\\SWM-G\\", "Motor.ini")));
            MotionManager.SetMachineSpeedPercentage(100);

            //ÀË¬d¥Ø«eIOª¬ºA
            IOSWMG.GetIo().GetOutBit(0x00, 0x00, ref currentDoValue1);
            if (currentDoValue1 == 1)
                checkBoxDo1.Checked = true;
            else checkBoxDo1.Checked = false;
        }

        private void buttonCloseDevice_Click(object sender, EventArgs e)
        {
            if (SWMG.IsOpened)
            {
                
                MotionManager.Close();
                IOManager.Close();
                SWMG.CloseDevice();
            }
        }

        private void buttonStartServo_Click(object sender, EventArgs e)
        {
            if (SWMG.IsOpened)
            {
                for (int i = 0; i < Enum.GetNames(typeof(Axis)).Length; i++)
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
            MotionManager.StartJog(Axis.SampleX, false, 100);
        }

        private void buttonJOGNegative_MouseUp(object sender, MouseEventArgs e)
        {
            //SWMG.GetCoreMotion().Motion.ExecQuickStop(0);
            MotionManager.Stop(Axis.SampleX);
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
            MotionManager.StartJog(Axis.SampleX, true, 100);
        }

        private void buttonJOGPositive_MouseUp(object sender, MouseEventArgs e)
        {
            //SWMG.GetCoreMotion().Motion.ExecQuickStop(0);
            MotionManager.Stop(Axis.SampleX);
        }

        private void buttonStopServo_Click(object sender, EventArgs e)
        {
            if (SWMG.IsOpened)
            {
                if (SWMG.IsOpened)
                {
                    for (int i = 0; i < Enum.GetNames(typeof(Axis)).Length; i++)
                        MotionManager.ServoOff((Axis)i);
                }
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
            MotionManager.AbsMove(Axis.SampleX, (double)numericUpDownTargetPos.Value, 100);
        }

        private void checkBoxDo1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDo1.Checked)
            {
                IOManager.SetDO(IOManager.DO.testDO1, true);
            }
            else
            {
                IOManager.SetDO(IOManager.DO.testDO1, false);
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

        private void checkBoxDO_2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDO_2.Checked)
            {
                IOManager.SetDO(IOManager.DO.testDO2, true);

            }
            else
            {
                IOManager.SetDO(IOManager.DO.testDO2, false);

            }
        }

        private void buttonYJogNegative_MouseDown(object sender, MouseEventArgs e)
        {
            MotionManager.StartJog(Axis.SampleY, false, 100);
        }

        private void buttonYJogNegative_MouseUp(object sender, MouseEventArgs e)
        {
            MotionManager.Stop(Axis.SampleY);
        }

        private void buttonYAbsMove_Click(object sender, EventArgs e)
        {
            MotionManager.AbsMove(Axis.SampleY, (double)numericUpDownYAbsTarget.Value, 100);
        }

        private void buttonYJogPositive_MouseDown(object sender, MouseEventArgs e)
        {
            MotionManager.StartJog(Axis.SampleY, true, 100);
        }

        private void buttonYJogPositive_MouseUp(object sender, MouseEventArgs e)
        {
            MotionManager.Stop(Axis.SampleY);
        }

        private void buttonZJogNegative_MouseDown(object sender, MouseEventArgs e)
        {
            MotionManager.StartJog(Axis.SampleZ, false, 100);
        }

        private void buttonZJogNegative_MouseUp(object sender, MouseEventArgs e)
        {
            MotionManager.Stop(Axis.SampleZ);
        }

        private void buttonZAbsMove_Click(object sender, EventArgs e)
        {
            MotionManager.AbsMove(Axis.SampleZ, (double)numericUpDownZAbsTarget.Value, 100);
        }

        private void buttonZJogPositive_MouseDown(object sender, MouseEventArgs e)
        {
            MotionManager.StartJog(Axis.SampleZ, true, 100);
        }

        private void buttonZJogPositive_MouseUp(object sender, MouseEventArgs e)
        {
            MotionManager.Stop(Axis.SampleZ);
        }
    }
}
