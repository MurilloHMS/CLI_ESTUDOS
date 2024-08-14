namespace CLI_Estudos;

public class MenuConversores
{
    public static void MostraMenu()
    {
        string opcoes;
        do
        {
            Console.Clear();
            Console.WriteLine("Selecione uma das opcoes Abaixo"); 
            Console.WriteLine("1. Conversor PDF para Excel");

            Console.WriteLine("Digite S para sair.");
            opcoes = Console.ReadKey(true).KeyChar.ToString();
            switch(opcoes)
            {
                case "1":
                  PdfToExcelConversor.Run();
                  break;

            }
        }while(opcoes != "s");
    }
}
