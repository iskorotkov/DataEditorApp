using System;
using System.Windows;
using DataEditorApp.Users;

namespace DataEditorApp.GUI
{
    public interface ISubmitFormContext
    {
        public string SubmitButtonText { get; }
        public string FormTitle { get; }
        public Visibility CreationDateEnabled { get; }

        public bool IsLoginValid(string login);
        public void SubmitChanges(User? oldUserData, string login, string password, DateTime? creationDate);
    }
}
