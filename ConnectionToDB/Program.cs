using System;
using System.Data;

namespace ConnectionToDB
{
    public class Program
    {
        private static DataSet _currState = null; // локальное представление запроса из бд
        private static string _connectionStr = "";
        private static string _message = "Tool for Databases. Created by zZen :)";

        static int Main(string[] args)
        {
            GreetingsHandler.RectangleGreetings(_message);
            Console.WriteLine("\nSelect \"/help\" to watch commands.");

            return exec();
        }

        private static int exec()
        {
            Console.Write("dbmanager> ");
            switch (Console.ReadLine())
            {
                case "/getxml":
                    {
                        if (_connectionStr == "")
                        {
                            Console.WriteLine("dbmanager> You are didn't selected connection. Use /settings");
                            return exec();
                        }
                        else
                        {
                            Console.Write("dbmanager/getxml> Path of XML file: ");
                            XmlTranslator.Path = @Console.ReadLine();

                            Console.Write("dbmanager/getxml> Query to database: ");
                            XmlTranslator.Query = @Console.ReadLine();
                            XmlTranslator.ConnectionString = _connectionStr;

                            Console.WriteLine((XmlTranslator.GetXml()) ? $"Xml file saved in {XmlTranslator.Path}" : "File saving failed");

                            return exec();
                        }
                    }

                case "/vt":
                    {
                        Console.WriteLine("dbmanager> ");
                        Loger.DisplayDataTable((_currState is null) ? null : _currState.Tables[0]);

                        return exec();
                    }

                case "/help":
                    {
                        Console.WriteLine($"\n- Type query               (/query)" +
                                          $"\n- View table               (/vt)" +
                                          $"\n- Settings                 (/settings)" +
                                          $"\n- Clear console            (/clear)" +
                                          $"\n- Exit application         (/exit)" +
                                          $"\n- Execute stored procedure (/storedproc)" +
                                          $"\n- Save table to XML file   (/getxml)" +
                                          $"\n- ...\n");

                        return exec();
                    }

                case "/settings":
                    {
                        string _result = SettingsApp.Start();

                        if (_result == "0")
                        {
                            return exec();
                        }
                        else
                        {
                            _connectionStr = _result;
                            return exec();
                        }
                    }

                case "/query":
                    {
                        Console.Write("dbmanager> Input query: ");
                        ConnectionManager.Query = Console.ReadLine();
                        ConnectionManager.ConnectionString = _connectionStr;
                        _currState = ConnectionManager.Connect(ConnectionManager.SqlTypeConnection.MSSql);

                        if (_currState.Tables.Count == 0)
                        {
                            return exec();
                        }

                        DataTable db = _currState.Tables[0];
                        Loger.DisplayDataTable(db);
                        return exec();
                    }

                case "/storedproc":
                    {
                        Console.Write("dbmanager/storedprocedure> Input name of stored procedure: ");
                        ConnectionManager.Query = Console.ReadLine();
                        ConnectionManager.ConnectionString = _connectionStr;
                        try
                        {
                            DataSet _ds = ConnectionManager.EmitStoredProcedure();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Неудача D:\nСкорее всего не инициализирована строка подключения. Используйте /settings");
                        }
                        return exec();
                    }

                case "/clear":
                    {
                        Console.Clear();
                        return exec();
                    }

                case "/exit":
                    {
                        return 0;
                    }

                default:
                    {
                        Console.Write("dbmanager> ");
                        Console.WriteLine(@"This command not exist! Type /help");
                        return exec();
                    }

            }
        }
    }
}
