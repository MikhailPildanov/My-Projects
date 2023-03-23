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
    
    public partial class log_in : Form
    {
        DataBase database = new DataBase();
        int userid;

        public log_in()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }



       
        private void login_Click(object sender, EventArgs e)
        {
           
            var loginUser = textBox1.Text;
            var passUser = md5.hashPassword(textBox2.Text);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select ID_пользователя, Логин, Пароль, Админ from Пользователь where Логин = '{loginUser}' and Пароль = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count == 1)
            {
                var user = new checkUser(Convert.ToInt32(table.Rows[0].ItemArray[0]), table.Rows[0].ItemArray[1].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[3]));
                userid = Convert.ToInt32(table.Rows[0].ItemArray[0]);
                MessageBox.Show($"Вы успешно вошли! {userid}", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormMain formMain = new FormMain(user, userid);
                Advt advt = new Advt(user);
                DTP dtp = new DTP(user, userid);
                Panel_Admin panel_Admin = new Panel_Admin();
                Add_auto add_Auto = new Add_auto();
                Add_advt add_Advt = new Add_advt(user);
                
                this.Hide();
                formMain.ShowDialog();
               
                this.Show();
                

            }
            else
                MessageBox.Show("Неправильный логин или пароль!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void log_in_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            pictureBox3.Visible = false;
            textBox1.MaxLength = 30;
            textBox2.MaxLength = 50;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sign_up frm_sign = new sign_up();
            frm_sign.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
            pictureBox3.Visible = false;
            pictureBox2.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            pictureBox3.Visible = true;
            pictureBox2.Visible = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
