using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AutoSales
{
    
    public partial class DTP : Form
    {
        enum RowState
            {
                Existed,
                New,
                Modified,
                ModifiedNew,
                Deleted
            }

        private readonly checkUser user2;

        DataBase dataBase = new DataBase();

        int selectedRow;

        int userid;
        public DTP(checkUser user,int userid)
        {
            this.userid = userid;
            user2 = user;
            InitializeComponent();
        }

        private void IsAdmin()
        {
            btnDelete.Enabled = user2.IsAdmin;
            btnUpdate.Enabled = user2.IsAdmin;
            
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID_ДТП", "id дтп");
            dataGridView1.Columns.Add("Дата", "Дата");
            dataGridView1.Columns.Add("Описание", "Описание");
            dataGridView1.Columns.Add("Место", "Место");
            dataGridView1.Columns.Add("Виновник", "Виновник");
            dataGridView1.Columns.Add("ID_объявления", "id объявления");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;
            ((DataGridViewTextBoxColumn)dataGridView1.Columns[3]).MaxInputLength = 80;
            ((DataGridViewTextBoxColumn)dataGridView1.Columns[4]).MaxInputLength = 40;
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetDateTime(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetInt32(5), RowState.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"select * from ДТП";

            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void DTP_Load(object sender, EventArgs e)
        {
            IsAdmin();
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

             

            }
        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = $"select * from ДТП where concat (ID_ДТП, Дата, Описание, Место, Виновник, ID_объявления) like '%" + textBox_search.Text + "%' ";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }

            read.Close();
        }

        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[6].Value = RowState.Deleted;
                return;
            }

            dataGridView1.Rows[index].Cells[6].Value = RowState.Deleted;


        }

        private void Update()
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[6].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from ДТП where ID_ДТП = {id}";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var date = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var descr = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var place = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var vinovnik = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var IDadvt = dataGridView1.Rows[index].Cells[5].Value.ToString();

                    var changeQuery = $"update ДТП set Дата = '{date}', Описание = '{descr}', Место = '{place}', Виновник = '{vinovnik}' where ID_ДТП = '{id}'";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();

                }
            }

            dataBase.closeConnection();
        }

        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            
                try
                {
                    var id = dataGridView1.Rows[selectedRow].Cells[0].Value.ToString();
                    DateTime date;
                    var desc = dataGridView1.Rows[selectedRow].Cells[2].Value.ToString();
                    var place = dataGridView1.Rows[selectedRow].Cells[3].Value.ToString();
                    var vinovnik = dataGridView1.Rows[selectedRow].Cells[4].Value.ToString();
                    int IDadvt;
                    
                    if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty && dataGridView1.Rows[selectedRowIndex].Cells[2].Value.ToString() != string.Empty && dataGridView1.Rows[selectedRowIndex].Cells[3].Value.ToString() != string.Empty && dataGridView1.Rows[selectedRowIndex].Cells[4].Value.ToString() != string.Empty)
                    {
                    
                        if (DateTime.TryParse(dataGridView1.Rows[selectedRow].Cells[1].Value.ToString(), out date) && int.TryParse(dataGridView1.Rows[selectedRow].Cells[5].Value.ToString(), out IDadvt))
                        {
                            dataGridView1.Rows[selectedRowIndex].SetValues(id, date, desc, place, vinovnik, IDadvt);
                            dataGridView1.Rows[selectedRowIndex].Cells[6].Value = RowState.Modified;

                        }
                        else
                        {
                            MessageBox.Show("Неправильно введены данные!");
                        }
                    }
                    

                
                   

                    else
                    {
                        MessageBox.Show("Поля не должны быть пустыми!");
                        RefreshDataGrid(dataGridView1);
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка! Поля не могут быть пустыми", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RefreshDataGrid(dataGridView1);
                }
            
        }

        private void ClearFileds()
        {
            textBox_search.Text = "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFileds();
        }

        Add_DTP add_DTP;
        private void button1_Click(object sender, EventArgs e)
        {
            if (add_DTP == null || add_DTP.IsDisposed)
            {
                add_DTP = new Add_DTP(userid);
                add_DTP.Show();
            }
        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteRow();
            Update();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Change();
            ClearFileds();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Change();
            Update();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFileds();
        }

        private void textBox_dtpID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_date_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_descr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_place_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_vinovnik_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_advt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_search_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.ContextMenuStrip = contextMenuStrip1;
            TextBox tb = (TextBox)e.Control;
            tb.KeyPress -= tb_KeyPress;
            tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            string vlCell = ((TextBox)sender).Text;
            bool temp = (vlCell.IndexOf(".") == -1);

            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].IsInEditMode == true)
                if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                    e.Handled = false;
                else
                    e.Handled = true;
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].IsInEditMode == true)
                if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                    e.Handled = false;
                else
                    e.Handled = true;
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].IsInEditMode == true)
                if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                    e.Handled = false;
                else
                    e.Handled = true;
        }
    }
}
