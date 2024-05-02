using System.Collections.ObjectModel;
using MauiApp1.Model;
using MauiApp1.ViewModel;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel viewModel;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            this.BindingContext = viewModel;
        }

        public void OnSearchBarTextChanged(object sender, TextChangedEventArgs eventArguments)
        {
            string text = eventArguments.NewTextValue;
            viewModel.FilterContacts(text);
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs eventArguments)
        {
            if (eventArguments.CurrentSelection.FirstOrDefault() is ContactLastMessage selectedContact)
            {
                string route = $"///ChatPage?chatId={selectedContact.ChatId}";
                await Shell.Current.GoToAsync(route);

                ((CollectionView)sender).SelectedItem = null;
            }
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs arguments)
        {
            base.OnNavigatedTo(arguments);

            viewModel.RefreshContacts(string.Empty);
        }
    }
}
