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

namespace DB6
{
    public partial class Form1 : Form
    {
        private DB _db = null;
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
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            string sort = SortServicemans();
            _db.SelectIntoGrid($"SELECT * FROM Servicemans ORDER BY {sort}", table);

        }
        private void ShowResultMessageBox(string text)
        {
            MessageBox.Show(text, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HandleException(string text)
        {
            MessageBox.Show(text, "Произошла ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }
        #region  Servicemans



        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string sort = SortServicemans();
                _db.SelectIntoGrid($"SELECT * FROM Servicemans WHERE Id_Serviceman = { Convert.ToInt32(numericUpDown1.Value)} ORDER BY {sort}", table);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }

        }
        private string SortServicemans()
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: return "Id_Serviceman asc";
                case 1: return "Last_Name asc";
                case 2: return "First_Name asc";
                case 3: return "Age asc";
                default: return "Id_Serviceman asc";
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string sort = SortServicemans();
                _db.SelectIntoGrid($"SELECT * FROM Servicemans WHERE Last_Name LIKE '%{textBox1.Text}%' ORDER BY {sort}", table);
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
                string sort = SortServicemans();
                _db.SelectIntoGrid($"SELECT * FROM Servicemans ORDER BY {sort}", table);
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
                Form form = new EditServiceman(-1);
                form.ShowDialog();

                string sort = SortServicemans();
                _db.SelectIntoGrid($"SELECT * FROM Servicemans ORDER BY {sort}", table);
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
                int rows = _db.RunQuery($"DELETE FROM Servicemans WHERE Id_Serviceman =  {Convert.ToInt32(numericUpDown2.Value)}");
                string result;

                if (rows > 0)
                    result = "Serviceman successfully deleted!";
                else
                    result = "No serviceman with this ID!";

                ShowResultMessageBox(result);

                string sort = SortServicemans();
                _db.SelectIntoGrid($"SELECT * FROM Servicemans ORDER BY {sort}", table);
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
                Form form = new EditServiceman(Convert.ToInt32(numericUpDown2.Value));
                form.ShowDialog();

                string sort = SortServicemans();
                _db.SelectIntoGrid($"SELECT * FROM Servicemans ORDER BY {sort}", table);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }
        }
        #endregion

        #region  Ranks
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex==0)
            {
                string sort = SortServicemans();
                _db.SelectIntoGrid($"SELECT * FROM Servicemans ORDER BY {sort}", table);
            }
            if (tabControl1.SelectedIndex == 1)
            {
                string sort = SortRanks();
                _db.SelectIntoGrid($"SELECT * FROM Ranks ORDER BY {sort}", table);
            }
            if (tabControl1.SelectedIndex == 2)
            {
                string sort = SortBranches();
                _db.SelectIntoGrid($"SELECT * FROM Branches ORDER BY {sort}", table);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                string sort = SortRanks();
                _db.SelectIntoGrid($"SELECT * FROM Ranks WHERE Id_rank = {Convert.ToInt32(numericUpDown4.Value)} ORDER BY {sort}", table);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }
        }
        private string SortRanks()
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0: return "Id_rank asc";
                case 1: return "Name_of_rank asc";
                default: return "Id_rank asc";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sort = SortServicemans();
            _db.SelectIntoGrid($"SELECT * FROM Servicemans ORDER BY {sort}", table);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sort = SortRanks();
            _db.SelectIntoGrid($"SELECT * FROM Ranks WHERE Id_rank = {Convert.ToInt32(numericUpDown4.Value)} ORDER BY {sort}", table);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                string sort = SortRanks();
                _db.SelectIntoGrid($"SELECT * FROM Ranks WHERE Name_of_rank LIKE '%{textBox2.Text}%' ORDER BY {sort}", table);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                string sort = SortRanks();
                _db.SelectIntoGrid($"SELECT * FROM Ranks ORDER BY {sort}", table);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                Form form = new EditRank(-1);
                form.ShowDialog();

                string sort = SortRanks();
                _db.SelectIntoGrid($"SELECT * FROM Ranks ORDER BY {sort}", table);
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
                int rows = _db.RunQuery($"DELETE FROM Ranks WHERE Id_rank = {Convert.ToInt32(numericUpDown3.Value)}");
                string result;

                if (rows > 0)
                    result = "Rank successfully deleted!";
                else
                    result = "No rank with this ID!";

                ShowResultMessageBox(result);

                string sort = SortRanks();
                _db.SelectIntoGrid($"SELECT * FROM Ranks ORDER BY {sort}", table);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }
        }
        #endregion
        #region  Branches
        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                string sort = SortBranches();
                _db.SelectIntoGrid($"SELECT * FROM Branches WHERE Id_Branch = {Convert.ToInt32(numericUpDown6.Value)} ORDER BY {sort}", table);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }
        }
        private string SortBranches()
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0: return "Id_Branch asc";
                case 1: return "Branch_Name asc";
                default: return "Id_Branch asc";
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                string sort = SortBranches();
                _db.SelectIntoGrid($"SELECT * FROM Branches WHERE Branch_Name LIKE '%{textBox3.Text}%' ORDER BY {sort}", table);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                string sort = SortBranches();
                _db.SelectIntoGrid($"SELECT * FROM Branches ORDER BY {sort}", table);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                Form form = new EditBranches(-1);
                form.ShowDialog();

                string sort = SortBranches();
                _db.SelectIntoGrid($"SELECT * FROM Branches ORDER BY {sort}", table);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                int rows = _db.RunQuery($"DELETE FROM Branches WHERE Id_Branch = {Convert.ToInt32(numericUpDown5.Value)}");
                string result;

                if (rows > 0)
                    result = "Branch successfully deleted!";
                else
                    result = "No Branch with this ID!";

                ShowResultMessageBox(result);

                string sort = SortRanks();
                _db.SelectIntoGrid($"SELECT * FROM Branches ORDER BY {sort}", table);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                Form form = new EditBranches(Convert.ToInt32(numericUpDown5.Value));
                form.ShowDialog();

                string sort = SortBranches();
                _db.SelectIntoGrid($"SELECT * FROM Branches ORDER BY {sort}", table);
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ShowResultMessageBox(_db.MaxAge());
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
                ShowResultMessageBox(_db.ServicemansAges());
            }
            catch (Exception ex)
            {
                HandleException(ex.Message);
            }

        }
    }



}
