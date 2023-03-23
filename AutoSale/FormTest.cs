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


    public partial class FormTest: Form
    {
        enum RowState
        {
            Existed,
            New,
            Modified,
            ModifiedNew,
            Deleted
        }

        private readonly checkUser _user;

        DataBase dataBase = new DataBase();

        int selectedRow;

        public FormTest(checkUser user)
        {
            _user = user;
            InitializeComponent();
        }


        private void IsAdmin()
        {
            btnDelete.Enabled = _user.IsAdmin;
            btnUpdate.Enabled = _user.IsAdmin;
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
           
        }


        private void ClearFileds()
        {
            textBox_advtID.Text = "";
            textBox_color.Text = "";
            textBox_desc.Text = "";
            textBox_date.Text = "";
            textBox_price.Text = "";
            textBox_auto.Text = "";
            textBox_user.Text = "";
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetDateTime(3), record.GetDecimal(4), record.GetInt32(5), record.GetInt32(6), record.GetString(7), RowState.ModifiedNew);

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

                textBox_advtID.Text = row.Cells[0].Value.ToString();
                textBox_color.Text = row.Cells[1].Value.ToString();
                textBox_desc.Text = row.Cells[2].Value.ToString();
                textBox_date.Text = row.Cells[3].Value.ToString();
                textBox_price.Text = row.Cells[4].Value.ToString();
                textBox_auto.Text = row.Cells[5].Value.ToString();
                textBox_user.Text = row.Cells[6].Value.ToString();

            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFileds();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add_advt add_Advt = new Add_advt(_user);
            add_Advt.Show();
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

        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[7].Value = RowState.Deleted;
                return;
            }

            dataGridView1.Rows[index].Cells[7].Value = RowState.Deleted;


        }

        private void Update()
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[7].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from Объявление where ID_объявления = {id}";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
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
            ClearFileds();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id = dataGridView1.Rows[selectedRow].Cells[0].Value.ToString();
            var color = dataGridView1.Rows[selectedRow].Cells[1].Value.ToString();
            var desc = dataGridView1.Rows[selectedRow].Cells[2].Value.ToString();
            DateTime date = DateTime.Now;
            decimal price;
            int AutoID;
            int UserID;

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (decimal.TryParse(dataGridView1.Rows[selectedRow].Cells[4].Value.ToString(), out price) && (price > 0) && int.TryParse(dataGridView1.Rows[selectedRow].Cells[5].Value.ToString(), out AutoID) && int.TryParse(dataGridView1.Rows[selectedRow].Cells[6].Value.ToString(), out UserID))
                {
                    dataGridView1.Rows[selectedRowIndex].SetValues(id, color, desc, date, price, AutoID, UserID);
                    dataGridView1.Rows[selectedRowIndex].Cells[7].Value = RowState.Modified;

                }
                else
                {
                    MessageBox.Show("Неправильно введены данные!");
                }


            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Change();
            ClearFileds();
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
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.OemPeriod)
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

        private void FormTest_Load(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = $"{_user.Id}: {_user.Login}: {_user.Status}";
            IsAdmin();
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            Change();
            Update();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            deleteRow();
            Update();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            AddTest addTest = new AddTest(_user);
            addTest.Show();
        }
    }
}
