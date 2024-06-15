namespace CLI_Estudos;

public class CalculadoraDeCustos
{
    public static void Run()
    {
        Console.Clear();
        Console.Write("Digite o valor de custo do produto: ");
        double custo = double.Parse(Console.ReadLine());
        Console.Clear();
        Console.Write("Digite o valor de venda do produto: ");
        double venda = double.Parse(Console.ReadLine());
        Console.Clear();
        double lucroBruto = venda - custo;
        double margemLucro = (lucroBruto/venda) * 100;
        Console.WriteLine($"Foi informado R$ {custo} de custo e R$ {venda} como valor de venda.");
        Console.WriteLine($"O lucro Bruto é R$ {lucroBruto} e a margem {margemLucro}%");
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}
