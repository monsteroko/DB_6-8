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
        public EditServiceman(int id)
        {
            InitializeComponent();
            _id = id;
        }
        private int _id;
        private DB _db = DB.GetInstance();
        private void Insert()
        {
            string query0 ="SELECT COUNT(*) FROM Servicemans";
            int ids = _db.RunQuery(query0);
            string query =
                "INSERT INTO Servicemans VALUES(ids+1,'{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','Convert.ToInt32(numericUpDown1.Value)','Convert.ToInt32(numericUpDown2.Value)','Convert.ToInt32(numericUpDown3.Value)')";

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
                "UPDATE Servicemans SET " +
                $"Last_Name = '{textBox1.Text}',First_Name = '{textBox2.Text}',Pathronymic = '{textBox3.Text}',Age = '{Convert.ToInt32(numericUpDown1.Value)}',Id_rank = '{Convert.ToInt32(numericUpDown2.Value)}',Branches_id_Branch = '{Convert.ToInt32(numericUpDown3.Value)}', " +
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

        private void EditServiceman_Load(object sender, EventArgs e)
        {
            if (_id != -1)
            {
                Serviceman name = _db.GetServiceman(_id);

                if (name == null)
                {
                    ShowErrorMessageBox("No serviceman with this ID!");
                    return;
                }
            }
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
    }
}
