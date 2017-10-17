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
            ISaveUser userpersistenz = new UserPersistenz();
            SaveResponse response = userpersistenz.CreateUser(new User("", txt_lastname.Text, "", txt_username.Text));
            MessageBox.Show(response.Message, response.Erfolg ? "Erfolg" : "Fehler", MessageBoxButtons.OK,
                response.Erfolg ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }
    }
}
