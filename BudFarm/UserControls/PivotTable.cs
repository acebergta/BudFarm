using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BudFarm.UserControls
{
    public partial class PivotTable : UserControl
    {
        public PivotTable()
        {
            InitializeComponent();
            Filling();
            DesignData();
            LoadData();
        }

        private void Filling()
        {
            string conStr = @"Data Source = .\SQLEXPRESS; Initial Catalog = BudFarmDB; Integrated Security = true";
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM dbo.Report";
                    command.ExecuteNonQuery();
                    string[] days = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
                    string[] begMonth = { "20190101", "20190201", "20190301", "20190401", "20190501", "20190601", "20190701", "20190801", "20190901", "20191001", "20191101", "20191201" };
                    string[] endMonth = { "20190131", "20190228", "20190331", "20190430", "20190531", "20190630", "20190731", "20190830", "20190931", "20191030", "20191131", "20191230" };
                    //int[] begMonth = { 20190101, 20190201, 20190301, 20190401, 20190501, 20190601, 20190701, 20190801, 20190901, 20191001, 20191101, 20191201 };
                    //int[] endMonth = { 20190131, 20190228, 20190331, 20190430, 20190531, 20190630, 20190731, 20190830, 20190931, 20191030, 20191131, 20191230 };
                    for (int i = 0; i < 6; i++)
                    {
                        command.CommandText = "SELECT SUM(con_sum) FROM dbo.Consumption" +
                            " WHERE con_date >= '" + (string)begMonth[i] + "' and con_date <= '" + (string)endMonth[i] + "'";
                        int sumConsump = (int)command.ExecuteScalar();
                        command.CommandText = "SELECT SUM(inc_sum) FROM dbo.Income " +
                            " WHERE inc_date >= '" + (string)begMonth[i] + "' and inc_date <= '" + (string)endMonth[i] + "'";
                        int sumIncom = (int)command.ExecuteScalar();
                        int diff = sumIncom - sumConsump;
                        string month = days[i];

                        command.CommandText = "INSERT INTO dbo.Report (rep_month, rep_consumption, rep_income, rep_difference) " +
                            "Values (@TranzMonth, @TranzCon, @TranzInc, @TranzDif)";
                        command.Parameters.AddWithValue("@TranzMonth", Convert.ToString(month));
                        command.Parameters.AddWithValue("@TranzCon", sumConsump);
                        command.Parameters.AddWithValue("@TranzInc", sumIncom);
                        command.Parameters.AddWithValue("@TranzDif", diff);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            string conStr = @"Data Source = .\SQLEXPRESS; Initial Catalog = BudFarmDB; Integrated Security = true";
            SqlConnection connection = new SqlConnection(conStr);
            connection.Open();
            SqlCommand command = new SqlCommand();
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.Report ORDER BY rep_id";
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[5]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
            }
            reader.Close();
            connection.Close();
            foreach (string[] s in data)
            {
                dataGridView1.Rows.Add(s);
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
    }
}
