using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCalc
{
    public partial class ValueBox : Form
    {
        public ValueBox()
        {
            InitializeComponent();
        }

        public ValueBox(string _label, string _value)
        {
            InitializeComponent();
            label_label.Text = _label;
            tb_Value.Text = _value;
        }

        private void b_set_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string BoxValue
        {
            get { return tb_Value.Text; }
        }

        private void tb_Value_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
