using SnakeMobile.Database;

namespace SnakeMobile;

public partial class LoginPage: ContentPage
{
    private readonly IUserRepo _repo;

    // Constructor: Use dependency injection for the UserRepo
    public LoginPage(IUserRepo repo)
    {
        
        InitializeComponent();
        _repo = repo; // Store reference to the repo
    }

    public async void OnRegisterBtnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage(_repo));
    }

    public async void OnLoginBtnClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text;
        var pass = PasswordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pass))
        {
            await DisplayAlert("Error", "All fields are required!", "Ok");
            return;
        }

        var user = await _repo.Login(username, pass);
        if (user != null)
        {
            // Store username securely
            await SecureStorage.SetAsync("username", user.Username);

            // Show success message
            await DisplayAlert("Success", "You have been logged in", "Ok");

            // Navigate to the MainPage
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            // Show error if user not found
            await DisplayAlert("Error", "Invalid username or password.", "Ok");
        }
    }
}