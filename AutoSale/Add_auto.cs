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
    public partial class Add_auto : Form
    {


        DataBase dataBase = new DataBase();

        public Add_auto()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; 
        }

        private static Add_auto add;

        public static Add_auto auto
        {
            get
            {
                if (add == null || add.IsDisposed) add = new Add_auto();
                return add;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
         
        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            if (textBox_model.Text != string.Empty && textBox_marka.Text != string.Empty && textBox_strana.Text != string.Empty)
            {
                var marka = textBox_marka.Text;
                var model = textBox_model.Text;
                var strana = textBox_strana.Text;
                int year;
                int power;

                if (int.TryParse(textBox_year.Text, out year) && int.TryParse(textBox_power.Text, out power) && year >= 1900 && year <= 2022)
                {
                    var addQuery = $"insert into Автомобиль (Марка, Модель, Страна_производитель, Год_выпуска, Мощность) values ('{marka}', '{model}', '{strana}', {year}, '{power}' )";

                    var command = new SqlCommand(addQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();

                    MessageBox.Show("Автомобиль успешно добавлен!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Неправильно введен год и мощность", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Ошибка! Поля не могут быть пустыми", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dataBase.closeConnection();

            
        }

        private void textBox_marka_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
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
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox_power_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void ClearField()
        {
            textBox_marka.Text = "";
            textBox_model.Text = "";
            textBox_strana.Text = "";
            textBox_year.Text = "";
            textBox_power.Text = "";
        }

        private void Add_auto_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearField();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Поля могут содержать только буквы латинского алфавита, а также цифры. Не могут содержать специальные символы и т.д.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
