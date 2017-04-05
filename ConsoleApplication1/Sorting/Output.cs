using MKSimControllerApiTest.Controller;
using System;

namespace ConsoleApplication5.Sorting
{
    public enum Output
    {
        [Q(1, 0)]
        ROLETES,

        [Q(1, 1)]
        ESTEIRA_CAIXAS_PEQUENAS,

        [Q(1, 2)]
        ESTEIRA_CAIXAS_MEDIAS,

        [Q(1, 3)]
        ESTEIRA_CAIXAS_GRANDES,

        [Q(1, 4)]
        ESTEIRA_ALIMENTACAO_1,

        [Q(1, 5)]
        ESTEIRA_ALIMENTACAO_2,

        [Q(1, 6)]
        MESA_ROTATORIA_CAIXA_PEQUENAS,

        [Q(1, 7)]
        MESA_ROTATORIA_CAIXA_GRANDES,
    


    }

    public class QAttribute : Attribute
    {
        public byte Slot { get; set; }
        public byte Address { get; set; }

        public QAttribute(byte s, byte a)
        {
            Slot = s;
            Address = a;
        }

        public bool IsOn
        {
            get
            {
                return PLC.instance.Q[Slot, Address];
            }
        }

        public void PowerOn()
        {
            PLC.instance.Q[Slot, Address] = true;
        }

        public void PowerOff()
        {
            PLC.instance.Q[Slot, Address] = false;
        }
    }
}
