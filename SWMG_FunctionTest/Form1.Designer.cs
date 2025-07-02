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
            groupBox2 = new GroupBox();
            buttonYJogNegative = new Button();
            buttonYJogPositive = new Button();
            numericUpDownYAbsTarget = new NumericUpDown();
            buttonYAbsMove = new Button();
            groupBox3 = new GroupBox();
            buttonZJogNegative = new Button();
            buttonZJogPositive = new Button();
            numericUpDownZAbsTarget = new NumericUpDown();
            buttonZAbsMove = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTargetPos).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownYAbsTarget).BeginInit();
            groupBox3.SuspendLayout();
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
            checkBoxDo1.Location = new Point(665, 293);
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
            checkBoxDI_Test.Location = new Point(665, 392);
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
            checkBoxDO_2.Location = new Point(665, 333);
            checkBoxDO_2.Name = "checkBoxDO_2";
            checkBoxDO_2.Size = new Size(57, 19);
            checkBoxDO_2.TabIndex = 13;
            checkBoxDO_2.Text = "DO_2";
            checkBoxDO_2.UseVisualStyleBackColor = true;
            checkBoxDO_2.CheckedChanged += checkBoxDO_2_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonJOGNegative);
            groupBox1.Controls.Add(buttonJOGPositive);
            groupBox1.Controls.Add(numericUpDownTargetPos);
            groupBox1.Controls.Add(buttonAbsMove);
            groupBox1.Location = new Point(399, 33);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(176, 107);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "SampleX";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(buttonYJogNegative);
            groupBox2.Controls.Add(buttonYJogPositive);
            groupBox2.Controls.Add(numericUpDownYAbsTarget);
            groupBox2.Controls.Add(buttonYAbsMove);
            groupBox2.Location = new Point(399, 161);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(176, 107);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "SampleY";
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
            groupBox3.Controls.Add(buttonZJogNegative);
            groupBox3.Controls.Add(buttonZJogPositive);
            groupBox3.Controls.Add(numericUpDownZAbsTarget);
            groupBox3.Controls.Add(buttonZAbsMove);
            groupBox3.Location = new Point(399, 284);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(176, 107);
            groupBox3.TabIndex = 16;
            groupBox3.TabStop = false;
            groupBox3.Text = "SampleZ";
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDownYAbsTarget).EndInit();
            groupBox3.ResumeLayout(false);
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
    }
}
