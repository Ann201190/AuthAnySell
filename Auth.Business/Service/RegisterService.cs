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
    public class RegisterService : IRegisterService
    {
        private readonly CustomDbContext _dbContext;
        private readonly IEmailSender _emailSender;

        public RegisterService(CustomDbContext dbContext, IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _emailSender = emailSender;
        }

        public async Task<bool> RegisterUserAsync(string email, string password)
        {
            var user = await _dbContext.Accounts.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                var StringConfirm = GenRandomString();
                await _dbContext.Accounts.AddAsync(new Account
                {
                    Email = email,
                    Password = HashPassword(password),
                    Role = Role.Manager,
                    StringConfirm = StringConfirm
                });

                var url = "http://localhost:4200/confirm/" + StringConfirm;
                //ссылка для подтверждения регистрации
                StringConfirm = $"<div class=\"content\"><p>Thanks for signing up with AnySell! You must follow this link to activate your account: </p><p><a href=\"{url}\" class=\"btn\">Activate your account</a></p></div>";// url;
                await _emailSender.SendEmailAsync("litvincevaann201190@gmail.com", "Confirm your account on AnySell", StringConfirm); 

                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }
        private string GenRandomString()
        {
            int length = 50;
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
        public string HashPassword(string password)  // Шифрование пароля для записи в БД
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(checkSum).Replace("-", string.Empty);
        }
    }
}
