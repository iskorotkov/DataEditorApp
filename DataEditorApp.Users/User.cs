using System;

namespace DataEditorApp.Users
{
    public struct User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
