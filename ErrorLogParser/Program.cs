using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = args[0];
            string output = args[1];
            List<string> errors = new List<string>();

            for (int fileNumber = 52; fileNumber < 80; fileNumber++)
            {
                string[] log = System.IO.File.ReadAllLines(file + fileNumber.ToString());

                for (int i = 3; i < log.Length; i += 8)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(log[i], "mobile", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    {
                        string error = log[i + 2] + " " + log[i + 3];
                        int counter = 0;

                        foreach (string e in errors)
                        {
                            if (e == error)
                            {
                                counter++;
                            }
                        }

                        if (counter == 0)
                        {
                            errors.Add(error);
                            using (System.IO.StreamWriter result = new System.IO.StreamWriter(@file + ".parsed", true))
                            {
                                result.WriteLineAsync(error);
                            }
                        }
                    }

                }
            }

            Console.WriteLine("Success");
            Console.ReadKey();
        }
    }
}
