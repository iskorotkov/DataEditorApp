﻿using System;
using System.Windows.Controls;
using DbAuthApp.InputFieldDecoration;

namespace DataEditorApp.Controls.Validation
{
    public abstract class ValidationBox : UserControl
    {
        protected TextBoxDecorator? Decorator { get; private set; }

        public bool IsCorrect => Decorator.IsCorrect;
        public abstract string Text { get; set; }
        public TextBoxDecorator.OnIsCorrectChangedDelegate OnIsCorrectChanged => Decorator.OnIsCorrectChanged;
        public Predicate<string>? IsAvailable { get; set; } = null;
        public void ResetInputBox() => Decorator.Reset();
        public bool AllowEmpty { get; set; } = true;

        protected void RegisterTextBox(TextBox inputBox)
        {
            Decorator = new TextBoxDecorator(inputBox);
            inputBox.TextChanged += (sender, args) => OnTextChanged();
            if (AllowEmpty)
                Decorator.InputIsCorrect();
        }

        protected void RegisterPasswordBox(PasswordBox inputBox)
        {
            Decorator = new TextBoxDecorator(inputBox);
            inputBox.PasswordChanged += (sender, args) => OnTextChanged();
            if (AllowEmpty)
                Decorator.InputIsCorrect();
        }

        protected abstract void OnTextChanged();
    }
}
