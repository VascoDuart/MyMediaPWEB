using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyMedia.Data.Models;
using System.Threading.Tasks;

namespace MyMedia.Data.Seed {
    public static class DbInitializer {
        public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider) {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


            string[] roleNames = { "Administrador", "Funcionario", "Cliente", "Fornecedor" };

            foreach (var roleName in roleNames) {
                if (!await roleManager.RoleExistsAsync(roleName)) {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }


            var adminEmail = "admin@mymedia.pt";
            if (await userManager.FindByEmailAsync(adminEmail) == null) {
                var adminUser = new ApplicationUser {
                    UserName = adminEmail,
                    Email = adminEmail,
                    NomeCompleto = "Administrador Master",
                    IsAtivo = true 
                };

                var result = await userManager.CreateAsync(adminUser, "P@ssword123");

                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(adminUser, "Administrador");
                }
            }


            var funcEmail = "funcionario@mymedia.pt";
            if (await userManager.FindByEmailAsync(funcEmail) == null) {
                var funcUser = new ApplicationUser {
                    UserName = funcEmail,
                    Email = funcEmail,
                    NomeCompleto = "Funcionário Loja",
                    IsAtivo = true
                };

                var result = await userManager.CreateAsync(funcUser, "P@ssword123");

                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(funcUser, "Funcionario");
                }
            }


            var fornecedorEmail = "fornecedor@test.pt";
            if (await userManager.FindByEmailAsync(fornecedorEmail) == null) {
                var fornecedorUser = new ApplicationUser {
                    UserName = fornecedorEmail,
                    Email = fornecedorEmail,
                    NomeCompleto = "Fornecedor de Exemplo",
                    IsAtivo = false
                };

                var result = await userManager.CreateAsync(fornecedorUser, "P@ssword123");

                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(fornecedorUser, "Fornecedor");
                }
            }
        }
    }
}