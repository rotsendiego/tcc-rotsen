using MKSimControllerApiTest.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Language
{
    public class Parser
    {
        public static Statement lastStatement;

        public static Func<PLC, bool> Parse(string line)
        {
            if (line.StartsWith("LEIA", StringComparison.InvariantCultureIgnoreCase))
            {
                lastStatement = new ReadStatement(line.Substring(4).Trim());
            }
            else if (line.StartsWith("VERIFICA", StringComparison.InvariantCultureIgnoreCase))
            {
                var args = line.Substring(8).Split(' ').Where(s => !string.IsNullOrEmpty(s.Trim())).Select(s => s.Trim());
                lastStatement = new AssertStatement(args.First(), args.Last());
            }
            else if (line.StartsWith("PRESSIONA", StringComparison.InvariantCultureIgnoreCase))
            {
                var args = line.Substring(9).Split(' ').Where(s => !string.IsNullOrEmpty(s.Trim())).Select(s => s.Trim());
                lastStatement = new PushStatement(args.First());
            }
            else if (line.StartsWith("SOLTA", StringComparison.InvariantCultureIgnoreCase))
            {
                var args = line.Substring(5).Split(' ').Where(s => !string.IsNullOrEmpty(s.Trim())).Select(s => s.Trim());
                lastStatement = new PullStatement(args.First());
            }
            else
            {
                return plc => true;
            }

            return ExecuteLastStatement;
        }

        public static bool ExecuteLastStatement(PLC plc)
        {
            lastStatement.Execute(plc);

            return !lastStatement.Error;
        }
    }
}
