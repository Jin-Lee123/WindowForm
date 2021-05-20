using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appdevelop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;    //로그인 화면을 보이지 않게 한다.
            FM_PassWord fmpassword = new FM_PassWord();
            fmpassword.ShowDialog();
            this.Visible = true;     //화면닫으면 다시 로그인 화면 나옴!
        }
    }
}
