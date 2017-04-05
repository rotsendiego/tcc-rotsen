using MKSimControllerApiTest.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ConsoleApplication5.Util;

using static ConsoleApplication5.Sorting.Sensor;
using static ConsoleApplication5.Sorting.Output;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Thread plcThread;
        public bool dispose = false;

        public MainWindow()
        {
            InitializeComponent();

            Application.Current.Exit += Current_Exit;
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            if (plcThread != null)
            {
                dispose = true;
                plcThread.Join();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (plcThread == null)
            {
                dispose = false;
                plcThread = new Thread(Run);
                plcThread.Start();
            }
            else
            {
                dispose = true;
                plcThread.Join();
                plcThread = null;
            }
        }

        public void Run()
        {
            using (var plc = new PLC())
            {
                while (!dispose)
                {

                    //pressionar o botao
                    //verificar saida do botao 
                    //x tempo por 3x
                    //Q1.4 = O quando o botao1 = false 
                    //Q1.4 = 1 quando botao = true
                    ESTEIRA_ALIMENTACAO_1.Output().PowerOn();
                    ESTEIRA_ALIMENTACAO_2.Output().PowerOn();
            




                }
                plc.Q = new byte[] { 0, 0, 0 };
            }
        }
    }
}
