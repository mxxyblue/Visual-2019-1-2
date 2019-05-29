using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A14_UnitConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            if(txtCm.Text != "")
            {
                txtInch.Text = (double.Parse(txtCm.Text) * 0.3937).ToString();
                txtFt.Text = (double.Parse(txtCm.Text) * 0.0328).ToString();
                txtYd.Text = (double.Parse(txtCm.Text) * 0.0109).ToString();
            }
            else if (txtInch.Text != "")
            {
                txtCm.Text = (double.Parse(txtInch.Text) *2.54).ToString();
                txtFt.Text = (double.Parse(txtInch.Text) * 0.0833).ToString();
                txtYd.Text = (double.Parse(txtInch.Text) * 0.0278).ToString();
            }
            else if (txtFt.Text != "")
            {
                txtInch.Text = (double.Parse(txtFt.Text) * 12).ToString();
                txtCm.Text = (double.Parse(txtFt.Text) * 30.48).ToString();
                txtYd.Text = (double.Parse(txtFt.Text) * 0.333).ToString();
            }
            else if (txtYd.Text != "")
            {
                txtInch.Text = (double.Parse(txtYd.Text) * 36).ToString();
                txtCm.Text = (double.Parse(txtYd.Text) * 91.438).ToString();
                txtFt.Text = (double.Parse(txtYd.Text) * 3).ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCm.Text = "";
            txtFt.Text = "";
            txtInch.Text = "";
            txtYd.Text = "";
        }
    }
}
