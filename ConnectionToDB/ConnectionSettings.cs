using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace ConnectionToDB
{
    public class ConnectionSettings
    {
        public ConnectionSettings(bool hasPassword)
        {
            b_hasPassword = hasPassword;
        }

        private bool b_hasPassword = false;
        public bool HasPassword {
            get
            {
                return b_hasPassword;
            }
            set
            {
                b_hasPassword = value;
            }
        }

        private string m_user = "";
        public string UserID
        {
            get { return m_user; }
            set
            {
                if (value != "")
                {
                    m_user = InputNickname(value);
                }
            }

        }

        private string m_password = "";
        public string Password
        {
            get { return m_password; }
            set
            {
                if (value != "")
                {
                    m_password = InputPassword(value);
                }
            }
        }

        private string m_server = "";
        public string Server
        {
            get { return m_server; }
            set
            {
                if (value != "")
                {
                    m_server = InputServer(@value);
                }
            }
        }

        private string m_cluster = "";
        public string Cluster
        {
            get { return m_cluster; }
            set
            {
                if (value != "")
                {
                    m_cluster = InputNickname(value);
                }
            }
        }

        private string m_port = "";
        public string Port
        {
            get { return m_port; }
            set
            {
                if (value != "")
                {
                    m_port = InputPort(value);
                }
            }
        }

        private string m_IS = "";
        public string IntegratedSecurity
        {
            get { return m_IS; }
            set
            {
                if (value != "")
                {
                    m_IS = inputIS(value);
                }
            }
        }

        private static string InputPassword(string password)
        {
            if (password == "")
            {
                Console.WriteLine("Wrong password type!");
                return InputPassword(Console.ReadLine());
            }
            else
            {
                return password;
            }
        }

        private static string InputPort(string @port)
        {
            Regex portReg = new Regex(@"\b\d{2-4}\b");
            MatchCollection ports = portReg.Matches(port);

            if (ports.Count > 1 || ports.Count == 0)
            {
                Console.WriteLine("Wrong type of port!");
                return InputPort(Console.ReadLine());
            }
            else
            {
                return port;
            }
        }

        private static string inputIS(string IS)
        {
            if ((IS == "True") || (IS == "False"))
            {
                return IS;
            }
            else
            {
                Console.WriteLine("Input has to be: True/False");
                return inputIS(Console.ReadLine());
            }

        }

        private static string InputServer(string @ipText)
        {
            if (ipText == "localhost")
                return ipText;

            Regex ip = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            MatchCollection result = ip.Matches(@ipText);

            if (result.Count > 1 || result.Count == 0)
            {
                Console.WriteLine("Wrong IP type!");
                return InputServer(@Console.ReadLine());
            }
            else
            {
                return result[0].Value;
            }
        }

        private static string InputNickname(string @nickname)
        {
            Regex nicknameReg = new Regex(@"^[^0-9]\w+$");
            MatchCollection result = nicknameReg.Matches(@nickname);

            if (result.Count > 1 || result.Count == 0)
            {
                Console.WriteLine("Wrong Nickname type!");
                return InputServer(@Console.ReadLine());
            }
            else
            {
                return result[0].Value;
            }
        }

        public static explicit operator string(ConnectionSettings setts)
        {
            if (!setts.HasPassword)
            {
                return $"Data Source={setts.Server};Initial Catalog={setts.Cluster};Integrated Security={setts.IntegratedSecurity}";
            }
            else
            {
                return $"Data Source = {setts.Server}; Initial Catalog = {setts.Cluster}; Integrated Security = {setts.IntegratedSecurity};" +
                       $"User ID = {setts.UserID}; Password = {setts.Password}";
            }
        }

    }
}
