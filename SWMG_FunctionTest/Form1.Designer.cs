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
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTargetPos).BeginInit();
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
            buttonJOGNegative.Location = new Point(399, 71);
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
            buttonJOGPositive.Location = new Point(501, 71);
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
            numericUpDownTargetPos.Location = new Point(399, 136);
            numericUpDownTargetPos.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownTargetPos.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            numericUpDownTargetPos.Name = "numericUpDownTargetPos";
            numericUpDownTargetPos.Size = new Size(96, 23);
            numericUpDownTargetPos.TabIndex = 7;
            // 
            // buttonAbsMove
            // 
            buttonAbsMove.Location = new Point(501, 136);
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
            checkBoxDo1.Location = new Point(399, 228);
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
            checkBoxDI_Test.Location = new Point(399, 277);
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(484, 230);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 12;
            label1.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(buttonChangeDeviceName);
            Controls.Add(checkBoxDI_Test);
            Controls.Add(checkBoxDo1);
            Controls.Add(buttonAbsMove);
            Controls.Add(numericUpDownTargetPos);
            Controls.Add(buttonStopServo);
            Controls.Add(buttonJOGPositive);
            Controls.Add(buttonJOGNegative);
            Controls.Add(buttonStartServo);
            Controls.Add(textBoxDeviceName);
            Controls.Add(buttonCloseDevice);
            Controls.Add(buttonOpenDevice);
            Name = "Form1";
            Text = "Form1";
            FormClosing += FormClose;
            ((System.ComponentModel.ISupportInitialize)numericUpDownTargetPos).EndInit();
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
        private Label label1;
    }
}
