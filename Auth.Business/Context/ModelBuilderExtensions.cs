using Auth.Business.Entities;
using Auth.Business.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Auth.Business.Context
{

    public static class ModelBuilderExtensions
    {     
         public static void Seed(this ModelBuilder modelBuilder)
         {
            // уникальность email
            modelBuilder.Entity<Account>()
              .HasIndex(a => a.Email)
              .IsUnique();
         }

        public static void Initialize(CustomDbContext dbContext)
        {
            if (!dbContext.Accounts.Any())
            { 
            var cashier = new Account
            {
                Id = new Guid("BFBC7481-FB3C-4192-A093-519F40F1B811"),
                Email = "litvinceva@gmail.com",
                Password = "25D55AD283AA400AF464C76D713C07AD",
                Role =  Role.Cashier ,
                StringConfirm ="DGDRRGERGRGRGHTRHB1"
            };

            var manager = new Account
            {
                Id = new Guid("0c0da525-f888-4222-995c-fa1dd0f00cbc"),
                Email = "admin@gmail.com",
                Password = "5E8667A439C68F5145DD2FCBECF02209",
                Role = Role.Manager ,
                StringConfirm = "FERUIJIOJIJJNDFJBJGB"
            };

                //заполнение базы данных        
                dbContext.Accounts.Add(cashier);
                dbContext.Accounts.Add(manager);

                //сохраняем изменения
                dbContext.SaveChanges();
            }
        }
    }
}
