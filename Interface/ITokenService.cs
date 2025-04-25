using CoffeeShopApi.Model;

namespace CoffeeShopApi.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}