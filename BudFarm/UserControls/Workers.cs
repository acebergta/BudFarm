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
    public partial class Workers : UserControl
    {
        public Workers()
        {
            InitializeComponent();
            LoadData();
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

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            string conStr = @"Data Source = .\SQLEXPRESS; Initial Catalog = BudFarmDB; Integrated Security = true";
            SqlConnection connection = new SqlConnection(conStr);
            connection.Open();
            SqlCommand command = new SqlCommand();
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.Workers ORDER BY wor_id";
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

        private void Button1_Click(object sender, EventArgs e)
        {
            addWorkers1.BringToFront();
            addWorkers1.Enabled = true;
            LoadData();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            deleteWorker1.BringToFront();
            addWorkers1.Enabled = true;
            LoadData();

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
