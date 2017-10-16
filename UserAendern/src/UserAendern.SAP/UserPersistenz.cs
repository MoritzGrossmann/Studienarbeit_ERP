using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAendern.Domain;
using UserAendern.SAP.SapServiceReference;

namespace UserAendern.SAP
{
    public class UserPersistenz : ILoadUser, ISaveUser
    {

        private readonly Z_HH_USERClient _sapConnection = new Z_HH_USERClient()
        {
            ClientCredentials = { UserName = { UserName = "wsuser", Password = "vorsichtloop" } }
        };

        public IEnumerable<User> GetUsers
        {
            get
            {
                USERGetList list = new USERGetList();
                list.MaxRows = 500;
                list.MaxRowsSpecified = true;
                USERGetListResponse response = _sapConnection.USERGetList(list);
                return response.UserList.Select(FromSap);
            }
        }

        public User GetUserDetails(string username)
        {
            USERGetDetail details = new USERGetDetail {UserName = username};
            USERGetDetailResponse userdetails = _sapConnection.USERGetDetail(details);
            return new User(userdetails.Address.Firstname, userdetails.Address.Lastname, userdetails.Address.Fullname, username);
        }    
        
        public bool CreateUser(User user)
        {
            USERCreate1 createUser = new USERCreate1();
            createUser.UserName = user.UserName;
            createUser.Password = new Bapipwd() {Bapipwd1 = "init1234"};
            USERCreate1Response createResponse = _sapConnection.USERCreate1(createUser);
            return true;
        }

        public bool DeleteUser(User user)
        {
            USERDelete deleteUser = new USERDelete();
            deleteUser.UserName = user.UserName;
            USERDeleteResponse deleteResponse = _sapConnection.USERDelete(deleteUser);
            return true;
        }

        public bool ChangeUser(User user)
        {
            throw new NotImplementedException();
        }

        private User FromSap(Bapiusname sapuser)
        {
            return new User(sapuser.Firstname, sapuser.Lastname, sapuser.Fullname, sapuser.Username);
        }
    }
}
