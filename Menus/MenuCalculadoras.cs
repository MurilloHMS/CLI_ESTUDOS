namespace CLI_Estudos;

public class MenuCalculadoras
{
    public static void MostraMenu()
    {
        string opcoes;
        do
        {
            Console.Clear();
            Console.WriteLine("Selecione uma das opcoes Abaixo");
            Console.WriteLine("1. Calculadora de margem de lucro");
            Console.WriteLine("2. Calculadora de margem de lucro Mercado Livre");
            Console.WriteLine("3. Calculadora de produtos usados Mercado Livre");
            Console.WriteLine("4. Calculadora de Xml Alfa Transportes");

            Console.WriteLine("Digite S para sair");
            opcoes = Console.ReadKey(true).KeyChar.ToString();
            switch(opcoes)
            {
                case "1":
                    CalculadoraDeCustos.Run();
                    break;
                case "2":
                    CalculadoraDeCustosMercadoLivre.Run();
                    break;

                case "4":
                    CalculosNfeAlfaTransportes.Run();
                    break;
            }
        }while(opcoes != "s");
    }
}
