using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAendern.Console.SapServiceReference;

namespace UserAendern.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Z_HH_USERClient _sapConnection = new Z_HH_USERClient()
            {
                ClientCredentials = { UserName = { UserName = "wsuser", Password = "vorsichtloop" } }
            };

            USERGetList list = new USERGetList();
            list.MaxRows = 200;
            list.MaxRowsSpecified = true;
            USERGetListResponse response = _sapConnection.USERGetList(list);

            foreach (var user in response.UserList)
            {
                System.Console.WriteLine(user.Username);
            }
            System.Console.Read();
        }
    }
}
