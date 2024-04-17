using MauiApp1.Model;
using MauiApp1.ViewModel;
using System.Collections.ObjectModel;

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

        public void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string text = e.NewTextValue;
            viewModel.FilterContacts(text);
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is ContactLastMessage selectedContact)
            {
                string route = $"///ChatPage?chatId={selectedContact.ChatId}";
                await Shell.Current.GoToAsync(route);

                ((CollectionView)sender).SelectedItem = null;
            }
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            viewModel.RefreshContacts("");
        }
    }

}
