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
    public enum AssertStatus
    {
        LIGADA = 1,
        LIGADO = 1,
        DESLIGADO = 0,
        DESLIGADA = 0
    }

    public class AssertStatement : Statement
    {
        public IAttribute input;
        public QAttribute output;

        public ReadStatement readStatement;
        public AssertStatus assertStatus;

        public AssertStatement(string a, string status)
        {
            readStatement = new ReadStatement(a);
            assertStatus = (AssertStatus)Enum.Parse(typeof(AssertStatus), status);
        }

        public override void Execute(params object[] programParams)
        {
            readStatement.Execute(programParams);

            var expextedResult = (int)assertStatus > 0 ? 1 : 0;

            Error = readStatement.Result != expextedResult;
        }
    }
}
