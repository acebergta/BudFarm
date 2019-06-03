using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BudFarm.UserControls
{
    public partial class AddWorkers : UserControl
    {
        public AddWorkers()
        {
            InitializeComponent();
            DesignUC();
        }

        private void DesignUC()
        {
            textBox1.Clear();
            textBox1.ForeColor = Color.Silver;
            textBox1.Text = "Введите ФИО";

            textBox2.Clear();
            textBox2.ForeColor = Color.Silver;
            textBox2.Text = "Введите Должность";

            textBox3.Clear();
            textBox3.ForeColor = Color.Silver;
            textBox3.Text = "Введите Оклад";

            textBox4.Clear();
            textBox4.ForeColor = Color.Silver;
            textBox4.Text = "Введите номер телефона";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source = .\SQLEXPRESS; Initial Catalog = BudFarmDB; Integrated Security = true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO dbo.Workers (wor_name, wor_position, wor_salary, wor_phoneNum) VALUES (@WorName, @WorPosition, " +
                        "@WorSalary, @WorPhone)";
                    command.Parameters.AddWithValue("@WorName", textBox1.Text);
                    command.Parameters.AddWithValue("@WorPosition", textBox2.Text);
                    command.Parameters.AddWithValue("@WorSalary", textBox3.Text);
                    command.Parameters.AddWithValue("@WorPhone", textBox4.Text);
                    command.ExecuteNonQuery();
                    DesignUC();
                    this.Enabled = false;
                    this.SendToBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.SendToBack();
        }

        private void TextBox1_Enter(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.Clear();
            textBox1.Enter -= TextBox1_Enter;
        }
        private void TextBox2_Enter(object sender, EventArgs e)
        {
            textBox2.ForeColor = Color.Black;
            textBox2.Clear();
            textBox2.Enter -= TextBox2_Enter;
        }
        private void TextBox3_Enter(object sender, EventArgs e)
        {
            textBox3.ForeColor = Color.Black;
            textBox3.Clear();
            textBox3.Enter -= TextBox3_Enter;
        }
        private void TextBox4_Enter(object sender, EventArgs e)
        {
            textBox4.ForeColor = Color.Black;
            textBox4.Clear();
            textBox4.Enter -= TextBox4_Enter;
        }

    }
}
