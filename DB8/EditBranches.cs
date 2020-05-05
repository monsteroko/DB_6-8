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
    public partial class EditBranches : Form
    {
        private DataRowView _row;
        public EditBranches(DataRowView row)
        {
            InitializeComponent();
            _row = row;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 50)
            {
                textBox1.Text = textBox1.Text.Substring(0, 50);
            }
            Random rand = new Random();
            if (_row["Id_Branch"] ==null)
            {
                _row["Id_Branch"] = rand.Next(5, 20);
            }
            if (_row["Platoons_id_Platoon"] == null)
            {
                _row["Platoons_id_Platoon"] = rand.Next(1, 5);
            }
            _row["Branch_Name"] = textBox1.Text;
            this.Close();

        }

        private void EditBranches_Load(object sender, EventArgs e)
        {
            textBox1.Text = _row["Branch_Name"].ToString();
        }
    }
}
