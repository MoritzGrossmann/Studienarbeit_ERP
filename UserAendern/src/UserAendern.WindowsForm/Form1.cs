using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserAendern.Domain;
using UserAendern.SAP;

namespace UserAendern.WindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ILoadUser userPersistenz = new UserPersistenz();
            foreach (var user in userPersistenz.GetUsers)
            {
                listbox_users.Items.Add(user);
            }
        }
    }
}
