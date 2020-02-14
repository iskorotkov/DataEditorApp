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

        public override string Text
        {
            get => InputBox.Password.Trim();
            set => InputBox.Password = value;
        }

        protected override void OnTextChanged()
        {
            if (_checker.IsStrong(Text))
                Decorator.InputIsCorrect();
            else
                Decorator.InputIsIncorrect("Password isn't strong enough");
        }
    }
}
