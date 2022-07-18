using Auth.Business.Context;
using Auth.Business.Entities;
using Auth.Business.Enums;
using Auth.Business.Service.Interfaces;
using Auth.Business.SmtpClientEmailSender.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Business.Service
{
    public class RegistrationService : IRegistrationService
    {
        private readonly CustomDbContext _dbContext;
        private readonly IEmailSender _emailSender;

        public RegistrationService(CustomDbContext dbContext, IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _emailSender = emailSender;
        }

        public async Task<bool> RegistrationManagerAsync(string email, string password)
        {
            var user = await _dbContext.Accounts.SingleOrDefaultAsync(u => u.Email == email.ToLower());

            if (user == null)
            {
                return await RegistrationAll(email, password, Role.Manager);
            }
            return false;
        }
        private string GenerationRandomString(int lengthString)
        {
            int length = lengthString;
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(length - 1);
            int Position = 0;

            for (int i = 0; i < length; i++)
            {
                Position = rnd.Next(0, alphabet.Length - 1);
                sb.Append(alphabet[Position]);             
            }
            return sb.ToString();
        }

        public async Task<bool> RegistrationCashierAsync(string email)
        {  
            var password = GenerationRandomString(8);

            var user = await _dbContext.Accounts.SingleOrDefaultAsync(u => u.Email == email.ToLower());

            if (user == null)
            {
                return await RegistrationAll(email, password, Role.Cashier);
            }
            return false;
        }

       private async Task<bool> RegistrationAll(string email, string password, Role role)
        {
            var StringConfirm = GenerationRandomString(50);
            await _dbContext.Accounts.AddAsync(new Account
            {
                Email = email.ToLower(),
                Password = HashPassword(password),
                Role = role,
                StringConfirm = StringConfirm
            });

            var url = "http://localhost:4200/confirm/" + StringConfirm;

            //ссылка для подтверждения регистрации
             StringConfirm = $"<div class=\"content\"><h1 class=\"logo\">AnySell</h1><div class=\"info\"><div>Thanks for signing up with AnySell!</div><div>Your login: {email}</div><div>Your password: {password}</div><div> You must follow this link to activate your account.</div><div><a href=\"{url}\" class=\"btn\">Activate your account</a></div></div></div>";
            await _emailSender.SendEmailAsync("vangogie91@gmail.com", "Confirm your account on AnySell", StringConfirm);

            var result = await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            return result;
        }

        public string HashPassword(string password)  // Шифрование пароля для записи в БД
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(checkSum).Replace("-", string.Empty);
        }
    }
}
