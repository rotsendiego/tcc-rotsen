using ConsoleApplication5.Sorting;
using MKSimControllerApiTest.Controller;
using ConsoleApplication5.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Language
{
    public class ReadStatement : Statement
    {
        public IAttribute input;
        public QAttribute output;

        public ReadStatement(string a)
        {
            Result = -1;

            Sensor s1;
            Output q;

            if (Enum.TryParse(a, out s1))
            {
                input = s1.Input();
            }
            else if (Enum.TryParse(a, out q))
            {
                output = q.Output();
            }
            else if (a.StartsWith("i", StringComparison.InvariantCultureIgnoreCase))
            {
                // TODO Tratar escrita em blocos
                input = new IAttribute(byte.Parse(a.Substring(1).Split('.')[0]), byte.Parse(a.Substring(1).Split('.')[1]));
            }
            else
            {
                output = new QAttribute(byte.Parse(a.Substring(1).Split('.')[0]), byte.Parse(a.Substring(1).Split('.')[1]));
            }
        }

        public override void Execute(params object[] programParams)
        {
            var plc = (PLC)programParams[0];

            if (input != null)
            {
                Result = input.IsHigh ? 1 : 0;
            }
            else if (output != null)
            {
                Result = output.IsOn ? 1 : 0;
            }
        }
    }
}
