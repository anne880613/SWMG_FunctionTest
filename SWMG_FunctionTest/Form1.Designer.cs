namespace SWMG_FunctionTest
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonOpenDevice = new Button();
            buttonCloseDevice = new Button();
            textBoxDeviceName = new TextBox();
            buttonStartServo = new Button();
            buttonJOGNegative = new Button();
            buttonJOGPositive = new Button();
            buttonStopServo = new Button();
            numericUpDownTargetPos = new NumericUpDown();
            buttonAbsMove = new Button();
            checkBoxDo1 = new CheckBox();
            checkBoxDI_Test = new CheckBox();
            buttonChangeDeviceName = new Button();
            checkBoxDO_2 = new CheckBox();
            groupBox1 = new GroupBox();
            buttonXStop = new Button();
            numericUpDownRelMove = new NumericUpDown();
            buttonXRelMove = new Button();
            groupBox2 = new GroupBox();
            buttonYStop = new Button();
            numericUpDownYRelMove = new NumericUpDown();
            buttonYJogNegative = new Button();
            buttonYRelMove = new Button();
            buttonYJogPositive = new Button();
            numericUpDownYAbsTarget = new NumericUpDown();
            buttonYAbsMove = new Button();
            groupBox3 = new GroupBox();
            buttonZStop = new Button();
            numericUpDownZRelMove = new NumericUpDown();
            buttonZRelMove = new Button();
            buttonZJogNegative = new Button();
            buttonZJogPositive = new Button();
            numericUpDownZAbsTarget = new NumericUpDown();
            buttonZAbsMove = new Button();
            buttonXPosition = new Button();
            labelXPosition = new Label();
            button1 = new Button();
            label1 = new Label();
            buttonHoming = new Button();
            labelXHoming = new Label();
            buttonSetXZero = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTargetPos).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRelMove).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownYRelMove).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownYAbsTarget).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownZRelMove).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownZAbsTarget).BeginInit();
            SuspendLayout();
            // 
            // buttonOpenDevice
            // 
            buttonOpenDevice.Location = new Point(46, 118);
            buttonOpenDevice.Name = "buttonOpenDevice";
            buttonOpenDevice.Size = new Size(118, 41);
            buttonOpenDevice.TabIndex = 0;
            buttonOpenDevice.Text = "OpenDevice";
            buttonOpenDevice.UseVisualStyleBackColor = true;
            buttonOpenDevice.Click += buttonOpenDevice_Click;
            // 
            // buttonCloseDevice
            // 
            buttonCloseDevice.Location = new Point(46, 182);
            buttonCloseDevice.Name = "buttonCloseDevice";
            buttonCloseDevice.Size = new Size(118, 41);
            buttonCloseDevice.TabIndex = 1;
            buttonCloseDevice.Text = "CloseDevice";
            buttonCloseDevice.UseVisualStyleBackColor = true;
            buttonCloseDevice.Click += buttonCloseDevice_Click;
            // 
            // textBoxDeviceName
            // 
            textBoxDeviceName.Location = new Point(46, 33);
            textBoxDeviceName.Name = "textBoxDeviceName";
            textBoxDeviceName.Size = new Size(118, 23);
            textBoxDeviceName.TabIndex = 2;
            // 
            // buttonStartServo
            // 
            buttonStartServo.Location = new Point(223, 64);
            buttonStartServo.Name = "buttonStartServo";
            buttonStartServo.Size = new Size(85, 37);
            buttonStartServo.TabIndex = 3;
            buttonStartServo.Text = "StartServo";
            buttonStartServo.UseVisualStyleBackColor = true;
            buttonStartServo.Click += buttonStartServo_Click;
            // 
            // buttonJOGNegative
            // 
            buttonJOGNegative.Location = new Point(6, 32);
            buttonJOGNegative.Name = "buttonJOGNegative";
            buttonJOGNegative.Size = new Size(75, 23);
            buttonJOGNegative.TabIndex = 4;
            buttonJOGNegative.Text = "JOG-";
            buttonJOGNegative.UseVisualStyleBackColor = true;
            buttonJOGNegative.MouseDown += buttonJOGNegative_MouseDown;
            buttonJOGNegative.MouseUp += buttonJOGNegative_MouseUp;
            // 
            // buttonJOGPositive
            // 
            buttonJOGPositive.Location = new Point(87, 32);
            buttonJOGPositive.Name = "buttonJOGPositive";
            buttonJOGPositive.Size = new Size(75, 23);
            buttonJOGPositive.TabIndex = 5;
            buttonJOGPositive.Text = "JOG+";
            buttonJOGPositive.UseVisualStyleBackColor = true;
            buttonJOGPositive.MouseDown += buttonJOGPositive_MouseDown;
            buttonJOGPositive.MouseUp += buttonJOGPositive_MouseUp;
            // 
            // buttonStopServo
            // 
            buttonStopServo.Location = new Point(223, 122);
            buttonStopServo.Name = "buttonStopServo";
            buttonStopServo.Size = new Size(85, 37);
            buttonStopServo.TabIndex = 6;
            buttonStopServo.Text = "StopServo";
            buttonStopServo.UseVisualStyleBackColor = true;
            buttonStopServo.Click += buttonStopServo_Click;
            // 
            // numericUpDownTargetPos
            // 
            numericUpDownTargetPos.Location = new Point(6, 72);
            numericUpDownTargetPos.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownTargetPos.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            numericUpDownTargetPos.Name = "numericUpDownTargetPos";
            numericUpDownTargetPos.Size = new Size(75, 23);
            numericUpDownTargetPos.TabIndex = 7;
            // 
            // buttonAbsMove
            // 
            buttonAbsMove.Location = new Point(87, 72);
            buttonAbsMove.Name = "buttonAbsMove";
            buttonAbsMove.Size = new Size(75, 23);
            buttonAbsMove.TabIndex = 8;
            buttonAbsMove.Text = "AbsMove";
            buttonAbsMove.UseVisualStyleBackColor = true;
            buttonAbsMove.Click += buttonAbsMove_Click;
            // 
            // checkBoxDo1
            // 
            checkBoxDo1.AutoSize = true;
            checkBoxDo1.Location = new Point(683, 291);
            checkBoxDo1.Name = "checkBoxDo1";
            checkBoxDo1.Size = new Size(57, 19);
            checkBoxDo1.TabIndex = 9;
            checkBoxDo1.Text = "DO_1";
            checkBoxDo1.UseVisualStyleBackColor = true;
            checkBoxDo1.CheckedChanged += checkBoxDo1_CheckedChanged;
            // 
            // checkBoxDI_Test
            // 
            checkBoxDI_Test.AutoSize = true;
            checkBoxDI_Test.Location = new Point(683, 390);
            checkBoxDI_Test.Name = "checkBoxDI_Test";
            checkBoxDI_Test.Size = new Size(66, 19);
            checkBoxDI_Test.TabIndex = 10;
            checkBoxDI_Test.Text = "DI_Test";
            checkBoxDI_Test.UseVisualStyleBackColor = true;
            checkBoxDI_Test.CheckedChanged += checkBoxDI_Test_CheckedChanged;
            // 
            // buttonChangeDeviceName
            // 
            buttonChangeDeviceName.Location = new Point(46, 60);
            buttonChangeDeviceName.Name = "buttonChangeDeviceName";
            buttonChangeDeviceName.Size = new Size(118, 41);
            buttonChangeDeviceName.TabIndex = 11;
            buttonChangeDeviceName.Text = "ChangeName";
            buttonChangeDeviceName.UseVisualStyleBackColor = true;
            buttonChangeDeviceName.Click += buttonChangeDeviceName_Click;
            // 
            // checkBoxDO_2
            // 
            checkBoxDO_2.AutoSize = true;
            checkBoxDO_2.Location = new Point(683, 331);
            checkBoxDO_2.Name = "checkBoxDO_2";
            checkBoxDO_2.Size = new Size(57, 19);
            checkBoxDO_2.TabIndex = 13;
            checkBoxDO_2.Text = "DO_2";
            checkBoxDO_2.UseVisualStyleBackColor = true;
            checkBoxDO_2.CheckedChanged += checkBoxDO_2_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonXStop);
            groupBox1.Controls.Add(numericUpDownRelMove);
            groupBox1.Controls.Add(buttonXRelMove);
            groupBox1.Controls.Add(buttonJOGNegative);
            groupBox1.Controls.Add(buttonJOGPositive);
            groupBox1.Controls.Add(numericUpDownTargetPos);
            groupBox1.Controls.Add(buttonAbsMove);
            groupBox1.Location = new Point(399, 19);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(263, 136);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "SampleX";
            // 
            // buttonXStop
            // 
            buttonXStop.Location = new Point(182, 32);
            buttonXStop.Name = "buttonXStop";
            buttonXStop.Size = new Size(75, 23);
            buttonXStop.TabIndex = 11;
            buttonXStop.Text = "STOP";
            buttonXStop.UseVisualStyleBackColor = true;
            buttonXStop.Click += buttonXStop_Click;
            // 
            // numericUpDownRelMove
            // 
            numericUpDownRelMove.Location = new Point(6, 103);
            numericUpDownRelMove.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownRelMove.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            numericUpDownRelMove.Name = "numericUpDownRelMove";
            numericUpDownRelMove.Size = new Size(75, 23);
            numericUpDownRelMove.TabIndex = 9;
            // 
            // buttonXRelMove
            // 
            buttonXRelMove.Location = new Point(87, 103);
            buttonXRelMove.Name = "buttonXRelMove";
            buttonXRelMove.Size = new Size(75, 23);
            buttonXRelMove.TabIndex = 10;
            buttonXRelMove.Text = "RelMove";
            buttonXRelMove.UseVisualStyleBackColor = true;
            buttonXRelMove.Click += buttonXRelMove_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(buttonYStop);
            groupBox2.Controls.Add(numericUpDownYRelMove);
            groupBox2.Controls.Add(buttonYJogNegative);
            groupBox2.Controls.Add(buttonYRelMove);
            groupBox2.Controls.Add(buttonYJogPositive);
            groupBox2.Controls.Add(numericUpDownYAbsTarget);
            groupBox2.Controls.Add(buttonYAbsMove);
            groupBox2.Location = new Point(399, 155);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(263, 136);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "SampleY";
            // 
            // buttonYStop
            // 
            buttonYStop.Location = new Point(182, 32);
            buttonYStop.Name = "buttonYStop";
            buttonYStop.Size = new Size(75, 23);
            buttonYStop.TabIndex = 12;
            buttonYStop.Text = "STOP";
            buttonYStop.UseVisualStyleBackColor = true;
            buttonYStop.Click += buttonYStop_Click;
            // 
            // numericUpDownYRelMove
            // 
            numericUpDownYRelMove.Location = new Point(6, 101);
            numericUpDownYRelMove.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownYRelMove.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            numericUpDownYRelMove.Name = "numericUpDownYRelMove";
            numericUpDownYRelMove.Size = new Size(75, 23);
            numericUpDownYRelMove.TabIndex = 11;
            // 
            // buttonYJogNegative
            // 
            buttonYJogNegative.Location = new Point(6, 32);
            buttonYJogNegative.Name = "buttonYJogNegative";
            buttonYJogNegative.Size = new Size(75, 23);
            buttonYJogNegative.TabIndex = 4;
            buttonYJogNegative.Text = "JOG-";
            buttonYJogNegative.UseVisualStyleBackColor = true;
            buttonYJogNegative.MouseDown += buttonYJogNegative_MouseDown;
            buttonYJogNegative.MouseUp += buttonYJogNegative_MouseUp;
            // 
            // buttonYRelMove
            // 
            buttonYRelMove.Location = new Point(87, 101);
            buttonYRelMove.Name = "buttonYRelMove";
            buttonYRelMove.Size = new Size(75, 23);
            buttonYRelMove.TabIndex = 12;
            buttonYRelMove.Text = "RelMove";
            buttonYRelMove.UseVisualStyleBackColor = true;
            buttonYRelMove.Click += buttonYRelMove_Click;
            // 
            // buttonYJogPositive
            // 
            buttonYJogPositive.Location = new Point(87, 32);
            buttonYJogPositive.Name = "buttonYJogPositive";
            buttonYJogPositive.Size = new Size(75, 23);
            buttonYJogPositive.TabIndex = 5;
            buttonYJogPositive.Text = "JOG+";
            buttonYJogPositive.UseVisualStyleBackColor = true;
            buttonYJogPositive.MouseDown += buttonYJogPositive_MouseDown;
            buttonYJogPositive.MouseUp += buttonYJogPositive_MouseUp;
            // 
            // numericUpDownYAbsTarget
            // 
            numericUpDownYAbsTarget.Location = new Point(6, 72);
            numericUpDownYAbsTarget.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownYAbsTarget.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            numericUpDownYAbsTarget.Name = "numericUpDownYAbsTarget";
            numericUpDownYAbsTarget.Size = new Size(75, 23);
            numericUpDownYAbsTarget.TabIndex = 7;
            // 
            // buttonYAbsMove
            // 
            buttonYAbsMove.Location = new Point(87, 72);
            buttonYAbsMove.Name = "buttonYAbsMove";
            buttonYAbsMove.Size = new Size(75, 23);
            buttonYAbsMove.TabIndex = 8;
            buttonYAbsMove.Text = "AbsMove";
            buttonYAbsMove.UseVisualStyleBackColor = true;
            buttonYAbsMove.Click += buttonYAbsMove_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(buttonZStop);
            groupBox3.Controls.Add(numericUpDownZRelMove);
            groupBox3.Controls.Add(buttonZRelMove);
            groupBox3.Controls.Add(buttonZJogNegative);
            groupBox3.Controls.Add(buttonZJogPositive);
            groupBox3.Controls.Add(numericUpDownZAbsTarget);
            groupBox3.Controls.Add(buttonZAbsMove);
            groupBox3.Location = new Point(399, 291);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(263, 136);
            groupBox3.TabIndex = 16;
            groupBox3.TabStop = false;
            groupBox3.Text = "SampleZ";
            // 
            // buttonZStop
            // 
            buttonZStop.Location = new Point(182, 32);
            buttonZStop.Name = "buttonZStop";
            buttonZStop.Size = new Size(75, 23);
            buttonZStop.TabIndex = 13;
            buttonZStop.Text = "STOP";
            buttonZStop.UseVisualStyleBackColor = true;
            buttonZStop.Click += buttonZStop_Click;
            // 
            // numericUpDownZRelMove
            // 
            numericUpDownZRelMove.Location = new Point(6, 100);
            numericUpDownZRelMove.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownZRelMove.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            numericUpDownZRelMove.Name = "numericUpDownZRelMove";
            numericUpDownZRelMove.Size = new Size(75, 23);
            numericUpDownZRelMove.TabIndex = 11;
            // 
            // buttonZRelMove
            // 
            buttonZRelMove.Location = new Point(87, 100);
            buttonZRelMove.Name = "buttonZRelMove";
            buttonZRelMove.Size = new Size(75, 23);
            buttonZRelMove.TabIndex = 12;
            buttonZRelMove.Text = "RelMove";
            buttonZRelMove.UseVisualStyleBackColor = true;
            buttonZRelMove.Click += buttonZRelMove_Click;
            // 
            // buttonZJogNegative
            // 
            buttonZJogNegative.Location = new Point(6, 32);
            buttonZJogNegative.Name = "buttonZJogNegative";
            buttonZJogNegative.Size = new Size(75, 23);
            buttonZJogNegative.TabIndex = 4;
            buttonZJogNegative.Text = "JOG-";
            buttonZJogNegative.UseVisualStyleBackColor = true;
            buttonZJogNegative.MouseDown += buttonZJogNegative_MouseDown;
            buttonZJogNegative.MouseUp += buttonZJogNegative_MouseUp;
            // 
            // buttonZJogPositive
            // 
            buttonZJogPositive.Location = new Point(87, 32);
            buttonZJogPositive.Name = "buttonZJogPositive";
            buttonZJogPositive.Size = new Size(75, 23);
            buttonZJogPositive.TabIndex = 5;
            buttonZJogPositive.Text = "JOG+";
            buttonZJogPositive.UseVisualStyleBackColor = true;
            buttonZJogPositive.MouseDown += buttonZJogPositive_MouseDown;
            buttonZJogPositive.MouseUp += buttonZJogPositive_MouseUp;
            // 
            // numericUpDownZAbsTarget
            // 
            numericUpDownZAbsTarget.Location = new Point(6, 72);
            numericUpDownZAbsTarget.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownZAbsTarget.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            numericUpDownZAbsTarget.Name = "numericUpDownZAbsTarget";
            numericUpDownZAbsTarget.Size = new Size(75, 23);
            numericUpDownZAbsTarget.TabIndex = 7;
            // 
            // buttonZAbsMove
            // 
            buttonZAbsMove.Location = new Point(87, 72);
            buttonZAbsMove.Name = "buttonZAbsMove";
            buttonZAbsMove.Size = new Size(75, 23);
            buttonZAbsMove.TabIndex = 8;
            buttonZAbsMove.Text = "AbsMove";
            buttonZAbsMove.UseVisualStyleBackColor = true;
            buttonZAbsMove.Click += buttonZAbsMove_Click;
            // 
            // buttonXPosition
            // 
            buttonXPosition.Location = new Point(683, 51);
            buttonXPosition.Name = "buttonXPosition";
            buttonXPosition.Size = new Size(95, 23);
            buttonXPosition.TabIndex = 17;
            buttonXPosition.Text = "GetXPosition";
            buttonXPosition.UseVisualStyleBackColor = true;
            buttonXPosition.Click += buttonXPosition_Click;
            // 
            // labelXPosition
            // 
            labelXPosition.AutoSize = true;
            labelXPosition.Location = new Point(683, 77);
            labelXPosition.Name = "labelXPosition";
            labelXPosition.Size = new Size(63, 15);
            labelXPosition.TabIndex = 18;
            labelXPosition.Text = "X Position";
            // 
            // button1
            // 
            button1.Location = new Point(209, 285);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 19;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(209, 327);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 20;
            label1.Text = "label1";
            // 
            // buttonHoming
            // 
            buttonHoming.Location = new Point(683, 118);
            buttonHoming.Name = "buttonHoming";
            buttonHoming.Size = new Size(75, 23);
            buttonHoming.TabIndex = 21;
            buttonHoming.Text = "Home";
            buttonHoming.UseVisualStyleBackColor = true;
            buttonHoming.Click += buttonHoming_Click;
            // 
            // labelXHoming
            // 
            labelXHoming.AutoSize = true;
            labelXHoming.Location = new Point(683, 144);
            labelXHoming.Name = "labelXHoming";
            labelXHoming.Size = new Size(64, 15);
            labelXHoming.TabIndex = 22;
            labelXHoming.Text = "X Homing";
            // 
            // buttonSetXZero
            // 
            buttonSetXZero.Location = new Point(683, 182);
            buttonSetXZero.Name = "buttonSetXZero";
            buttonSetXZero.Size = new Size(75, 23);
            buttonSetXZero.TabIndex = 23;
            buttonSetXZero.Text = "SetXZero";
            buttonSetXZero.UseVisualStyleBackColor = true;
            buttonSetXZero.Click += buttonSetXZero_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonSetXZero);
            Controls.Add(labelXHoming);
            Controls.Add(buttonHoming);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(labelXPosition);
            Controls.Add(buttonXPosition);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(checkBoxDO_2);
            Controls.Add(buttonChangeDeviceName);
            Controls.Add(checkBoxDI_Test);
            Controls.Add(checkBoxDo1);
            Controls.Add(buttonStopServo);
            Controls.Add(buttonStartServo);
            Controls.Add(textBoxDeviceName);
            Controls.Add(buttonCloseDevice);
            Controls.Add(buttonOpenDevice);
            Name = "Form1";
            Text = "Form1";
            FormClosing += FormClose;
            ((System.ComponentModel.ISupportInitialize)numericUpDownTargetPos).EndInit();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDownRelMove).EndInit();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDownYRelMove).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownYAbsTarget).EndInit();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDownZRelMove).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownZAbsTarget).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonOpenDevice;
        private Button buttonCloseDevice;
        private TextBox textBoxDeviceName;
        private Button buttonStartServo;
        private Button buttonJOGNegative;
        private Button buttonJOGPositive;
        private Button buttonStopServo;
        private NumericUpDown numericUpDownTargetPos;
        private Button buttonAbsMove;
        private CheckBox checkBoxDo1;
        private CheckBox checkBoxDI_Test;
        private Button buttonChangeDeviceName;
        private CheckBox checkBoxDO_2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button buttonYJogNegative;
        private Button buttonYJogPositive;
        private NumericUpDown numericUpDownYAbsTarget;
        private Button buttonYAbsMove;
        private GroupBox groupBox3;
        private Button buttonZJogNegative;
        private Button buttonZJogPositive;
        private NumericUpDown numericUpDownZAbsTarget;
        private Button buttonZAbsMove;
        private NumericUpDown numericUpDownRelMove;
        private Button buttonXRelMove;
        private NumericUpDown numericUpDownYRelMove;
        private Button buttonYRelMove;
        private NumericUpDown numericUpDownZRelMove;
        private Button buttonZRelMove;
        private Button buttonXStop;
        private Button buttonXPosition;
        private Label labelXPosition;
        private Button button1;
        private Label label1;
        private Button buttonHoming;
        private Label labelXHoming;
        private Button buttonYStop;
        private Button buttonZStop;
        private Button buttonSetXZero;
    }
}
