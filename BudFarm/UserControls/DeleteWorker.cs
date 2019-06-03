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
    public partial class DeleteWorker : UserControl
    {
        public DeleteWorker()
        {
            InitializeComponent();
            textBox1.Clear();
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
                    command.CommandText = "DELETE FROM dbo.Workers WHERE wor_id = @WorID";
                    command.Parameters.AddWithValue("@WorID", textBox1.Text);
                    command.ExecuteNonQuery();
                    textBox1.Clear();
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
    }
}
