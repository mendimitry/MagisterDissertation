using Npgsql;
using ServerPing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class FormAuth : Form
    {
        public FormAuth()
        {
            InitializeComponent();
            Application.EnableVisualStyles();

           //Application.SetCompatibleTextRenderingDefault(false);
        }

       
        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Desktop\magister\DiplomaWorkZhalninDmitryIVT92-master\SERVER\Cleaner1.mdf;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Role FROM Login WHERE Username='" + textBox1.Text + "' AND Password='" + textBox2.Text + "' ", connection);
            DataTable table = new DataTable();

            adapter.Fill(table);
            //object[] role = table.Rows[0].ItemArray;
            //string Role = role[0].ToString();

            if (table.Rows.Count == 1)
            {
                //MessageBox.Show("Вы зашли под Ролью " + Role);
                MainFormServer mainForm = new MainFormServer();



                mainForm.Show();
                this.Hide();


            }
            
            else
            {
                //MessageBox.Show("Логин или пароль неверен");
                DialogResult result = MessageBox.Show(
        "ОШИБКА: ERROR21 - Неверный логин или пароль\r\nДля первичной авторизации необходимо написать разработчику\r\nDAZhalnin@yandex.ru",
        "Информационное сообщение",
        MessageBoxButtons.OK,
        MessageBoxIcon.Warning,
        MessageBoxDefaultButton.Button1);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
            }
            
        }
    }
}
