using DbAuthApp.Passwords;

namespace DataEditorApp.Controls.Validation
{
    public partial class PasswordValidationBox : ValidationBox
    {
        private readonly PasswordChecker _checker = new PasswordChecker();

        public PasswordValidationBox()
        {
            InitializeComponent();
            RegisterPasswordBox(InputBox);
        }

        public override void OnTextChanged()
        {
            var password = InputBox.Password.Trim();
            if (_checker.IsStrong(password))
                Decorator.InputIsCorrect();
            else
                Decorator.InputIsIncorrect("Password isn't strong enough");
        }
    }
}
