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
    public partial class FormMain : Form
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
        int userid;
        public FormMain(checkUser user, int userid)
        {
            this.userid = userid;
            _user = user;
            InitializeComponent();
            
        }

        private void IsAdmin()
        {
            управлениеToolStripMenuItem1.Enabled = _user.IsAdmin;
            btnDelete.Enabled = _user.IsAdmin;
            btnUpdate.Enabled = _user.IsAdmin;
            button1.Enabled = _user.IsAdmin;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID_автомобиля", "id");
            dataGridView1.Columns.Add("Марка", "Марка");
            dataGridView1.Columns.Add("Модель", "Модель");
            dataGridView1.Columns.Add("Страна_производитель", "Страна");
            dataGridView1.Columns.Add("Год_выпуска", "Год");
            dataGridView1.Columns.Add("Мощность", "Мощность");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[6].Visible = false;
            ((DataGridViewTextBoxColumn)dataGridView1.Columns[1]).MaxInputLength = 40;
            ((DataGridViewTextBoxColumn)dataGridView1.Columns[2]).MaxInputLength = 70;
            ((DataGridViewTextBoxColumn)dataGridView1.Columns[3]).MaxInputLength = 40;
            ((DataGridViewTextBoxColumn)dataGridView1.Columns[4]).MaxInputLength = 4;
            ((DataGridViewTextBoxColumn)dataGridView1.Columns[5]).MaxInputLength = 4;
            dataGridView1.Columns[0].ReadOnly = true;

        }

        private void ClearFields()
        {
            textBox_search.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";

        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetInt32(4), record.GetInt32(5), RowState.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"select * from Автомобиль";

            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }

        private void FormMain_Load_1(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = $"{_user.Id}: {_user.Login}: {_user.Status}";
            IsAdmin();
            CreateColumns();
            RefreshDataGrid(dataGridView1);

            comboBox1.Items.Add("Год_выпуска");
            comboBox1.Items.Add("Мощность");

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

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
            ClearFields();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Add_auto.auto.Show();
          
        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = $"select * from Автомобиль where concat (ID_автомобиля, Марка, Модель, Страна_производитель, Год_выпуска, Мощность) like '%" + textBox_search.Text + "%' ";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while(read.Read())
            {
                ReadSingleRow(dgw, read);
            }

            read.Close();
        }

        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if(dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[6].Value = RowState.Deleted;
                return;
            }

            dataGridView1.Rows[index].Cells[6].Value = RowState.Deleted;
        }

        private void UpdateRow()
        {
            dataBase.openConnection();

            for(int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[6].Value;

                if (rowState == RowState.Existed)
                    continue;

                if(rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from Автомобиль where ID_автомобиля = {id}";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if(rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var marka = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var model = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var strana = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var year = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var power = dataGridView1.Rows[index].Cells[5].Value.ToString();

                    var changeQuery = $"update Автомобиль set Марка = '{marka}', Модель = '{model}', Страна_производитель = '{strana}', Год_выпуска = '{year}', Мощность = '{power}' where ID_автомобиля = '{id}'";

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
            UpdateRow();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Change();
            UpdateRow();
        }

        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            
                try
                {

                    var id = dataGridView1.Rows[selectedRow].Cells[0].Value.ToString();
                    var marka = dataGridView1.Rows[selectedRow].Cells[1].Value.ToString();
                    var model = dataGridView1.Rows[selectedRow].Cells[2].Value.ToString();
                    var strana = dataGridView1.Rows[selectedRow].Cells[3].Value.ToString();
                    int year;
                    int power;

                    if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty && dataGridView1.Rows[selectedRowIndex].Cells[1].Value.ToString() != string.Empty && dataGridView1.Rows[selectedRowIndex].Cells[2].Value.ToString() != string.Empty && dataGridView1.Rows[selectedRowIndex].Cells[3].Value.ToString() != string.Empty)
                    {
                        if (int.TryParse(dataGridView1.Rows[selectedRow].Cells[4].Value.ToString(), out year) && int.TryParse(dataGridView1.Rows[selectedRow].Cells[5].Value.ToString(), out power) && dataGridView1.Rows[selectedRowIndex].Cells[4].Value.ToString() != string.Empty && dataGridView1.Rows[selectedRowIndex].Cells[5].Value.ToString() != string.Empty && year >= 1900 && year <= 2022)
                        {
                            dataGridView1.Rows[selectedRowIndex].SetValues(id, marka, model, strana, year, power);
                            dataGridView1.Rows[selectedRowIndex].Cells[6].Value = RowState.Modified;
                        }
                        else
                        {
                            MessageBox.Show("Неправильно введены данные!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Поля не могут быть пустыми");
                        RefreshDataGrid(dataGridView1);
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка! Ячейки не могут быть пустыми", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RefreshDataGrid(dataGridView1);
                }
            
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Change();
            ClearFields();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void управлениеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            Panel_Admin.manage.Show();
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        Advt advt;
        private void автомобильToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (advt == null || advt.IsDisposed)
            {
                advt = new Advt(_user);
                advt.Show();
            }

        }

        DTP dtp;
        private void дТПToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dtp == null || dtp.IsDisposed)
            {
                dtp = new DTP(_user, userid);
                dtp.Show();
            }
        }

        private void textBox_carID_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox_carID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_marka_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar ==(char)Keys.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_model_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_strana_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_year_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_power_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void тестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTest test = new FormTest(_user);
            test.Show();
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
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].IsInEditMode == true)
                if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
                    e.Handled = false;
                else
                    e.Handled = true;
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].IsInEditMode == true)
                if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                    e.Handled = false;
                else
                    e.Handled = true;
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].IsInEditMode == true)
                if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                    e.Handled = false;
                else
                    e.Handled = true;
        }

        private void FilterYear(DataGridView dgw, int OT, int DO)
        {
            dgw.Rows.Clear();

            string searchString = $"select * from Автомобиль where Год_выпуска >= '{OT}' and Год_выпуска <= '{DO}'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }

            read.Close();
        }

        private void FilterPower(DataGridView dgw, int OT, int DO)
        {
            dgw.Rows.Clear();

            string searchString = $"select * from Автомобиль where Мощность >= '{OT}' and Мощность <= '{DO}'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }

            read.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int OT;
            int DO;
            if (int.TryParse(textBox1.Text, out OT) && int.TryParse(textBox2.Text, out DO) && (OT < DO))
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    FilterYear(dataGridView1, OT, DO);
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    FilterPower(dataGridView1, OT, DO);
                }

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

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        
    }
}