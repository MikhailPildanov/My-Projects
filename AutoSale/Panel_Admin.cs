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
    public partial class Panel_Admin : Form
    {

        DataBase dataBase = new DataBase();


        public Panel_Admin()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private static Panel_Admin add;

        public static Panel_Admin manage
        {
            get
            {
                if (add == null || add.IsDisposed) add = new Panel_Admin();
                return add;
            }
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID_пользователя", "ID");
            dataGridView1.Columns.Add("Логин", "Логин");
            dataGridView1.Columns.Add("Пароль", "Пароль");
            var checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "IsAdmin";
            dataGridView1.Columns.Add(checkColumn);
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
        }

        private void ReadSingleRow(IDataRecord record)
        {
            dataGridView1.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetBoolean(3));
        }

        private void RefreshDataGrid()
        {
            dataGridView1.Rows.Clear();

            string queryString = $"SELECT * FROM Пользователь;";

            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                ReadSingleRow(reader);
            }

            reader.Close();

            dataBase.closeConnection();
        }

        private void Panel_Admin_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                var isadmin = dataGridView1.Rows[index].Cells[3].Value.ToString();

                var changeQuery = $"UPDATE Пользователь SET Админ = '{isadmin}' WHERE ID_пользователя = '{id}'";

                var command = new SqlCommand(changeQuery, dataBase.getConnection());
                command.ExecuteNonQuery();
            }

            dataBase.closeConnection();

            RefreshDataGrid();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;


            var id = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells[0].Value);
            var deleteQuery = $"DELETE FROM Пользователь where ID_пользователя = {id}";

            var command = new SqlCommand(deleteQuery, dataBase.getConnection());
            command.ExecuteNonQuery();

            dataBase.closeConnection();

            RefreshDataGrid();
        }
    }
}
