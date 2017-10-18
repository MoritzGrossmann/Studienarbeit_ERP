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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            ISaveUser userpersistenz = new RfcUserPersistence();
            BapiReturn response = userpersistenz.CreateUser(new User("", txt_lastname.Text, "", txt_username.Text));
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
    }
}
