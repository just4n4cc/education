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
using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;

namespace lab04
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        List<string> list = new List<string>();

        private bool _FileNotRead = true;

        public bool FileNotRead
        {
            get => _FileNotRead;
            set
            {
                _FileNotRead = value;
            }
        }
        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Text files (*.txt)|*.txt";

            if (fd.ShowDialog() == true)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();

                string text = File.ReadAllText(fd.FileName);

                char[] separators = new char[] {' ', '.', ',', '!', '?', '/', '\t', '\n'};

                string[] textArray = text.Split(separators);

                foreach (string strItem in textArray)
                {
                    string str = strItem.Trim();

                    if (list.Contains(str) == false)
                    {
                        list.Add(str);
                    }
                }

                timer.Stop();
                ReadTimeTextBox.Text = timer.Elapsed.ToString() + list.Count.ToString();
                FileNotRead = false;
            }
            else
            {
                MessageBox.Show("Выберите файл!");
            }
        }
        private void SearchWordButton_Click(object sender, RoutedEventArgs e)
        {
            if (FileNotRead)
            {
                MessageBox.Show("Выберите файл!");
                return;
            }
            string word = SearchWordTextBox.Text.Trim();

            if (!string.IsNullOrWhiteSpace(word) && list.Count > 0)
            {
                string wordUpper = word.ToUpper();

                List<string> tempList = new List<string>();

                Stopwatch timer = new Stopwatch();
                timer.Start();

                foreach (string str in list)
                {
                    if (str.ToUpper().Contains(word.ToUpper()))
                    {
                        tempList.Add(str);
                    }
                }

                timer.Stop();
                SearchTimeTextBox.Text = timer.Elapsed.ToString();

                WordList.Items.Clear();

                foreach (string str in tempList)
                {
                    WordList.Items.Add(str);
                }
            }
            else
            {
                MessageBox.Show("Выберите файл и введите слово для поиска");
            }
        }
    }
    
}
