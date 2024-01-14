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

    public async Task<string> GetUserCulture()
    {
        User? user = await usersRepository.GetUser();

        return user == null ? string.Empty : user.Culture;
    }
}
