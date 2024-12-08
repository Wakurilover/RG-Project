using RandomG.Models;
using RandomG.Services;
using System.Linq;
using Plugin.Maui.Audio;

namespace RandomG;

public partial class GamePage : ContentPage
{
    private FirestoreService _firestoreService;
    private List<GameModel> _filteredGames;
    private string GameType;
    private readonly IAudioManager _audioManager;
    public GamePage(string categoriesID, string GameType)
    {
        InitializeComponent();
        
        // Initialize Firestore Service
        _firestoreService = new FirestoreService();
        this.GameType = GameType;
        // Load Games
        LoadGames(categoriesID);
        
    }

    private async void LoadGames(string categoriesID)
    {
        try
        {
            // Fetch games from Firestore
            var games = await _firestoreService.GetAllGame();

            // Filter games by CategoriesID
            _filteredGames = games.Where(x => x.CategoriesID == categoriesID).ToList();
            _filteredGames = _filteredGames.Where(x => x.GameTypeID.Contains(this.GameType)).ToList();

            // Bind games to the CollectionView
            GamesCollectionView.ItemsSource = _filteredGames;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load games: {ex.Message}", "OK");
        }
    }

    private async void OnRandomGameWithAnimationClicked(object sender, EventArgs e)
    {
        // Get the selected items from the CollectionView
        var selectedGames = GamesCollectionView.SelectedItems?.Cast<GameModel>().ToList();

        if (selectedGames == null || !selectedGames.Any())
        {
            RandomGameLabel.Text = "เลือกก่อนสิวะ.";
            return;
        }

        // Show and reset the ProgressBar
        ShuffleProgressBar.IsVisible = true;
        ShuffleProgressBar.Progress = 0;

        // Simulate shuffling animation
        for (int i = 0; i <= 100; i++)
        {
            ShuffleProgressBar.Progress = i / 100.0;
            await Task.Delay(10); // Delay for animation effect
        }

        // Hide ProgressBar after animation
        ShuffleProgressBar.IsVisible = false;

        // Randomly pick a game from the selected items
        Random random = new Random();
        var randomGame = selectedGames[random.Next(selectedGames.Count)];


        // Display the random game name
        RandomGameLabel.Text = $"-x-x-x-x-x-x-x จ๊ะเอ๋~ เล่น {randomGame.Name} นะ :3x-x-x-x-x-x-x-";

        
    }
}
