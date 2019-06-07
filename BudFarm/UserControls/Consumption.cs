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
    public partial class Consumption : UserControl
    {
        public Consumption()
        {
            InitializeComponent();
            LoadData();
            Statistics();
            DesignData();
        }
        private void DesignData()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        private void Statistics()
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            connectionString.DataSource = @".\SQLEXPRESS";
            connectionString.InitialCatalog = "BudFarmDB";
            connectionString.IntegratedSecurity = true;
            using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command = connection.CreateCommand();
                    //Всего
                    command.CommandText = "SELECT SUM(con_sum) FROM dbo.Consumption";
                    object summa = command.ExecuteScalar();
                    label3.Text = summa.ToString() + " рублей";

                    //За месяц
                    command.CommandText = "SELECT SUM(con_sum) FROM dbo.Consumption WHERE con_date > getdate() - 31";
                    summa = command.ExecuteScalar();
                    label5.Text = summa.ToString() + " рублей";

                    //За квартал
                    command.CommandText = "SELECT SUM(con_sum) FROM dbo.Consumption WHERE con_date > getdate() - 91";
                    summa = command.ExecuteScalar();
                    label7.Text = summa.ToString() + " рублей";

                    //За полгода
                    command.CommandText = "SELECT SUM(con_sum) FROM dbo.Consumption WHERE con_date > getdate() - 182";
                    summa = command.ExecuteScalar();
                    label9.Text = summa.ToString() + " рублей";
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void LoadData()
        {
            string conStr = @"Data Source = .\SQLEXPRESS; Initial Catalog = BudFarmDB; Integrated Security = true";
            SqlConnection connection = new SqlConnection(conStr);
            connection.Open();
            SqlCommand command = new SqlCommand();
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Consumption ORDER BY con_id";
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[4]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
            }
            reader.Close();
            connection.Close();
            foreach (string[] s in data)
            {
                dataGridView1.Rows.Add(s);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string conStr = @"Data source = .\SQLEXPRESS; Initial Catalog = BudFarmDB; Integrated Security = true";
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand();
                        command = connection.CreateCommand();
                        string tranzName = textBox1.Text.ToString();
                        string tranzSum = textBox2.Text.ToString();

                        textBox1.Clear();
                        textBox2.Clear();
                        command.CommandText = "INSERT INTO dbo.Consumption (con_name, con_sum) VALUES (@TranzName, @TranzSum)";
                        command.Parameters.AddWithValue("@TranzName", tranzName);
                        command.Parameters.AddWithValue("@TranzSum", tranzSum);
                        command.ExecuteNonQuery();
                        dataGridView1.Rows.Clear();
                        LoadData();
                        Statistics();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Одно из полей не заполнено!");
            }

        }

        private void DataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 0)
            {
                if (double.Parse(e.CellValue1.ToString()) > double.Parse(e.CellValue2.ToString()))
                {
                    e.SortResult = 1;
                }
                else if (double.Parse(e.CellValue1.ToString()) < double.Parse(e.CellValue2.ToString()))
                {
                    e.SortResult = -1;
                }
                else
                {
                    e.SortResult = 0;
                }
                e.Handled = true;
            }
            else if (e.Column.Index == 1)
            {
                dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            else if (e.Column.Index == 2)
            {
                if (double.Parse(e.CellValue1.ToString()) > double.Parse(e.CellValue2.ToString()))
                {
                    e.SortResult = 1;
                }
                else if (double.Parse(e.CellValue1.ToString()) < double.Parse(e.CellValue2.ToString()))
                {
                    e.SortResult = -1;
                }
                else
                {
                    e.SortResult = 0;
                }
                e.Handled = true;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
                connectionString.DataSource = @".\SQLEXPRESS";
                connectionString.InitialCatalog = "BudFarmDB";
                connectionString.IntegratedSecurity = true;
                using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand();
                        command = connection.CreateCommand();
                        command.CommandText = "DELETE FROM dbo.Consumption WHERE con_id = @tranzId";
                        string tranzId = textBox3.Text.ToString();
                        textBox3.Clear();
                        command.Parameters.AddWithValue("@tranzId", tranzId);
                        command.ExecuteNonQuery();
                        dataGridView1.Rows.Clear();
                        LoadData();
                        Statistics();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Введите ID");
            }

        }
    }
}
