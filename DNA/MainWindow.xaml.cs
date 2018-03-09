using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading;

namespace DNA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            Client mainClient = new Client();
            _Utility.GenerateDNA(mainClient);

            List<Client> clients = new List<Client>();
            for (int i = 0; i < 10; i++)
            {
                Client cl = new Client();
                _Utility.GenerateDNA(cl);
                clients.Add(cl);
            }

            /*
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 10; i++)
                threads.Add(new Thread(() => _Utility.PercentCount(ref clients.ToArray()[i])));

            foreach (Thread thread in threads)
            {
                thread.Start();
                thread.Join();
            }
            */


            Thread m = new Thread(() => _Utility.Percentage(clients.ToArray()[0]));
            m.Start();

            Thread print = new Thread(() =>
            {
                m.Join();
                
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i < clients.Count; i++)
                {
                    sb.Append(String.Format("A Percent: {0}\n", clients.ElementAt(i).APercent));
                    sb.Append(String.Format("G Percent: {0}\n", clients.ElementAt(i).GPercent));
                    sb.Append(String.Format("C Percent: {0}\n", clients.ElementAt(i).CPercent));
                    sb.Append(String.Format("T Percent: {0}\n\n\n", clients.ElementAt(i).TPercent));
                    
                }

                MainTextBlock.Text = sb.ToString();
            });

            print.Start();


        }



        public class Client
        {
            public string DNA { set; get; }
            public int APercent { set; get; }
            public int GPercent { set; get; }
            public int CPercent { set; get; }
            public int TPercent { set; get; }


        }


        public static class _Utility
        {
            private static Random rnd = new Random();

            public static void PercentCount(ref Client cl)
            {
                int A = 0, C = 0, T = 0, G = 0;

                for (int i = 0; i < 10000000; i++)
                {
                    switch (cl.DNA.ElementAt(i))
                    {
                        case 'A': A++; break;
                        case 'C': C++; break;
                        case 'T': T++; break;
                        case 'G': G++; break;
                    }
                }

                cl.APercent = A * 100 / 10000000;
                cl.CPercent = C * 100 / 10000000;
                cl.TPercent = T * 100 / 10000000;
                cl.GPercent = G * 100 / 10000000;
            }

            public static void GenerateDNA(Client cl)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < 10000000; i++)
                {
                    int number = rnd.Next(1, 5);

                    switch (number)
                    {
                        case 1: sb.Append('A'); break;
                        case 2: sb.Append('G'); break;
                        case 3: sb.Append('C'); break;
                        case 4: sb.Append('T'); break;
                    }
                }

                cl.DNA = sb.ToString();
            }

            public static void Percentage(Client cl)
            {
                int A = 0, C = 0, T = 0, G = 0;

                for (int i = 0; i < 10000000; i++)
                {
                    switch (cl.DNA.ElementAt(i))
                    {
                        case 'A': A++; break;
                        case 'C': C++; break;
                        case 'T': T++; break;
                        case 'G': G++; break;
                    }
                }

                cl.APercent = A * 100 / 10000000;
                cl.CPercent = C * 100 / 10000000;
                cl.TPercent = T * 100 / 10000000;
                cl.GPercent = G * 100 / 10000000;
            
            }

        }

    }
}

