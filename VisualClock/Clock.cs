using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace VisualClock {
    public class Clock : Control {

        CancellationTokenSource CancelHandler = new CancellationTokenSource();
        Task TimerTask;

        ContextMenuStrip menu = new ContextMenuStrip();

        public ClockMode ClockMode { get; set; } = ClockMode.Ticking;

        public Clock() : base() {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;

            MouseDown += ShowMenu;
            ToolStripMenuItem closeItem = new ToolStripMenuItem("Close");
            closeItem.Click += (o, e) => Environment.Exit(0);
            menu.Items.Add(closeItem);

            TimerTask = new Task(TimerHandler);
            Start();
        }

        private void ShowMenu(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                menu.Show((Control)sender, e.Location);
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            if (Parent == null) return;
            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.HighQuality;

            g.FillEllipse(new SolidBrush(BackColor), 0, 0, Width - 1, Height - 1);

            var time = DateTime.Now.TimeOfDay;
            time = new TimeSpan(time.Days, time.Hours % 12, time.Minutes, time.Seconds, time.Milliseconds);

            var mid = new PointF((float)Width / 2, (float)Height / 2);
            double radSeconds = Math.PI * ((270 + (int)(((int)time.TotalMilliseconds % 60000) * 0.006)) % 360) / 180;
            double radMinutes = Math.PI * ((270 + (int)(((int)time.TotalSeconds % 3600) * 0.1)) % 360) / 180;
            double radHours = Math.PI * ((270 + (int)(((int)time.TotalMinutes % 3600) * 0.1)) % 360) / 180;

            g.DrawLine(new Pen(Color.Black) { Width = 1.5f }, mid, new PointF((float)(mid.X + mid.X * 0.8 * Math.Cos(radSeconds)), (float)(mid.Y + mid.Y * 0.7 * Math.Sin(radSeconds))));
            g.DrawLine(new Pen(Color.Black) { Width = 2.5f }, mid, new PointF((float)(mid.X + mid.X * 0.65 * Math.Cos(radMinutes)), (float)(mid.Y + mid.Y * 0.7 * Math.Sin(radMinutes))));
            g.DrawLine(new Pen(Color.Black) { Width = 4f }, mid, new PointF((float)(mid.X + mid.X * 0.4 * Math.Cos(radHours)), (float)(mid.Y + mid.Y * 0.7 * Math.Sin(radHours))));

            for (int i = 0; i < 360; i += 5) {
                var radLoc = Math.PI * i / 180;
                var start = new PointF((float)(mid.X + mid.X * 0.95 * Math.Cos(radLoc)), (float)(mid.Y + mid.Y * 0.95 * Math.Sin(radLoc)));
                var end = new PointF((float)(mid.X + mid.X * Math.Cos(radLoc)), (float)(mid.Y + mid.Y * Math.Sin(radLoc)));
                g.DrawLine(new Pen(Color.Black) { Width = i % 30 == 0 ? 2 : .5f }, start, end);
            }

        }

        async void TimerHandler() {
            while (!CancelHandler.IsCancellationRequested) {
                Task timing = Task.Delay((int)ClockMode);
                Invalidate();
                await timing;
            }
        }

        public void Start() {
            TimerTask.Start();
        }

        public void Stop() {
            CancelHandler.Cancel();
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);

            if (Width != Height) {
                if (Width < Height) Width = Height;
                else Height = Width;
            }

            if (Width < 50) {
                Width = 50;
            }

            if (Height < 50) {
                Height = 50;
            }

            GraphicsPath newReg = new GraphicsPath();
            newReg.AddEllipse(new Rectangle(0, 0, Width, Height));
            Region = new Region(newReg);
        }


    }

    public enum ClockMode {
        Smooth = 80,
        ExtraSmooth = 16,
        Ticking = 850
    }
}
