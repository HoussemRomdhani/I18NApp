using Models;
using Repositories;

namespace Services;

public class UserService
{
    private readonly UsersRepository usersRepository;
    public UserService(UsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }

    public async Task<string> GetUserCultureOrDefault()
    {
        User? user = await usersRepository.GetUser();

        string culture = user != null ? user.Culture : CultureDefaults.CultureFR;

        string result  = !string.IsNullOrWhiteSpace(culture) ? culture : CultureDefaults.CultureFR;

        return result;
    }
}
