using GAF.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GAF.Api.Data;

    public class AppDbContext : IdentityDbContext<User>
{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {

    }
    public DbSet<Category> Categories {get; set;}
    public DbSet<Report> Reports {get; set;}
    public DbSet<Transaction> Transactions {get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SeedDefaultUser(builder);
    }

    private static void SeedDefaultUser(ModelBuilder builder)
    {
              #region Populate Roles - Perfis de Usuário
        List<IdentityRole> roles = new()
        {
            new IdentityRole() {
               Id = "0b44ca04-f6b0-4a8f-a953-1f2330d30894",
               Name = "Administrador",
               NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole() {
               Id = "bec71b05-8f3d-4849-88bb-0e8d518d2de8",
               Name = "Usuário",
               NormalizedName = "USUÁRIO"
            },
        };
        builder.Entity<IdentityRole>().HasData(roles);
        #endregion

        #region Populate Usuário
        List<User> usuarios = new() {
            new User(){
                Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                Email = "raissa.stsborges@gmail.com",
                NormalizedEmail = "RAISSA.STSBORGES@GMAIL.COM",
                UserName = "RaissaBorges",
                NormalizedUserName = "RAISSABORGES",
                LockoutEnabled = true,
                EmailConfirmed = true,
                Nome = "Raíssa Borges",
            }
        };
        foreach (var user in usuarios)
        {
            PasswordHasher<IdentityUser> pass = new();
            user.PasswordHash = pass.HashPassword(user, "123456");
        }
        builder.Entity<User>().HasData(usuarios);
        #endregion

        #region Populate UserRole - Usuário com Perfil
        List<IdentityUserRole<string>> userRoles = new()
        {
            new IdentityUserRole<string>() {
                UserId = usuarios[0].Id,
                RoleId = roles[0].Id
            },
            new IdentityUserRole<string>() {
                UserId = usuarios[0].Id,
                RoleId = roles[1].Id
            },
        };
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        #endregion
    }

}

