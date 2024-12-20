using Avalonia.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace ContactManager;
    public partial class MainWindow : Window
    {
        private ObservableCollection<Contact> Contacts { get; } = new ObservableCollection<Contact>();

        public MainWindow()
        {
            InitializeComponent();
            ContactsListBox.ItemsSource = Contacts;
            FilterComboBox.SelectedIndex = 0;
        }

        private void AddEditButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            
            var contact = new Contact
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text,
                Email = EmailTextBox.Text,
                IsFavorite = FavoriteCheckBox.IsChecked ?? false
            };

            Contacts.Add(contact);
            ClearInputFields();
            ApplyFilterAndSort();
        }
        private void OnFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilterAndSort();
        }
        private void OnSortByFirstNameClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ContactsListBox.ItemsSource = new ObservableCollection<Contact>(Contacts.OrderBy(c => c.FirstName));
        }
        private void OnSortByLastNameClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ContactsListBox.ItemsSource = new ObservableCollection<Contact>(Contacts.OrderBy(c => c.LastName));
        }
        private void ApplyFilterAndSort()
        {
            var filter = (FilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var filteredContacts = filter == "Favorites" ? Contacts.Where(c => c.IsFavorite) : Contacts;

            ContactsListBox.ItemsSource = new ObservableCollection<Contact>(filteredContacts);
        }
        private void ClearInputFields()
        {
            FirstNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            PhoneNumberTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            FavoriteCheckBox.IsChecked = false;
        }
        private void OnDeleteButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (ContactsListBox.SelectedItem is Contact selectedContact)
            {
                Contacts.Remove(selectedContact);
                ApplyFilterAndSort();
            }
        }
    }
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsFavorite { get; set; }

        public override string ToString() => $"{FirstName} {LastName} ({PhoneNumber})";
    }