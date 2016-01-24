using Xamarin.Forms;

namespace Contacts.Views
{
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();

            ContactList.ItemSelected += async (sender, args) =>
            {
                var contactDetailPage = new ContactDetailPage {BindingContext = args.SelectedItem};
                await Navigation.PushAsync(contactDetailPage, true);
            };
        }
    }
}
