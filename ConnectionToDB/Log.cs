using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionToDB
{
    public static class Loger
    {
        public static void DisplayDataTable(DataTable dt)
        {
            if (!(dt is null))
            {
                foreach (DataColumn col in dt.Columns)
                {
                    Console.Write($"{col.ColumnName}\t");
                }
                Console.WriteLine();

                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.Write($"{item.ToString()}\t");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Table not exsist!");
            }
        }
    }
}
