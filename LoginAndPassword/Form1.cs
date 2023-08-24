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

namespace LoginAndPassword
{
    public partial class Form1 : Form
    {
        string connectionString = "Data Source=ASUS;Initial Catalog = TestDB; Integrated Security = True;";
        SqlConnection connection;
        string login;
        string password;

        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            label1.Text = "Login";
            label2.Text = "Password";
            button1.Text = "Sign up";
            button2.Text = "Log in";
        }


        //login
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            login = textBox2.Text;

        }

        //sign up
        private void button1_Click(object sender, EventArgs e)
        {
            string queryCheck = "SELECT COUNT(*) FROM register WHERE login_user = @Login";
            using (SqlCommand command = new SqlCommand(queryCheck, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Login", login);
                int count = (int)command.ExecuteScalar();
                if (count == 1)
                {
                    MessageBox.Show("A user with this login already exists", "Sign up", MessageBoxButtons.OK);
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                else
                {
                    string queryAdd = "insert into register(login_user, password_user) values (@Login, @Password)";
                    using (SqlCommand commandAdd = new SqlCommand(queryAdd, connection))
                    {
                        string login1 = login;
                        string password1 = password;

                        commandAdd.Parameters.AddWithValue("@Login", login);
                        commandAdd.Parameters.AddWithValue("@Password", password);
                        commandAdd.ExecuteNonQuery();
                        MessageBox.Show("You successfully signed up", "Sign up", MessageBoxButtons.OK);
                    }
                }
                connection.Close();
            }
        }

        //password
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            password = textBox1.Text;
        }

        //log in
        private void button2_Click(object sender, EventArgs e)
        {
            string queryCheck = "SELECT COUNT(*) FROM register WHERE login_user = @Login AND password_user = @Password";
            using (SqlCommand command = new SqlCommand(queryCheck, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", password);
                int count = (int)command.ExecuteScalar();
                if (count == 1)
                {
                    MessageBox.Show("You're logged into your account", "Log in", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Invalid login or password", "Log in", MessageBoxButtons.OK);
                    textBox1.Text = "";
                    textBox2.Text = "";
                }  
                connection.Close();
            }
        }
    }
}
