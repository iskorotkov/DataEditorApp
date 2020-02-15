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

        public override string Text
        {
            get => _processor.RemoveWhitespaces(InputBox.Text);
            set => InputBox.Text = value;
        }

        protected override void OnTextChanged()
        {
            if (Text.Length == 0)
                Decorator.Reset();
            else if (_checker.IsCorrect(Text))
            {
                if (IsAvailable != null && !IsAvailable(Text))
                    Decorator.InputIsIncorrect("User with this login already exists");
                else
                    Decorator.InputIsCorrect();
            }
            else
                Decorator.InputIsIncorrect("Login doesn't match pattern");
        }
    }
}
