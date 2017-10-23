using System;
using SAP.Middleware.Connector;

namespace UserAendern.SAP
{
    class SapDestinationConfig : IDestinationConfiguration
    {
        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        public bool ChangeEventsSupported()
        {
            throw new NotImplementedException();
        }

        public RfcConfigParameters GetParameters(string destinationName)
        {
            RfcConfigParameters param = new RfcConfigParameters();
            param.Add(RfcConfigParameters.Name, "QA");
            param.Add(RfcConfigParameters.Client, "902");
            param.Add(RfcConfigParameters.User, "IDES-001");
            param.Add(RfcConfigParameters.Password, "IDES-001?");
            param.Add(RfcConfigParameters.Language, "DE");
            param.Add(RfcConfigParameters.AppServerHost, "/H/proxy.hof-university.de/S/3299/H/saprouter.hcc.in.tum.de/S/3299/H/i48lp1");
            param.Add(RfcConfigParameters.SystemNumber, "48");
            param.Add(RfcConfigParameters.SystemID, "I48");
            return param;
        }
    }
}
