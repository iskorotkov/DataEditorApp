using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataEditorApp.Users;
using DataEditorApp.View.Postgres;
using Npgsql;

namespace DataEditorApp.GUI
{
    public partial class ViewPage : Page
    {
        public ViewPage()
        {
            InitializeComponent();
            PopulateUsersList();
        }

        public IEnumerable<User> GetSelectedUsers()
        {
            return UsersLv.SelectedItems.Cast<User>();
        }

        private void PopulateUsersList()
        {
            var conStr = new UsersConnectionStringBuilder().Build();
            try
            {
                List<User> users;
                using (var con = new NpgsqlConnection(conStr))
                {
                    con.Open();
                    users = new GetAllUsersCommand(con).Execute();
                }

                UsersLv.Items.Clear();
                foreach (var user in users)
                {
                    UsersLv.Items.Add(user);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error while reading users data");
            }
        }

        public void RemoveSelectedUsers()
        {
            var selected = GetSelectedUsers().ToList();
            foreach (var item in selected)
            {
                UsersLv.Items.Remove(item);
            }
        }
    }
}
