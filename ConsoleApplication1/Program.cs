using MKSimControllerApiTest.Controller;
using System;
using System.Threading;
using ConsoleApplication5.Util;

using static ConsoleApplication5.Sorting.Sensor;
using static ConsoleApplication5.Sorting.Output;
using System.IO;
using ConsoleApplication1.Language;

namespace MKSimControllerApiTest
{
    class Program
    {
        public const int ESTEIRA_ENTRADA_1 = 2 * 2 * 2 * 2;
        public const int ESTEIRA_ENTRADA_2 = ESTEIRA_ENTRADA_1 * 2;

        static void Main(string[] args)
        {
            bool error = false;

            var plc = new PLC();


            var lines = File.ReadAllLines("script.txt");

            foreach (var line in lines)
            {
                if (!Parser.Parse(line)(plc))
                {
                    Console.WriteLine("ERROR: " + line);
                    error = true;
                    break;
                }
            }

            plc.Dispose();
            if (!error) Console.WriteLine("SUCESSO");

            Console.ReadLine();
        }





        static void Teste()
        {

            using (var plc = new PLC())
            {
                plc.I = new byte[] { 12, 64, 1 };
                plc.I[9, 1] = false;
            }

            using (var plc = new PLC())
            {
                plc.Q = new byte[] { 0, 1, 2, 3, 4 };
                plc.Q[0] = 3;
                plc.Q[0, 2] = true;

                Console.WriteLine(plc.Q[0]);
                Console.WriteLine(plc.Q);

                if (plc.Q[0, 1])
                {
                    plc.I = new byte[] { 0, 1, 2, 3, 4 };
                    plc.I[0] = 3;
                    plc.I[0, 2] = false;

                    if (plc.Q[0, 2] = !plc.I[0, 2])
                    {
                        Console.WriteLine(plc.I[0]);
                        Console.WriteLine(plc.I);
                    }
                }
            }
        }
    }
}
