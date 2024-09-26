namespace CLI_Estudos;

public class MenuProgramas
{
    public static void MostraMenu()
    {
        string opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("Selecione uma opçao no menu abaixo");
            Console.WriteLine("1. Calculadoras");
            Console.WriteLine("2. Conversores");
            Console.WriteLine("3. Geradores");
            Console.WriteLine("4. Jogos");
            Console.WriteLine("5. Simuladores");
            Console.WriteLine("6. Sistemas");
            Console.WriteLine("7. Gerenciadores");
            Console.WriteLine("8. Verificadores"); 
            Console.WriteLine("Digite S para sair");
 
            opcao = Console.ReadKey(true).KeyChar.ToString().ToLower();
            switch (opcao)
            {
                case "1":
                    MenuCalculadoras.MostraMenu();
                    break;
                case "2":
                    MenuConversores.MostraMenu();    
                    break;
                case "4":
                    MenuJogos.MostraMenu();
                    break;

                case "6" :
                    MenuSistemas.MostraMenu();
                    break;

                case "8":
                    MenuVerificadores.MostraMenu();
                    break;
            }
        } while (opcao != "s");
    }
}
