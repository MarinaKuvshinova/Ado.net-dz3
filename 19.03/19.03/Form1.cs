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

namespace _19._03
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=HOME-ПК; Initial Catalog=Sales; Integrated Security=true;";
        SqlConnection sqlConnection = null;
        SqlDataAdapter sqlDateAdapter = null;
        DataSet dataSet = null;
        SqlCommandBuilder sqlCommanBulder = null;
        public Form1()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(connectionString);
            btnModifiedValues.Click += btnModifiedValues_Click;
            try
            {
                string query = "select * from Shoppers";
                dataGridView1.DataSource = null;
                dataSet = new DataSet();
                sqlDateAdapter = new SqlDataAdapter(query, sqlConnection);
                sqlCommanBulder = new SqlCommandBuilder(sqlDateAdapter);
                sqlDateAdapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }

        private void btnModifiedValues_Click(object sender, EventArgs e)
        {
            //dataGridView2.DataSource = null;
            //var SourceData = dataSet.Tables[0].GetChanges(DataRowState.Modified);
            //dataGridView2.DataSource = SourceData;
            //sqlDateAdapter.Update(dataSet.Tables[0].Select(null, null, DataViewRowState.ModifiedCurrent));

            dataGridView2.DataSource = null;
            sqlDateAdapter.Update(dataSet.Tables[0].Select(null, null, DataViewRowState.ModifiedCurrent));
            dataGridView2.DataSource = dataSet.Tables[0].GetChanges(DataRowState.Modified);



        }
    }
}
