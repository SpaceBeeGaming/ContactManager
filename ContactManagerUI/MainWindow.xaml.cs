using ContactManagerLibrary;
using Squirrel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Diagnostics;

namespace ContactManagerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ContactModel> contacts;


        public MainWindow()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Utilities.CheckForUpdates();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(ContactListBox.SelectedItem is null))
            {
                ContactModel contact = ContactListBox.SelectedItem as ContactModel;
                EditWindow editWindow = new EditWindow("Edit Contact", contact);
                editWindow.Show();
                editWindow.Closed += EditWindow_Closed;
            }
        }
        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow("New Contact");
            editWindow.Show();
            editWindow.Closed += EditWindow_Closed;
        }
        private void EditWindow_Closed(object sender, EventArgs e) => ContactListBox.ItemsSource = ContactHandler.LoadContacts();

        private void ContactListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContactModel selectedItem = ContactListBox.SelectedItem as ContactModel;

            if (selectedItem is null)
            {
                ContactListBox.SelectedIndex = 0;
            }
            else
            {
                LargeImage.Source = File.Exists(selectedItem.Icon)
                    ? new BitmapImage(new Uri(selectedItem.Icon))
                    : new BitmapImage(new Uri("Resources/UserPlaceHolder.png", UriKind.Relative));

                FirstNameTextBox.Text = selectedItem.FirstName;
                MiddleNameTextBox.Text = selectedItem.MiddleName;
                LastNameTextBox.Text = selectedItem.LastName;
                FullNameTextBox.Text = selectedItem.FullName;
                PhoneTextBox.Text = selectedItem.PhoneNumber;
                EmailTextBox.Text = selectedItem.Email;
                IdTextBox.Text = selectedItem.Id.ToString();
            }
        }



        private bool ContactFilter(object item)
        {
            return string.IsNullOrWhiteSpace(SearchBox.Text) || SearchBox.Text == "Search"
                ? true
                : (item as ContactModel).FullName.IndexOf(SearchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e) => SearchBox.Clear();
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(ContactListBox is null))
            {
                CollectionViewSource.GetDefaultView(ContactListBox.ItemsSource).Refresh();
            }
        }

        private void ContactManagerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            contacts = ContactHandler.LoadContacts();
            ContactListBox.ItemsSource = contacts;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ContactListBox.ItemsSource);
            view.Filter = ContactFilter;
            ContactListBox.SelectedIndex = 0;

            CreditsWindow credits = new CreditsWindow();
            credits.Show();
        }
    }
}
