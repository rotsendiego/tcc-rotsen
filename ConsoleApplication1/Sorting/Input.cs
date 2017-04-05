 using MKSimControllerApiTest.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5.Sorting
{
    public enum Sensor
    {
        [I(5, 2)]
        SAIDA_ESTEIRA_ALIMENTACAO_1,

        [I(5, 3)]
        SAIDA_ESTEIRA_ALIMENTACAO_2,

        [I(5, 4)]
        CAIXAS_PEQUENAS,

        [I(5, 5)]
        CAIXAS_MEDIAS,

        [I(5, 6)]
        CAIXAS_GRANDES,

        [I(5, 7)]
        ENTRADA_CAIXAS_MESA_ROTATORIA,

        [I(6, 0)]
        SAIDA_CAIXA_MESA_ROTATORIA,

        [I(6, 1)]
        ENTRADA_ESTEIRA_CAIXA_PEQUENA,

        [I(6, 2)]
        ENTRADA_ESTEIRA_CAIXA_MÉDIA,

        [I(6, 3)]
        ENTRADA_ESTEIRA_CAIXA_GRANDE,

        [I(6, 4)]
        SAIDA_ESTEIRA_CAIXA_PEQUENA,

        [I(6, 5)]
        SAIDA_ESTEIRA_CAIXA_MÉDIA,

        [I(6, 6)]
        SAIDA_ESTEIRA_CAIXA_GRANDE,

        [I(6, 7)]
        MESA_ROTATORIA_POSICAO_PEQUENA,

        [Q(4, 3)]
        BOTAO_1
    }


    public class IAttribute : Attribute
    {
        public byte Slot { get; set; }
        public byte Address { get; set; }

        public IAttribute(byte s, byte a)
        {
            Slot = s;
            Address = a;
        }

        public bool IsHigh
        {
            get
            {
                return PLC.instance.I[Slot, Address];
            }
        }

        public void Set()
        {
            PLC.instance.I[Slot, Address] = true;
        }

        public void Unset()
        {
            PLC.instance.I[Slot, Address] = false;
        }
    }
}
