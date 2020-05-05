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

    }

}
