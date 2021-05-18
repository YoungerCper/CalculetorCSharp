using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationTestinMetricsLab1
{
    public partial class Form1 : Form
    {

        private string f;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f += "1";
            textBox1.Text = f;
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            f += "+";
            textBox1.Text = f;
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            f += "-";
            textBox1.Text = f;
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            f += "*";
            textBox1.Text = f;
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            f += "/";
            textBox1.Text = f;
        }

        private void buttonPi_Click(object sender, EventArgs e)
        {
            f += "pi";
            textBox1.Text = f;
        }

        private void buttonOpenBracket_Click(object sender, EventArgs e)
        {
            f += "(";
            textBox1.Text = f;
        }

        private void buttonCloseBracket_Click(object sender, EventArgs e)
        {
            f += ")";
            textBox1.Text = f;
        }

        private void buttonSqaureStep_Click(object sender, EventArgs e)
        {
            f += "^(0.5)";
            textBox1.Text = f;
        }

        private void buttonSquare_Click(object sender, EventArgs e)
        {
            f += "^2";
            textBox1.Text = f;
        }

        private void buttonExponential_Click(object sender, EventArgs e)
        {
            f += "e";
            textBox1.Text = f;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f += "2";
            textBox1.Text = f;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            f += "3";
            textBox1.Text = f;
        }

        private void button0_Click(object sender, EventArgs e)
        {
            f += "0";
            textBox1.Text = f;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            f += "4";
            textBox1.Text = f;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            f += "5";
            textBox1.Text = f;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            f += "6";
            textBox1.Text = f;
        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            f += ".";
            textBox1.Text = f;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            f += "7";
            textBox1.Text = f;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            f += "8";
            textBox1.Text = f;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            f += "9";
            textBox1.Text = f;
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            Formula f = new HardFormula(this.f);
            textBox1.Text = f.getResult();
            this.f = f.getResult();
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            f = "";
            textBox1.Text = f;
        }

        private void buttonClearLast_Click(object sender, EventArgs e)
        {
            string h = "";
            for(int i = 0; i< f.Length - 1; i++)
            {
                h += f[i];
            }
            f = h;
            textBox1.Text = f;
        }
    }
}

