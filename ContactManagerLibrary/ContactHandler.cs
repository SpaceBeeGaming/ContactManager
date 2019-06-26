using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ContactManagerLibrary
{
    public static class ContactHandler
    {
        private static readonly string ContactLocation = $@"C:\Users\{Environment.UserName}\AppData\Local\ContactManager\Contacts.json";
        private static void SaveContacts(List<ContactModel> contacts)
        {
            string ContactJson = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            File.WriteAllText(ContactLocation, ContactJson);
        }

        public static List<ContactModel> LoadContacts()
        {
            if (!File.Exists(ContactLocation))
            {
                return new List<ContactModel>();
            }
            else
            {
                string ContactJson = File.ReadAllText(ContactLocation);
                return JsonConvert.DeserializeObject<List<ContactModel>>(ContactJson);
            }
        }

        public static void SaveContact(ContactModel contact) => SaveContacts(AddContact(contact));

        private static List<ContactModel> AddContact(ContactModel contactToAdd)
        {
            List<ContactModel> contacts = LoadContacts();
            foreach (ContactModel contact in contacts)
            {
                if (contact.Id == contactToAdd.Id)
                {
                    contacts.Remove(contact);
                    contacts.Add(contactToAdd);
                    return contacts;
                }
            }
            contacts.Add(contactToAdd);
            return contacts;
        }

        public static void DeleteContact(ContactModel contact) => SaveContacts(RemoveContact(contact));
        private static List<ContactModel> RemoveContact(ContactModel contact)
        {
            List<ContactModel> contacts = LoadContacts();
            foreach (ContactModel item in contacts)
            {
                if (item.Id == contact.Id)
                {
                    contacts.Remove(item);
                    return contacts;
                }
            }
            return contacts;
        }
    }
}

