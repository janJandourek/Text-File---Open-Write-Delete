using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, RoutedEventArgs e)                         //otevření souboru
        {
            OpenFileDialog OD = new OpenFileDialog()
            {
                Filter = "txt soubory (*.txt)|*.txt",
                Title = "Otevřít TXT soubor"
            };

            if (OD.ShowDialog() == true)
            {
                path = OD.FileName;
                buttonWrite.IsEnabled = true;
                buttonVymazObsah.IsEnabled = true;

                Read(path);

                rectangeStatus.Fill = Brushes.Gray;
                textBlockTest.Text = " Otevřeno!";
            }
        }

        private void Read(string path)
        {
            textBlockOutput.Text = "";
            using (StreamReader sr = new StreamReader(path))
            {
                string radek;
                while ((radek = sr.ReadLine()) != null)
                {
                    textBlockOutput.Text += radek + "\n";
                }
            }
        }

        private void buttonWrite_Click(object sender, RoutedEventArgs e)
        {
            string Input = textBoxInput.Text;
            DateTime dateInput = DateTime.Now;
            string Date = dateInput.ToString();


            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.Write($"{Input} - {Date}\n");
            }
            textBoxInput.Text = "";
            rectangeStatus.Fill = Brushes.Green;
            textBlockTest.Text = "  Zapsáno!";
            Read(path);
        }

        private void textBoxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            rectangeStatus.Fill = Brushes.Red;
            textBlockTest.Text = "Neuloženo!";
        }

        private void buttonVymazObsah_Click(object sender, RoutedEventArgs e)
        {
            if (path != "")
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write("");
                }
                Read(path);
            }

            rectangeStatus.Fill = Brushes.Yellow;
            textBlockTest.Text = " Vymazáno!";
        }
    }
}