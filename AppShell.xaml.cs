using RandomG.Services;
using RandomG.ViewModels;

namespace RandomG
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            var firestoreService = new FirestoreService();
            BindingContext = new RandomGViewModel(firestoreService);

        }
    }
}
