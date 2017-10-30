using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAendern.SAP
{
    public class SapCommunicationException : Exception
    {
        public SapCommunicationException()
        {
        }

        public SapCommunicationException(string message)
            : base(message)
        {
        }

        public SapCommunicationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
