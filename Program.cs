using System;
using System.IO;
using System.Collections.Generic;

namespace CLI_Estudos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("Seja Bem Vindo a CLI de estudos");
                Console.WriteLine("Feito para aprimorar conhecimentos em c#");
                Console.WriteLine("Selecione uma opçao no menu abaixo");
                Console.WriteLine("1. Programas");
                Console.WriteLine("2. Configurações");
                Console.WriteLine("3. Logs");
                Console.WriteLine("Digite S para sair");

                opcao = Console.ReadKey(true).KeyChar.ToString().ToLower();
                
                switch (opcao)
                {
                    case "1":
                    MenuProgramas.MostraMenu();
                        break;
                }
            } while (opcao != "s");

            
        }
    }
}

