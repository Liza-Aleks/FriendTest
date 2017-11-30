﻿using Model;
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

namespace Friendship_test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBUsage q = new DBUsage();
        FriendTestEntities1 db = new FriendTestEntities1();
        List<Person> people = new List<Person>();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            string a ="wd";
            foreach (var item in people)
            {
                if (item.Login == a && item.Password == a)
                {
                    Test wnd = new Test(this);
                    wnd.Show();
                }

                else
                    MessageBox.Show("Your login or passoword is incorrect");
            }
           

        }
    }
}
