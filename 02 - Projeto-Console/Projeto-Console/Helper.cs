using System;
using System.Text.RegularExpressions;

namespace Projeto_CRUD
{
    internal static class Helper
    {
        public static bool ValidaNome(string validador)
        {
            if (Regex.IsMatch(validador, "^[a-zA-Zà-úÀ-Ú' ']+$") && validador.Length <= 128)
            {
                return true;
            }
            return false;
        }

        public static void FinalMetodo()
        {
            Console.Write("\nPressione qualquer tecla...");
            Console.ReadLine();
            Console.Clear();
        }

    }

}