using SnakeMobile.Database;

namespace SnakeMobile;

public partial class RegisterPage : ContentPage
{
    private readonly IUserRepo _repo;
    public RegisterPage(IUserRepo repo)
	{
		InitializeComponent();
        _repo = repo;
    }

    // Event handler for the Register button
    private async void OnRegisterBtnClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "All fields are required!", "OK");
            return;
        }

        // Call the Register method in the IUserRepo
        await _repo.Register(username, password);

        // Show success message and navigate back to the login page
        await DisplayAlert("Success", "Registration successful! Please log in.", "OK");
        await Navigation.PopAsync();
    }

    // Event handler for the Login button
    private async void OnLoginBtnClicked(object sender, EventArgs e)
    {
        // Navigate back to the login page
        await Navigation.PopAsync();
    }
}