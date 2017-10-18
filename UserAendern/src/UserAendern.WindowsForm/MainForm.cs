using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Xsl;
using UserAendern.Domain;
using UserAendern.SAP;
using UserAendern.WindowsForm.View;

namespace UserAendern.WindowsForm
{
    public partial class MainForm : Form
    {
        private IEnumerable<User> _users = new List<User>();

        private ILoadUser _userpersistenceLoad = new RfcUserPersistence();

        private ISaveUser _userpersistenceSave = new RfcUserPersistence();

        public MainForm()
        {
            InitializeComponent();
            _users = _userpersistenceLoad.GetUsers;
            foreach (var user in _users)
            {
                listbox_users.Items.Add(new UserRow(user));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listbox_users_SelectedValueChanged(object sender, EventArgs e)
        {
            string user = listbox_users.SelectedItem.ToString();
            txt_username.Text = user;

            var details = _userpersistenceLoad.GetUserDetail(user);

            txt_street.Text = details.Address.Street;
            txt_number.Text = details.Address.Number;
            txt_postcode.Text = details.Address.Postcode;
            txt_city.Text = details.Address.City;
            txt_firstname.Text = details.Firstname;
            txt_lastname.Text = details.Lastname;
        }

        private void btn_unlock_Click(object sender, EventArgs e)
        {
            var response = _userpersistenceSave.UnlockUser(txt_username.Text);
            MessageBox.Show(response.Message, response.Type.IsError ? "Fehler" : "Erfolg", MessageBoxButtons.OK,
                GetIconFromBapiReturn(response));
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

        private void btn_delete_Click(object sender, EventArgs e)
        {

            var response = _userpersistenceSave.DeleteUser(txt_username.Text);


            MessageBox.Show(response.Message, response.Type.IsError ? "Fehler" : "Erfolg", MessageBoxButtons.OK,
                GetIconFromBapiReturn(response));
        }

        private void txt_query_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                listbox_users.Items.Clear();
                foreach (var user in _users.Where(u => u.UserName.Contains(txt_query.Text)))
                {
                    listbox_users.Items.Add(new UserRow(user));
                }
            }
        }

        private void btn_lock_Click(object sender, EventArgs e)
        {
            var response = _userpersistenceSave.LockUser(txt_username.Text);
            MessageBox.Show(response.Message, response.Type.IsError ? "Fehler" : "Erfolg", MessageBoxButtons.OK,
                GetIconFromBapiReturn(response));
        }

        private MessageBoxIcon GetIconFromBapiReturn(BapiReturn bapiReturn)
        {
            if (bapiReturn.Type.IsInfo)
            {
                return MessageBoxIcon.Information;
            }
            if (bapiReturn.Type.IsError)
            {
                return MessageBoxIcon.Error;
            }
            if (bapiReturn.Type.IsWarning)
            {
                return MessageBoxIcon.Warning;
            }
            else
            {
                return MessageBoxIcon.None;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
