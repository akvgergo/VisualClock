using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace VisualClock {
    public partial class Form1 : Form {

        Point clickLoc = default(Point);
        bool moving = false;

        public Form1() {
            InitializeComponent();
            clock1.MouseDown += HandleMouseDown;
            clock1.MouseUp += HandleMouseUp;
            clock1.MouseMove += HandleMove;
        }

        private void HandleMove(object sender, MouseEventArgs e) {
            if (moving) {
                var control = (Control)sender;
                var newLoc = new Point(e.X + control.Location.X, e.Y + control.Location.Y);
                Location = new Point(Location.X - clickLoc.X + newLoc.X, Location.Y - clickLoc.Y + newLoc.Y);
            }
        }

        private void HandleMouseUp(object sender, MouseEventArgs e) {
            moving = false;
        }

        private void HandleMouseDown(object sender, MouseEventArgs e) {
            moving = true;
            var control = (Control)sender;
            clickLoc = new Point(e.X + control.Location.X, e.Y + control.Location.Y);
        }

        [DllImport("user32.dll")]
        public static extern void GetCursorPos(out Point p);
    }
}
