namespace CLI_Estudos;

public class CalculadoraDeCustosMercadoLivre
{
    public static void Run()
    {
        string opcoes;
        do
        {
            Console.Clear();
            Console.WriteLine("Selecione uma opção abaixo");
            Console.WriteLine("1. Calculo Produto individual");
            Console.WriteLine("2. Calculo Lista de produtos");
            Console.WriteLine("Digite S para sair");

            opcoes = Console.ReadKey(true).KeyChar.ToString();
            switch(opcoes)
            {
                case "1":
                    CalculoProdutoIndividual();
                    break;
                case "2":

                    break;
            }

        }while(opcoes != "s");
    }

    public static void CalculoProdutoIndividual()
    {
        Console.Clear();
        Console.WriteLine("Digite o valor de custo do produto");
        double custo = double.Parse(Console.ReadLine());

        Console.Clear();
        Console.WriteLine("Digite a venda Desejada");
        double venda = double.Parse(Console.ReadLine());

        Console.Clear();
        Console.WriteLine("Deseja digitar a margem de lucro desejada? Margem sugerida 70%");
        string opcaoEscolhida = Console.ReadKey(true).KeyChar.ToString();
        double margemDesejada = 70;
        if (opcaoEscolhida is "s")
        {
            Console.Clear();
            Console.WriteLine("Digite a margem de lucro Desejada");
            margemDesejada = double.Parse(Console.ReadLine());
        }


        double lucroBruto = venda - custo;
        double margemDeLucro = (lucroBruto/venda) * 100;

        double vendaSugerida = custo / (1 - (margemDesejada / 100));
        double margemDeLucroSugerida = (vendaSugerida - custo) / vendaSugerida * 100;


        Console.Clear();
        Console.WriteLine("Resultados: ");
        Console.WriteLine($"Custo do produto: R$ {custo}");
        Console.WriteLine($"Valor de venda atual: R$ {venda}");
        Console.WriteLine($"Lucro bruto: R$ {lucroBruto}");
        Console.WriteLine($"Margem de lucro atual: {margemDeLucro}%");
        Console.WriteLine();
        Console.WriteLine($"Preço de venda sugerido: R$ {vendaSugerida:F2}");
        Console.WriteLine($"Lucro sugerido: R$ {(vendaSugerida - custo):F2}");
        Console.WriteLine($"Margem de lucro sugerida: {margemDeLucroSugerida:F2}%");

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}
