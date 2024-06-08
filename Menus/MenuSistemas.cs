namespace CLI_Estudos;

public class MenuSistemas
{

    public static void MostraMenu()
    {
        string opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("Selecione uma opçao no menu abaixo");
            Console.WriteLine("1. Cadastro De Usuario");
            Console.WriteLine("Digite S para voltar ao menu anterior"); 
            
             opcao = Console.ReadKey(true).KeyChar.ToString().ToLower();
            switch (opcao)
            {
                case "1" :
                    new CadastroUsuario();
                    break;
            }
        } while (opcao != "s");
    }
}
