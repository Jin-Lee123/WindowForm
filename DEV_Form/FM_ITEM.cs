﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DEV_Form
{

    public partial class FM_ITEM : Form
    {
        private SqlConnection Connect = null;  //접속 정보 객체 명령
        // 접속 주소
        private string strConn = "Data Source=61.105.9.203;Initial Catalog=AppDev; User ID=kfqs1;Password=1234";

        public FM_ITEM()
        {
            InitializeComponent();
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

                SqlDataAdapter Adapter = new SqlDataAdapter("SELECT ITEMCODE,  " +
                                                             "       ITEMNAME,  " +
                                                             "       ITEMDESC,  " +
                                                             "       ITEMDESC2, " +
                                                             "       ENDFLAG,   " +
                                                             "       PRODDATE,  " +
                                                             "       MAKEDATE,  " +
                                                             "       MAKER,     " +
                                                             "       EDITDATE,  " +
                                                             "       EDITOR     " +
                                                             "  FROM TB_TESTITEM_LJ WITH(NOLOCK) " +
                                                             " WHERE ITEMCODE LIKE '%" + sItemCode + "%' " +
                                                             "   AND ITEMNAME LIKE '%" + sItemName + "%' " +
                                                             "   AND ITEMDESC LIKE '%" + sItemdesc + "%' " +
                                                             "   AND ENDFLAG  = '" + sEndFlag + "'", Connect);
                DataTable dtTemp = new DataTable();
                Adapter.Fill(dtTemp);

                if (dtTemp.Rows.Count == 0) return;
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

        }
    }
}