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
    public partial class sign_up : Form
    {   
        DataBase dataBase = new DataBase();
        public sign_up()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void sign_up_Load(object sender, EventArgs e)
        {
            textBox_pass2.PasswordChar = '*';
            pictureBox3.Visible = false;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            
            if (checkuser())
            {
                return;
            }

            var login = textBox_login2.Text;
            var password = md5.hashPassword(textBox_pass2.Text);

            string querystring = $"insert into Пользователь(Логин, Пароль, Админ) values('{login}', '{password}', 0)";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            dataBase.openConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Пользователь успешно создан!", "Успех!");
                log_in frm_login = new log_in();
                this.Hide();
                frm_login.ShowDialog();

            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }
            dataBase.closeConnection();



        }

        private Boolean checkuser()
        {
            var loginUser = textBox_login2.Text;
            var passUser = textBox_pass2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select ID_пользователя, Логин, Пароль, Админ from Пользователь where Логин = '{loginUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (loginUser.Length > 30 || passUser.Length > 50)
            {
                MessageBox.Show("Недопустимая длина логина или пароля");
                return true;
            }

            if(table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else
            {
                return false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox_login2.Text = "";
            textBox_pass2.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox_pass2.UseSystemPasswordChar = false;
            pictureBox3.Visible = false;
            pictureBox2.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox_pass2.UseSystemPasswordChar = true;
            pictureBox3.Visible = true;
            pictureBox2.Visible = false;
        }

        private void textBox_login2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_pass2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_login2_Enter(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.Clear();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            log_in log = new log_in();
            log.Show();
            this.Hide();
        }

        private void button_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Логин и пароль должен содержать только буквы латинского алфавита, а также цифры. Не может содержать пробелы, специальные символы и т.д.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
