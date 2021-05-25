using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEV_Form
{
    public partial class FM_CUST : Form
    {
        private SqlConnection Connect = null;  //접속 정보 객체 명령
        // 접속 주소
        private string strConn = "Data Source=61.105.9.203;Initial Catalog=AppDev; User ID=kfqs1;Password=1234";
        public FM_CUST()
        {
            InitializeComponent();
        }

        private void FM_CUST_Load(object sender, EventArgs e)
        {
            try
            {
                // 콤보박스 품목 상세 데이터 조회 및 추가
                // 접속 정보 커넥션에 등록 및 객체 선언
                Connect = new SqlConnection(strConn);
                Connect.Open();

                if (Connect.State != System.Data.ConnectionState.Open)
                {
                    MessageBox.Show("데이터 베이스 연결에 실패 하였습니다.");
                    return;
                }
                SqlDataAdapter adapter = new SqlDataAdapter("select DISTINCT CUSTTYPE from TB_CUST_LJ", Connect);
                DataTable dtTemp = new DataTable();
                adapter.Fill(dtTemp);


                // 원하는 날짜 픽스
                CustStart.Text = string.Format("{0:yyyy-MM-01}", DateTime.Now);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Connect.Close();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Connect = new SqlConnection(strConn);
                Connect.Open();
                if (Connect.State != System.Data.ConnectionState.Open)
                {
                    MessageBox.Show("데이터 베이스 연결에 실패하였습니다.");
                    return;
                }
                //조회를 위한 파라매터 설정
                string sCustCode = txtCustCode.Text;   //품목 코드
                string sCustName = txtCustName.Text;   //품목명
                string sStartDate = CustStart.Text;     //출시 시작일자
                string sEndDate = CustEnd.Text;         //출시 종료 일자


                string sBizType = "";
                if (rdo1.Checked == true) sBizType = "상용차부품";   
                if (rdo2.Checked == true) sBizType = "자동차부품";   
                if (rdo3.Checked == true) sBizType = "절삭가공";     
                if (rdo4.Checked == true) sBizType = "펌프압축기";   

                string sSql = "SELECT CUSTCODE,  " +
                                                             "       CUSTTYPE,    " +
                                                             "       CUSTNAME,    " +
                                                             "       BIZCLASS,    " +
                                                             "       BIZTYPE,     " +
                                                             "       USEFLAG,     " +
                                                             "       FIRSTDATE,   " +
                                                             "       MAKEDATE,    " +
                                                             "      MAKER  ,      " +
                                                             "      EDITDATE ,    " +
                                                             "       EDITOR     FROM TB_CUST_LJ WITH(NOLOCK) " +
                                                             " WHERE CUSTCODE LIKE '%" + sCustCode + "%' " +
                                                             "   AND CUSTNAME LIKE '%" + sCustName + "%' " +
                                                             "   AND BizType  = '" + sBizType + "'" +
                                                             "   AND FIRSTDATE BETWEEN '" + sStartDate + "' AND '" + sEndDate + "'";
                                                            


                SqlDataAdapter Adapter = new SqlDataAdapter(sSql, Connect);
                DataTable dtTemp = new DataTable();
                Adapter.Fill(dtTemp);

                if (dtTemp.Rows.Count == 0)
                {
                    dgvGrid.DataSource = null;
                    return;
                }
                dgvGrid.DataSource = dtTemp;  //데이터 그리드 뷰에 데이터 테이블 등록

                //그리드뷰의  헤더 명칭 선언
                dgvGrid.Columns["CUSTCODE"].HeaderText = "거래처 코드";
                dgvGrid.Columns["CUSTTYPE"].HeaderText = "거래처 타입";
                dgvGrid.Columns["CUSTNAME"].HeaderText = "거래처 명";
                dgvGrid.Columns["BIZCLASS"].HeaderText = "업태";
                dgvGrid.Columns["BIZTYPE"].HeaderText = "종목";
                dgvGrid.Columns["USEFLAG"].HeaderText = "사용여부";
                dgvGrid.Columns["FIRSTDATE"].HeaderText = "거래일자";
                dgvGrid.Columns["MAKEDATE"].HeaderText = "등록일자";
                dgvGrid.Columns["MAKER"].HeaderText = "등록자";
                dgvGrid.Columns["EDITDATE"].HeaderText = "수정일시";
                dgvGrid.Columns["EDITOR"].HeaderText = "수정자";

                //그리드 뷰의 폭 지정
                dgvGrid.Columns[0].Width = 100;
                dgvGrid.Columns[1].Width = 200;
                dgvGrid.Columns[2].Width = 200;
                dgvGrid.Columns[3].Width = 200;
                dgvGrid.Columns[4].Width = 100;

                //컬럼의 수정 여부를 지정 한다
                dgvGrid.Columns["CUSTCODE"].ReadOnly = true;
                dgvGrid.Columns["MAKER"].ReadOnly = true;
                dgvGrid.Columns["MAKEDATE"].ReadOnly = true;
                dgvGrid.Columns["EDITOR"].ReadOnly = true;
                dgvGrid.Columns["EDITDATE"].ReadOnly = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connect.Close();
            }
        }

        private void dgvGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //잘못누름
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //데이터 그리드 뷰 에 신규 행 추가
            DataRow dr = ((DataTable)dgvGrid.DataSource).NewRow();
            ((DataTable)dgvGrid.DataSource).Rows.Add(dr);
            dgvGrid.Columns["CUSTCODE"].ReadOnly = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 선택된 행 데이터 저장
            if (dgvGrid.Rows.Count == 0) return;
            if (MessageBox.Show("선택된 데이터를 등록 하시겠습니까?", "데이터등록",
                                MessageBoxButtons.YesNo) == DialogResult.No) return;

            string sCustCode = dgvGrid.CurrentRow.Cells["CUSTCODE"].Value.ToString();
            string sCustType = dgvGrid.CurrentRow.Cells["CUSTTYPE"].Value.ToString();
            string sCustName = dgvGrid.CurrentRow.Cells["CUSTNAME"].Value.ToString();
            string sBizClass = dgvGrid.CurrentRow.Cells["BIZCLASS"].Value.ToString();
            string sBizType = dgvGrid.CurrentRow.Cells["BIZTYPE"].Value.ToString();
            string sUseFlag = dgvGrid.CurrentRow.Cells["USEFLAG"].Value.ToString();
            string sFirstDate = dgvGrid.CurrentRow.Cells["FIRSTDATE"].Value.ToString();
            string sMakeDate = dgvGrid.CurrentRow.Cells["MAKEDATE"].Value.ToString();
            string sMaker = dgvGrid.CurrentRow.Cells["MAKER"].Value.ToString();
            string sEditDate = dgvGrid.CurrentRow.Cells["EDITDATE"].Value.ToString();
            string sEditor = dgvGrid.CurrentRow.Cells["EDITOR"].Value.ToString();

            SqlCommand cmd = new SqlCommand();
            SqlTransaction Tran;

            Connect = new SqlConnection(strConn);
            Connect.Open();

            //트랜잭션 설정
            Tran = Connect.BeginTransaction("TestTran");
            cmd.Transaction = Tran;
            cmd.Connection = Connect;

            cmd.CommandText = "UPDATE TB_CUST_LJ                                          " +
                                      "    SET CUSTCODE    = '" + sCustCode + "',         " +
                                      "        CUSTTYPE    = '" + sCustType + "',         " +
                                      "        CUSTNAME    = '" + sCustName + "',         " +
                                      "        BIZCLASS    = '" + sBizClass + "',         " +
                                      "        BIZTYPE     = '" + sBizType + "',          " +
                                      "        USEFLAG     = '" + sUseFlag + "',          " +
                                      "        FIRSTDATE   = '" + sFirstDate + "',        " +
                                      "        MAKEDATE    = '" + sMakeDate + "',         " +
                                      "        MAKER       = '" + sMaker + "',            " +
                                      "        EDITDATE    = GETDATE()                    " +
                                      "        EDITOR      = '" + Common.LogInId + "',    " +

                                      "  WHERE ITEMCODE = '" + sCustCode + "'" +
                                      " IF (@@ROWCOUNT =0)                   " +
                                      "INSERT INTO TB_CUST_LJ(CUSTCODE,CUSTTYPE ,CUSTNAME,BIZCLASS, BIZTYPE, USEFLAG,FIRSTDATE , MAKEDATE,MAKER , EDITDATE, EDITOR  ) " +
                                      "VALUES('" + sCustCode + "','" + sCustType + "','" + sCustName + "','" + sBizClass + "','" + sBizType + "','" + sUseFlag + "','" + sFirstDate + "','" + sMakeDate + "','" + sMaker + "',GETDATE(),'" + Common.LogInId + "')";
            cmd.ExecuteNonQuery();

            //성공시 DB COMMIT
            Tran.Commit();
            MessageBox.Show("정상적으로 등록 하였습니다.");
            Connect.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 선택된 행을 삭제한다.
            if (this.dgvGrid.Rows.Count == 0) return;
            if (MessageBox.Show("선택된 데이터를 삭제 하시겠습니까", "데이터삭제", MessageBoxButtons.YesNo)
                == DialogResult.No) return;

            SqlCommand cmd = new SqlCommand();
            SqlTransaction tran;

            Connect = new SqlConnection(strConn);
            Connect.Open();

            //트랙잭션 관리를 위한 권한 위임
            tran = Connect.BeginTransaction("TranStrat");
            cmd.Transaction = tran;
            cmd.Connection = Connect;

            try
            {
                string Custcode = dgvGrid.CurrentRow.Cells["CUSTCODE"].Value.ToString();
                cmd.CommandText = "delete TB_CUST_LJ WHERE CUSTCODE = '" + Custcode + "'";

                cmd.ExecuteNonQuery();

                //성공 시 DB Commit
                tran.Commit();
                MessageBox.Show("정상적으로 삭제 하였습니다.");
                btnSearch_Click(null, null);  //데이터 재조회
            }
            catch (Exception)
            {
                tran.Rollback();
                MessageBox.Show("데이터 삭제에 실패 하였습니다.");

            }
            finally
            {
                Connect.Close();
            }
        }
    }
    
}
