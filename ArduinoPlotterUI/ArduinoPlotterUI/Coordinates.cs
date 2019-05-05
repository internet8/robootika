using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace ArduinoPlotterUI
{
    [Serializable()]
    public class Coordinates
    {
        public int? X { get; set; }
        public int? Y { get; set; }
        public int Type { get; set; } // 0 is pencil drawing, 1 is line, 2 is rectangle and 3 is ellips
        public Color Paint { get; set; }
        public Boolean Pencil_up { get; set; }

        public Coordinates(int? x, int? y, int type, Color paint, Boolean pencil_up)
        {
            X = x;
            Y = y;
            Type = type;
            Paint = paint;
            Pencil_up = pencil_up;
        }

        [STAThread]
        static void Main()
        {
            Debug.Listeners.Add(new ConsoleTraceListener());
            Debug.WriteLine("Application started");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ArduinoPlotterUI_form());
        }
    }
}
