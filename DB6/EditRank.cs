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
        private int _id;
        private DB _db = DB.GetInstance();
        public EditRank(int id)
        {
            InitializeComponent();
            _id = id;
        }
        private void Insert()
        {
            string query0 = "SELECT COUNT(*) FROM Ranks";
            int ids = _db.RunQuery(query0);
            string query =
                "INSERT INTO Ranks VALUES(ids+1,'{textBox1.Text}')";

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
                "UPDATE Ranks SET " +
                $"Name_of_rank = '{textBox1.Text}' " +
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

        private void EditRank_Load(object sender, EventArgs e)
        {
            if (_id != -1)
            {
                string name = _db.GetRank(_id);

                if (name == null)
                {
                    ShowErrorMessageBox("No rank with this ID!");
                    return;
                }
            }
        }
    }
}
