using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace CLI_Estudos
{
    public class CalculosNfeAlfaTransportes
    {
        private static string _resultado;

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
                    Console.WriteLine($"{i + 4}: {Path.GetFileName(directories[i])}");
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
                    CalcularNotas(currentDirectory);
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
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                    }
                }
            }
        }

        private static void CalcularNotas(string currentDirectory)
        {
            string[] folderFiles = Directory.GetFiles(currentDirectory);
            StringBuilder resultado = new StringBuilder();
            decimal totalValorPrest = 0m;

            for (int i = 0; i < folderFiles.Length; i++)
            {
                var (resultadoArquivo, valorPrest) = ProcessarXML(folderFiles[i]);
                resultado.Append(resultadoArquivo);
                if (i < folderFiles.Length - 1)
                {
                    resultado.Append(" + ");
                }
                resultado.AppendLine();
                totalValorPrest += valorPrest;
            }

            resultado.AppendLine();
            resultado.AppendLine($"Total: {totalValorPrest.ToString("C", CultureInfo.CurrentCulture)}");
            Console.WriteLine(resultado.ToString());

            _resultado = resultado.ToString();

            Console.WriteLine();
            Console.WriteLine();
            if (!string.IsNullOrEmpty(_resultado))
            {
                Console.WriteLine("Deseja Imprimir os dados? [S/N]");
                string opcao = Console.ReadLine().ToLower();

                if (opcao == "s")
                {
                    ListarESelecionarImpressora();
                }
                else if (opcao == "n")
                {
                    Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
                }
            }
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }

        private static void ListarESelecionarImpressora()
        {
            Console.WriteLine("Impressoras disponíveis:");
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {PrinterSettings.InstalledPrinters[i]}");
            }

            Console.Write("Escolha uma impressora pelo número: ");
            string input = Console.ReadLine();
            int printerIndex;

            if (int.TryParse(input, out printerIndex) && printerIndex >= 1 && printerIndex <= PrinterSettings.InstalledPrinters.Count)
            {
                string printerName = PrinterSettings.InstalledPrinters[printerIndex - 1];
                ImprimirResultado(printerName);
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
        }

        private static void ImprimirResultado(string printerName)
        {
            PrintDocument printDoc = new PrintDocument
            {
                PrinterSettings = { PrinterName = printerName }
            };
            printDoc.PrintPage += PrintDocument_PrintPage;
            printDoc.Print();
        }

        private static void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font fonte = new Font("Arial", 8);
            RectangleF areaImpressao = e.MarginBounds;

            e.Graphics?.DrawString(_resultado, fonte, Brushes.Black, areaImpressao);
        }

        private static (string, decimal) ProcessarXML(string filePath)
        {
            try
            {
                XDocument xmldoc = XDocument.Load(filePath);
                XNamespace ns = "http://www.portalfiscal.inf.br/cte";

                var vPrest = xmldoc.Descendants(ns + "vPrest").FirstOrDefault();
                decimal vTPrest = 0m;

                if (vPrest != null)
                {
                    string vTPrestString = vPrest.Element(ns + "vTPrest")?.Value;
                    vTPrest = !string.IsNullOrEmpty(vTPrestString) && decimal.TryParse(vTPrestString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal vTPrestValue) ? vTPrestValue : 0m;
                    return ($"{vTPrest:C} ", vTPrest);
                }
                else
                {
                    return ("", 0m);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
