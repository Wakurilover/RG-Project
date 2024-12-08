namespace RandomG;

public partial class TypePage : ContentPage
{
    public TypePage()
    {
        InitializeComponent();
    }
    private async void OnMobileGameButtonClicked(object sender, EventArgs e)
    {
        // Navigate to CategoryPage for Mobile Games
        await Navigation.PushAsync(new CategoryPage("MOBILE"));
    }

    private async void OnPcGameButtonClicked(object sender, EventArgs e)
    {
        // Navigate to CategoryPage for PC Games
        await Navigation.PushAsync(new CategoryPage("PC"));
    }

}