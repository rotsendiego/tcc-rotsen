using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Language
{
    public abstract class Statement
    {
        public int Result { get; protected set; }
        public bool Error { get; protected set; }
        public int Delay { get; protected set; }

        public abstract void Execute(params object[] programParams);
    }
}
