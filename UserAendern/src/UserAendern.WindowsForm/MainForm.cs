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

        private User _currentUser;

        private UserDetails _currentUserDetails;

        private readonly ILoadUser _userpersistenceLoad = new RfcUserPersistence();

        private readonly ISaveUser _userpersistenceSave = new RfcUserPersistence();

        public MainForm()
        {
            InitializeComponent();
            RefreshUsers();
        }

        private void listbox_users_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string user = listbox_users.SelectedItem.ToString();
                txt_username.Text = user;

                _currentUser = new User("", "", "", user);

                try
                {

                    var details = _userpersistenceLoad.GetUserDetail(user);

                    _currentUserDetails = details;

                    txt_street.Text = details.Address.Street;
                    txt_number.Text = details.Address.Number;
                    txt_postcode.Text = details.Address.Postcode;
                    txt_city.Text = details.Address.City;
                    txt_firstname.Text = details.Firstname;
                    txt_lastname.Text = details.Lastname;
                }
                catch (SapCommunicationException ex)
                {
                    MessageBox.Show(ex.Message, @"Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (NullReferenceException)
            {
                
            }
        }

        private void btn_unlock_Click(object sender, EventArgs e)
        {

            try
            {
                var response = _userpersistenceSave.UnlockUser(txt_username.Text);
                MessageBox.Show(response.Message, response.Type.IsError ? "Fehler" : "Erfolg", MessageBoxButtons.OK,
                    GetIconFromBapiReturn(response));
            } 
            catch (SapCommunicationException ex)
            {
                MessageBox.Show(ex.Message, @"Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_add_user_Click(object sender, EventArgs e)
        {
            BenutzerHinzufuegen form = new BenutzerHinzufuegen() { Main = this};
            form.Show();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {

                var response = _userpersistenceSave.DeleteUser(txt_username.Text);
                if (response.Type.IsSuccess)
                {
                    RefreshUsers();
                }

                MessageBox.Show(response.Message, response.Type.IsError ? "Fehler" : "Erfolg", MessageBoxButtons.OK,
                    GetIconFromBapiReturn(response));
            }                 
            catch (SapCommunicationException ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void txt_query_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                listbox_users.Items.Clear();
                foreach (var user in _users.Where(u => u.UserName.ToUpper().Contains(txt_query.Text.ToUpper())))
                {
                    listbox_users.Items.Add(new UserRow(user));
                }
            }
        }

        private void btn_lock_Click(object sender, EventArgs e)
        {
            try { 
            var response = _userpersistenceSave.LockUser(txt_username.Text);
            MessageBox.Show(response.Message, response.Type.IsError ? "Fehler" : "Erfolg", MessageBoxButtons.OK,
                GetIconFromBapiReturn(response));
            }
            catch (SapCommunicationException ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (bapiReturn.Type.IsSuccess)
            {
                return MessageBoxIcon.Asterisk;
            }
            else
            {
                return MessageBoxIcon.None;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try { 
            var response = _userpersistenceSave.ChangeUser(_currentUser, new UserDetails(txt_firstname.Text, txt_lastname.Text, new Address(txt_street.Text, txt_number.Text, txt_postcode.Text, txt_city.Text), _currentUserDetails.IsLocked));
            MessageBox.Show(response.Message, response.Type.IsError ? "Fehler" : "Erfolg", MessageBoxButtons.OK,
                GetIconFromBapiReturn(response));
            }
            catch (SapCommunicationException ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void RefreshUsers()
        {

            try
            {
                _users = _userpersistenceLoad.GetUsers;
                listbox_users.Items.Clear();

                if (txt_query.Text.Trim().Equals(""))
                {
                    foreach (var user in _users)
                    {
                        listbox_users.Items.Add(new UserRow(user));
                    }
                }
                else
                {
                    foreach (var user in _users.Where(u => u.UserName.ToUpper().Contains(txt_query.Text.ToUpper())))
                    {
                        listbox_users.Items.Add(new UserRow(user));
                    }
                }
            }
            catch (SapCommunicationException ex)
            {
                MessageBox.Show(ex.Message, "Fehler",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RefreshUsers();
        }
    }
}
