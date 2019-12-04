using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionToDB
{
    public static class GreetingsHandler
    {
        public static void RectangleGreetings(string _message)
        {
            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < _message.Length + 2; ++j)
                {
                    if (i == 0 || i == 4)
                        Console.Write("*");

                    if ((i == 1 || i == 2 || i == 3) && (j == 0))
                    {
                        Console.Write("*");
                    }

                    if (i == 1 || i == 3)
                    {
                        if (j == _message.Length)
                            Console.Write("*");
                        else
                            Console.Write(" ");
                    }

                    if (i == 2)
                    {
                        if (j == _message.Length)
                            Console.Write("*");
                        if (j >= 0 && j < _message.Length)
                            Console.Write(_message[j]);

                    }
                }
                Console.WriteLine("");
            }
        }

    }
}
