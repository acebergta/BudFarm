using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BudFarm
{
    public partial class LoginUI : Form
    {
        public LoginUI()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder conStr = new SqlConnectionStringBuilder();
            conStr.DataSource = @".\SQLEXPRESS";
            conStr.InitialCatalog = "BudFarmDB";
            conStr.UserID = "Buhgalter123";
            conStr.Password = "qwert";
            //conStr.UserID = textBox1.Text;
            //conStr.Password = textBox2.Text;
            using (SqlConnection connection = new SqlConnection(conStr.ConnectionString))
            {
                try
                {
                    connection.Open();
                    if (connection.Database == "BudFarmDB")
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Thread.Sleep(60);
                            this.Opacity = this.Opacity - 0.1;
                        }
                        connection.Close();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    textBox1.Clear();
                    textBox2.Clear();
                }
            }
        }
    }
}
