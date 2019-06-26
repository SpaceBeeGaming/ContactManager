using System;
using System.Linq;

namespace ContactManagerLibrary
{
    public class ContactModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Icon => HasIcon ? $@"C:\Users\{Environment.UserName}\AppData\Local\ContactManager\Contact Images\{LastName}_{FirstName}.png" : @"Resources\UserPlaceHolder.png";

        public string FullName => string.IsNullOrWhiteSpace(MiddleName) ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName.First()}. {LastName}";
        public bool HasIcon { get; set; }
        public ContactModel() => Id = Guid.NewGuid();
    }
}
