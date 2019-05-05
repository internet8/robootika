namespace ArduinoPlotterUI
{
    partial class ArduinoPlotterUI_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArduinoPlotterUI_form));
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.label_coordinates = new System.Windows.Forms.Label();
            this.label_all_coordinates = new System.Windows.Forms.Label();
            this.undo = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openJpgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highQualityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumQualityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowQualityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.differentColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highQualityToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumQualityToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lowQualityToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sudokuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.examplesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.albertEinsteinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smileyCatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.black = new System.Windows.Forms.Button();
            this.brown = new System.Windows.Forms.Button();
            this.yellow = new System.Windows.Forms.Button();
            this.blue = new System.Windows.Forms.Button();
            this.red = new System.Windows.Forms.Button();
            this.green = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Circle = new System.Windows.Forms.Button();
            this.Square = new System.Windows.Forms.Button();
            this.Line = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Pencil = new System.Windows.Forms.Button();
            this.sizeXaxis = new System.Windows.Forms.NumericUpDown();
            this.sizeYaxis = new System.Windows.Forms.NumericUpDown();
            this.labelXaxis = new System.Windows.Forms.Label();
            this.labelYaxis = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.console_out = new System.Windows.Forms.TextBox();
            this.console_in = new System.Windows.Forms.TextBox();
            this.plotterPort = new System.IO.Ports.SerialPort(this.components);
            this.redraw = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.myTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sizeXaxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeYaxis)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            this.Canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Canvas.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Canvas.Location = new System.Drawing.Point(124, 36);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(600, 450);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // label_coordinates
            // 
            this.label_coordinates.AutoSize = true;
            this.label_coordinates.Location = new System.Drawing.Point(585, 489);
            this.label_coordinates.Name = "label_coordinates";
            this.label_coordinates.Size = new System.Drawing.Size(69, 13);
            this.label_coordinates.TabIndex = 1;
            this.label_coordinates.Text = "Coordinates: ";
            this.label_coordinates.Click += new System.EventHandler(this.label_coordinates_Click);
            // 
            // label_all_coordinates
            // 
            this.label_all_coordinates.AutoSize = true;
            this.label_all_coordinates.Location = new System.Drawing.Point(121, 489);
            this.label_all_coordinates.Name = "label_all_coordinates";
            this.label_all_coordinates.Size = new System.Drawing.Size(88, 13);
            this.label_all_coordinates.TabIndex = 2;
            this.label_all_coordinates.Text = "All coordinates: 0";
            // 
            // undo
            // 
            this.undo.Location = new System.Drawing.Point(21, 289);
            this.undo.Name = "undo";
            this.undo.Size = new System.Drawing.Size(87, 23);
            this.undo.TabIndex = 3;
            this.undo.Text = "Undo";
            this.undo.UseVisualStyleBackColor = true;
            this.undo.Click += new System.EventHandler(this.undo_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.drawToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(946, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem1,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.openJpgToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem1.Text = "New";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open drawing";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openJpgToolStripMenuItem
            // 
            this.openJpgToolStripMenuItem.Name = "openJpgToolStripMenuItem";
            this.openJpgToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openJpgToolStripMenuItem.Text = "Open jpg";
            this.openJpgToolStripMenuItem.Click += new System.EventHandler(this.openJpgToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // drawToolStripMenuItem
            // 
            this.drawToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oneColorToolStripMenuItem,
            this.differentColorsToolStripMenuItem,
            this.sudokuToolStripMenuItem,
            this.examplesToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.drawToolStripMenuItem.Name = "drawToolStripMenuItem";
            this.drawToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.drawToolStripMenuItem.Text = "Draw";
            // 
            // oneColorToolStripMenuItem
            // 
            this.oneColorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.highQualityToolStripMenuItem,
            this.mediumQualityToolStripMenuItem,
            this.lowQualityToolStripMenuItem});
            this.oneColorToolStripMenuItem.Name = "oneColorToolStripMenuItem";
            this.oneColorToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.oneColorToolStripMenuItem.Text = "One color";
            // 
            // highQualityToolStripMenuItem
            // 
            this.highQualityToolStripMenuItem.Name = "highQualityToolStripMenuItem";
            this.highQualityToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.highQualityToolStripMenuItem.Text = "High quality";
            this.highQualityToolStripMenuItem.Click += new System.EventHandler(this.highQualityToolStripMenuItem_Click);
            // 
            // mediumQualityToolStripMenuItem
            // 
            this.mediumQualityToolStripMenuItem.Name = "mediumQualityToolStripMenuItem";
            this.mediumQualityToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.mediumQualityToolStripMenuItem.Text = "Medium quality";
            // 
            // lowQualityToolStripMenuItem
            // 
            this.lowQualityToolStripMenuItem.Name = "lowQualityToolStripMenuItem";
            this.lowQualityToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.lowQualityToolStripMenuItem.Text = "Low quality";
            // 
            // differentColorsToolStripMenuItem
            // 
            this.differentColorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.highQualityToolStripMenuItem1,
            this.mediumQualityToolStripMenuItem1,
            this.lowQualityToolStripMenuItem1});
            this.differentColorsToolStripMenuItem.Name = "differentColorsToolStripMenuItem";
            this.differentColorsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.differentColorsToolStripMenuItem.Text = "Different colors";
            // 
            // highQualityToolStripMenuItem1
            // 
            this.highQualityToolStripMenuItem1.Name = "highQualityToolStripMenuItem1";
            this.highQualityToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.highQualityToolStripMenuItem1.Text = "High quality";
            this.highQualityToolStripMenuItem1.Click += new System.EventHandler(this.highQualityToolStripMenuItem1_Click);
            // 
            // mediumQualityToolStripMenuItem1
            // 
            this.mediumQualityToolStripMenuItem1.Name = "mediumQualityToolStripMenuItem1";
            this.mediumQualityToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.mediumQualityToolStripMenuItem1.Text = "Medium quality";
            // 
            // lowQualityToolStripMenuItem1
            // 
            this.lowQualityToolStripMenuItem1.Name = "lowQualityToolStripMenuItem1";
            this.lowQualityToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.lowQualityToolStripMenuItem1.Text = "Low quality";
            // 
            // sudokuToolStripMenuItem
            // 
            this.sudokuToolStripMenuItem.Name = "sudokuToolStripMenuItem";
            this.sudokuToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.sudokuToolStripMenuItem.Text = "Sudoku";
            // 
            // examplesToolStripMenuItem
            // 
            this.examplesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.albertEinsteinToolStripMenuItem,
            this.smileyCatToolStripMenuItem});
            this.examplesToolStripMenuItem.Name = "examplesToolStripMenuItem";
            this.examplesToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.examplesToolStripMenuItem.Text = "Examples";
            // 
            // albertEinsteinToolStripMenuItem
            // 
            this.albertEinsteinToolStripMenuItem.Name = "albertEinsteinToolStripMenuItem";
            this.albertEinsteinToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.albertEinsteinToolStripMenuItem.Text = "Albert Einstein";
            // 
            // smileyCatToolStripMenuItem
            // 
            this.smileyCatToolStripMenuItem.Name = "smileyCatToolStripMenuItem";
            this.smileyCatToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.smileyCatToolStripMenuItem.Text = "Running Horse";
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.stopToolStripMenuItem.Text = "Abort";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.black);
            this.panel1.Controls.Add(this.brown);
            this.panel1.Controls.Add(this.yellow);
            this.panel1.Controls.Add(this.blue);
            this.panel1.Controls.Add(this.red);
            this.panel1.Controls.Add(this.green);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(21, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(87, 82);
            this.panel1.TabIndex = 6;
            // 
            // black
            // 
            this.black.BackColor = System.Drawing.Color.Black;
            this.black.Location = new System.Drawing.Point(54, 42);
            this.black.Name = "black";
            this.black.Size = new System.Drawing.Size(19, 20);
            this.black.TabIndex = 17;
            this.black.UseVisualStyleBackColor = false;
            this.black.Click += new System.EventHandler(this.black_Click);
            // 
            // brown
            // 
            this.brown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.brown.Location = new System.Drawing.Point(29, 42);
            this.brown.Name = "brown";
            this.brown.Size = new System.Drawing.Size(19, 20);
            this.brown.TabIndex = 16;
            this.brown.UseVisualStyleBackColor = false;
            this.brown.Click += new System.EventHandler(this.brown_Click);
            // 
            // yellow
            // 
            this.yellow.BackColor = System.Drawing.Color.Yellow;
            this.yellow.Location = new System.Drawing.Point(4, 42);
            this.yellow.Name = "yellow";
            this.yellow.Size = new System.Drawing.Size(19, 20);
            this.yellow.TabIndex = 15;
            this.yellow.UseVisualStyleBackColor = false;
            this.yellow.Click += new System.EventHandler(this.yellow_Click);
            // 
            // blue
            // 
            this.blue.BackColor = System.Drawing.Color.Blue;
            this.blue.Location = new System.Drawing.Point(54, 16);
            this.blue.Name = "blue";
            this.blue.Size = new System.Drawing.Size(19, 20);
            this.blue.TabIndex = 14;
            this.blue.UseVisualStyleBackColor = false;
            this.blue.Click += new System.EventHandler(this.blue_Click);
            // 
            // red
            // 
            this.red.BackColor = System.Drawing.Color.Red;
            this.red.Location = new System.Drawing.Point(4, 16);
            this.red.Name = "red";
            this.red.Size = new System.Drawing.Size(19, 20);
            this.red.TabIndex = 13;
            this.red.UseVisualStyleBackColor = false;
            this.red.Click += new System.EventHandler(this.red_Click);
            // 
            // green
            // 
            this.green.BackColor = System.Drawing.Color.Lime;
            this.green.Location = new System.Drawing.Point(29, 16);
            this.green.Name = "green";
            this.green.Size = new System.Drawing.Size(19, 20);
            this.green.TabIndex = 12;
            this.green.UseVisualStyleBackColor = false;
            this.green.Click += new System.EventHandler(this.green_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Colors";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.Circle);
            this.panel2.Controls.Add(this.Square);
            this.panel2.Controls.Add(this.Line);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(21, 134);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(87, 102);
            this.panel2.TabIndex = 7;
            // 
            // Circle
            // 
            this.Circle.Location = new System.Drawing.Point(3, 74);
            this.Circle.Name = "Circle";
            this.Circle.Size = new System.Drawing.Size(79, 23);
            this.Circle.TabIndex = 9;
            this.Circle.Text = "Ellips";
            this.Circle.UseVisualStyleBackColor = true;
            this.Circle.Click += new System.EventHandler(this.Circle_Click);
            // 
            // Square
            // 
            this.Square.Location = new System.Drawing.Point(3, 45);
            this.Square.Name = "Square";
            this.Square.Size = new System.Drawing.Size(79, 23);
            this.Square.TabIndex = 8;
            this.Square.Text = "Rectangle";
            this.Square.UseVisualStyleBackColor = true;
            this.Square.Click += new System.EventHandler(this.Square_Click);
            // 
            // Line
            // 
            this.Line.Location = new System.Drawing.Point(3, 16);
            this.Line.Name = "Line";
            this.Line.Size = new System.Drawing.Size(79, 23);
            this.Line.TabIndex = 7;
            this.Line.Text = "Line";
            this.Line.UseVisualStyleBackColor = true;
            this.Line.Click += new System.EventHandler(this.Line_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Shapes";
            // 
            // Pencil
            // 
            this.Pencil.Location = new System.Drawing.Point(21, 251);
            this.Pencil.Name = "Pencil";
            this.Pencil.Size = new System.Drawing.Size(87, 23);
            this.Pencil.TabIndex = 8;
            this.Pencil.Text = "Pencil";
            this.Pencil.UseVisualStyleBackColor = true;
            this.Pencil.Click += new System.EventHandler(this.Pencil_Click);
            // 
            // sizeXaxis
            // 
            this.sizeXaxis.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.sizeXaxis.Location = new System.Drawing.Point(5, 16);
            this.sizeXaxis.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.sizeXaxis.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.sizeXaxis.MinimumSize = new System.Drawing.Size(1, 0);
            this.sizeXaxis.Name = "sizeXaxis";
            this.sizeXaxis.Size = new System.Drawing.Size(78, 20);
            this.sizeXaxis.TabIndex = 9;
            this.sizeXaxis.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // sizeYaxis
            // 
            this.sizeYaxis.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.sizeYaxis.Location = new System.Drawing.Point(5, 16);
            this.sizeYaxis.Maximum = new decimal(new int[] {
            450,
            0,
            0,
            0});
            this.sizeYaxis.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.sizeYaxis.MinimumSize = new System.Drawing.Size(1, 0);
            this.sizeYaxis.Name = "sizeYaxis";
            this.sizeYaxis.Size = new System.Drawing.Size(77, 20);
            this.sizeYaxis.TabIndex = 10;
            this.sizeYaxis.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // labelXaxis
            // 
            this.labelXaxis.AutoSize = true;
            this.labelXaxis.Location = new System.Drawing.Point(26, 0);
            this.labelXaxis.Name = "labelXaxis";
            this.labelXaxis.Size = new System.Drawing.Size(35, 13);
            this.labelXaxis.TabIndex = 11;
            this.labelXaxis.Text = "X-axis";
            // 
            // labelYaxis
            // 
            this.labelYaxis.AutoSize = true;
            this.labelYaxis.Location = new System.Drawing.Point(26, 0);
            this.labelYaxis.Name = "labelYaxis";
            this.labelYaxis.Size = new System.Drawing.Size(35, 13);
            this.labelYaxis.TabIndex = 12;
            this.labelYaxis.Text = "Y-axis";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.labelXaxis);
            this.panel3.Controls.Add(this.sizeXaxis);
            this.panel3.Location = new System.Drawing.Point(21, 329);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(87, 47);
            this.panel3.TabIndex = 13;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.sizeYaxis);
            this.panel4.Controls.Add(this.labelYaxis);
            this.panel4.Location = new System.Drawing.Point(21, 395);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(87, 47);
            this.panel4.TabIndex = 14;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "file.bin";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "file.bin";
            // 
            // console_out
            // 
            this.console_out.BackColor = System.Drawing.Color.White;
            this.console_out.HideSelection = false;
            this.console_out.Location = new System.Drawing.Point(744, 37);
            this.console_out.Multiline = true;
            this.console_out.Name = "console_out";
            this.console_out.ReadOnly = true;
            this.console_out.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.console_out.Size = new System.Drawing.Size(181, 422);
            this.console_out.TabIndex = 15;
            // 
            // console_in
            // 
            this.console_in.BackColor = System.Drawing.Color.White;
            this.console_in.Location = new System.Drawing.Point(744, 465);
            this.console_in.Name = "console_in";
            this.console_in.Size = new System.Drawing.Size(181, 20);
            this.console_in.TabIndex = 17;
            this.console_in.KeyDown += new System.Windows.Forms.KeyEventHandler(this.console_in_KeyDown);
            // 
            // plotterPort
            // 
            this.plotterPort.PortName = "COM6";
            this.plotterPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.plotterPort_DataReceived);
            // 
            // redraw
            // 
            this.redraw.Location = new System.Drawing.Point(21, 462);
            this.redraw.Name = "redraw";
            this.redraw.Size = new System.Drawing.Size(87, 23);
            this.redraw.TabIndex = 18;
            this.redraw.Text = "Redraw";
            this.redraw.UseVisualStyleBackColor = true;
            this.redraw.Click += new System.EventHandler(this.redraw_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(744, 491);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(181, 11);
            this.progressBar.TabIndex = 19;
            // 
            // myTimer
            // 
            this.myTimer.Interval = 1000;
            this.myTimer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // ArduinoPlotterUI_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(946, 507);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.redraw);
            this.Controls.Add(this.console_in);
            this.Controls.Add(this.console_out);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.Pencil);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.undo);
            this.Controls.Add(this.label_all_coordinates);
            this.Controls.Add(this.label_coordinates);
            this.Controls.Add(this.Canvas);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArduinoPlotterUI_form";
            this.Text = "ArduinoPlotterUI";
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sizeXaxis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeYaxis)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.Label label_coordinates;
        private System.Windows.Forms.Label label_all_coordinates;
        private System.Windows.Forms.Button undo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oneColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem differentColorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sudokuToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Circle;
        private System.Windows.Forms.Button Square;
        private System.Windows.Forms.Button Line;
        private System.Windows.Forms.Button Pencil;
        private System.Windows.Forms.Button black;
        private System.Windows.Forms.Button brown;
        private System.Windows.Forms.Button yellow;
        private System.Windows.Forms.Button blue;
        private System.Windows.Forms.Button red;
        private System.Windows.Forms.Button green;
        private System.Windows.Forms.ToolStripMenuItem examplesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem albertEinsteinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smileyCatToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown sizeXaxis;
        private System.Windows.Forms.NumericUpDown sizeYaxis;
        private System.Windows.Forms.Label labelXaxis;
        private System.Windows.Forms.Label labelYaxis;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highQualityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumQualityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowQualityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highQualityToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mediumQualityToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem lowQualityToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox console_out;
        private System.Windows.Forms.TextBox console_in;
        public System.IO.Ports.SerialPort plotterPort;
        private System.Windows.Forms.Button redraw;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Timer myTimer;
        private System.Windows.Forms.ToolStripMenuItem openJpgToolStripMenuItem;
    }
}

