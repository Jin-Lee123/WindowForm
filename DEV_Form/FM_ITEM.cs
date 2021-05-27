using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace DEV_Form
{

    public partial class FM_ITEM : Form, ChildInterFace
    {
        private SqlConnection Connect = null;  //접속 정보 객체 명령
        // 접속 주소
        private string strConn = "Data Source=222.235.141.8;Initial Catalog=AppDev; User ID=kfqs1;Password=1234";

        public FM_ITEM()
        {
            InitializeComponent();
        }
        
        public void Inquire()
        {
            btnSearch_Click(null, null);
        }
        public void DoNew()
        {

        }
        public void Delete()
        {

        }
        public void Save()
        {

        }

        private void FM_ITEM_Load(object sender, EventArgs e)
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
                SqlDataAdapter adapter = new SqlDataAdapter("select DISTINCT ITEMDESC from TB_TESTITEM_LJ", Connect);
                DataTable dtTemp = new DataTable();
                adapter.Fill(dtTemp);

                cboItemDesc.DataSource = dtTemp;
                cboItemDesc.DisplayMember = "ITEMDESC";   //눈으로 보여줄 항목
                cboItemDesc.ValueMember = "ITEMDESC";   //실제 데이터를 처리할 코드 항목
                cboItemDesc.Text = "";

                // 원하는 날짜 픽스
                dtpStart.Text = string.Format("{0:yyyy-MM-01}", DateTime.Now);
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
                string sItemCode = txtItemCode.Text;   //품목 코드
                string sItemName = txtItemCode.Text;   //품목명
                string sStartDate = dtpStart.Text;     //출시 시작일자
                string sEndDate = dtpEnd.Text;         //출시 종료 일자
                string sItemdesc = cboItemDesc.Text;   //이름으로만 검색

                string sEndFlag = "N";
                if (rdoEnd.Checked == true) sEndFlag = "Y";   //단종여부
                if (chkNameOnly.Checked == true) sItemCode = "";   //이름으로만 검색

                SqlDataAdapter Adapter = new SqlDataAdapter("SELECT ITEMCODE,  "                            +
                                                             "       ITEMNAME,  "                           +
                                                             "       ITEMDESC,  "                           +
                                                             "       ITEMDESC2, "                           +
                                                             "       CASE WHEN ENDFLAG = 'Y' THEN '단종'"   +
                                                             "            WHEN ENDFLAG = 'N' THEN '생산'"   +
                                                             "            END AS ENDFLAG,"                  +
                                                             "       PRODDATE,  "                           +
                                                             "       MAKEDATE,  "                           +
                                                             "       MAKER,     "                           +
                                                             "       EDITDATE,  "                           +
                                                             "       EDITOR     "                           +
                                                             "  FROM TB_TESTITEM_LJ WITH(NOLOCK) "          +
                                                             " WHERE ITEMCODE LIKE '%" + sItemCode + "%' "  +
                                                             "   AND ITEMNAME LIKE '%" + sItemName + "%' "  +
                                                             "   AND ITEMDESC LIKE '%" + sItemdesc + "%' "  +
                                                             "   AND ENDFLAG  = '" + sEndFlag + "'" +
                                                             "   AND PRODDATE BETWEEN '" + sStartDate + "' AND '" + sEndDate + "'"
                                                             , Connect);
                DataTable dtTemp = new DataTable();
                Adapter.Fill(dtTemp);

                if (dtTemp.Rows.Count == 0)
                {
                    dgvGrid.DataSource = null;
                    return;
                }
                dgvGrid.DataSource = dtTemp;  //데이터 그리드 뷰에 데이터 테이블 등록

                //그리드뷰의  헤더 명칭 선언
                dgvGrid.Columns["ITEMCODE"].HeaderText  = "폼목 코드";
                dgvGrid.Columns["ITEMNAME"].HeaderText  = "폼목 명";
                dgvGrid.Columns["ITEMDESC"].HeaderText  = "폼목 상세";
                dgvGrid.Columns["ITEMDESC2"].HeaderText = "폼목 상세2";
                dgvGrid.Columns["ENDFLAG"].HeaderText  = "단종 여부";
                dgvGrid.Columns["MAKEDATE"].HeaderText  = "생산 일시";
                dgvGrid.Columns["MAKER"].HeaderText     = "등록자";
                dgvGrid.Columns["EDITDATE"].HeaderText  = "수정일시";
                dgvGrid.Columns["EDITOR"].HeaderText    = "수정자";

                //그리드 뷰의 폭 지정
                dgvGrid.Columns[0].Width = 100;
                dgvGrid.Columns[1].Width = 200;
                dgvGrid.Columns[2].Width = 200;
                dgvGrid.Columns[3].Width = 200;
                dgvGrid.Columns[4].Width = 100;

                //컬럼의 수정 여부를 지정 한다
                dgvGrid.Columns["ITEMCODE"].ReadOnly = true;
                dgvGrid.Columns["MAKER"].ReadOnly    = true;
                dgvGrid.Columns["MAKEDATE"].ReadOnly = true;
                dgvGrid.Columns["EDITOR"].ReadOnly   = true;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //데이터 그리드 뷰 에 신규 행 추가
            DataRow dr = ((DataTable)dgvGrid.DataSource).NewRow();
            ((DataTable)dgvGrid.DataSource).Rows.Add(dr);
            dgvGrid.Columns["ITEMCODE"].ReadOnly = false;
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
                string Itemcode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();
                cmd.CommandText = "delete TB_TESTITEM_LJ WHERE ITEMCODE = '" + Itemcode + "'";

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 선택된 행 데이터 저장
            if (dgvGrid.Rows.Count == 0) return;
            if (MessageBox.Show("선택된 데이터를 등록 하시겠습니까?", "데이터등록",
                                MessageBoxButtons.YesNo) == DialogResult.No) return;

            string sItemCode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();
            string sItemName = dgvGrid.CurrentRow.Cells["ITEMNAME"].Value.ToString();
            string sItemDesc = dgvGrid.CurrentRow.Cells["ITEMDESC"].Value.ToString();
            string sItemDesc2 = dgvGrid.CurrentRow.Cells["ITEMDESC2"].Value.ToString();
            string sItemEndFlag = dgvGrid.CurrentRow.Cells["ENDFLAG"].Value.ToString();
            string sProdDate = dgvGrid.CurrentRow.Cells["PRODDATE"].Value.ToString();

            SqlCommand cmd = new SqlCommand();
            SqlTransaction Tran;

            Connect = new SqlConnection(strConn);
            Connect.Open();

            // 데이터 조회 후 해당 데이터가 있는지 확인 후 UPDATE, INSERT 구문 분기
            /*string sSql = "SELECT ITEMCODE FROM TB_TESTITEM_LJ WHERE ITEMCODE ='" + sItemCode + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(sSql, Connect);
            DataTable dtTemp = new DataTable();
            adapter.Fill(dtTemp);*/

            //트랜잭션 설정
            Tran = Connect.BeginTransaction("TestTran");
            cmd.Transaction = Tran;
            cmd.Connection = Connect;

            cmd.CommandText = "UPDATE TB_TestItem_LJ                                         " +
                                      "    SET ITEMNAME   = '" + sItemName   + "',        " +
                                      "        ITEMDESC   = '" + sItemDesc   + "',        " +
                                      "        ITEMDESC2  = '" + sItemDesc2  + "',        " +
                                      "        ENDFLAG    = '" + "N"         + "',        " +
                                      "        PRODDATE   = '" + sProdDate   + "',        " +
                                   
                                      "        EDITOR = '"    + Common.LogInId + "',  " +
                                      "        EDITDATE = GETDATE()          " +
                                      "  WHERE ITEMCODE = '" + sItemCode + "'" +
                                      " IF (@@ROWCOUNT =0)                   " +
                                      "INSERT INTO TB_TestItem_LJ(ITEMCODE,           ITEMNAME,            ITEMDESC,           ITEMDESC2,          ENDFLAG,           PRODDATE,      MAKEDATE,     MAKER) " +
                                      "VALUES('" + sItemCode + "','" + sItemName + "','" + sItemDesc + "','" + sItemDesc2 + "','" + "N" + "','" + sProdDate + "',GETDATE(),'"+Common.LogInId+ "')";

            /*// 데이터가 있는경우 UPDATE, 없는경우 INSERT
            if(dtTemp.Rows.Count == 0)
            {
                // 데이터가 없으니 INSERT 해라
                cmd.CommandText = "INSERT INTO TB_TESTITEM_LJ (ITEMCODE,       ITEMNAME,                 ITEMDESC,        ITEMDESC2,                ENDFLAG,                        PRODDATE,        MAKEDATE,        MAKER)" +
                                  "                      VALUES ('" + sItemCode + "','" + sItemName + "','" + sItemDesc + "','" + sItemDesc2 + "','"+"N"+"','" + sProdDate + "',GETDATE(),'" + "" + "')";
            }
            else
            {
                //데이터가 없으니 UPDATE 해라
                cmd.CommandText = "UPDATE TB_TESTITEM_LJ                               " +
                                  "    SET ITEMNAME = '"   + sItemName    + "',        " +
                                  "        ITEMDESC = '"   + sItemDesc    + "',        " +
                                  "        ITEMDESC2 = '"  + sItemDesc2   + "',        " +
                                  "        ENDFLAG = '"    + "N" + "',        " +
                                  "        PRODDATE = '"   + sProdDate    + "',        " +
                                  "        EDITOR = '',  " +
                                  //"        EDITOR = '"    + Commoncs.LoginUserID + "',  " +
                                  "        EDITDATE = GETDATE()     "                       +
                                  "  WHERE ITEMCODE = '" + sItemCode                        + "'";

            }*/
            cmd.ExecuteNonQuery();

            //성공시 DB COMMIT
            Tran.Commit();
            MessageBox.Show("정상적으로 등록 하였습니다.");
            Connect.Close();


        }

        private void btnPicCome_Click(object sender, EventArgs e)
        {
            string sImageFile = string.Empty;
            //이미지 불러오기 및 저장, 파일 탐색기 호출

            OpenFileDialog Dialog = new OpenFileDialog();
            if(Dialog.ShowDialog() == DialogResult.OK)
            {
                sImageFile = Dialog.FileName;
                pictureBoxItemImage.Tag = Dialog.FileName;
                // 지정된 파일에서 이미지를 만들어 픽쳐박스에 넣는다.
                pictureBoxItemImage.Image = Bitmap.FromFile(sImageFile);
            }

        }

        private void pictureBoxItemImage_Click(object sender, EventArgs e)
        {
            // 픽처박스 크기 최대화 및 이전 사이즈로
            if(this.pictureBoxItemImage.Dock == System.Windows.Forms.DockStyle.Fill)
            {
                //이미지 가득채워져있는 상태이면 원상태로 바꾸라.
                this.pictureBoxItemImage.Dock = System.Windows.Forms.DockStyle.None;
            }
            else
            {
                // 이미지가 가득 채워져 있지 않으면 가득 채워라.
                pictureBoxItemImage.Dock = System.Windows.Forms.DockStyle.Fill;
                // 이미지를 가장 앞으로 가지고 온다.
                pictureBoxItemImage.BringToFront()
;            }
        }

        private void btnPicSave_Click(object sender, EventArgs e)
        {
            // 픽쳐 박스 이미지 저장.
            if (dgvGrid.Rows.Count == 0) return;
            if (pictureBoxItemImage.Image == null) return;
            if (pictureBoxItemImage.Tag.ToString() == "") return;
            if (MessageBox.Show("선택된 이미지로 등록 하시겠습니까?", "이미지 등록", MessageBoxButtons.YesNo) == DialogResult.No) return;

            Byte[] bImage = null;
            Connect = new SqlConnection(strConn);
            try
            {
                // 파일을 불러오기 위한 파일 경로 방법 지정
                FileStream steram = new FileStream(pictureBoxItemImage.Tag.ToString(),
                                                   FileMode.Open,
                                                   FileAccess.Read);
                // 읽어 들인 파일을 바이너리 코드로 변환
                BinaryReader reader = new BinaryReader(steram);
                //만들어진 바이너리 코드 이미지를 Byte 화 하여 저장.
                bImage = reader.ReadBytes(Convert.ToInt32(steram.Length));
                reader.Close();
                steram.Close();
                // 바이너리 코드는 컴퓨터가 인식할 수 있는 0과 1로 구선돈 이진토드
                // 바이트코드는  CPU가  아닌 가상 머신에서 이해할 수 있는 코드

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connect;
                Connect.Open();

                string sItemCode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();
                cmd.CommandText = "UPDATE TB_TESTITEM_LJ SET ITEMIMG = @IMAGE WHERE ITEMCODE = @ITEMCODE";
                cmd.Parameters.AddWithValue("@IMAGE", bImage);
                cmd.Parameters.AddWithValue("ITEMCODE", sItemCode);
                cmd.ExecuteNonQuery();
                Connect.Close();
                MessageBox.Show("이미지가 등록 되었습니다.");



            }
            catch (Exception ex)
            {

                
            }
            finally
            {

            }
        }

        private void dgvGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 선택 시 해당 품목 이지미 가져오기
            string sItemCode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();

            Connect = new SqlConnection(strConn);
            Connect.Open();

            try
            {
                //이미지 초기화
                pictureBoxItemImage.Image = null;
                string sSql = "SELECT ITEMIMG FROM TB_TESTITEM_LJ WHERE ITEMCODE = '" + sItemCode + "'AND ITEMIMG IS NOT NULL";
                SqlDataAdapter Adapter = new SqlDataAdapter(sSql, Connect);
                DataTable dtTemp = new DataTable();
                Adapter.Fill(dtTemp);

                if (dtTemp.Rows.Count == 0) return;
                
                byte[] bImage = null;
                bImage = (byte[])dtTemp.Rows[0]["ITEMIMG"];   //이미지를 byte 화 한다.
                if(bImage != null)
                {
                    pictureBoxItemImage.Image = new Bitmap(new MemoryStream(bImage));  // 메모리 스트림을 이용하여 파일을 
                    pictureBoxItemImage.BringToFront();
                }
              
            }
            catch (Exception ex)
            {

               
            }
            finally
            {
                Connect.Close();
            }
        }

        private void btnPicDelete_Click(object sender, EventArgs e)
        {
            // 품목에 저장된 이미지 삭제
            if (dgvGrid.Rows.Count == 0) return;
            if (MessageBox.Show("선택한 이미지를 삭제하시겠습니까?", "이미지삭제",
                MessageBoxButtons.YesNo)==DialogResult.No) return;

            SqlCommand cmd = new SqlCommand();
            Connect = new SqlConnection(strConn);
            Connect.Open();

            try
            {
                string sItemCode = dgvGrid.CurrentRow.Cells["ITEMCODE"].Value.ToString();
                cmd.CommandText = "UPDATE TB_TESTITEM_LJ SET ITEMIMG = null WHERE ITEMCODE = '"
                                  + sItemCode + "'";
                cmd.Connection = Connect;
                cmd.ExecuteNonQuery();
                pictureBoxItemImage.Image = null;
                MessageBox.Show("정상적으로 삭제 하였습니다.");
           
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connect.Close();
            }
        }
    }
}
