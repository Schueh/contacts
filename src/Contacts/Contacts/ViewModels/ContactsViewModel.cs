using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Contacts.Model;
using Contacts.Proxy;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        private readonly ContactProxy _contactProxy = new ContactProxy();

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }

        public ObservableCollection<Contact> Contacts { get; } = new ObservableCollection<Contact>();

        public ContactsViewModel()
        {
            LoadContacts();
        }

        private async Task LoadContacts()
        {
            IsBusy = true;

            try
            {
                foreach (Contact contact in await _contactProxy.GetContacts())
                {
                    Contacts.Add(contact);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private Command<Contact> _deleteContactCommand;
        public Command<Contact> DeleteContactCommand
        {
            get
            {
                return _deleteContactCommand ?? (_deleteContactCommand = new Command<Contact>(contact =>
                {
                    Contacts.Remove(contact);
                }));
            }
        } 

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}