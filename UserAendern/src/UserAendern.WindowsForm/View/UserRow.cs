using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UserAendern.Domain;

namespace UserAendern.WindowsForm.View
{
    public class UserRow
    {
        public User User { get; set; } 
        public UserRow(User user)
        {
            this.User = user;
        }

        
        public override string ToString() 
        {
            return this.User.UserName;
        }
    }
} 