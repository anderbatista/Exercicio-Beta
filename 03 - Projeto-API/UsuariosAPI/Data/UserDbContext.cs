using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using UsuariosAPI.Models;

namespace UsuariosAPI.Data
{
    public class UserDbContext : IdentityDbContext<CustomIdentityUser, IdentityRole<int>, int>
    {
        public UserDbContext(DbContextOptions<UserDbContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CustomIdentityUser>()
                .HasIndex(b => b.CPF)
                .IsUnique();

            CustomIdentityUser admin = new CustomIdentityUser()
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@cleartech.com",
                NormalizedEmail = "ADMIN@CLEARTECH.COM",
                EmailConfirmed = false,
                CPF = "00000000000",
                Cep = "000000000",
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 1,
                DataCriacao = DateTime.Now,
                DataNascimento = DateTime.Now,
                Status = true,
                Logradouro = "logradouro",
                Bairro = "bairro",
                Numero = 000,
                Complemento = "complemento"
            };
            CustomIdentityUser lojista = new CustomIdentityUser()
            {
                UserName = "lojista",
                NormalizedUserName = "LOJISTA",
                Email = "lojista@cleartech.com",
                NormalizedEmail = "LOJISTA@CLEARTECH.COM",
                EmailConfirmed = false,
                CPF = "11111111111",
                Cep = "111111111",
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 100,
                DataCriacao = DateTime.Now,
                DataNascimento = DateTime.Now,
                Status = true,
                Logradouro = "logradouro",
                Bairro = "bairro",
                Numero = 000,
                Complemento = "complemento"
            };
            CustomIdentityUser cliente = new CustomIdentityUser()
            {
                UserName = "cliente",
                NormalizedUserName = "CLIENTE",
                Email = "cliente@cleartech.com",
                NormalizedEmail = "CLIENTE@CLEARTECH.COM",
                EmailConfirmed = false,
                CPF = "22222222222",
                Cep = "222222222",
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 1000,
                DataCriacao = DateTime.Now,
                DataNascimento = DateTime.Now,
                Status = true,
                Logradouro = "logradouro",
                Bairro = "bairro",
                Numero = 000,
                Complemento = "complemento"
            };
            PasswordHasher<CustomIdentityUser> hasher = new PasswordHasher<CustomIdentityUser>();

            admin.PasswordHash = hasher.HashPassword(admin, "Senha@123");
            lojista.PasswordHash = hasher.HashPassword(lojista, "Senha@123");
            cliente.PasswordHash = hasher.HashPassword(cliente, "Senha@123");

            builder.Entity<CustomIdentityUser>().HasData(admin);
            builder.Entity<CustomIdentityUser>().HasData(lojista);
            builder.Entity<CustomIdentityUser>().HasData(cliente);

            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Id = 1,
                Name = "admin",
                NormalizedName = "ADMIN"
            });
            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Id = 100,
                Name = "lojista",
                NormalizedName = "LOJISTA"
            });
            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Id = 1000,
                Name = "cliente",
                NormalizedName = "CLIENTE"
            });
            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 1,
                UserId = 1
            });
            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 100,
                UserId = 100
            });
            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 1000,
                UserId = 1000
            });
        }
    }
}
