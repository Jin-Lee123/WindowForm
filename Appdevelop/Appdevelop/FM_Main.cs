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
    public partial class FM_Main : Form
    {
        public FM_Main()
        {
            InitializeComponent();
            Form1 Login = new Form1();
            Login.ShowDialog();
            tssUserName.Text = Login.Tag.ToString();
            // 버튼에 이벤트 추가
            this.stbExit.Click += new System.EventHandler(this.stbExit_Click);
        }
        private void stbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // 잘못누름9
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tssNowDate.Text = DateTime.Now.ToString();
        }
    }
}
