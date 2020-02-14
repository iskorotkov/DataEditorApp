using DbAuthApp.Login;

namespace DataEditorApp.Controls.Validation
{
    public partial class LoginValidationBox : ValidationBox
    {
        private readonly LoginChecker _checker = new LoginChecker();
        private readonly LoginProcessor _processor = new LoginProcessor();

        public LoginValidationBox()
        {
            InitializeComponent();
            RegisterTextBox(InputBox);
        }

        public override void OnTextChanged()
        {
            var login = _processor.RemoveWhitespaces(InputBox.Text);
            if (_checker.IsCorrect(login))
            {
                if (IsAvailable != null && !IsAvailable(login))
                    Decorator.InputIsIncorrect("User with this login already exists");
                else
                    Decorator.InputIsCorrect();
            }
            else
                Decorator.InputIsIncorrect("Login doesn't match pattern");
        }
    }
}
