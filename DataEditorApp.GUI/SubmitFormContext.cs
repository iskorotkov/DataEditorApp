using DataEditorApp.Users;

namespace DataEditorApp.GUI
{
    public interface ISubmitFormContext
    {
        public string SubmitButtonText { get; }
        public string FormTitle { get; }

        public bool CheckLogin(string login);
        public void SubmitChanges(User user);
    }

    public class AddContext : ISubmitFormContext
    {
        public string SubmitButtonText => "Add";
        public string FormTitle => "Add user";

        public bool CheckLogin(string login)
        {
            throw new System.NotImplementedException();
        }

        public void SubmitChanges(User user)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ModifyContext : ISubmitFormContext
    {
        public string SubmitButtonText => "Modify";
        public string FormTitle => "Modify user";

        public bool CheckLogin(string login)
        {
            throw new System.NotImplementedException();
        }

        public void SubmitChanges(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
