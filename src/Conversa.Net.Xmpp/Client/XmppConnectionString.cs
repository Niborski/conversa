// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

using Conversa.Net.Xmpp.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Conversa.Net.Xmpp.Client
{
    /// <summary>
    /// Represents a Connection String
    /// </summary>
    public sealed class XmppConnectionString
    {
        /// <summary>
        /// Synonyms list
        /// </summary>
        static Dictionary<string, string> Synonyms = GetSynonyms();

        /// <summary>
        /// Gets a value indicating whether the given key value is a synonym
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsSynonym(string key)
        {
            return Synonyms.ContainsKey(key);
        }

        /// <summary>
        /// Gets the synonym for the give key value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSynonym(string key)
        {
            return Synonyms[key];
        }

        /// <summary>
        /// Gets the synonyms.
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> GetSynonyms()
        {
            var synonyms = new Dictionary<string, string>();

            synonyms.Add(XmppConnectionStringSynonyms.Server
                       , XmppConnectionStringKeywords.Server);

            synonyms.Add(XmppConnectionStringSynonyms.ServiceName
                       , XmppConnectionStringKeywords.ServiceName);

            synonyms.Add(XmppConnectionStringSynonyms.UserId
                       , XmppConnectionStringKeywords.UserId);

            synonyms.Add(XmppConnectionStringSynonyms.UserAddress
                       , XmppConnectionStringKeywords.UserId);

            synonyms.Add(XmppConnectionStringSynonyms.UID
                       , XmppConnectionStringKeywords.UserId);

            synonyms.Add(XmppConnectionStringSynonyms.UserPassword
                       , XmppConnectionStringKeywords.UserPassword);

            synonyms.Add(XmppConnectionStringSynonyms.Resource
                       , XmppConnectionStringKeywords.Resource);

            synonyms.Add(XmppConnectionStringSynonyms.ConnectionTimeout
                       , XmppConnectionStringKeywords.ConnectionTimeout);

            synonyms.Add(XmppConnectionStringSynonyms.UseProxy
                       , XmppConnectionStringKeywords.UseProxy);

            synonyms.Add(XmppConnectionStringSynonyms.ProxyType
                       , XmppConnectionStringKeywords.ProxyType);

            synonyms.Add(XmppConnectionStringSynonyms.ProxyServer
                       , XmppConnectionStringKeywords.ProxyServer);

            synonyms.Add(XmppConnectionStringSynonyms.ProxyPortNumber
                       , XmppConnectionStringKeywords.ProxyPortNumber);

            synonyms.Add(XmppConnectionStringSynonyms.ProxyUserName
                       , XmppConnectionStringKeywords.ProxyUserName);

            synonyms.Add(XmppConnectionStringSynonyms.ProxyPassword
                       , XmppConnectionStringKeywords.ProxyPassword);

            synonyms.Add(XmppConnectionStringSynonyms.HttpBinding
                       , XmppConnectionStringKeywords.HttpBinding);

            synonyms.Add(XmppConnectionStringSynonyms.ResolveHostName
                       , XmppConnectionStringKeywords.ResolveHostName);

            synonyms.Add(XmppConnectionStringSynonyms.PacketSize
                       , XmppConnectionStringKeywords.PacketSize);

            return synonyms;
        }

        private Dictionary<string, object> options;

        /// <summary>
        /// Gets the login server.
        /// </summary>
        /// <value>The server.</value>
        public string HostName
        {
            get { return this.GetString(XmppConnectionStringKeywords.Server); }
        }

        /// <summary>
        /// Gets a value indicating whether to resolve host names.
        /// </summary>
        /// <value>
        ///   <c>true</c> if host name should be resolved; otherwise, <c>false</c>.
        /// </value>
        public bool ResolveHostName
        {
            get { return this.GetBoolean(XmppConnectionStringKeywords.ResolveHostName); }
        }

        /// <summary>
        /// Gets the service name.
        /// </summary>
        /// <value>The port.</value>
        public string ServiceName
        {
            get { return this.GetString(XmppConnectionStringKeywords.ServiceName); }
        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public XmppAddress UserAddress
        {
            get { return this.GetString(XmppConnectionStringKeywords.UserId); }
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>The password.</value>
        public string UserPassword
        {
            get { return this.GetString(XmppConnectionStringKeywords.UserPassword); }
        }

        /// <summary>
        /// Gets or sets the user XMPP resource
        /// </summary>
        public string Resource
        {
            get { return this.GetString(XmppConnectionStringKeywords.Resource); }
        }

        /// <summary>
        /// Gets the connection timeout.
        /// </summary>
        /// <value>The connection timeout.</value>
        public int ConnectionTimeout
        {
            get { return this.GetInt32(XmppConnectionStringKeywords.ConnectionTimeout); }
        }

        /// <summary>
        /// Gets a value that indicating whether the connection should be done throught a proxy
        /// </summary>
        public bool UseProxy
        {
            get { return this.GetBoolean(XmppConnectionStringKeywords.UseProxy); }
        }

        /// <summary>
        /// Gets the proxy type
        /// </summary>
        public string ProxyType
        {
            get { return this.GetString(XmppConnectionStringKeywords.ProxyType); }
        }

        /// <summary>
        /// Gets the proxy server
        /// </summary>
        public string ProxyServer
        {
            get { return this.GetString(XmppConnectionStringKeywords.ProxyServer); }
        }

        /// <summary>
        /// Gets the proxy port number
        /// </summary>
        public int ProxyPortNumber
        {
            get { return this.GetInt32(XmppConnectionStringKeywords.ProxyPortNumber); }
        }

        /// <summary>
        /// Gets the proxy user name
        /// </summary>
        public string ProxyUserName
        {
            get { return this.GetString(XmppConnectionStringKeywords.ProxyUserName); }
        }

        /// <summary>
        /// Gets the proxy password
        /// </summary>
        public string ProxyPassword
        {
            get { return this.GetString(XmppConnectionStringKeywords.ProxyPassword); }
        }

        /// <summary>
        /// Gets a value that indicates if http binding should be used
        /// </summary>
        public bool UseHttpBinding
        {
            get { return this.GetBoolean(XmppConnectionStringKeywords.HttpBinding); }
        }

        /// <summary>
        /// Gets the buffer packet size
        /// </summary>
        public uint PacketSize
        {
            get { return this.GetUInt32(XmppConnectionStringKeywords.PacketSize); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:XmppConnectionString"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public XmppConnectionString(string connectionString)
        {
            this.options = new Dictionary<string, object>();
            this.Load(connectionString);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            var cs = new StringBuilder();
            var e  = this.options.GetEnumerator();

            while (e.MoveNext())
            {
                if (e.Current.Value != null)
                {
                    if (cs.Length > 0)
                    {
                        cs.Append(";");
                    }

                    cs.AppendFormat(CultureInfo.CurrentUICulture, "{0}={1}", e.Current.Key, e.Current.Value);
                }
            }

            return cs.ToString();
        }

        private void SetDefaultOptions()
        {
            this.options.Clear();

            this.options.Add(XmppConnectionStringKeywords.Server            , null);
            this.options.Add(XmppConnectionStringKeywords.ServiceName       , "5222");
            this.options.Add(XmppConnectionStringKeywords.UserId            , null);
            this.options.Add(XmppConnectionStringKeywords.UserPassword      , null);
            this.options.Add(XmppConnectionStringKeywords.Resource          , null);
            this.options.Add(XmppConnectionStringKeywords.ConnectionTimeout , -1);
            this.options.Add(XmppConnectionStringKeywords.PacketSize        , 4096);
        }

        private void Load(string connectionString)
        {
            string[] keyPairs = connectionString.Split(';');

            this.SetDefaultOptions();

            foreach (string keyPair in keyPairs)
            {
                string[] values = keyPair.Split('=');

                if (values.Length == 2
                 && !String.IsNullOrEmpty(values[0])
                 && !String.IsNullOrEmpty(values[1]))
                {
                    values[0] = values[0].ToLower();

                    if (Synonyms.ContainsKey(values[0]))
                    {
                        this.options[(string)Synonyms[values[0]]] = values[1].Trim();
                    }
                }
            }

            this.Validate();
        }

        private void Validate()
        {
            if (String.IsNullOrEmpty(this.HostName)
             || String.IsNullOrEmpty(this.UserAddress)
             || String.IsNullOrEmpty(this.UserAddress.UserName)
             || String.IsNullOrEmpty(this.UserAddress.DomainName)
             || String.IsNullOrEmpty(this.UserPassword))
            {
                throw new XmppException("Invalid connection string options.");
            }
        }

        private string GetString(string key)
        {
            if (this.options.ContainsKey(key))
            {
                return (string)this.options[key];
            }

            return null;
        }

        private int GetInt32(string key)
        {
            if (this.options.ContainsKey(key))
            {
                return Convert.ToInt32(this.options[key], CultureInfo.CurrentUICulture);
            }

            return 0;
        }

        private uint GetUInt32(string key)
        {
            if (this.options.ContainsKey(key))
            {
                return Convert.ToUInt32(this.options[key], CultureInfo.CurrentUICulture);
            }

            return 0;
        }

        private bool GetBoolean(string key)
        {
            if (this.options.ContainsKey(key))
            {
                return Convert.ToBoolean(this.options[key], CultureInfo.CurrentUICulture);
            }

            return false;
        }
    }
}
