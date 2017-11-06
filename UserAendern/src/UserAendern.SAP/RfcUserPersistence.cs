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

        private static readonly RfcDestination Dest = RfcDestinationManager.GetDestination(new SapDestinationConfig().GetParameters(null));
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
            IRfcFunction fun;

            try
            {
                RfcRepository repository = Dest.Repository;
                fun = repository.CreateFunction("BAPI_USER_GET_DETAIL");
                fun.SetValue("USERNAME", username);
                fun.Invoke(Dest);
            }
            catch (RfcCommunicationException ex)
            {
                throw new SapCommunicationException("Keine Verbindung zum SAP-Server", ex);
            }

            IRfcStructure address = fun.GetStructure("ADDRESS");

            string street = address.GetString("STREET");
            string number = address.GetString("HOUSE_NO");
            string postcode = address.GetString("POSTL_COD1");
            string city = address.GetString("CITY");

            return new Address(street,number, postcode, city);
        }

        public UserDetails GetUserDetail(string username)
        {
            IRfcFunction fun;

            try
            {
                RfcRepository repository = Dest.Repository;
                fun = repository.CreateFunction("BAPI_USER_GET_DETAIL");

                fun.SetValue("USERNAME", username);
                fun.Invoke(Dest);
            }
            catch (RfcCommunicationException ex)
            {
                throw new SapCommunicationException("Keine Verbindung zum SAP-Server", ex);
            }

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

                return new UserDetails(firstname, lastname, new Address(street, number, postcode, city),
                    new Lock(wrongLogOn, noPAssword, localLock, globalLock));

        }

        public IEnumerable<User> GetUsers
        {
            get
            {
                IRfcFunction fun;

                try
                {
                    RfcRepository repository = Dest.Repository;
                    fun = repository.CreateFunction("BAPI_USER_GETLIST");
                    fun.Invoke(Dest);
                }
                catch (RfcCommunicationException ex)
                {
                    throw new SapCommunicationException("Keine Verbindung zum SAP-Server", ex);
                }

                IRfcTable table = fun.GetTable("USERLIST");

                List<User> users = new List<User>();
                foreach (var row in table)
                {

                    users.Add(FromRow(row));
                }

                return users;
            }
        }

        public BapiReturn CreateUser(User user, string password)
        {
            IRfcFunction fun;
            RfcRepository repository;

            if (password.Trim() == "")
            {
                throw new SapCommunicationException("Bitte geben sie ein Passowrt ein!");
            }

            try
            {
                repository = Dest.Repository;
                fun = repository.CreateFunction("BAPI_USER_CREATE1");
                var address = repository.GetStructureMetadata("BAPIADDR3").CreateStructure();
                var logond = repository.GetStructureMetadata("BAPILOGOND").CreateStructure();
                var bapipassword = repository.GetStructureMetadata("BAPIPWD").CreateStructure();

                address.SetValue("LASTNAME", user.LastName);
                bapipassword.SetValue("BAPIPWD", password);

                fun.SetValue("USERNAME", user.UserName);
                fun.SetValue("ADDRESS", address);
                fun.SetValue("PASSWORD", bapipassword);
                fun.SetValue("LOGONDATA", logond);
                fun.Invoke(Dest);
            }
            catch (RfcCommunicationException ex)
            {
                throw new SapCommunicationException("Keine Verbindung zum SAP-Server", ex);
            }

            IRfcTable returnTable = fun.GetTable("RETURN");
            var firstreturn = returnTable[0];

            var response = new BapiReturn(FromSapReturn(firstreturn.GetString("TYPE")), firstreturn.GetString("MESSAGE"));

            try
            {
                if (response.Type.Equals(BapiReturnType.Error))
                {
                    RollbackTransaction(repository);
                }
                else
                {
                    CommitTransaction(repository);
                }
            }
            catch (RfcCommunicationException ex)
            {
                throw new SapCommunicationException("Keine Verbindung zum SAP-Server", ex);
            }

            return response;
        }

        public BapiReturn DeleteUser(string user)
        {
            RfcRepository repository;
            IRfcFunction fun; 

            try
            {
                repository = Dest.Repository;
                fun = repository.CreateFunction("BAPI_USER_DELETE");
                fun.SetValue("USERNAME", user);
                fun.Invoke(Dest);
            }
            catch (RfcCommunicationException ex)
            {
                throw new SapCommunicationException("Keine Verbindung zum SAP-Server", ex);
            }

            IRfcTable returnTable = fun.GetTable("RETURN");
            var firstreturn = returnTable[0];
            var response = new BapiReturn(FromSapReturn(firstreturn.GetString("TYPE")), firstreturn.GetString("MESSAGE"));

            try
            {
                if (response.Type.Equals(BapiReturnType.Error))
                {
                    RollbackTransaction(repository);
                }
                else
                {
                    CommitTransaction(repository);
                }
            }
            catch (RfcCommunicationException ex)
            {
                throw new SapCommunicationException("Keine Verbindung zum SAP-Server", ex);
            }
            return response;
        }

        public BapiReturn ChangeUser(User user, UserDetails userDetails)
        {
            IRfcFunction fun;
            RfcRepository repository;

            try
            {
                repository= Dest.Repository;

                var address = repository.GetStructureMetadata("BAPIADDR3").CreateStructure();
                var addressx = repository.GetStructureMetadata("BAPIADDR3X").CreateStructure();

                fun = repository.CreateFunction("BAPI_USER_CHANGE");

                fun.SetValue("USERNAME", user.UserName);

                address.SetValue("STREET", userDetails.Address.Street);
                address.SetValue("HOUSE_NO", userDetails.Address.Number);
                address.SetValue("POSTL_COD1", userDetails.Address.Postcode);
                address.SetValue("CITY", userDetails.Address.City);
                address.SetValue("FIRSTNAME", userDetails.Firstname);
                address.SetValue("LASTNAME", userDetails.Lastname);


                addressx.SetValue("STREET", "X");
                addressx.SetValue("HOUSE_NO", "X");
                addressx.SetValue("POSTL_COD1", "X");
                addressx.SetValue("CITY", "X");
                addressx.SetValue("FIRSTNAME", "X");
                addressx.SetValue("LASTNAME", "X");

                fun.SetValue("ADDRESS", address);
                fun.SetValue("ADDRESSX", addressx);


                fun.Invoke(Dest);
            }
            catch (RfcCommunicationException ex)
            {
                throw new SapCommunicationException("Keine Verbindung zum SAP-Server", ex);
            }

            IRfcTable returnTable = fun.GetTable("RETURN");

            var firstreturn = returnTable[0];

            var response = new BapiReturn(FromSapReturn(firstreturn.GetString("TYPE")), firstreturn.GetString("MESSAGE"));


            try { 
                if (response.Type.Equals(BapiReturnType.Error))
                {
                    RollbackTransaction(repository);
                }
                else
                {
                    CommitTransaction(repository);
                }
            }
            catch (RfcCommunicationException ex)
            {
                throw new SapCommunicationException("Keine Verbindung zum SAP-Server", ex);
            }
            return response;
        }

        public BapiReturn LockUser(string user)
        {
            IRfcFunction fun;
            try
            {
                RfcRepository repository = Dest.Repository;
                fun = repository.CreateFunction("BAPI_USER_LOCK");
                fun.SetValue("USERNAME", user);
                fun.Invoke(Dest);
            }
            catch (RfcCommunicationException ex)
            {
                throw new SapCommunicationException("Keine Verbindung zum SAP-Server", ex);
            }

            IRfcTable returnTable = fun.GetTable("RETURN");
            var firstreturn = returnTable[0];

            return new BapiReturn(FromSapReturn(firstreturn.GetString("TYPE")), firstreturn.GetString("MESSAGE"));
        }

        public BapiReturn UnlockUser(string user)
        {
            IRfcFunction fun;
            try
            {
                RfcRepository repository = Dest.Repository;
                fun = repository.CreateFunction("BAPI_USER_UNLOCK");
                fun.SetValue("USERNAME", user);
                fun.Invoke(Dest);
            }
            catch (RfcCommunicationException ex)
            {
                throw new SapCommunicationException("Keine Verbindung zum SAP-Server", ex);
            }

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

        private void RollbackTransaction(RfcRepository repository)
        {
            repository.CreateFunction("BAPI_TRANSACTION_ROLLBACK").Invoke(Dest);
        }

        private void CommitTransaction(RfcRepository repository)
        {
            repository.CreateFunction("BAPI_TRANSACTION_COMMIT").Invoke(Dest);
        }
    }
}