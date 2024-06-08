namespace CLI_Estudos;

public class MenuJogos
{
    public static void MostraMenu()
    {
        string opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("1. Jogo Da Velha");
            Console.WriteLine("2. Jogo Da Cobrinha");
            Console.WriteLine("Digite S para voltar ao menu anterior");

            opcao = Console.ReadKey(true).KeyChar.ToString().ToLower();
            switch(opcao)
            {
                case "1":
                    new JogoDaVelha().Iniciar();
                    break;
            }
        }while(opcao != "s");
    }
}
