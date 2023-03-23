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
    public partial class Add_DTP : Form
    {

        DataBase dataBase = new DataBase();

        public Add_DTP(int userid)
        {
            this.userid = userid;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            comboBox1.KeyPress += (sender, e) => e.Handled = true;
        }

        
        int userid;
        
        

        private void ClearFileds()
        {
          
            textBox_descr.Text = "";
            textBox_place.Text = "";
            textBox_vinovnik.Text = "";
            
           
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();
            

            if (textBox_descr.Text != string.Empty && textBox_place.Text != string.Empty && textBox_vinovnik.Text != string.Empty)
            {
                DateTime date;
                var descr = textBox_descr.Text;
                var place = textBox_place.Text;
                var vinovnik = textBox_vinovnik.Text;
                int IDadvt;


                if (int.TryParse(comboBox1.SelectedValue.ToString(), out IDadvt) && DateTime.TryParse(dateTimePicker1.Text, out date))
                {
                    int n = 1;

                    if (n > 0)
                    {


                        var addQuery1 = $"SELECT COUNT(*) FROM Объявление WHERE ID_объявления = '{IDadvt}'";

                        var commandAuto = new SqlCommand(addQuery1, dataBase.getConnection());

                        int j = (int)commandAuto.ExecuteScalar();

                        if (j > 0)
                        {

                            var addQuery = $"insert into ДТП (Дата, Описание, Место, Виновник, ID_объявления) values ('{date}', '{descr}', '{place}', '{vinovnik}', '{IDadvt}')";

                            var command = new SqlCommand(addQuery, dataBase.getConnection());
                            command.ExecuteNonQuery();

                            MessageBox.Show("Запись о ДТП успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                        else
                        {
                            MessageBox.Show("Запись не была создана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь может выбирать только свои объявления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                   


                }
                else
                {
                    MessageBox.Show("Неправильно введены цена, цвет либо описание", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Ошибка! Поля не могут быть пустыми", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dataBase.closeConnection();
        
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Поля могут содержать только буквы латинского алфавита, а также цифры. Не могут содержать специальные символы и т.д.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Add_DTP_Load(object sender, EventArgs e)
        {
           
            
            string command = $"select ID_объявления from Объявление where ID_пользователя = '{userid}'";
            SqlDataAdapter da = new SqlDataAdapter(command, dataBase.getConnection());

            DataSet ds = new DataSet();
            dataBase.openConnection();
            da.Fill(ds);
            dataBase.closeConnection();

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.ValueMember = "ID_объявления";
            comboBox1.DisplayMember = "ID_объявления";
        }

       
    }
}
