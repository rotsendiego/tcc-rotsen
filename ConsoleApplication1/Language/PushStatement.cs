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
    public class PushStatement : Statement
    {
        public IAttribute input;

        public PushStatement(string a)
        {
            Delay = 30;

            Sensor s1;

            if (Enum.TryParse(a, out s1))
            {
                input = s1.Input();
            }
            else if (a.StartsWith("i", StringComparison.InvariantCultureIgnoreCase))
            {
                // TODO Tratar escrita em blocos
                input = new IAttribute(byte.Parse(a.Substring(1).Split('.')[0]), byte.Parse(a.Substring(1).Split('.')[1]));
            }
        }

        public override void Execute(params object[] programParams)
        {
            var plc = (PLC)programParams[0];

            if (input != null)
            {
                input.Set();
            }

            System.Threading.Thread.Sleep(Delay);
        }
    }
}
