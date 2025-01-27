﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataEditorApp.Deletion.Postgres;
using Npgsql;

namespace DataEditorApp.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewPage _viewPage;

        public MainWindow()
        {
            InitializeComponent();
            _viewPage = new ViewPage();
            MainFrame.Content = _viewPage;
            _viewPage.UsersLv.SelectionChanged += UsersSelectionChanged;
        }

        private void UsersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = _viewPage.GetSelectedUsers().Count();
            ModifyUser.IsEnabled = selected == 1;
            DeleteUsers.IsEnabled = selected >= 1;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            ShowWindowWithPage(new AddPage(_viewPage.UsersLv));
        }

        private static void ShowWindowWithPage(Page page)
        {
            new Window
            {
                Content = page,
                Width = 400,
                Height = 300,
            }.Show();
        }

        private void ModifyUser_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedUsers = _viewPage.GetSelectedUsers().ToList();
            if (selectedUsers.Count != 1)
            {
                return;
            }

            ShowWindowWithPage(new ModifyPage(selectedUsers[0], _viewPage.UsersLv));
        }

        private void DeleteUsers_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedUsers = _viewPage.GetSelectedUsers().ToList();
            if (selectedUsers.Count == 0)
            {
                return;
            }

            _viewPage.RemoveSelectedUsers();
            using var con = new NpgsqlConnection(new UsersConnectionStringBuilder().Build());
            con.Open();
            new DeleteUsersCommand(con, selectedUsers).Execute();
        }
    }
}
