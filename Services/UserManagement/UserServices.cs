using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotionCream.API.Data;
using LotionCream.API.Models.Dtos;
using LotionCream.API.Models.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace LotionCream.API.Services.UserManagement
{
    public class UserServices : IUserServices
    {
        private readonly LotionCreamDBContext _lotionCreamDbContext;
        public UserServices(LotionCreamDBContext lotionCreamDbContext)
        {
            _lotionCreamDbContext = lotionCreamDbContext;
        }
        
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var allUsers = await _lotionCreamDbContext.Users.ToListAsync();
           
                if(allUsers.Count() > 0){
                 var userDto = new List<UserDto>();
                    userDto.AddRange(allUsers.OrderBy(a =>a.RegisterDate).Select(x => new UserDto(){
                    UserID = x.UserID,  
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username,
                    Gender = x.Gender,
                    SkinColor = x.SkinColor,
                    Age = x.Age,
                    EmailAddress = x.EmailAddress,
                    IsActive = x.IsActive,
                    FullName = x.FullName,
                    RegisterDate = x.RegisterDate
                    }));
                   return userDto;
                }
                return null;
        }
    
        public async Task<IEnumerable<UserDto>> GetAllUsersByGender(string gender)
        {
           var allUserByGender = await _lotionCreamDbContext.Users.Where(x=>x.Gender.ToString()==gender).ToListAsync();
           var userByGenderDto = new List<UserDto>();
           if(allUserByGender.Count() > 0){
                 userByGenderDto.AddRange(allUserByGender.OrderBy(a =>a.RegisterDate).Select(x => new UserDto(){
                    UserID = x.UserID,  
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username,
                    Gender = x.Gender,
                    SkinColor = x.SkinColor,
                    Age = x.Age,
                    EmailAddress = x.EmailAddress,
                    IsActive = x.IsActive,
                    FullName = x.FullName,
                    RegisterDate = x.RegisterDate
                    }));
                   return userByGenderDto;
           }
           return null;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersBySkinColor(string color)
        {
            var userByColor = await _lotionCreamDbContext.Users.Where(x=>x.SkinColor.ToString() == color).ToListAsync();
              var userByUsernameDto = new List<UserDto>();
           if(userByColor.Count() > 0)
            {
                 userByUsernameDto.AddRange(userByColor.OrderBy(a =>a.RegisterDate).Select(x => new UserDto(){
                   UserID = x.UserID,  
                   FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username,
                    Gender = x.Gender,
                    SkinColor = x.SkinColor,
                    Age = x.Age,
                    EmailAddress = x.EmailAddress,
                    IsActive = x.IsActive,
                    FullName = x.FullName,
                    RegisterDate = x.RegisterDate
                    }));
                   return userByUsernameDto;
            }
            return null;
        }

        public async Task<UserDto> GetUserByUserName(string username)
        {
            var x = await _lotionCreamDbContext.Users.Where(a=>a.Username == username).FirstOrDefaultAsync();
            if(x != null)
            {
                var userByUsernameDto = new UserDto(){
                    UserID = x.UserID,  
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username,
                    Gender = x.Gender,
                    SkinColor = x.SkinColor,
                    Age = x.Age,
                    EmailAddress = x.EmailAddress,
                    IsActive = x.IsActive,
                    FullName = x.FullName,
                    RegisterDate = x.RegisterDate
                    };
                   return userByUsernameDto;
            }
            return null;
        }
        public async Task<UserDto> GetUserByID(long ID)
        {
           var userById = await _lotionCreamDbContext.Users.FirstOrDefaultAsync(x=>x.UserID == ID);
           if(userById != null)
           {
               var userDto = new UserDto()
               {
                     UserID = userById.UserID,  
                    FirstName = userById.FirstName,
                    LastName = userById.LastName,
                    Username = userById.Username,
                    Gender = userById.Gender,
                    SkinColor = userById.SkinColor,
                    Age = userById.Age,
                    EmailAddress = userById.EmailAddress,
                    IsActive = userById.IsActive,
                    FullName = userById.FullName,
                    RegisterDate= userById.RegisterDate
               };
               return userDto;
           }
           return null;
        }

        public async Task<User> DeleteUser(long ID)
        {
            var getUserToRemove = await _lotionCreamDbContext.Users.FindAsync(ID);
            if(getUserToRemove != null){
                _lotionCreamDbContext.Remove(getUserToRemove);
                await _lotionCreamDbContext.SaveChangesAsync();
                return getUserToRemove;
            }
            return null;
        }

        public async Task UpdateUser(UserDto user)
        {
            var userToUpdate = await _lotionCreamDbContext.Users.FindAsync(user.UserID);
            if(userToUpdate != null)
            {
                _lotionCreamDbContext.Entry(userToUpdate).State = EntityState.Modified;
                await _lotionCreamDbContext.SaveChangesAsync();
            
            }
        }



    
        public async Task<User> Login(string username, string password)
        {
           try
           {
               //Get the user
               var logUser = await _lotionCreamDbContext.Users
                            .FirstOrDefaultAsync(x=>x.Username == username);
               if(logUser == null)
               {
                   return null;
               }
                    
            //check password
               if(!VerifyPasswordHash(password, logUser.PasswordHash, logUser.PasswordSalt))
                   {
                        return null;
                   }
            //Authentication is succefull
                return logUser; 
           }
           catch (Exception)
           {
              return null;
           }

        }
       private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
       {
           using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
           {
               var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               for(int i = 0; i < computedHash.Length; i++)
               {
                  if(computedHash[i] != passwordHash[i])
                        return false;
               }
               return true;
           }
       }
// MEMBER REGISTRATION ACTION
        public async Task<User> Register(User register, string password)
        {
            if(String.IsNullOrEmpty(register.EmailAddress)){
                throw new ArgumentNullException("Email cannot be empty");
            }
             if(String.IsNullOrEmpty(register.Username)){
                throw new ArgumentNullException("username cannot be empty");
            }
            //PASSWORD ENCRYPTION
            byte[] passwordHash, passwordSalt;
            CreatePasswordEncrypt(password, out passwordHash, out passwordSalt);

            register.PasswordHash = passwordHash;
            register.PasswordSalt = passwordSalt;

            await _lotionCreamDbContext.Users.AddAsync(register);
            await _lotionCreamDbContext.SaveChangesAsync();
            return register;
        }
//CREATION OF PASSWORD ENCRYPTION
        private void CreatePasswordEncrypt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using(var hmac = new System.Security.Cryptography.HMACSHA512())
           {
               passwordSalt = hmac.Key;
               passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }
        }

//CHECKING IF USER EXIST ALREADY
        public async Task<bool> UserExists(string username, string emailAddress)
        {
            if(await _lotionCreamDbContext.Users
                        .AnyAsync(x => x.Username == username && x.EmailAddress == emailAddress))
            {
                return true;
            }
            return false;
        }
//DISPOSING ALL INSTANCES CREATED 
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserServices() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}