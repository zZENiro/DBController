using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionToDB
{
    public static class SettingsApp
    {
        private static List<string> l_connectionStrs = new List<string>();
        public static List<string> ConnectionStrings
        {
            get
            {
                return l_connectionStrs;
            }
            private set
            {
                l_connectionStrs = value;
            }
        }

        public static string Start()
        {
            Console.WriteLine($"Settings interface: \n- \"/add\"\n- \"/choose\"\n- \"/exit\"\n");

            return exec();
        }

        private static string exec()
        {
            Console.Write("settings> ");
            switch (Console.ReadLine())
            {
                case "/add":
                    {
                        Console.WriteLine("Input variables: ");
                        Console.Write("settings/Password?(Y/N)> ");
                        string YN = inputYN(Console.ReadLine());

                        ConnectionSettings connectionSettings;
                        if (YN == "N")
                        {
                            connectionSettings = new ConnectionSettings(false);
                            Console.Write("settings/Server> ");
                            connectionSettings.Server = Console.ReadLine();

                            Console.Write("settings/Cluster> ");
                            connectionSettings.Cluster = Console.ReadLine();

                            Console.Write("settings/Integrated Security> ");
                            connectionSettings.IntegratedSecurity = Console.ReadLine();

                            Console.WriteLine("settings/Done!");
                        }
                        else
                        {
                            connectionSettings = new ConnectionSettings(true);
                            Console.Write("settings/Server> ");
                            connectionSettings.Server = Console.ReadLine();

                            Console.Write("settings/Cluster> ");
                            connectionSettings.Cluster = Console.ReadLine();

                            Console.Write("settings/Integrated Security> ");
                            connectionSettings.IntegratedSecurity = Console.ReadLine();

                            Console.Write("settings/User Name> ");
                            connectionSettings.UserID = Console.ReadLine();

                            Console.Write("settings/Password> ");
                            connectionSettings.Password = Console.ReadLine();
                            Console.WriteLine("settings/Done!");
                        }

                        l_connectionStrs.Add((string)connectionSettings);

                        return exec();
                    }
                case "/choose":
                    {
                        Console.Write("settings/choose> ");
                        Console.WriteLine("Choose connection string: ");
                        for (int i = 0; i < l_connectionStrs.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}: {l_connectionStrs[i]}");
                        }

                        return inputIndex(Console.ReadLine());
                    }
                case "/exit":
                    {
                        return "0";
                    }
                default:
                    Console.WriteLine("Wrong command!");
                    return exec();
            }
        }

        private static string inputIndex(string index)
        {
            if (index == "/exit")
            {
                return exec();
            }
            int _index = -1;
            bool succes = int.TryParse(index, out _index);

            if (succes && _index != -1)
            {
                if (_index <= l_connectionStrs.Count && !(_index <= 0))
                    return l_connectionStrs[_index - 1];
                else
                {
                    Console.Write("settings/choose> ");
                    Console.WriteLine("Not exsit! Input another index.");
                    Console.Write("settings/choose> ");
                    return inputIndex(Console.ReadLine());
                }
            }
            else
            {
                Console.Write("settings/choose> ");
                Console.WriteLine("Wrong index! Input another index.");
                Console.Write("settings/choose> ");
                return inputIndex(Console.ReadLine());
            }
        }

        private static string inputYN(string YN)
        {
            if (!(YN == "Y" || YN == "N"))
            {
                Console.WriteLine("Wrong type");
                return inputYN(Console.ReadLine());
            }
            else
            {
                return YN;
            }
        }
    }
}
