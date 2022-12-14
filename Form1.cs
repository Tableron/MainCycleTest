using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainCycleTest
{
    public partial class Form1 : Form
    {
        private MainCycle _mainCycle;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           label1.Text = $"Data={_mainCycle.Data}";
        }

        internal void SetMainCycle(MainCycle mainCycle)
        {
            _mainCycle = mainCycle;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _mainCycle.Stop();
        }
    }
}
