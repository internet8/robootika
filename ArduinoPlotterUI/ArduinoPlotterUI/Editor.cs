using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;
using System.IO.Ports;


namespace ArduinoPlotterUI
{
    public partial class ArduinoPlotterUI_form : Form
    {
        Graphics g;
        bool pencil_down = false;
        Color color = Color.Black;
        int? x = null;
        int? y = null;
        bool pencil = true;
        bool drawRectangle = false;
        bool drawLine = false;
        bool drawEllips = false;
        bool drawing = false;
        bool abort = false;
        bool color_bool = false;
        int coordinatesInList = 0;
        int current_time = 0;
        Color current_color = Color.Empty;

        Point coordinates;
        List<Coordinates> allCoordinates = new List<Coordinates>();

        public ArduinoPlotterUI_form()
        {
            InitializeComponent();
            g = Canvas.CreateGraphics();
            createCoordinates(0, 0, 0, Color.Empty, true);
            display_on_console("Type help to see the list of commands." + Environment.NewLine);
            try
            {         
                plotterPort.Open();    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //System.Environment.Exit(1);
            }
            if (!plotterPort.IsOpen)
            {
                display_on_console("Plotter is not connected! Connect the plotter and restart the program." + Environment.NewLine);
            }
        }

        // hiir üles
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (pencil)
            {
                createCoordinates(x, y, 0, color, true);
                pencil_down = false;
                x = null;
                y = null;
            }
            else if (drawLine)
            {
                drawMyLine(new Point(x ?? e.X, y ?? e.Y), new Point(e.X, e.Y), color);
                x = null;
                y = null;
                createCoordinates(e.X, e.Y, 1, color, true);
            }
        }

        // hiir alla
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {

            if (pencil)
            {
                pencil_down = true;
            }
            else if (drawRectangle)
            {
                Pen p = new Pen(color);
                g.DrawRectangle(p, e.X, e.Y, (int)Math.Ceiling(sizeXaxis.Value), (int)Math.Ceiling(sizeYaxis.Value));
                getRectangleCoordinates(e.X, e.Y, (int)Math.Ceiling(sizeXaxis.Value), (int)Math.Ceiling(sizeYaxis.Value));
            }
            else if (drawEllips)
            {
                Pen p = new Pen(color);
                //g.DrawEllipse(p, e.X, e.Y, (int)Math.Ceiling(sizeXaxis.Value), (int)Math.Ceiling(sizeYaxis.Value));
                drawEllipseWithLines(e.X, e.Y, (int)Math.Ceiling(sizeXaxis.Value), (int)Math.Ceiling(sizeYaxis.Value), p);
            }
            else if (drawLine) {
                x = e.X;
                y = e.Y;
                createCoordinates(e.X, e.Y, 1, color, false);
            }
        }

        // hiir liigub
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEventArgs getCoordinates = (MouseEventArgs)e;
            coordinates = getCoordinates.Location;
            label_coordinates.Text = $"Coordinates: " + coordinates.ToString();

