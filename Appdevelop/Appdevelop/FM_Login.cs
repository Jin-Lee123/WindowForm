using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Appdevelop
{
    public partial class Form1 : Form
    {
        private SqlConnection Connect = null;         // 데이터 베이스 접속 정보
        private SqlTransaction Tran;                  // 데이터 베이스 관리 권한
        private SqlCommand cmd = new SqlCommand();    // 데이터 베이스 명령 전달
        public Form1()
        {
            InitializeComponent();
            this.Tag = "FAIL";
        }
        private int PwFailCount = 0;
        private void button2_Click(object sender, EventArgs e)

        {
            try
            {
                Connect = new SqlConnection("Data Source=61.105.9.203;Initial Catalog=AppDev; User ID=kfqs1;Password=1234");

                Connect.Open(); //데이터 베이스에 접속 한다.

                // DB 접속이 되지 않았을 경우 메세지 리턴후 이벤트 종료
                if (Connect.State != System.Data.ConnectionState.Open)
                {
                    MessageBox.Show("데이터 베이스 연결에 실패하였습니다.");
                    return;
                }

                // 1. ID,PW 입력위한 
                string Loinid = string.Empty;  // 로그인 ID   
                string PerPw = string.Empty;   // 비밀번호

                Loinid = textBox1.Text;
                PerPw = textBox2.Text;

                //기존 비밀번호 찾기
                SqlDataAdapter Adapter = new SqlDataAdapter("SELECT PW, username FROM TB_USER_LJ WHERE USERID = '" + Loinid + "'", Connect);
                DataTable DtTemp = new DataTable();


                Adapter.Fill(DtTemp);

                // 1. 아이디가 존재 여부 확인
                if (DtTemp.Rows.Count == 0)
                {
                    MessageBox.Show("ID가 일치하지 않습니다..");
                    textBox1.Text = "";      //일치 안하면 아이디 창 비워지구
                    textBox2.Focus();        //커서를 위치로 가져다 주는것
                    return;
                }

                // 2. 비밀번호가 일치하는지 확인
                else if (DtTemp.Rows[0]["PW"].ToString() != PerPw)
                {
                    textBox2.Text = "";
                    textBox2.Focus();
                    PwFailCount += 1;
                    MessageBox.Show("비밀번호가 일치하지 않습니다. 누적 횟수 :" + PwFailCount.ToString());
                    if (PwFailCount == 3)
                    {
                        MessageBox.Show("3회 이상 비밀번호 잘못입력하여 프로그램 종료.");
                        this.Close();
                    }
                    return;
                }

                // 3. 로그인 성공!
                else
                {
                    Common.LogInId = textBox1.Text;
                    Common.LogInName = DtTemp.Rows[0]["USERNAME"].ToString();  //유저 명을 Common 에 등록
                    this.Tag = DtTemp.Rows[0]["USERNAME"].ToString(); // 유저 명을 메인화면으로 보냄
                    MessageBox.Show(Tag + "님 반갑습니다");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("로그인 작업중 오류가 발생하였습니다." + ex.ToString());
            }
            finally
            {
                Connect.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;    //로그인 화면을 보이지 않게 한다.
            FM_PassWord fmpassword = new FM_PassWord();
            fmpassword.ShowDialog();
            this.Visible = true;     //화면닫으면 다시 로그인 화면 나옴!
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)  //엔터 쳐서 로그인 하기
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(null, null);  //엔터 쳐서 로그인 하기
            }
        }
    }
}
