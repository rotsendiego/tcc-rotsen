using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MKSimControllerApiTest.Controller
{
    public class PLC : IDisposable
    {
        public static PLC instance;

        private TcpClient client;
        private StreamWriter writer;
        private StreamReader reader;
        private string lastResponse;

        public _I I { get; set; }
        public _Q Q { get; set; }

        public PLC()
        {
            instance = this;
            Q = _Q.instance;
            I = _I.instance;

            client = new TcpClient("127.0.0.1", 0xfa57);
            writer = new StreamWriter(client.GetStream());
            reader = new StreamReader(client.GetStream());
            writer.AutoFlush = true;
        }

        public void WriteOutput(byte[] values)
        {
            writer.WriteLine(string.Format("O<-{0}", string.Join(",", values.Select(b => b.ToString()))));
            lastResponse = reader.ReadLine();
        }

        public void WriteOutput(int a, int val)
        {
            writer.WriteLine(string.Format("O{0}={1}", a, val));
            lastResponse = reader.ReadLine();
        }

        public void SetOutput(int a, int b, bool val)
        {
            var output = ReadOutput(a);

            if (val)
            {
                output |= (1 << b);
            }
            else
            {
                output &= ~(1 << b);
            }

            WriteOutput(a, output);
        }

        public void WriteInput(byte[] values)
        {
            writer.WriteLine(string.Format("I<-{0}", string.Join(",", values.Select(b => b.ToString()))));
            lastResponse = reader.ReadLine();
        }

        public void WriteInput(int a, int val)
        {
            writer.WriteLine(string.Format("I{0}={1}", a, val));
            lastResponse = reader.ReadLine();
        }

        public void SetInput(int a, int b, bool val)
        {
            var input = ReadInput(a);

            if (val)
            {
                input |= (1 << b);
            }
            else
            {
                input &= ~(1 << b);
            }

            WriteInput(a, input);
        }

        public byte[] ReadInput()
        {
            writer.WriteLine("I");
            lastResponse = reader.ReadLine();
            return lastResponse.Split(',').Select(s => byte.Parse(s)).ToArray();
        }

        public byte[] ReadOutput()
        {
            writer.WriteLine("O");
            lastResponse = reader.ReadLine();
            return lastResponse.Split(',').Select(s => byte.Parse(s)).ToArray();
        }

        public int ReadInput(int i)
        {
            writer.WriteLine(string.Format("I{0}", i));
            lastResponse = reader.ReadLine();
            return int.Parse(lastResponse);
        }

        public int ReadOutput(int i)
        {
            writer.WriteLine(string.Format("O{0}", i));
            lastResponse = reader.ReadLine();
            return int.Parse(lastResponse);
        }

        public bool ReadInput(int a, int b)
        {
            return (ReadInput(a) & (1 << b)) > 0;
        }

        public bool ReadOutput(int a, int b)
        {
            return (ReadOutput(a) & (1 << b)) > 0;
        }

        public void Dispose()
        {
            writer.WriteLine("exit");
            client.Close();
        }

        #region Output
        public class _Q
        {
            public static _Q instance;
            static _Q() { instance = new _Q(); }
            private _Q() { }

            public bool this[byte a, byte b]
            {
                get
                {
                    return PLC.instance.ReadOutput(a, b);
                }
                set
                {
                    PLC.instance.SetOutput(a, b, value);
                }
            }

            public int this[byte a]
            {
                get
                {
                    return PLC.instance.ReadOutput(a);
                }
                set
                {
                    PLC.instance.WriteOutput(a, value);
                }
            }

            public override string ToString()
            {
                PLC.instance.ReadOutput();
                return PLC.instance.lastResponse;
            }

            static public implicit operator _Q(byte[] values)
            {
                PLC.instance.WriteOutput(values);
                return _Q.instance;
            }

            static public explicit operator byte[](_Q i)
            {
                return PLC.instance.ReadOutput();
            }
        }
        #endregion

        #region Input
        public class _I
        {
            public static _I instance;
            static _I() { instance = new _I(); }
            private _I() { }

            public bool this[byte a, byte b]
            {
                get
                {
                    return PLC.instance.ReadInput(a, b);
                }
                set
                {
                    PLC.instance.SetInput(a, b, value);
                }
            }

            public int this[byte a]
            {
                get
                {
                    return PLC.instance.ReadInput(a);
                }
                set
                {
                    PLC.instance.WriteInput(a, value);
                }
            }

            public override string ToString()
            {
                PLC.instance.ReadInput();
                return PLC.instance.lastResponse;
            }

            static public implicit operator _I(byte[] values)
            {
                PLC.instance.WriteInput(values);
                return _I.instance;
            }

            static public explicit operator byte[](_I i)
            {
                return PLC.instance.ReadInput();
            }
        }
        #endregion
    }
}
