using DB6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB8
{
    public partial class Form1 : Form
    {
        private DB _db = null;
        private DataSet _dataSet;
        private DataView _servicemans, _ranks, _branches;

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                    return;

                _servicemans.Delete(dataGridView1.SelectedRows[0].Index);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }

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

        private void Form1_Load(object sender, EventArgs e)
        {
            _dataSet = _db.SelectAllTables();
            _servicemans = _dataSet.Tables[0].DefaultView;
            _ranks = _dataSet.Tables[1].DefaultView;
            _branches = _dataSet.Tables[2].DefaultView;
            dataGridView1.DataSource = _servicemans;
            dataGridView2.DataSource = _ranks;
            dataGridView3.DataSource = _branches;

        }
        private void ShowResultMessageBox(string text)
        {
            MessageBox.Show(text, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView row = _servicemans.AddNew();

                row.BeginEdit();
                Form form = new EditServiceman(row);
                form.ShowDialog();
                row.EndEdit();
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                    return;

                DataRowView row = _servicemans[dataGridView1.SelectedRows[0].Index];

                row.BeginEdit();
                Form form = new EditServiceman(row);
                form.ShowDialog();
                row.EndEdit();
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView row = _ranks.AddNew();

                row.BeginEdit();
                Form form = new EditRank(row);
                form.ShowDialog();
                row.EndEdit();
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count == 0)
                    return;

                DataRowView row = _ranks[dataGridView2.SelectedRows[0].Index];

                row.BeginEdit();
                Form form = new EditRank(row);
                form.ShowDialog();
                row.EndEdit();
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count == 0)
                    return;

                _ranks.Delete(dataGridView2.SelectedRows[0].Index);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView row = _branches.AddNew();

                row.BeginEdit();
                Form form = new EditBranches(row);
                form.ShowDialog();
                row.EndEdit();
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView3.SelectedRows.Count == 0)
                    return;

                DataRowView row = _branches[dataGridView3.SelectedRows[0].Index];

                row.BeginEdit();
                Form form = new EditBranches(row);
                form.ShowDialog();
                row.EndEdit();
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView3.SelectedRows.Count == 0)
                    return;

                _branches.Delete(dataGridView3.SelectedRows[0].Index);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            int index = dataGridView1.SelectedRows[0].Index;
            int rnkId = (byte)_servicemans[index]["Id_rank"];

            foreach (var row in dataGridView2.Rows.OfType<DataGridViewRow>())
            {
                row.Selected = false;
            }

            foreach (var row in dataGridView3.Rows.OfType<DataGridViewRow>())
            {
                row.Selected = false;
            }

            for (int i = 0; i < _ranks.Count; i++)
            {
                if ((byte)_ranks[i]["Id_rank"] == rnkId)
                {
                    dataGridView2.Rows[i].Selected = true;
                }
            }
        }
        private void HandleException(string text)
        {
            MessageBox.Show(text, "Произошла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }

    }
}
