using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP.Middleware.Connector;
using UserAendern.Domain;

namespace UserAendern.SAP
{
    public class RfcUserPersistence : ILoadUser, ISaveUser
    {
        private bool FromSap(string sapBool)
        {
            if (sapBool == "X")
            {
                return true;
            }
            return false;
        }

        public Address GetUserAddress(string username)
        {

            RfcDestination dest = RfcDestinationManager.GetDestination(new SapDestinationConfig().GetParameters(null));
            RfcRepository rep = dest.Repository;
            var fun = rep.CreateFunction("BAPI_USER_GET_DETAIL");
            fun.SetValue("USERNAME", username);
            fun.Invoke(dest);
            IRfcStructure address = fun.GetStructure("ADDRESS");

            string street = address.GetString("STREET");
            string number = address.GetString("HOUSE_NO");
            string postcode = address.GetString("POSTL_COD1");
            string city = address.GetString("CITY");

            return new Address(street,number, postcode, city);
        }

        public UserDetails GetUserDetail(string username)
        {
            RfcDestination dest = RfcDestinationManager.GetDestination(new SapDestinationConfig().GetParameters(null));
            RfcRepository rep = dest.Repository;
            var fun = rep.CreateFunction("BAPI_USER_GET_DETAIL");
            
            fun.SetValue("USERNAME", username);
            fun.Invoke(dest);

            IRfcStructure address = fun.GetStructure("ADDRESS");

            string street = address.GetString("STREET");
            string number = address.GetString("HOUSE_NO");
            string postcode = address.GetString("POSTL_COD1");
            string city = address.GetString("CITY");
            string firstname = address.GetString("FIRSTNAME");
            string lastname = address.GetString("LASTNAME");

            IRfcStructure locked = fun.GetStructure("ISLOCKED");

            bool wrongLogOn = FromSap(locked.GetString("WRNG_LOGON"));
            bool noPAssword = FromSap(locked.GetString("NO_USER_PW"));
            bool localLock = FromSap(locked.GetString("LOCAL_LOCK"));
            bool globalLock = FromSap(locked.GetString("GLOB_LOCK"));

            return new UserDetails(firstname, lastname,new Address(street, number, postcode, city),
                new Lock(wrongLogOn, noPAssword, localLock, globalLock));
        }

        public IEnumerable<User> GetUsers
        {
            get
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(new SapDestinationConfig().GetParameters(null));
                RfcRepository rep = dest.Repository;
                IRfcFunction fun = rep.CreateFunction("BAPI_USER_GETLIST");
                //fun.SetValue("MAX_ROWS", "50");
                fun.Invoke(dest);
                IRfcTable table = fun.GetTable("USERLIST");

                List<User> users = new List<User>();
                foreach (var row in table)
                {

                    users.Add(FromRow(row));
                }

                return users;
            }
        }

        public BapiReturn CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public BapiReturn DeleteUser(string user)
        {
            RfcDestination dest = RfcDestinationManager.GetDestination(new SapDestinationConfig().GetParameters(null));
            RfcRepository rep = dest.Repository;
            var fun = rep.CreateFunction("BAPI_USER_DELETE");
            fun.SetValue("USERNAME", user);
            fun.Invoke(dest);

            IRfcTable returnTable = fun.GetTable("RETURN");
            var firstreturn = returnTable[0];

            return new BapiReturn(FromSapReturn(firstreturn.GetString("TYPE")), firstreturn.GetString("MESSAGE"));
        }

        public BapiReturn ChangeUser(User user, UserDetails userDetails)
        {
            RfcDestination dest = RfcDestinationManager.GetDestination(new SapDestinationConfig().GetParameters(null));
            RfcRepository rep = dest.Repository;
            var fun = rep.CreateFunction("BAPI_USER_CHANGE");
            fun.SetValue("USERNAME", user.UserName);
            //fun.SetValue("ADDRESSX", "X");

            //TODO set Address
          
            fun.Invoke(dest);

            IRfcTable returnTable = fun.GetTable("RETURN");
            var firstreturn = returnTable[0];

            return new BapiReturn(FromSapReturn(firstreturn.GetString("TYPE")), firstreturn.GetString("MESSAGE"));
        }

        public BapiReturn LockUser(string user)
        {
            RfcDestination dest = RfcDestinationManager.GetDestination(new SapDestinationConfig().GetParameters(null));
            RfcRepository rep = dest.Repository;
            var fun = rep.CreateFunction("BAPI_USER_LOCK");
            fun.SetValue("USERNAME", user);
            fun.Invoke(dest);
            IRfcTable returnTable = fun.GetTable("RETURN");
            var firstreturn = returnTable[0];

            return new BapiReturn(FromSapReturn(firstreturn.GetString("TYPE")), firstreturn.GetString("MESSAGE"));
        }

        public BapiReturn UnlockUser(string user)
        {
            RfcDestination dest = RfcDestinationManager.GetDestination(new SapDestinationConfig().GetParameters(null));
            RfcRepository rep = dest.Repository;
            var fun = rep.CreateFunction("BAPI_USER_UNLOCK");
            fun.SetValue("USERNAME", user);
            fun.Invoke(dest);

            IRfcTable returnTable = fun.GetTable("RETURN");
            var firstreturn = returnTable[0];

            return new BapiReturn(FromSapReturn(firstreturn.GetString("TYPE")), firstreturn.GetString("MESSAGE"));
        }

        private User FromRow(IRfcStructure row)
        {
            return new User(row.GetString("FIRSTNAME"), row.GetString("LASTNAME"), row.GetString("FULLNAME"), row.GetString("USERNAME"));
        }

        private BapiReturnType FromSapReturn(string returnType)
        {
            switch (returnType)
            {
                case "S":
                    return BapiReturnType.Success;
                case "W":
                    return BapiReturnType.Warning;
                case "E":
                    return BapiReturnType.Error;
                case "I":
                    return BapiReturnType.Info;
                case "A":
                    return BapiReturnType.Abort;
            }
            return BapiReturnType.Info;
        }
    }
}
