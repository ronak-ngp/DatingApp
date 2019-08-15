using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DummyClass : IAuthRespository
    {
        private readonly DataContext _context;
        public DummyClass(DataContext context)
        {
            _context = context;
        }
        public async Task<UserDatingApp> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == userName );

            Console.WriteLine("In dummy method");

            if(user == null)
                return null;
            if(!VerifyPasswordHash(password, user.PassowrdHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passowrdHash, byte[] passwordSalt)
        {
            using(var hMac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hMac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i =0;i < computeHash.Length;i++)
                {
                    if(computeHash[i] != passowrdHash[i])
                    {
                        return false;
                    }
                }

            }

             Console.WriteLine("In dummy method");

            return true;
        }

        public async Task<UserDatingApp> Register(UserDatingApp user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PassowrdHash = passwordHash;
            user.PasswordSalt = passwordSalt;

           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();

            Console.WriteLine("In dummy method");

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hMac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hMac.Key;
                passwordHash = hMac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

             Console.WriteLine("In dummy method");
        }

        public async Task<bool> UserExists(string userName)
        {
             Console.WriteLine("In dummy method");
            if(await _context.Users.AnyAsync(x => x.Username == userName))
              return true;

              return false;
              
        }
    }
}