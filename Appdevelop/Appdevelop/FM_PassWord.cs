using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Appdevelop
{
    public partial class FM_PassWord : Form
    {
        private SqlConnection Connect = null;         // 데이터 베이스 접속 정보
        private SqlTransaction Tran;                  // 데이터 베이스 관리 권한
        private SqlCommand cmd = new SqlCommand();    // 데이터 베이스 명령 전달
        public FM_PassWord()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 비밀 번호를 변경한다.
            string strCon = "Data Source=(local);Initial Catalog=AppDev; Integrated Security=true;";
            Connect = new SqlConnection(strCon);

            Connect.Open(); //데이터 베이스에 접속 한다.

            // 1. ID 존재여부 확인
            // 2. 이전 비밀번호가 일치하는지 확인
            // 3. 바뀔 비밀번호로 등록
            // 4. 변경 여부 메세지 처리
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
