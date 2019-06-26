using ContactManagerLibrary;
using System.Windows;
using System.IO;

namespace ContactManagerUI
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {

        public ContactModel Contact { get; set; }
        public EditWindow(string Title)
        {
            InitializeComponent();
            EditWindow1.Title = Title;
            WindowHeader.Text = Title;
            DeleteButton.Visibility = Visibility.Hidden;
            Contact = new ContactModel();
        }

        public EditWindow(string Title, ContactModel contact)
        {
            InitializeComponent();
            EditWindow1.Title = Title;
            WindowHeader.Text = Title;
            Contact = contact;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstNameTextBox.Text != Contact.FirstName)
            {
                Contact.FirstName = FirstNameTextBox.Text;
            }
            if (MiddleNameTextBox.Text != Contact.MiddleName)
            {
                Contact.MiddleName = MiddleNameTextBox.Text;
            }
            if (LastNameTextBox.Text != Contact.LastName)
            {
                Contact.LastName = LastNameTextBox.Text;
            }
            if (EmailTextBox.Text != Contact.Email)
            {
                Contact.Email = EmailTextBox.Text;
            }
            if (PhoneTextBox.Text != Contact.PhoneNumber)
            {
                Contact.PhoneNumber = PhoneTextBox.Text;
            }
            if (HasIconCheckBox.IsChecked != Contact.HasIcon)
            {
                Contact.HasIcon = (bool)HasIconCheckBox.IsChecked;
            }

            if (!string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                ContactHandler.SaveContact(Contact);
                Close();
                if (Contact.HasIcon)
                {
                    MessageBox.Show($"Place your Icon to '{Directory.GetParent(Contact.Icon)}' and name it '{Contact.LastName}_{Contact.FirstName}.png'", "Icon", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("You Cannot save an empty contact!\nAdd at least 'First Name'.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            FirstNameTextBox.Text = Contact.FirstName;
            MiddleNameTextBox.Text = Contact.MiddleName;
            LastNameTextBox.Text = Contact.LastName;
            FullNameTextBox.Text = Contact.FullName;
            EmailTextBox.Text = Contact.Email;
            PhoneTextBox.Text = Contact.PhoneNumber;
            IdTextBox.Text = Contact.Id.ToString();
            HasIconCheckBox.IsChecked = Contact.HasIcon;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Proceed?", "Delete Contact", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                ContactHandler.DeleteContact(Contact);
                Close();
            }
        }
    }
}
