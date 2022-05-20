using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Calculator calculator;
        public Form1()
        {
            InitializeComponent();
        }
        //初始化大小
        private void Form1_Load(object sender, EventArgs e)
        {
             this.Width = 510;
            this.Height = 710;
            calculator = new Calculator();
        }

        //standard & programmer
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
			if (functionMenu.SelectedIndex == 0) {
                label1.Text = "standard";
                Clear();
                calculator.Clear();
                buttonHEX.Enabled = false;
                buttonOCT.Enabled = false;
                buttonBIN.Enabled = false;
                buttonA.Enabled = false;
                buttonB.Enabled = false;
                buttonC.Enabled = false;
                buttonD.Enabled = false;
                buttonE.Enabled = false;
                buttonF.Enabled = false;
                buttonPoint.Enabled = true;
                buttonNeg.Enabled = true;
                buttonMu.Enabled = true;
                buttonSub.Enabled = true;
                buttonDid.Enabled = true;
                buttonPlus.Enabled = true;
                buttonCos.Enabled = true;
                buttonSin.Enabled = true;
                buttonEvolution.Enabled = true;
                buttonLn.Enabled = true;
                buttonPow.Enabled = true;
                buttonSquare.Enabled = true;
			} else {
                label1.Text = "programmer";
                Clear();
                calculator.Clear();
                buttonHEX.Enabled = true;
                buttonOCT.Enabled = true;
                buttonBIN.Enabled = true;
                buttonA.Enabled = false;
                buttonB.Enabled = false;
                buttonC.Enabled = false;
                buttonD.Enabled = false;
                buttonE.Enabled = false;
                buttonF.Enabled = false;
                buttonPoint.Enabled = false;
                buttonNeg.Enabled = false;
                buttonMu.Enabled = false;
                buttonSub.Enabled = false;
                buttonDid.Enabled = false;
                buttonPlus.Enabled = false;
                buttonCos.Enabled = false;
                buttonSin.Enabled = false;
                buttonEvolution.Enabled = false;
                buttonLn.Enabled = false;
                buttonPow.Enabled = false;
                buttonSquare.Enabled = false;
            }
        }
		//屏幕显示函数
		#region
		private void DisplayNewData(char n) {
            calculator.Input(n);
            textBox1.Text = calculator.NewData;
		}
        private void DisplayNewData() {
            textBox1.Text = calculator.NewData;
		}
        private void DisplayOldData() {
            textBox1.Text = calculator.OldData;
		}
        private void DisplayInput(char op) {
            richTextBox1.Text = calculator.OldData + op;
		}
        private void DisplayInput(double a) {
            richTextBox1.Text += a.ToString() + '=' + calculator.OldData;
		}
        private void Clear() {
            richTextBox1.Text = "";
            textBox1.Text = "";
		}
		#endregion
		//number
		#region
		private void button7_Click(object sender, EventArgs e)
        {
            DisplayNewData('0');
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DisplayNewData('1');
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DisplayNewData('2');
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            DisplayNewData('3');
        }
        private void button9_Click(object sender, EventArgs e)
        {
            DisplayNewData('4');
        }
        private void button10_Click(object sender, EventArgs e)
        {
            DisplayNewData('5');
        }
        private void button11_Click(object sender, EventArgs e)
        {
            DisplayNewData('6');
        }
        private void button12_Click(object sender, EventArgs e)
        {
            DisplayNewData('7');
        }
        private void button13_Click(object sender, EventArgs e)
        {
            DisplayNewData('8');
        }
        private void button14_Click(object sender, EventArgs e)
        {
            DisplayNewData('9');
        }
        private void buttonA_Click(object sender, EventArgs e) {
            DisplayNewData('A');
        }
        private void buttonB_Click(object sender, EventArgs e) {
            DisplayNewData('B');
        }
        private void buttonC_Click(object sender, EventArgs e) {
            DisplayNewData('C');
        }
        private void buttonD_Click(object sender, EventArgs e) {
            DisplayNewData('D');
        }
        private void buttonE_Click(object sender, EventArgs e) {
            DisplayNewData('E');
        }
        private void buttonF_Click(object sender, EventArgs e) {
            DisplayNewData('F');
        }
        //point
        private void button8_Click(object sender, EventArgs e)
        {
            DisplayNewData('.');
        }
        #endregion
        //四则运算
        #region
        private void button5_Click(object sender, EventArgs e)
        {
            calculator.Calculate(Calculator.Operator.Sub);
            DisplayNewData();
            DisplayInput('-');
        }
        private void button4_Click(object sender, EventArgs e)
        {
            calculator.Calculate(Calculator.Operator.Plus);
            DisplayNewData();
            DisplayInput('+');
        }
        private void button15_Click(object sender, EventArgs e)
        {
            calculator.Calculate(Calculator.Operator.Divide);
            DisplayNewData();
            DisplayInput('÷');
        }
        private void button16_Click(object sender, EventArgs e)
        {
            calculator.Calculate(Calculator.Operator.Multiply);
            DisplayNewData();
            DisplayInput('×');
        }
        #endregion
        //等号
        private void button3_Click(object sender, EventArgs e)
        {
			if (calculator.NewData != "") {
                double a = Convert.ToDouble(calculator.NewData);
                calculator.Calculate(Calculator.Operator.NONE);
                DisplayOldData();
                DisplayInput(a);
			}
            
        }
        //正负号
        private void button17_Click(object sender, EventArgs e)
        {
            calculator.Calculate(Calculator.Operator.NegativeSign);
            DisplayNewData();
        }
        //清空与退位
		private void buttonBack_Click(object sender, EventArgs e) {
            calculator.Remove();
            DisplayNewData();
		}
		private void buttonClear_Click(object sender, EventArgs e) {
            calculator.Clear();
            Clear();
		}
        //进制转换
        #region
        private void buttonHEX_Click(object sender, EventArgs e) {
            buttonA.Enabled = true;
            buttonB.Enabled = true;
            buttonC.Enabled = true;
            buttonD.Enabled = true;
            buttonE.Enabled = true;
            buttonF.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            calculator.ScaleConvert(calculator.CurScale, Calculator.Scale.HEX);
            DisplayNewData();
		}
		private void buttonDEC_Click(object sender, EventArgs e) {
            buttonA.Enabled = false;
            buttonB.Enabled = false;
            buttonC.Enabled = false;
            buttonD.Enabled = false;
            buttonE.Enabled = false;
            buttonF.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            calculator.ScaleConvert(calculator.CurScale, Calculator.Scale.DEC);
            DisplayNewData();
        }
		private void buttonOCT_Click(object sender, EventArgs e) {
            buttonA.Enabled = false;
            buttonB.Enabled = false;
            buttonC.Enabled = false;
            buttonD.Enabled = false;
            buttonE.Enabled = false;
            buttonF.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = false;
            button9.Enabled = false;
            calculator.ScaleConvert(calculator.CurScale, Calculator.Scale.OCT);
            DisplayNewData();
        }
		private void buttonBIN_Click(object sender, EventArgs e) {
            buttonA.Enabled = false;
            buttonB.Enabled = false;
            buttonC.Enabled = false;
            buttonD.Enabled = false;
            buttonE.Enabled = false;
            buttonF.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            calculator.ScaleConvert(calculator.CurScale, Calculator.Scale.BIN);
            DisplayNewData();
        }
		#endregion
		//四个科学计算与扩展的两个科学计算
		#region
		private void buttonEvolution_Click(object sender, EventArgs e) {
            calculator.Calculate(Calculator.Operator.Evolution);
            DisplayNewData();
        }
		private void buttonSquare_Click(object sender, EventArgs e) {
            calculator.Calculate(Calculator.Operator.Square);
            DisplayNewData();
        }
		private void buttonCos_Click(object sender, EventArgs e) {
            calculator.Calculate(Calculator.Operator.Cos);
            DisplayNewData();
        }
		private void buttonSin_Click(object sender, EventArgs e) {
            calculator.Calculate(Calculator.Operator.Sin);
            DisplayNewData();
        }
		private void buttonLn_Click(object sender, EventArgs e) {
            calculator.Calculate(Calculator.Operator.Ln);
            DisplayNewData();
        }
		private void buttonPow_Click(object sender, EventArgs e) {
            calculator.Calculate(Calculator.Operator.Pow);
            DisplayNewData();
            DisplayInput('^');
        }
		#endregion
	}
}
