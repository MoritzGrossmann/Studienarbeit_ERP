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
    public class UserPersistenz //: ILoadUser, ISaveUser
    {

        private readonly Z_HH_USERClient _sapConnection = new Z_HH_USERClient()
        {
            ClientCredentials = { UserName = { UserName = "wsuser", Password = "vorsichtloop" } }
        };

        public UserDetails GetUserDetail(string username)
        {
            throw new NotImplementedException();
        }

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
        
        public SaveResponse CreateUser(User user)
        {
            USERCreate1 createUser = new USERCreate1()
            {
                UserName = user.UserName,
                Password = new Bapipwd() { Bapipwd1 = "init1234" },
                Address = new Bapiaddr3()
                {
                    Lastname = user.LastName,
                    Firstname = user.FirstName,
                    Fullname = user.FullName
                },
                Name_In = new Bapibname()
                {
                    Bapibname1 = user.UserName
                }
            };

            try
            {
                USERCreate1Response createResponse = _sapConnection.USERCreate1(createUser);
            }
            catch (System.ServiceModel.FaultException e)
            {
                return new SaveResponse(false, e.Message);
            }
            catch (System.ServiceModel.CommunicationException e)
            {
                return new SaveResponse(false, e.Message);
            }
            return new SaveResponse(true, String.Format("User {0} erfolgreich angelegt", user.UserName));
        }

        public SaveResponse DeleteUser(User user)
        {
            USERDelete deleteUser = new USERDelete();
            deleteUser.UserName = user.UserName;

            try
            {
                USERDeleteResponse deleteResponse = _sapConnection.USERDelete(deleteUser);
            }
            catch (System.ServiceModel.FaultException e)
            {
                return new SaveResponse(false, e.Message);
            }
            catch (System.ServiceModel.CommunicationException e)
            {
                return new SaveResponse(false, e.Message);
            }
            return new SaveResponse(true, String.Format("User {0} erfolgreich gelöscht", user.UserName));
        }

        public SaveResponse ChangeUser(User user, Address address)
        {
            USERChange changeUser = new USERChange();
            changeUser.UserName = user.UserName;
            changeUser.Address.City = address.City;
            changeUser.Address.Street = address.Street;
            changeUser.Address.HouseNo = address.Number;
            changeUser.Address.PostlCod1 = address.Postcode;

            try
            {
                USERChangeResponse response = _sapConnection.USERChange(changeUser);
            }
            catch (System.ServiceModel.FaultException e)
            {
                return new SaveResponse(false, e.Message);
            }
            catch (System.ServiceModel.CommunicationException e)
            {
                return new SaveResponse(false, e.Message);
            }
            return new SaveResponse(true, String.Format("User {0} erfolgreich geändert", user.UserName));
        }

        public SaveResponse LockUser(string user)
        {
            USERLock lockUser = new USERLock() { UserName = user};

            try
            {
                USERLockResponse lockResponse = _sapConnection.USERLock(lockUser);
            }
            catch (System.ServiceModel.FaultException e)
            {
                return new SaveResponse(false, e.Message);
            }
            catch (System.ServiceModel.CommunicationException e)
            {
                return new SaveResponse(false, e.Message);
            }
            return new SaveResponse(true, String.Format("User {0} erfolgreich gesperrt", user));
        }

        public SaveResponse UnlockUser(string user)
        {
            USERUnlock unlockUser = new USERUnlock() {UserName = user};

            try
            {
                USERUnlockResponse unlockResponse = _sapConnection.USERUnlock(unlockUser);
            }
            catch (System.ServiceModel.FaultException e)
            {
                return new SaveResponse(false, e.Message);
            }
            catch (System.ServiceModel.CommunicationException e)
            {
                return new SaveResponse(false, e.Message);
            }
            return new SaveResponse(true, String.Format("User {0} erfolgreich ensperrt", user));
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
