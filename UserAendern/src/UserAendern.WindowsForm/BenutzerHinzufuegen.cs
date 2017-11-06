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
    public partial class BenutzerHinzufuegen : Form
    {
        public BenutzerHinzufuegen()
        {
            InitializeComponent();
        }

        public MainForm Main { get; set; }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (txt_password.Text != txt_password_wdh.Text)
            {
                MessageBox.Show("Passwörter stimmen nicht überein", "Fehler", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {

                ISaveUser userpersistenz = new RfcUserPersistence();

                try
                {
                    BapiReturn response =
                        userpersistenz.CreateUser(new User("", txt_lastname.Text, "", txt_username.Text),
                            txt_password.Text);
                    MessageBox.Show(response.Message, response.Type.IsError ? "Fehler" : "Erfolg", MessageBoxButtons.OK,
                        GetIconFromBapiReturn(response));
                    if (response.Type.Equals(BapiReturnType.Success))
                    {
                        Main.RefreshUsers();
                        this.Hide();
                    }
                }
                catch (SapCommunicationException ex)
                {
                    MessageBox.Show(ex.Message, @"Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
