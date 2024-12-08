using RandomG.Models;
using RandomG.Services;
using System.Collections.ObjectModel;

namespace RandomG;

public partial class CategoryPage : ContentPage
{
    private readonly FirestoreService _firestoreService;
    public List<CategoriesModel> Categories { get; set; }

    public CategoriesModel SelectedCategory { get; set; }

    public string GameType { get; set; }
    public CategoryPage(string GameType)
    {
        InitializeComponent();
        _firestoreService = new FirestoreService();
        Categories = new List<CategoriesModel>();
        this.GameType = GameType;

        LoadCategories();
    }

    private async Task LoadCategories()
    {
        try
        {
            this.Categories = await _firestoreService.GetAllCategories();
            CategoriesCollectionView.ItemsSource = Categories;

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load categories: {ex.Message}", "OK");
        }
    }

    private void OnCategorySelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            SelectedCategory = e.CurrentSelection[0] as CategoriesModel;
        }
    }

    private async void OnNavigateToGamePageClicked(object sender, EventArgs e)
    {
        if (SelectedCategory != null)
        {
            // Pass the Category.ID to the GamePage
            await Navigation.PushAsync(new GamePage(SelectedCategory.ID,this.GameType));
        }
    }
}