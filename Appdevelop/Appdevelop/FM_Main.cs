using System;
using System.Reflection;
using System.Windows.Forms;

namespace Appdevelop
{
    public partial class FM_Main : Form
    {
        public FM_Main()
        {
            InitializeComponent();
            //로그인 폼 호출
            Form1 Login = new Form1();
            Login.ShowDialog();

            tssUserName.Text = Login.Tag.ToString();
            if (Login.Tag.ToString() == "FAIL")
            {
                System.Environment.Exit(0);
            }


            // 버튼에 이벤트 (종료)추가
            this.stbExit.Click += new System.EventHandler(this.stbExit_Click);
            // 버튼에 이벤트 (닫기)추가
            this.stbClose.Click += new System.EventHandler(this.stbClose_Click);
            // 메뉴 클릭 이벤트 추가(테스트 화면 참조)
            this.M_SYSTEM.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.drp);
        }
        private void stbExit_Click(object sender, EventArgs e)     // 종료버튼
        {
            this.Close();
        }
        private void stbClose_Click(object sender, EventArgs e)     // 닫기버튼
        {
            // 열려있는 화면이 있는지 확인
            if (myTabControl1.TabPages.Count == 0) return;
            // 선택된 탭페이지를 닫는다.
            myTabControl1.SelectedTab.Dispose();
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

            //해당되는 폼이 이미 오픈되어 있는지 확인 후 활성화 또는 신규 오픈한다.
            for (int i = 0; i < myTabControl1.TabPages.Count; i++)
            {
                if (myTabControl1.TabPages[i].Name == e.ClickedItem.Name.ToString())
                {
                    myTabControl1.SelectedTab = myTabControl1.TabPages[i];
                    return;
                }
            }

            /* Showform.MdiParent = this;
             Showform.Show();*/

            myTabControl1.AddForm(Showform);   //탭페이지에 폼을 추가하여 오픈한다.
        }
    }

    public partial class MDIForm : TabPage
    {}
    public partial class MyTabControl : TabControl
    {
        public void AddForm(Form NewForm)
        {
            if (NewForm == null) return;    // 인자로 받은 폼이 없을 경우 실행중지
            NewForm.TopLevel = false;       // 인자로 받은 폼이 최상위 개채가 아님을 선언
            MDIForm page = new MDIForm();   // 탭 페이지 객체 생성
            page.Controls.Clear();          // 페이지 초기화
            page.Controls.Add(NewForm);     // 페이지에 폼 추가
            page.Text = NewForm.Text;       // 폼에서 지정한 명칭으로 탭 페이지 설정
            page.Name = NewForm.Name;       // 폼에서 지정한 이름으로 탭 페이지 설정
            base.TabPages.Add(page);        // 탭 컴트롤에 페이지를 추가한다.
            NewForm.Show();                 // 인자로 받은 폼을 보여준다.
            base.SelectedTab = page;        // 현재 선택된 페이지를 호촐한 폼의 페이지로 설정한다.
        }
    }
}
