using Microsoft.EntityFrameworkCore;
using ProductManagement.Dal.Data;
using ProductManagement.Dal.Entity;


namespace ProductManagement.Bl
{
    public class UsersService : IUsersService
    {
        private ProductManagementContext _context;

        public UsersService(ProductManagementContext context)
        {
            _context = context;
        }

        public async Task<Users> CheckIn(string username, string password)
        {
            var user = await _context.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();
            if (user == null) throw new NotImplementedException();
            return user;
        }

        public async Task<Users> GetUsersByEmailAsync(string email)
        {
            var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            if(user == null) throw new NotImplementedException();
            return user;
        }
    }

    public interface IUsersService
    {
        Task<Users> GetUsersByEmailAsync(string email);
        Task<Users> CheckIn(string username,string password);
    }
}
