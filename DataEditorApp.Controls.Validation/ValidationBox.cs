using System;
using System.Windows.Controls;
using DbAuthApp.InputFieldDecoration;

namespace DataEditorApp.Controls.Validation
{
    public abstract class ValidationBox : UserControl
    {
        private bool _allowEmpty;
        protected TextBoxDecorator? Decorator { get; private set; }

        public bool IsCorrect => Decorator.IsCorrect || AllowEmpty && Text.Length == 0;
        public abstract string Text { get; set; }
        public TextBoxDecorator.OnIsCorrectChangedDelegate IsCorrectChanged
        {
            get => Decorator.OnIsCorrectChanged;
            set => Decorator.OnIsCorrectChanged = value;
        }

        public Predicate<string>? IsAvailable { get; set; } = null;
        public void ResetInputBox() => Decorator.Reset();

        public bool AllowEmpty
        {
            get => _allowEmpty;
            set
            {
                _allowEmpty = value;
                OnTextChanged();
            }
        }

        protected void RegisterTextBox(TextBox inputBox)
        {
            Decorator = new TextBoxDecorator(inputBox);
            inputBox.TextChanged += (sender, args) => OnTextChanged();
            OnTextChanged();
        }

        protected void RegisterPasswordBox(PasswordBox inputBox)
        {
            Decorator = new TextBoxDecorator(inputBox);
            inputBox.PasswordChanged += (sender, args) => OnTextChanged();
            OnTextChanged();
        }

        protected abstract void OnTextChanged();
    }
}
