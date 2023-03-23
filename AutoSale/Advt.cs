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
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModidiedNew,
        Deleted
    }

    public partial class Advt : Form
    {

        private readonly checkUser _user;

        DataBase dataBase = new DataBase();

        int selectedRow;

        public Advt(checkUser user)
        {
            _user = user;
            InitializeComponent();
        }

       
       

        private void IsAdmin()
        {
            btnDelete.Enabled = _user.IsAdmin;
            btnSave.Enabled = _user.IsAdmin;

        }


        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID_объявления", "id");
            dataGridView1.Columns.Add("Цвет", "Цвет");
            dataGridView1.Columns.Add("Описание", "Описание");
            dataGridView1.Columns.Add("Дата_публикации", "Дата публикации");
            dataGridView1.Columns.Add("Цена", "Цена (руб)");
            dataGridView1.Columns.Add("ID_автомобиля", "id автомобиля");
            dataGridView1.Columns.Add("ID_пользователя", "id пользователя");
            dataGridView1.Columns.Add("Автомобиль", "Автомобиль");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[7].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[0].ReadOnly = true;
            ((DataGridViewTextBoxColumn)dataGridView1.Columns[1]).MaxInputLength = 30;
            ((DataGridViewTextBoxColumn)dataGridView1.Columns[4]).MaxInputLength = 10;
        }

        
        private void ClearFileds()
        {
            textBox_search.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetDateTime(3), record.GetDecimal(4), record.GetInt32(5), record.GetInt32(6), record.GetString(7), RowState.ModidiedNew);

        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"select * from Объявление";

            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();

        }

        private void Advt_Load(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = $"{_user.Id}: {_user.Login}: {_user.Status}";
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            
        }

        Add_advt add_Advt;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (add_Advt == null || add_Advt.IsDisposed)
            {
                add_Advt = new Add_advt(_user);
                add_Advt.Show();
            }
        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = $"select * from Объявление where concat (ID_объявления, Цвет, Описание, Дата_публикации, Цена, ID_автомобиля, ID_пользователя, Автомобиль) like '%" + textBox_search.Text + "%' ";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }

            read.Close();
        }

        private void FilterPrice(DataGridView dgw, int priceOT, int priceDO)
        {
            dgw.Rows.Clear();

            string searchString = $"select * from Объявление where Цена >= '{priceOT}' and Цена <= '{priceDO}'";

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
                dataGridView1.Rows[index].Cells[8].Value = RowState.Deleted;
                return;
            }

            dataGridView1.Rows[index].Cells[8].Value = RowState.Deleted;


        }

        private void Update()
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[8].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from Объявление where ID_объявления = {id}";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if(rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var color = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var desc = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var date = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var price = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var AutoID = dataGridView1.Rows[index].Cells[5].Value.ToString();
                    var UserID = dataGridView1.Rows[index].Cells[6].Value.ToString();

                    var changeQuery = $"update Объявление set Цвет = '{color}', Описание = '{desc}', Дата_публикации = '{date}', Цена = '{price}' where ID_объявления = '{id}'";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();

                }
            }

            dataBase.closeConnection();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Change();
            Update();
        }

        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            
                try
                {
                    var id = dataGridView1.Rows[selectedRow].Cells[0].Value.ToString();
                    var color = dataGridView1.Rows[selectedRow].Cells[1].Value.ToString();
                    var desc = dataGridView1.Rows[selectedRow].Cells[2].Value.ToString();
                    DateTime date = DateTime.Now;
                    decimal price;
                    int AutoID;
                    int UserID;
                    var Auto = dataGridView1.Rows[selectedRow].Cells[7].Value.ToString();


                    if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty && dataGridView1.Rows[selectedRowIndex].Cells[1].Value.ToString() != string.Empty && dataGridView1.Rows[selectedRowIndex].Cells[2].Value.ToString() != string.Empty && dataGridView1.Rows[selectedRowIndex].Cells[4].Value.ToString() != string.Empty)
                    {
                        if (decimal.TryParse(dataGridView1.Rows[selectedRow].Cells[4].Value.ToString(), out price) && (price > 0) && (color.Length > 0) && int.TryParse(dataGridView1.Rows[selectedRow].Cells[5].Value.ToString(), out AutoID) && int.TryParse(dataGridView1.Rows[selectedRow].Cells[6].Value.ToString(), out UserID))
                        {
                            decimal price1 = decimal.Truncate(price);
                            dataGridView1.Rows[selectedRowIndex].SetValues(id, color, desc, date, price1, AutoID, UserID, Auto);
                            dataGridView1.Rows[selectedRowIndex].Cells[8].Value = RowState.Modified;

                        }
                        else
                        {
                            MessageBox.Show("Неправильно введены данные!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Цвет, цена и описание не должны быть пустыми!");
                        RefreshDataGrid(dataGridView1);
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка! Ячейки цвет, описание и цена не могут быть пустыми", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RefreshDataGrid(dataGridView1);
                }
            
        }

       

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFileds();
        }

        private void textBox_advtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_color_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_date_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar ==(char)Keys.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_price_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_auto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_user_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_desc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                e.Handled = false;
            else
                e.Handled = true;
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
            
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].IsInEditMode == true)
                if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                    e.Handled = false;
                else
                    e.Handled = true;
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].IsInEditMode == true)
                if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                    e.Handled = false;
                else
                    e.Handled = true;
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].IsInEditMode == true)
                if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                    e.Handled = false;
                else
                    e.Handled = true;
        }

        private void textBox_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int PriceOT;
            int PriceDO;
            if (int.TryParse(textBox1.Text, out PriceOT) && int.TryParse(textBox2.Text, out PriceDO) && (PriceOT < PriceDO))
            {
                FilterPrice(dataGridView1, PriceOT, PriceDO);
            }
            else
            {
                MessageBox.Show("Неправильно введены данные!");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                e.Handled = false;
            else
                e.Handled = true;
        }

       
    }
}
