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
    public partial class AddTest : Form
    {
        private readonly checkUser user1;

        DataBase dataBase = new DataBase();

        public AddTest(checkUser user)
        {
            user1 = user;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void ClearFileds()
        {
            textBox_color.Text = "";
            textBox_desc.Text = "";
            textBox_date.Text = "";
            textBox_price.Text = "";
            textBox_auto2.Text = "";

        }

        private void Add_advt_Load(object sender, EventArgs e)
        {
            textBox_user2.Text = $"{user1.Id}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearFileds();
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

        private void textBox_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_auto2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_user2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_desc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Логин и пароль должен содержать только буквы латинского алфавита, а также цифры. Не может содержать пробелы, специальные символы и т.д.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AddTest_Load(object sender, EventArgs e)
        {
            textBox_user2.Text = $"{user1.Id}";
            string command = "select ID_автомобиля, Год_выпуска, (Марка + ' ' + Модель ) as name from Автомобиль";
            SqlDataAdapter da = new SqlDataAdapter(command, dataBase.getConnection());

            DataSet ds = new DataSet();
            dataBase.openConnection();
            da.Fill(ds);
            dataBase.closeConnection();

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.ValueMember = "ID_автомобиля";
            comboBox1.DisplayMember = "name";
        }

        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

          
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataBase.openConnection();

            var color = textBox_color.Text;
            var desc = textBox_desc.Text;
            DateTime date = DateTime.Now;
            decimal price;
            int AutoID;
            int UserID;
            var Auto = comboBox1.Text;

            if (int.TryParse(comboBox1.SelectedValue.ToString(), out AutoID) && decimal.TryParse(textBox_price.Text, out price) && (price > 0) && int.TryParse(textBox_user2.Text, out UserID))
            {

                var addQuery1 = $"SELECT COUNT(*) FROM Пользователь WHERE ID_пользователя = '{UserID}'";

                var commandUser = new SqlCommand(addQuery1, dataBase.getConnection());

                int i = (int)commandUser.ExecuteScalar();

                var addQuery2 = $"SELECT COUNT(*) FROM Автомобиль WHERE ID_автомобиля = '{AutoID}'";

                var commandAuto = new SqlCommand(addQuery2, dataBase.getConnection());

                int j = (int)commandAuto.ExecuteScalar();

                if (i > 0 && j > 0)
                {

                    var addQuery = $"insert into Объявление (Цвет, Описание, Дата_публикации, Цена, ID_автомобиля, ID_пользователя, Автомобиль) values ('{color}', '{desc}', '{date}', '{price}', '{AutoID}', '{UserID}', '{Auto}')";

                    var command = new SqlCommand(addQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();

                    MessageBox.Show("Объявление успешно создано!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Объявление не было создано", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            dataBase.closeConnection();
        }
    }
}
