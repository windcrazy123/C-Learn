using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp1
{
	public class Calculator
	{
		//初始化属性
		public Calculator() {
			NewData = "";
			OldData = "";
		}
		//操作符与进制
		public enum Operator
		{
			NONE,
			Plus, Sub, Multiply, Divide, Pow,
			NegativeSign,
			Evolution, Square, Sin, Cos, Ln
		}
		public enum Scale
		{
			BIN = 2, OCT = 8, DEC = 10, HEX = 16
		}
		//1~16进制转换表
		readonly Dictionary<char, int> map = new Dictionary<char, int>() {
			{'0',0},{'1',1},{'2',2},{'3',3},{'4',4},{'5',5},
			{'6',6},{'7',7},{'8',8},{'9',9},{'A',10},
			{'B',11},{'C',12},{'D',13},{'E',14},{'F',15}
		};
		//用户输入数据记录
		public string NewData { get; set; }
		public string OldData { get; set; }
		//存储当前操作数与进制
		public Operator CurOperator = Operator.NONE;
		public Scale CurScale = Scale.DEC;
		//输入函数
		public void Input(char n) {
			if (NewData.Contains(".") && n == '.' || CurScale != Scale.DEC && n == '.')
				throw new Exception("Input Error");
			NewData += n;
		}
		//清空与撤销
		public void Clear() {
			NewData = "";
			OldData = "";
			CurOperator = Operator.NONE;
		}
		public void Remove() {
			if (NewData != "")
				NewData = NewData.Remove(NewData.Length - 1);
		}
		//计算方法
		public void Calculate(Operator @operator = Operator.NONE) {
			if ((int)@operator >= 6 && NewData != "") {
				CalculateNotSaveOp(@operator);
			} else {
				if (NewData == "" && OldData == "") {
					OldData = "0";
				}
				if (NewData == "") {
					CurOperator = @operator;
				} else {
					switch (CurOperator) {
						case Operator.NONE:
							OldData = NewData;
							NewData = "";
							break;
						case Operator.Plus:
							OldData = (Convert.ToDouble(OldData) + Convert.ToDouble(NewData)).ToString();
							NewData = "";
							break;
						case Operator.Sub:
							OldData = (Convert.ToDouble(OldData) - Convert.ToDouble(NewData)).ToString();
							NewData = "";
							break;
						case Operator.Multiply:
							OldData = (Convert.ToDouble(OldData) * Convert.ToDouble(NewData)).ToString();
							NewData = "";
							break;
						case Operator.Divide:
							OldData = (Convert.ToDouble(OldData) / Convert.ToDouble(NewData)).ToString();
							NewData = "";
							break;
						case Operator.Pow:
							OldData = Math.Pow(Convert.ToDouble(OldData), Convert.ToDouble(NewData)).ToString();
							NewData = "";
							break;
						default:
							break;
					}
					CurOperator = @operator;
				}
			}
		}
		private void CalculateNotSaveOp(Operator @operator) {
			switch (@operator) {
				case Operator.NegativeSign:
					NewData = (Convert.ToDouble(NewData) * (-1)).ToString();
					break;
				case Operator.Evolution:
					if (Convert.ToDouble(NewData)<0) {
						NewData = "";
					} else {
						NewData = Math.Pow(Convert.ToDouble(NewData), 0.5).ToString();
					}
					break;
				case Operator.Square:
					NewData = Math.Pow(Convert.ToDouble(NewData), 2).ToString();
					break;
				case Operator.Sin:
					NewData = Math.Sin(Convert.ToDouble(NewData)).ToString();
					break;
				case Operator.Cos:
					NewData = Math.Cos(Convert.ToDouble(NewData)).ToString();
					break;
				case Operator.Ln:
					NewData = Math.Log(Convert.ToDouble(NewData)).ToString();
					break;
				default:
					break;
			}
		}
		//进制转换方法
		public void ScaleConvert(Scale scale, Scale toscale) {
			if (scale == toscale) {
				return;
			}
			if (NewData != "") {
				NewData = ConvertToDEC(scale, NewData);
				string s = "";
				NewData = DECToConvert(toscale, Convert.ToInt32(NewData), ref s);
				//CurScale = toscale;
			}
			CurScale = toscale;
		}
		private string DECToConvert(Scale toscale, int DecNum, ref string str) {
			if (DecNum == 0) {
				return null;
			} else {
				DECToConvert(toscale, DecNum / (int)toscale, ref str);
				foreach (var key in map.Keys) {
					if (map[key] == (DecNum % (int)toscale)) {
						str += key;
						break;
					}
				}
			}
			return str;
		}
		private string ConvertToDEC(Scale scale, string str) {
			if (scale != Scale.DEC) {
				string copyStr = Convert.ToDouble(str).ToString();
				str = "";
				if (scale == Scale.HEX) {
					for (int j = 0, i = copyStr.Length - 1; i >= 0; j++, i--)
						foreach (char key in map.Keys)
							if (copyStr[i] == key) {
								str += map[key] * Math.Pow((int)scale, j);
								break;
							}
				} else {
					for (int j = 0, i = copyStr.Length - 1; i >= 0; j++, i--)
						str += double.Parse(copyStr[i].ToString()) * Math.Pow((int)scale, j);
				}
			}
			return str;
		}
	}
}
