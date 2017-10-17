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
using UserAendern.WindowsForm.View;

namespace UserAendern.WindowsForm
{
    public partial class MainForm : Form
    {
        private User shownUser;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ILoadUser userPersistenz = new UserPersistenz();
            foreach (var user in userPersistenz.GetUsers)
            {
                listbox_users.Items.Add(new UserRow(user));
            }
        }

        private void listbox_users_SelectedValueChanged(object sender, EventArgs e)
        {
            string user = listbox_users.SelectedItem.ToString();
            txt_username.Text = user;
            //ILoadUser userPersistenz = new UserPersistenz();
            //User u = userPersistenz.GetUserDetails(user);

        }

        private void btn_unlock_Click(object sender, EventArgs e)
        {
            ISaveUser userpersistenz = new UserPersistenz();
            userpersistenz.LockUser(txt_username.Text);
        }

        private void listbox_users_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_add_user_Click(object sender, EventArgs e)
        {
            BenutzerHinzufuegen form = new BenutzerHinzufuegen();
            form.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
