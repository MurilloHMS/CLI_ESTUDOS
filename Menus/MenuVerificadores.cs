namespace CLI_Estudos
{
    class MenuVerificadores
    {
        public static void MostraMenu()
        {
            string opcoes;
            do
            {
                Console.Clear();
                Console.WriteLine("Selecione uma das opcoes abaixo");
                Console.WriteLine("1. Verificar XML Veiculos");
                Console.WriteLine("2. Alterar Nomes De notas");

                Console.WriteLine("Digite S para sair");

                opcoes = Console.ReadKey(true).KeyChar.ToString();

                switch(opcoes)
                {
                    case "1":
                        VerificarNotasVeiculos.Run();
                        break;
                    case "2":
                        AlterarNomeDeArquivos.Run();
                        break;
                }
            } while (opcoes != "s");
        }
    }
}