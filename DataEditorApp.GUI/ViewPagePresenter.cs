using System;
using System.Collections.Generic;
using System.Windows;
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
                users.ForEach(u =>
                    _page.UsersLv.Items.Add(
                        $"User #{u.Id}: login={u.Login}, creation date={u.CreationDate.Day}.{u.CreationDate.Month}.{u.CreationDate.Year}"));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error while reading users data");
            }
        }
    }
}
