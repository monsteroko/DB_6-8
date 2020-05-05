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
        private int _id;
        private DB _db = DB.GetInstance();
        public EditBranches(int id)
        {
            InitializeComponent();
            _id = id;
        }
        private void Insert()
        {
            var rand = new Random();
            int rnd = rand.Next(1, 5);
            string query0 = "SELECT COUNT(*) FROM Branches";
            int ids = _db.RunQuery(query0);
            string query =
                "INSERT INTO Branches VALUES(ids+1,'{textBox1.Text}',rnd)";

            try
            {
                int rows = _db.RunQuery(query);

                if (rows > 0)
                    ShowResultMessageBox("Successfully add");
                else
                    ShowErrorMessageBox("Can't add");
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox("Can't add");
            }
        }

        private void Update()
        {
            string query =
                "UPDATE Branches SET " +
                $"Branch_Name = '{textBox1.Text}' " +
                $"WHERE Id = {_id}";

            try
            {
                int rows = _db.RunQuery(query);

                if (rows > 0)
                    ShowResultMessageBox("Successfully changed!");
                else
                    ShowErrorMessageBox("Can't change");
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox("Can't change");
            }
        }

        private void ShowResultMessageBox(string text)
        {
            MessageBox.Show(text, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowErrorMessageBox(string text)
        {
            MessageBox.Show(text, "Error occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 50)
            {
                textBox1.Text = textBox1.Text.Substring(0, 50);
            }


            if (_id == -1)
                Insert();
            else
                Update();

            this.Close();
        }

        private void EditBranches_Load(object sender, EventArgs e)
        {
            if (_id != -1)
            {
                string name = _db.GetRank(_id);

                if (name == null)
                {
                    ShowErrorMessageBox("No branch with this ID!");
                    return;
                }
            }
        }
    }
}
