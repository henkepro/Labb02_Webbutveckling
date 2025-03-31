namespace Labb02_Webbutveckling.Middleware;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Labb02_Webbutveckling.Model;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public Customer LoggedInUser { get; private set; }
    public bool IsLoggedIn => LoggedInUser != null;
    public bool IsAdmin => LoggedInUser != null && LoggedInUser.IsAdmin;
    public int UserId => LoggedInUser != null ? LoggedInUser.Id : 0;
    public ShoppingCart UserCart { get; private set; }

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task Logout()
    {
        if(!IsLoggedIn) return;

        await _httpClient.PostAsJsonAsync("https://localhost:7262/api/auth/logout", LoggedInUser);
        LoggedInUser = null;
        UserCart = null;
    }

    public async Task<bool> Login(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7262/api/auth/login", new
        {
            Email = email.Trim(),
            Password = password.Trim()
        });

        if(!response.IsSuccessStatusCode) return false;

        LoggedInUser = await response.Content.ReadFromJsonAsync<Customer>();

        if(LoggedInUser == null) return false;

        await LoadUserCart();
        return true;
    }

    private async Task LoadUserCart()
    {
        if(!IsLoggedIn) return;

        var response = await _httpClient.GetAsync($"https://localhost:7262/api/fetch/shoppingcart/{UserId}");

        if(response.IsSuccessStatusCode)
        {
            UserCart = await response.Content.ReadFromJsonAsync<ShoppingCart>();
        }
        else
        {
            UserCart = await CreateNewCart();
        }
    }

    private async Task<ShoppingCart> CreateNewCart()
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7262/api/add/shoppingcart/create", new ShoppingCart
        {
            CustomerId = UserId,
            ShoppingCartProducts = new List<ShoppingCartProduct>()
        });

        if(response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ShoppingCart>();
        }

        return null;
    }
}
