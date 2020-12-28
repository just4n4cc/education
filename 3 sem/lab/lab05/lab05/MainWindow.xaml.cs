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

namespace lab05
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int Distance(string str1P, string str2P)
        {
            if ((str1P == null) || (str2P == null)) return -1;

            int len1 = str1P.Length;
            int len2 = str2P.Length;

            if ((len1 == 0) && (len2 == 0)) return 0;
            if (len1 == 0) return len2;
            if (len2 == 0) return len1;

            string str1 = str1P.ToUpper();
            string str2 = str2P.ToUpper();

            int[,] matrix = new int[len1 + 1, len2 + 1];

            for (int i = 0; i <= len1; i++) matrix[i, 0] = i;
            for (int i = 0; i <= len2; i++) matrix[0, i] = i;

            for (int i = 1; i <= len1; i++)
            {
                for (int j = 1; j <= len2; j++)
                {
                    int eq = (
                        (str1.Substring(i - 1, 1) ==
                        str2.Substring(j - 1, 1)) ? 0 : 1);

                    int ins = matrix[i, j - 1] + 1;
                    int del = matrix[i - 1, j] + 1;
                    int subst = matrix[i - 1, j - 1] + eq;

                    matrix[i, j] = Math.Min(Math.Min(ins, del), subst);

                    if ((i > 1) && (j > 1) &&
                    (str1.Substring(i - 1, 1) == str2.Substring(j - 2, 1)) &&
                    (str1.Substring(i - 2, 1) == str2.Substring(j - 1, 1)))
                    {
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j- 2] + eq);
                    }
                }
            }

            return matrix[len1, len2];
        }

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

                char[] separators = new char[] { ' ', '.', ',', '!', '?', '/', '\t', '\n' };

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
                Int32.TryParse(MaxDistanceTextBox.Text, out int max);
                List<string> tempList = new List<string>();

                Stopwatch timer = new Stopwatch();
                timer.Start();

                foreach (string str in list)
                {
                    int dist = Distance(str, word);
                    if (dist <= max)
                    {
                        if ((bool)YesRadioButton.IsChecked)
                        {
                            tempList.Add(str + " - " + dist.ToString());
                        }
                        else
                        {
                            tempList.Add(str);
                        }
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
