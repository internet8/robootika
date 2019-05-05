namespace CubeSolver
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.comboBoxBR = new System.Windows.Forms.ComboBox();
            this.ConnStatus = new System.Windows.Forms.Label();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxReceive = new System.Windows.Forms.TextBox();
            this.buttonCP = new System.Windows.Forms.Button();
            this.buttonOP = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxCam = new System.Windows.Forms.ComboBox();
            this.camBox = new System.Windows.Forms.PictureBox();
            this.whiteBox = new System.Windows.Forms.PictureBox();
            this.redBox = new System.Windows.Forms.PictureBox();
            this.greenBox = new System.Windows.Forms.PictureBox();
            this.blueBox = new System.Windows.Forms.PictureBox();
            this.orangeBox = new System.Windows.Forms.PictureBox();
            this.yellowBox = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.camBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.whiteBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orangeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yellowBox)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPort.FormattingEnabled = true;
            this.comboBoxPort.Location = new System.Drawing.Point(12, 41);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(165, 21);
            this.comboBoxPort.TabIndex = 0;
            // 
            // comboBoxBR
            // 
            this.comboBoxBR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBR.FormattingEnabled = true;
            this.comboBoxBR.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "115200"});
            this.comboBoxBR.Location = new System.Drawing.Point(183, 41);
            this.comboBoxBR.Name = "comboBoxBR";
            this.comboBoxBR.Size = new System.Drawing.Size(181, 21);
            this.comboBoxBR.TabIndex = 1;
            // 
            // ConnStatus
            // 
            this.ConnStatus.AutoSize = true;
            this.ConnStatus.Location = new System.Drawing.Point(12, 428);
            this.ConnStatus.Name = "ConnStatus";
            this.ConnStatus.Size = new System.Drawing.Size(75, 13);
            this.ConnStatus.TabIndex = 2;
            this.ConnStatus.Text = "Status: Closed";
            // 
            // textBoxSend
            // 
            this.textBoxSend.Enabled = false;
            this.textBoxSend.Location = new System.Drawing.Point(6, 193);
            this.textBoxSend.Multiline = true;
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(340, 20);
            this.textBoxSend.TabIndex = 0;
            this.textBoxSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSend_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxSend);
            this.groupBox2.Controls.Add(this.textBoxReceive);
            this.groupBox2.Location = new System.Drawing.Point(12, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(352, 221);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // textBoxReceive
            // 
            this.textBoxReceive.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBoxReceive.Enabled = false;
            this.textBoxReceive.ForeColor = System.Drawing.SystemColors.Window;
            this.textBoxReceive.Location = new System.Drawing.Point(6, 19);
            this.textBoxReceive.Multiline = true;
            this.textBoxReceive.Name = "textBoxReceive";
            this.textBoxReceive.ReadOnly = true;
            this.textBoxReceive.Size = new System.Drawing.Size(340, 168);
            this.textBoxReceive.TabIndex = 0;
            // 
            // buttonCP
            // 
            this.buttonCP.Enabled = false;
            this.buttonCP.ForeColor = System.Drawing.SystemColors.MenuText;
            this.buttonCP.Location = new System.Drawing.Point(102, 321);
            this.buttonCP.Name = "buttonCP";
            this.buttonCP.Size = new System.Drawing.Size(75, 23);
            this.buttonCP.TabIndex = 5;
            this.buttonCP.Text = "Close Port";
            this.buttonCP.UseVisualStyleBackColor = true;
            this.buttonCP.Click += new System.EventHandler(this.buttonCP_Click);
            // 
            // buttonOP
            // 
            this.buttonOP.ForeColor = System.Drawing.SystemColors.MenuText;
            this.buttonOP.Location = new System.Drawing.Point(12, 321);
            this.buttonOP.Name = "buttonOP";
            this.buttonOP.Size = new System.Drawing.Size(75, 23);
            this.buttonOP.TabIndex = 6;
            this.buttonOP.Text = "Open Port";
            this.buttonOP.UseVisualStyleBackColor = true;
            this.buttonOP.Click += new System.EventHandler(this.buttonOP_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Port Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Baud Rate";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(507, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Camera";
            // 
            // comboBoxCam
            // 
            this.comboBoxCam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCam.FormattingEnabled = true;
            this.comboBoxCam.Location = new System.Drawing.Point(507, 41);
            this.comboBoxCam.Name = "comboBoxCam";
            this.comboBoxCam.Size = new System.Drawing.Size(165, 21);
            this.comboBoxCam.TabIndex = 9;
            this.comboBoxCam.SelectedIndexChanged += new System.EventHandler(this.comboBoxCam_SelectedIndexChanged);
            // 
            // camBox
            // 
            this.camBox.BackColor = System.Drawing.SystemColors.Window;
            this.camBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.camBox.Location = new System.Drawing.Point(425, 81);
            this.camBox.Name = "camBox";
            this.camBox.Size = new System.Drawing.Size(320, 240);
            this.camBox.TabIndex = 11;
            this.camBox.TabStop = false;
            // 
            // whiteBox
            // 
            this.whiteBox.BackColor = System.Drawing.SystemColors.Window;
            this.whiteBox.Location = new System.Drawing.Point(448, 341);
            this.whiteBox.Name = "whiteBox";
            this.whiteBox.Size = new System.Drawing.Size(42, 42);
            this.whiteBox.TabIndex = 12;
            this.whiteBox.TabStop = false;
            this.whiteBox.Click += new System.EventHandler(this.whiteBox_Click);
            // 
            // redBox
            // 
            this.redBox.BackColor = System.Drawing.SystemColors.Window;
            this.redBox.Location = new System.Drawing.Point(494, 341);
            this.redBox.Name = "redBox";
            this.redBox.Size = new System.Drawing.Size(42, 42);
            this.redBox.TabIndex = 13;
            this.redBox.TabStop = false;
            this.redBox.Click += new System.EventHandler(this.redBox_Click);
            // 
            // greenBox
            // 
            this.greenBox.BackColor = System.Drawing.SystemColors.Window;
            this.greenBox.Location = new System.Drawing.Point(540, 341);
            this.greenBox.Name = "greenBox";
            this.greenBox.Size = new System.Drawing.Size(42, 42);
            this.greenBox.TabIndex = 14;
            this.greenBox.TabStop = false;
            this.greenBox.Click += new System.EventHandler(this.greenBox_Click);
            // 
            // blueBox
            // 
            this.blueBox.BackColor = System.Drawing.SystemColors.Window;
            this.blueBox.Location = new System.Drawing.Point(586, 341);
            this.blueBox.Name = "blueBox";
            this.blueBox.Size = new System.Drawing.Size(42, 42);
            this.blueBox.TabIndex = 15;
            this.blueBox.TabStop = false;
            this.blueBox.Click += new System.EventHandler(this.blueBox_Click);
            // 
            // orangeBox
            // 
            this.orangeBox.BackColor = System.Drawing.SystemColors.Window;
            this.orangeBox.Location = new System.Drawing.Point(632, 341);
            this.orangeBox.Name = "orangeBox";
            this.orangeBox.Size = new System.Drawing.Size(42, 42);
            this.orangeBox.TabIndex = 16;
            this.orangeBox.TabStop = false;
            this.orangeBox.Click += new System.EventHandler(this.orangeBox_Click);
            // 
            // yellowBox
            // 
            this.yellowBox.BackColor = System.Drawing.SystemColors.Window;
            this.yellowBox.Location = new System.Drawing.Point(678, 341);
            this.yellowBox.Name = "yellowBox";
            this.yellowBox.Size = new System.Drawing.Size(42, 42);
            this.yellowBox.TabIndex = 17;
            this.yellowBox.TabStop = false;
            this.yellowBox.Click += new System.EventHandler(this.yellowBox_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.yellowBox);
            this.Controls.Add(this.orangeBox);
            this.Controls.Add(this.blueBox);
            this.Controls.Add(this.greenBox);
            this.Controls.Add(this.redBox);
            this.Controls.Add(this.whiteBox);
            this.Controls.Add(this.camBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxCam);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOP);
            this.Controls.Add(this.buttonCP);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ConnStatus);
            this.Controls.Add(this.comboBoxBR);
            this.Controls.Add(this.comboBoxPort);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Name = "Form1";
            this.Text = "Cube Solver";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.camBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.whiteBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orangeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yellowBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.ComboBox comboBoxBR;
        private System.Windows.Forms.Label ConnStatus;
        private System.Windows.Forms.TextBox textBoxSend;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxReceive;
        private System.Windows.Forms.Button buttonCP;
        private System.Windows.Forms.Button buttonOP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxCam;
        private System.Windows.Forms.PictureBox camBox;
        private System.Windows.Forms.PictureBox whiteBox;
        private System.Windows.Forms.PictureBox redBox;
        private System.Windows.Forms.PictureBox greenBox;
        private System.Windows.Forms.PictureBox blueBox;
        private System.Windows.Forms.PictureBox orangeBox;
        private System.Windows.Forms.PictureBox yellowBox;
    }
}

