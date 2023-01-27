using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class ValidaUserService
    {
        private SignInManager<CustomIdentityUser> _signInManager;
        public ValidaUserService(SignInManager<CustomIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public static bool ValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            Console.WriteLine("Tamanho: " + cpf.Length);
            if (cpf.Length != 11) { return false; }
            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }
            resto = soma % 11;
            if (resto < 2) { resto = 0; }
            else { resto = 11 - resto; }
            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }
            resto = soma % 11;
            if (resto < 2) { resto = 0; }
            else { resto = 11 - resto; }
            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }
        public static bool ValidaDataNascimento(DateTime dataNascimento)
        {
            if (dataNascimento > DateTime.Today)
            {
                return false;
            }
            return true;
        }
    }
}
