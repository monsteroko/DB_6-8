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

namespace DB7
{
    public partial class Form1 : Form
    {
        private DB _db = null;
        private DataSet _dataSet;
        private DataView _servicemans, _ranks, _branches;

        private void Form1_Load(object sender, EventArgs e)
        {
            _dataSet = _db.SelectAllTables();
            _servicemans = _dataSet.Tables[0].DefaultView;
            _ranks = _dataSet.Tables[1].DefaultView;
            _branches = _dataSet.Tables[2].DefaultView;
            dataGridView1.DataSource = _servicemans;
            dataGridView2.DataSource = _ranks;
            dataGridView3.DataSource = _branches;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            _servicemans.RowFilter = null;
            dataGridView1.DataSource = _servicemans;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            _ranks.RowFilter = null;
            dataGridView2.DataSource = _ranks;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            _branches.RowFilter = null;
            dataGridView3.DataSource = _branches;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            }
            if (comboBox1.SelectedIndex == 1)
            {
                dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
            }
            if (comboBox1.SelectedIndex == 2)
            {
                dataGridView1.Sort(dataGridView1.Columns[4], ListSortDirection.Ascending);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                dataGridView2.Sort(dataGridView2.Columns[0], ListSortDirection.Ascending);
            }
            if (comboBox2.SelectedIndex == 1)
            {
                dataGridView2.Sort(dataGridView2.Columns[1], ListSortDirection.Ascending);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                dataGridView3.Sort(dataGridView3.Columns[0], ListSortDirection.Ascending);
            }
            if (comboBox3.SelectedIndex == 1)
            {
                dataGridView3.Sort(dataGridView3.Columns[1], ListSortDirection.Ascending);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _servicemans.RowFilter = string.Format("Last_Name LIKE '%{0}%'", textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _servicemans.RowFilter="Id_serviceman = "+(int)numericUpDown1.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _servicemans.RowFilter = "Age >= " + (int)numericUpDown2.Value;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _ranks.RowFilter = "Id_rank = " + (int)numericUpDown4.Value;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _ranks.RowFilter = string.Format("Name_of_rank LIKE '%{0}%'", textBox2.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _branches.RowFilter = "Id_Branch >= " + (int)numericUpDown5.Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _ranks.RowFilter = "Id_rank >= " + (int)numericUpDown3.Value;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            _branches.RowFilter = "Id_Branch = " + (int)numericUpDown6.Value;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _branches.RowFilter = string.Format("Branch_Name LIKE '%{0}%'", textBox3.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int maxage = 0;
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if (Convert.ToInt32(dataGridView1[4, i].Value) > maxage)
                    maxage = Convert.ToInt32(dataGridView1[4, i].Value);
            }
            MessageBox.Show("Max age: " + maxage.ToString(), "Age", MessageBoxButtons.OK);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Some ages...", "Age", MessageBoxButtons.OK);
        }

        public Form1()
        {
            InitializeComponent();
            try
            {
                _db = DB.GetInstance();
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }
        }
        private void HandleException(string text)
        {
            MessageBox.Show(text, "Произошла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }
    }
}
