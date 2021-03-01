using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoadingIndicator.WinForms;

namespace ThreadAbort
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			
		}



		private void button1_Click_1(object sender, EventArgs e)
		{
			for (int x = 0; x < 500; x++)
			{
				for (int i = 40; i < 80; i += 2)
				{
					SelectControlExtensions.SafeSelect(i);
				}

				label1.Text = x.ToString();
				Application.DoEvents();
			}
		} 

		private void button2_Click(object sender, EventArgs e)
		{
			for (int x = 0; x < 500; x++)
			{
				for (int i = 40; i < 80; i += 2)
				{
					SelectControlExtensions2.SafeSelect(i);
				}

				label1.Text = x.ToString();
				Application.DoEvents();
			}
		}
	}
}
