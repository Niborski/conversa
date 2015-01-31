using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversa.Net.Xmpp.Core
{
    /// <summary>
    /// Interface for XMPP Stanzas
    /// </summary>
    public interface IStanza
    {
        /// <summary>
        /// Gets or sets the stanza ID
        /// </summary>
        string Id 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets a value indicating wheter the current instance is a error stanza
        /// </summary>
        bool IsError
        {
            get;
        }
    }
}
