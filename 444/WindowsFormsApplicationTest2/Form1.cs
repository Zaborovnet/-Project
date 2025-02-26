﻿using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplicationTest2
{
    public partial class Form1 : Form
    {
        readonly BezierCurve bezier;
        readonly Marker[] markers = new Marker[4];

        public Form1()
        {
            InitializeComponent();
            markers[0] = new Marker(100, 200);
            markers[1] = new Marker(150, 250);
            markers[2] = new Marker(200, 150);
            markers[3] = new Marker(250, 200);
            for (int index = 0; index < markers.Length; index++)
            {
                Marker marker = markers[index];
                int i = index;
                marker.OnDrag += f =>
                {
                    bezier[i] = f;
                    pictureBox.Invalidate();
                };
                marker.OnMouseDown += f => { Cursor = Cursors.Hand; };
            }

            bezier = new BezierCurve(markers.Select(m => m.Location).ToArray());
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Pen pen = new Pen(Color.Gray, 1f);
            e.Graphics.DrawLines(pen, markers.Select(m=>m.Location).ToArray());
            foreach (Marker marker in markers)
            {
               marker.Draw(e.Graphics);
            }
            bezier.Draw(e.Graphics);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (Marker marker in markers)
                {
                    marker.MouseMove(e);
                    Thread.Sleep(0);
                }
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Marker marker in markers)
            {
                marker.MouseDown(e);
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (Marker marker in markers)
            {
                marker.MouseUp();
            }
            Cursor = Cursors.Arrow;
        }

        private void pictureBox_Click(object sender, System.EventArgs e)
        {

        }
    }
}