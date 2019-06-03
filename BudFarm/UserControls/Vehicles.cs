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
    public partial class Vehicles : UserControl
    {
        public Vehicles()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
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
                    command.CommandText = "SELECT veh_name FROM dbo.Vehicles ORDER BY veh_name";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader.GetValue(0).ToString());
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string autoName = listBox1.SelectedItem.ToString();
            label12.Text = autoName;
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

                    command.CommandText = "SELECT veh_automotive FROM dbo.Vehicles WHERE veh_name = @AutoName";
                    command.Parameters.AddWithValue("@AutoName", autoName);
                    object autoMotive = command.ExecuteScalar();
                    label13.Text = autoMotive.ToString();

                    command.CommandText = "SELECT veh_type FROM dbo.Vehicles WHERE veh_name = @AutoName";
                    object type = command.ExecuteScalar();
                    label14.Text = type.ToString();

                    command.CommandText = "SELECT veh_subtype FROM dbo.Vehicles WHERE veh_name = @AutoName";
                    object subType = command.ExecuteScalar();
                    label15.Text = subType.ToString();

                    command.CommandText = "SELECT veh_regNum FROM dbo.Vehicles WHERE veh_name = @AutoName";
                    object regNum = command.ExecuteScalar();
                    label16.Text = regNum.ToString();

                    command.CommandText = "SELECT veh_color FROM dbo.Vehicles WHERE veh_name = @AutoName";
                    object color = command.ExecuteScalar();
                    label17.Text = color.ToString();

                    command.CommandText = "SELECT veh_markModel FROM dbo.Vehicles WHERE veh_name = @AutoName";
                    object markModel = command.ExecuteScalar();
                    label18.Text = markModel.ToString();

                    command.CommandText = "SELECT veh_yearOfIssue FROM dbo.Vehicles WHERE veh_name = @AutoName";
                    object yearOfIssue = command.ExecuteScalar();
                    label19.Text = yearOfIssue.ToString();

                    command.CommandText = "SELECT veh_typeOfOwnership FROM dbo.Vehicles WHERE veh_name = @AutoName";
                    object typeOfOwnership = command.ExecuteScalar();
                    label20.Text = typeOfOwnership.ToString();

                    command.CommandText = "SELECT veh_driver FROM dbo.Vehicles WHERE veh_name = @AutoName";
                    object driver = command.ExecuteScalar();
                    if (driver.ToString() == "")
                    {
                        label21.Text = "В настоящий момент отсутствует";
                    }
                    else
                    {
                        label21.Text = driver.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
