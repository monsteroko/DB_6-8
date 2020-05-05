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
    public partial class EditRank : Form
    {
        private DataRowView _row;
        public EditRank(DataRowView row)
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
            if (_row["Id_rank"] == null)
            {
                Random rand = new Random();
                _row["Id_rank"] = rand.Next(5, 20);
            }
            _row["Name_of_rank"] = textBox1.Text;
            this.Close();
        }

        private void EditRank_Load(object sender, EventArgs e)
        {
            textBox1.Text = _row["Name_of_rank"].ToString();
        }
    }
}
