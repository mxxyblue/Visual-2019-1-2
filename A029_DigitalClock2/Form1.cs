﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace A029_DigitalClock2
{
    public partial class Form1 : Form
    {
        Timer t = new Timer();

        public Form1()
        {
            InitializeComponent();
            t.Interval = 10; //0.01초
            t.Tick += T_Tick;
            t.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            string s = String.Format("{0}:{1,3:000}",
                DateTime.Now.ToString(), DateTime.Now.Millisecond);
            label1.Text = s;
        }
    }
}