            if (pencil_down)
            {
                Pen p = new Pen(color);
                p.Width = 1;
                drawMyLine(new Point(x ?? e.X, y ?? e.Y), new Point(e.X, e.Y), color);
                x = e.X;
                y = e.Y;
                if ((x < 600 && y < 450) && (x > 0 && y > 0))
                {
                    createCoordinates(x, y, 0, color, false);
                }
            }
        }

        // ovaali joonistamine
        private void drawEllipseWithLines(int x, int y, int w, int h, Pen p)
        {
            int start_x = (int)Math.Round((x + w / 2) + (w / 2) * Math.Cos(0), 0);
            int start_y = (int)Math.Round((y + h / 2) + (h / 2) * Math.Sin(0), 0);
            int end_x = 0;
            int end_y = 0;
            for (double d = 0; d <= 6.3; d += 0.1)
            {
                createCoordinates(start_x, start_y, 3, color, false);
                end_x = (int)Math.Round((x + w / 2) + (w / 2) * Math.Cos(d), 0);
                end_y = (int)Math.Round((y + h / 2) + (h / 2) * Math.Sin(d), 0);
                drawMyLine(new Point(start_x, start_y), new Point(end_x, end_y), color);
                start_x = end_x;
                start_y = end_y;
            }
            createCoordinates(start_x, start_y, 3, color, true);
        }

        // ristküliku koordinaatide arvutamine
        private void getRectangleCoordinates(int x, int y, int w, int h)
        {
            int final_x = 0;
            int final_y = 0;
            for (int i = 1; i <= 5; i++)
            {
                switch (i)
                {
                    case 1:
                        final_x = x;
                        final_y = y;
                        break;
                    case 2:
                        final_x = x + w;
                        final_y = y;
                        break;
                    case 3:
                        final_x = x + w;
                        final_y = y + h;
                        break;
                    case 4:
                        final_x = x;
                        final_y = y + h;
                        break;
                    case 5:
                        final_x = x;
                        final_y = y;
                        break;
                }
                createCoordinates(final_x, final_y, 2, color, false);
            }
            createCoordinates(final_x, final_y, 2, color, true);
        }

        // koordinaadi objekti listi lisamine
        private void updateCoordinates(Coordinates c)
        {
            allCoordinates.Add(c);
            label_all_coordinates.Text = $"All coordinates: " + allCoordinates.Count.ToString();
        }

        // koordinaadi objekti loomine
        private void createCoordinates(int? x, int? y, int type, Color paint, Boolean pencil_up)
        {
            Coordinates coordinate = new Coordinates(x, y, type, paint, pencil_up);
            updateCoordinates(coordinate);
        }

        // joone tõmbamine
        private void drawMyLine(Point point1, Point point2, Color color)
        {
            Pen pen = new Pen(color);
            pen.Width = 1;
            g.DrawLine(pen, point1, point2);
        }

        // nupud
        private void label_coordinates_Click(object sender, EventArgs e)
        {
            // hetkel tühi
        }
        // exit nupp
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }
        // new page
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            allCoordinates.Clear();
            this.Refresh();
            createCoordinates(0, 0, 0, Color.Empty, true);
            g.Clear(Color.White);
        }
        // red color
        private void red_Click(object sender, EventArgs e)
        {
            color = Color.Red;
        }
        // green color
        private void green_Click(object sender, EventArgs e)
        {
            color = Color.Lime;
        }
        // blue color
        private void blue_Click(object sender, EventArgs e)
        {
            color = Color.Blue;
        }
        // yellow color
        private void yellow_Click(object sender, EventArgs e)
        {
            color = Color.Yellow;
        }
        // brown color
        private void brown_Click(object sender, EventArgs e)
        {
            color = Color.Brown;
        }
        // black color
        private void black_Click(object sender, EventArgs e)
        {
            color = Color.Black;
        }
        // square nupp
        private void Square_Click(object sender, EventArgs e)
        {
            drawRectangle = true;
            pencil = false;
            drawLine = false;
            drawEllips = false;
        }
        // pencil nupp
        private void Pencil_Click(object sender, EventArgs e)
        {
            pencil = true;
            drawRectangle = false;
            drawLine = false;
            drawEllips = false;
        }
        // line nupp
        private void Line_Click(object sender, EventArgs e)
        {
            drawLine = true;
            drawRectangle = false;
            pencil = false;
            drawEllips = false;
        }
        // ringi nupp
        private void Circle_Click(object sender, EventArgs e)
        {
            drawEllips = true;
            drawRectangle = false;
            pencil = false;
            drawLine = false;
        }
        // konsooli sisend
        private void console_in_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                display_on_console("-> " + console_in.Text);

                try
                {
                    if (console_in.Text.Equals("help"))
                    {
                        display_on_console("All commands: " + Environment.NewLine + "1. draw [color/colorless] [quality]" + Environment.NewLine + "2. calibrate [length(px)] [direction]"
                            + Environment.NewLine + "3. clear" + Environment.NewLine + "Examples: " + Environment.NewLine + "1. draw colorless high"
                            + Environment.NewLine + "2. calibrate 50 up" + Environment.NewLine);
                    }
                    else if (console_in.Text.Equals("clear"))
                    {
                        clear_console();
                    }
                    else if (console_in.Text.Equals("draw"))
                    {
                        // hetkel tühi
                    }
                    else if (console_in.Text.Substring(0, console_in.Text.IndexOf(" ")).Equals("calibrate"))
                    {
                        if (plotterPort.IsOpen)
                        {
                            display_on_console("Calibrating...");
                            string[] parameters = console_in.Text.Split(' ');
                            calibrate(Int32.Parse(parameters[1]), parameters[2]);
                        } else
                        {
                            display_on_console("Plotter isn't connected!");
                        }
                    }
                    else
                    {
                        display_on_console("Unknown command.");
                    }
                }
                catch
                {
                    display_on_console("Unknown command");
                }
            }
        }
        // faili avamine
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                pencil = false;
                drawRectangle = false;
                drawLine = false;
                drawEllips = false;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream fs = File.Open(openFileDialog1.FileName, FileMode.Open);
                    List<Coordinates> allCoordinates2 = (List<Coordinates>)formatter.Deserialize(fs);
                    drawUsingList(allCoordinates2);
                    allCoordinates.Clear();
                    foreach (Coordinates c in allCoordinates2)
                    {
                        if (c.X > -1)
                        {
                            allCoordinates.Add(c);
                        }
                    }
                    label_all_coordinates.Text = $"All coordinates: " + allCoordinates.Count.ToString();
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();   
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("You can't open this file.", ex);
            }
        }
        // JPG avamine
        private void openJpgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                pencil = false;
                drawRectangle = false;
                drawLine = false;
                drawEllips = false;
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.jpeg)|*.jpg; *.jpeg";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    Bitmap img = new Bitmap(open.FileName);
                    if (img.Width < 600 && img.Height < 450)
                    {
                        allCoordinates.Clear();
                        createCoordinates(0, 0, 0, Color.Empty, true);
                        for (int i = 0; i < img.Width; i += 2)
                        {
                            for (int j = 0; j < img.Height; j += 2)
                            {
                                Color pixel = img.GetPixel(i, j);

                                if (pixel.R <= 200 && pixel.G <= 200 && pixel.B <= 200)
                                {
                                    createCoordinates(i, j, 0, Color.Empty, false);
                                    createCoordinates(i, j, 0, Color.Empty, true);
                                }
                            }
                            Canvas.Image = img;
                        }
                        label_all_coordinates.Text = $"All coordinates: " + allCoordinates.Count.ToString();
                    } else
                    {
                        display_on_console("This image is too large. Max size is 600x450");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("You can't open this file.", ex);
            }
        }
        // faili salvestamine
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                pencil = false;
                drawRectangle = false;
                drawLine = false;
                drawEllips = false;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.Stream stream = File.OpenWrite(saveFileDialog1.FileName);
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, allCoordinates);                   
                    stream.Flush();
                    stream.Close();
                    stream.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error!", ex);
            }
        }

        // undo
        private void undo_Click(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = allCoordinates.Count-1; i >= 1; i--)
            {
                if (allCoordinates[i].Pencil_up)
                {
                    count++;
                }
                if (count == 1)
                {
                    allCoordinates.RemoveAt(i);
                    label_all_coordinates.Text = $"All coordinates: " + allCoordinates.Count.ToString();
                } else if (count == 2)
                {
                    break;
                } else if (i == 0)
                {
                    allCoordinates.RemoveAt(0);
                }
            }
            drawUsingList(allCoordinates);
        }

        // uuesti joonistamine
        private void redraw_Click(object sender, EventArgs e)
        {
            drawUsingList(allCoordinates);
        }

        // joonte tõmbamine listi abil
        private void drawUsingList(List<Coordinates> list)
        {
            this.Refresh();
            g.Clear(Color.White);
            for (int i = 0; i < list.Count(); i++)
            {
                if (!list[i].Pencil_up)
                {
                    drawMyLine(new Point(list[i].X ?? 0, list[i].Y ?? 0), new Point(list[i+1].X ?? 0, list[i+1].Y ?? 0), list[i].Paint);
                }
            }
        }

        // võtab listist andmed, kutsub vektori kalkuleerimise meetodi ning saadab tagastatud tulemi plotterile
        private void draw ()
        {
            try
            {
                //allCoordinates[coordinatesInList].X ?? default(int)
                //byte[] b = BitConverter.GetBytes(coordinatesInList);
                //plotterPort.Write(b, 0, 4);
                //plotterPort.Write();
                if (coordinatesInList < allCoordinates.Count-1 && drawing)
                {
                    if (color_bool && current_color != allCoordinates[coordinatesInList].Paint)
                    {
                        current_color = allCoordinates[coordinatesInList].Paint;
                        MessageBox.Show("Please insert " + current_color.ToString().Substring(current_color.ToString().IndexOf(".")+1) + " pencil.");
                    }
                    if (!abort)
                    {
                        set_progressbar(coordinatesInList * 100 / (allCoordinates.Count()-2));
                        plotterPort.Write(calculateLines(allCoordinates[coordinatesInList], allCoordinates[coordinatesInList+1]));
                        coordinatesInList++;
                    } else
                    {
                        plotterPort.Write(calculateLines(allCoordinates[coordinatesInList], allCoordinates[allCoordinates.Count()-1]));
                        drawing = false;
                        abort = false;
                    }
                } else if (coordinatesInList >= allCoordinates.Count - 1 || !drawing)
                {
                    coordinatesInList = 0;
                    drawing = false;
                    color_bool = false;
                    current_color = Color.Empty;
                    plotterPort.Write("done");
                    display_on_console("Completed!");
                    if (myTimer.Enabled)
                    {
                        display_on_console("Time: " + (current_time / 3600).ToString() + "h " + (current_time/60-(current_time / 3600)*60).ToString() + "m " + (current_time-(current_time/60)*60).ToString() + "s" + Environment.NewLine);
                        myTimer.Stop();
                        current_time = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // joone pikkuse ja suuna määramine
        private String calculateLines (Coordinates c1, Coordinates c2)
        {
            int x_length = 0;
            int y_length = 0;
            bool x_clockwise = false;
            bool y_clockwise = false;

            if (c2.X > c1.X)
            {
                x_length = (c2.X - c1.X) ?? default(int);
                x_clockwise = false;
            }
            else
            {
                x_length = (c1.X - c2.X) ?? default(int);
                x_clockwise = true;
            }

            if (c2.Y > c1.Y)
            {
                y_length = (c2.Y - c1.Y) ?? default(int);
                y_clockwise = false;
            }
            else
            {
                y_length = (c1.Y - c2.Y) ?? default(int);
                y_clockwise = true;
            }
            display_on_console("X=" + c1.X.ToString() + " : " + "Y=" + c1.Y.ToString());
            display_on_console("X=" + c2.X.ToString() + " : " + "Y=" + c2.Y.ToString());
            return x_length.ToString() + "A" + x_clockwise.ToString() + "B" + y_length.ToString() + "C" + y_clockwise.ToString() + "D" + c1.Pencil_up.ToString();
        }

        // menues
        private void highQualityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            start_drawing(false);
        }

        private void highQualityToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            start_drawing(true);
        }

        private void start_drawing (bool detect_if_color)
        {
            try
            {
                //plotterPort.Open();
                pencil = false;
                drawRectangle = false;
                drawLine = false;
                drawEllips = false;
                myTimer.Enabled = true;
                myTimer.Start();
                if (allCoordinates[allCoordinates.Count() - 1].X != 0 && allCoordinates[allCoordinates.Count() - 1].Y != 0)
                {
                    createCoordinates(0, 0, 0, Color.Empty, true);
                }
                display_on_console("Drawing started!");
                drawing = true;
                color_bool = detect_if_color;
                draw();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawing)
            {
                abort = true;
            }
        }
        // kui plotter saadab andmed
        private void plotterPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //MessageBox.Show(plotterPort.ReadLine());
            string received = plotterPort.ReadLine();
            received = received.Trim();

            if (received.Equals("done", StringComparison.OrdinalIgnoreCase))
            {
                draw();
            }
        }
        // taimer
        private void timer_Tick(object sender, EventArgs e)
        {
            current_time++;
        }
        // kuvab teksti konsoolis
        private void display_on_console (String text)
        {

            if (console_out.InvokeRequired == true)
            {
                console_out.Invoke((MethodInvoker)delegate { console_out.AppendText(text + Environment.NewLine); });
            }
            else
            {
                console_out.AppendText(text + Environment.NewLine);
            }
        }
        // progressbar kontrollimine
        private void set_progressbar (int value)
        {
            if (progressBar.InvokeRequired == true)
            {
                progressBar.Invoke((MethodInvoker)delegate { progressBar.Value = value; });
            } else
            {
                progressBar.Value = value;
            }
        }
        // puhastab konsooli
        private void clear_console ()
        {
            console_out.Text = "";
        }
        // kalibreerimise meetod
        private void calibrate (int length, String dir)
        {
            int x_length = 0;
            int y_length = 0;
            bool x_clockwise = false;
            bool y_clockwise = false;

            if (dir.Equals("up"))
            {
                y_length = length;
                y_clockwise = false;
            }
            else if (dir.Equals("down"))
            {
                y_length = length;
                y_clockwise = true;
            }
            else if (dir.Equals("right"))
            {
                x_length = length;
                x_clockwise = false;
            }
            else if (dir.Equals("left"))
            {
                x_length = length;
                x_clockwise = true;
            } else
            {
                display_on_console("Wrong direction parameter!");
            }

            plotterPort.Write(x_length.ToString() + "A" + x_clockwise.ToString() + "B" + y_length.ToString() + "C" + y_clockwise.ToString() + "D" + false.ToString());
        }
    }
}
