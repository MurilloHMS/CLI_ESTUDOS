using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using Newtonsoft.Json;
using System.Net.NetworkInformation;

namespace CLI_Estudos
{
    public class AlterarNomeDeArquivos
    {
        public static void Run()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Navegador de Diretórios");
                Console.WriteLine("------------------------");
                Console.WriteLine($"Diretório Atual: {currentDirectory}");
                Console.WriteLine("0: Selecionar este diretório");
                Console.WriteLine("1: Subir um nível");
                Console.WriteLine("2: Navegar para um diretório");
                Console.WriteLine("3: Sair");
                Console.WriteLine();

                string[] directories = Directory.GetDirectories(currentDirectory);
                string[] files = Directory.GetFiles(currentDirectory);

                Console.WriteLine("Diretórios");
                for (int i = 0; i < directories.Length; i++)
                {
                    Console.WriteLine($"{i + 4} : {Path.GetFileName(directories[i])}");
                }

                Console.WriteLine("\nArquivos:");
                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"  {Path.GetFileName(files[i])}");
                }

                Console.Write("\nEscolha uma opção: ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.WriteLine($"Você selecionou o diretório: {currentDirectory}");
                    IdentificarNotas(currentDirectory);
                    Console.ReadKey();
                    break;
                }
                else if (input == "1")
                {
                    currentDirectory = Directory.GetParent(currentDirectory)?.FullName ?? currentDirectory;
                }
                else if (input == "2")
                {
                    Console.Write("Digite o nome do diretório para navegar: ");
                    string newDir = Console.ReadLine();
                    string path = Path.Combine(currentDirectory, newDir);

                    if (Directory.Exists(path))
                    {
                        currentDirectory = path;
                    }
                    else
                    {
                        Console.WriteLine("Diretório não encontrado!");
                        Console.ReadKey();
                    }
                }
                else if (input == "3")
                {
                    break;
                }
                else
                {
                    int index;
                    if (int.TryParse(input, out index) && index >= 4 && index < directories.Length + 4)
                    {
                        currentDirectory = directories[index - 4];
                    }
                    else
                    {
                        Console.WriteLine("Opção invalida!");
                        Console.ReadKey();
                    }
                }
            }
        }

        private static async void IdentificarNotas(string currentDirectory)
        {
            string[] folderFiles = Directory.GetFiles(currentDirectory).Where(file => file.EndsWith(".pdf")).ToArray();
            var indices = await ObterIndices();
            foreach (string file in folderFiles)
            {
                AlterarNomesNotas(file, indices);
            }
            Console.WriteLine("Notas identificadas com sucesso");
        }

        private static void AlterarNomesNotas(string filePath, IEnumerable<DadosNota> indices)
        {            
            foreach (var nota in indices) 
            {
                var nomeDaNota = Path.GetFileNameWithoutExtension(filePath);
                if (nomeDaNota.Equals(nota.Identificador))
                {
                    if (!Directory.Exists(@"D:\dados\notas"))
                    {
                        Directory.CreateDirectory(@"D:\dados\notas");
                    }

                    File.Copy(filePath, @"D:\dados\notas\" + "NFe_"+ nota.NumeroNFe + ".pdf");
                }
            }
            
        }

        private static async Task<IEnumerable<DadosNota>> ObterIndices()
        {
            var dados = new List<DadosNota>(); 
            Console.Write("Digite o path do arquivo excel para ler: ");
            string path = Console.ReadLine();

            if(!File.Exists(path))
            {
                Console.WriteLine("arquivo inesistente");
                return new List<DadosNota>();
            }
            Console.WriteLine("obtendo dados do arquivo");
            using(XLWorkbook wb = new XLWorkbook(path))
            {
                var planilha = wb.Worksheet(1);
                var fileData = planilha.RowsUsed()
                    .Skip(1)
                    .Select(row => new DadosNota
                    {
                        Identificador = row.Cell(1).TryGetValue<string>(out var identificador) ? identificador : null,
                        NumeroNFe = row.Cell(2).TryGetValue<string>(out var numeroNFe) ? numeroNFe : null
                    }).ToList();

                lock (dados)
                {
                    dados.AddRange(fileData);
                }
                         
            }
            return dados;
        }
    }

    public class DadosNota
    {
        public string Identificador {  get; set; }
        public string NumeroNFe { get; set; }
    }

}

