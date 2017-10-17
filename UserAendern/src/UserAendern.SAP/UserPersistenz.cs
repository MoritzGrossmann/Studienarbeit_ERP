using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
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

        public Address GetUserAddress(string username)
        {
            USERGetDetail details = new USERGetDetail {UserName = username};
            USERGetDetailResponse userdetails = _sapConnection.USERGetDetail(details);
            return new Address(userdetails.Address.Street, userdetails.Address.HouseNo, userdetails.Address.PostlCod1, userdetails.Address.City);
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

        public bool ChangeUser(User user, Address address)
        {
            USERChange changeUser = new USERChange();
            changeUser.UserName = user.UserName;
            changeUser.Address.City = address.city;
            changeUser.Address.Street = address.Street;
            changeUser.Address.HouseNo = address.Number;
            changeUser.Address.PostlCod1 = address.postcode;
            USERChangeResponse response = _sapConnection.USERChange(changeUser);
            return true;
        }

        public bool LockUser(string user)
        {
            USERLock lockUser = new USERLock() { UserName = user};
            USERLockResponse lockResponse = _sapConnection.USERLock(lockUser);
            return true;
        }

        public bool UnlockUser(string user)
        {
            USERUnlock unlockUser = new USERUnlock() {UserName = user};
            USERUnlockResponse unlockResponse = _sapConnection.USERUnlock(unlockUser);
            String[] message = unlockResponse.Return.Select(FromBapiRet).ToArray();
            return true;
        }

        private User FromSap(Bapiusname sapuser)
        {
            return new User(sapuser.Firstname, sapuser.Lastname, sapuser.Fullname, sapuser.Username);
        }

        private string FromBapiRet(Bapiret2 bapireturn)
        {
            return bapireturn.Message;
        }
    }
}
