﻿using Auth.Business.Context;
using Auth.Business.Entities;
using Auth.Business.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Auth.Business.Service
{
    public  class AuthService: IAuthService
    {

        private readonly CustomDbContext _dbContext;
        public readonly IRegistrationService _registrationService;
        public AuthService(CustomDbContext dbContext, IRegistrationService registrationService)
        {
            _dbContext = dbContext;
            _registrationService = registrationService;
            ModelBuilderExtensions.Initialize(dbContext);
            var builder = new DbContextOptionsBuilder<CustomDbContext>();
        }

        public async Task<Account> AuthenticateUserAsync(string email, string password)
        {
            var hashPassword = _registrationService.HashPassword(password);
            return await _dbContext.Accounts.SingleOrDefaultAsync(u => u.Email == email.ToLower() && u.Password == hashPassword && u.Confirm == true);
        }

        public async Task<bool> ConfirmUserAsync(string stringConfirm) {
             var user  =  await _dbContext.Accounts.SingleOrDefaultAsync(u => u.StringConfirm == stringConfirm);
            if (user!=null)
            {
                user.Confirm = true;
                user.StringConfirm = "Confirmed";

                return await _dbContext.SaveChangesAsync() >= 0 ? true : false; 
            }

            return false;
        }
    }
}
