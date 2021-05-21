using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DEV_Form;

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
            if (Login.Tag.ToString() == "FAIL")
            {
                System.Environment.Exit(0);
            }

            // 버튼에 이벤트 추가
            this.stbExit.Click += new System.EventHandler(this.stbExit_Click);
            // 메뉴 클릭 이벤트 추가(테스트 화면 참조)
            this.M_SYSTEM.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.drp);
        }
        private void stbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // 잘못누름9
        }

        private void timer1_Tick(object sender, EventArgs e)  //시계
        {
            tssNowDate.Text = DateTime.Now.ToString();
        }
        private void drp(object sender, ToolStripItemClickedEventArgs e)  //테스트 화면 출력 (DEV_Form 에서 참조해서 화면에 띄움)
        {
            // 1. 단순히 폼을 호출하는 경우!
            //DEV_Form.MDI_TEST Form = new DEV_Form.MDI_TEST();
            //Form.MdiParent = this;
            //Form.Show();

            // 2. 프로그램 호출
            Assembly assemb = Assembly.LoadFrom(Application.StartupPath + @"\" + "DEV_Form.DLL");
            Type typeForm = assemb.GetType("DEV_Form." + e.ClickedItem.Name.ToString(), true);
            Form Showform = (Form)Activator.CreateInstance(typeForm);

            Showform.MdiParent = this;
            Showform.Show();
        }
    }
}
