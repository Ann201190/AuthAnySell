using Auth.Business.Context;
using Auth.Business.Entities;
using Auth.Business.Enums;
using Auth.Business.Service.Interfaces;
using Auth.Business.SmtpClientEmailSender.Interfaces;
using Auth.Business.ViewModels;
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

            StringConfirm = "<html><head><style>@import url('https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@300;600;800&family=Nunito:ital,wght@0,900;1,900&display=swap');.btn {padding: 0;border: none;font: inherit;color: inherit;background-color: transparent;cursor: pointer;outline: none;display: inline-block;text-align: center;text-decoration: none;margin: 2px 0;border: solid 1px transparent;border-radius: 4px;padding: 0.5em 1em;color: rgba(254, 161, 22);background-color: #ffffff;}.btn:active {transform: translateY(1px);filter: saturate(150%);}.btn:hover,.btn:focus {color: #ffffff;border-color: currentColor;background-color: #0F172B;}.btn::-moz-focus-inner {border: none;}h5 {color: #0F172B;}p {color: black;}body {margin: auto;font-family: 'Nunito', sans-serif;font-family: 'Nunito Sans', sans-serif;}.content {height: 750px;width: 100%;background: rgba(254, 161, 22);font-size: 25px;}.info {text-align: center;padding-top: 50px;}.ii a[href] {color: white !important}.logo {margin-left: 30px;}</style></head><body>" + $"<div class=\"content\"><h1 class=\"logo\">AnySell</h1><div class=\"info\"><div>Thanks for signing up with AnySell!</div><div>Your login: {email}</div><div>Your password: {password}</div><div> You must follow this link to activate your account.</div><div><a href=\"{url}\" class=\"btn\">Activate your account</a></div></div></div>" + "</body></html>"; 
            await _emailSender.SendEmailAsync(email.ToLower(), "Confirm your account on AnySell", StringConfirm);

            var result = await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            return result;
        }

        public string HashPassword(string password)  // Шифрование пароля для записи в БД
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(checkSum).Replace("-", string.Empty);
        }

        public async Task<bool> ChangePassword(string userEmail, ChangePasswordModel request)
        {
            if (userEmail == null
                || request == null
                || string.IsNullOrEmpty(request.CurrentPassword)
                || string.IsNullOrEmpty(request.NewPassword))
            {
                return false;
            }
            var user = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Email == userEmail);
            var currentPassword = HashPassword(request.CurrentPassword);

            if (user == null || user.Confirm == false || user.Password != currentPassword)
            {
                return false;
            }

            user.Password = HashPassword(request.NewPassword);

            var result = await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            return result;
        }
    }
}
