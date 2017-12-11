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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Profile : Page
    {
        MainWindow wnd;
        DBUsage q = new DBUsage();
        FriendTestEntities db = new FriendTestEntities();
        List<Person> people = new List<Person>();
        Person p = new Person();

        public Profile(Person per)
        {
            p = per;
            InitializeComponent();
            people = q.ShowPerson();
            labelName.Content = p.Name;
           
        }

        private void buttonCreateTest_Click(object sender, RoutedEventArgs e)
        {
            wnd.Content = new CreateTest();
            
        }
    }
}