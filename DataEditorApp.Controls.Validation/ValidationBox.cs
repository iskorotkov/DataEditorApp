using System;
using System.Windows.Controls;
using DbAuthApp.InputFieldDecoration;

namespace DataEditorApp.Controls.Validation
{
    public abstract class ValidationBox : UserControl
    {
        protected TextBoxDecorator? Decorator { get; private set; }

        public bool IsCorrect => Decorator.IsCorrect;
        public TextBoxDecorator.OnIsCorrectChangedDelegate OnIsCorrectChanged => Decorator.OnIsCorrectChanged;
        public Predicate<string>? IsAvailable { get; set; } = null;

        protected void RegisterTextBox(TextBox inputBox)
        {
            Decorator = new TextBoxDecorator(inputBox);
            inputBox.TextChanged += (sender, args) => OnTextChanged();
        }

        protected void RegisterPasswordBox(PasswordBox inputBox)
        {
            Decorator = new TextBoxDecorator(inputBox);
            inputBox.PasswordChanged += (sender, args) => OnTextChanged();
        }

        public abstract void OnTextChanged();
    }
}
