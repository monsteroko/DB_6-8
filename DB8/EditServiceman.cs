using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB6
{
    public partial class EditServiceman : Form
    {
        public EditServiceman(DataRowView row)
        {
            InitializeComponent();
            _row = row;
        }
        private DataRowView _row;

        private void EditServiceman_Load(object sender, EventArgs e)
        {
            textBox1.Text = _row["Last_Name"].ToString();
            textBox1.Text = _row["First_Name"].ToString();
            textBox1.Text = _row["Pathronymic"].ToString();
            numericUpDown1.Value = _row["Age"] == DBNull.Value ? numericUpDown1.Value : (int)_row["Age"];
            numericUpDown2.Value = _row["Id_Rank"] == DBNull.Value ? numericUpDown2.Minimum : (int)_row["Id_Rank"];
            numericUpDown3.Value = _row["Branches_id_Branch"] == DBNull.Value ? numericUpDown3.Minimum : (int)_row["Branches_id_Branch"];
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
                _row["Last_Name"] = DBNull.Value;
            else
                _row["Last_Name"] = textBox1.Text; 
            if (textBox2.Text == "")
                _row["First_Name"] = DBNull.Value;
            else
                _row["First_Name"] = textBox2.Text;
            if (textBox3.Text == "")
                _row["Pathronymic"] = DBNull.Value;
            else
                _row["Pathronymic"] = textBox3.Text;

            _row["Age"] = numericUpDown1.Value;
            _row["Id_Rank"] = numericUpDown2.Value;
            _row["Branches_id_Branch"] = numericUpDown3.Value;
            if (_row["Id_Serviceman"] == null)
            {
                Random rand = new Random();
                _row["Id_Serviceman"] = rand.Next(5, 20);
            }
            this.Close();
        }

    }
}