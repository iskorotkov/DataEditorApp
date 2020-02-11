using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DataEditorApp.Users;
using DataEditorApp.View.Postgres;
using Npgsql;

namespace DataEditorApp.GUI
{
    public class ViewPagePresenter
    {
        private readonly ViewPage _page;

        public ViewPagePresenter(ViewPage page)
        {
            _page = page;

            // TODO: Populate users list only when this page gets active
            PopulateUsersList();
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

                _page.UsersLv.Items.Clear();
                foreach (var user in users)
                {
                    _page.UsersLv.Items.Add(user);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error while reading users data");
            }
        }
    }
}
