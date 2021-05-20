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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connect = new SqlConnection("Data Source=61.105.9.203;Initial Catalog=AppDev; User ID=kfqs;Password=1234");

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
           
            // SQL 조회문을 실행시키기 위한 어댑터
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT PW FROM TB_USER_LJ WHERE USERID = '" + Loinid + "'", Connect);

            // 데이터를 담을 그릇
            DataTable DtTemp = new DataTable();

            // 어댑터 실행 후 그릇에 데이터 담기
            Adapter.Fill(DtTemp);

            // 1. 아이디가 일치하는지 확인
            if (DtTemp.Rows[0]["ID"].ToString() != Loinid)
            {
                MessageBox.Show("ID가 일치하지 않습니다..");
                return;
            }

            // 2. 비밀번호가 일치하는지 확인
            else if (DtTemp.Rows[0]["PW"].ToString() != PerPw)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
                return;
            }

            // 3. 바뀔 비밀번호로 등록
            else
            {
                // 메세지 박스 Y/N 선택후 N 일 경우 리턴
                MessageBox.Show("로그인에 성공하셨습니다");
               
                Tran = Connect.BeginTransaction("TEST_Tran");
                cmd.Transaction = Tran;        // 커맨드에 트랙잭션 사용 여부 등록
                cmd.Connection = Connect;      // 커맨드에 접속 정보 입력
                cmd.CommandText = "UPDATE TB_USER_LJ SET PW = '" + PerPw + "' WHERE USERID ='" + Loinid + "'";
                cmd.ExecuteNonQuery();         // C,U,D 실행 함수 실행

                Tran.Commit();                 // 변경 내용 승인
                MessageBox.Show("정상적으로 변경 하였습니다.");
                this.Close();
            }
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
