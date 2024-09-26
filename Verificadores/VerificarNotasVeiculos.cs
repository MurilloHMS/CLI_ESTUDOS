using System;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace CLI_Estudos
{
    public class VerificarNotasVeiculos
    {
        public static void Run()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            while(true)
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
                for(int i = 0; i < directories.Length; i++)
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

                if(input == "0")
                {
                    Console.WriteLine($"Você selecionou o diretório: {currentDirectory}");
                    IdentificarNotasVeiculos(currentDirectory);
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
                    if(int.TryParse(input, out index) && index >= 4 && index < directories.Length + 4)
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

        private static void IdentificarNotasVeiculos(string currentDirectory)
        {
            string[] folderFiles = Directory.GetFiles(currentDirectory).Where(file => file.EndsWith(".xml")).ToArray();
            foreach(string file in folderFiles)
            {
                ProcessarXML(file);
            }
            Console.WriteLine("Notas identificadas com sucesso");
        }

        private static void ProcessarXML(string filepath)
        {
            try
            {
                XDocument xmldoc = XDocument.Load(filepath);
                XNamespace ns = "http://www.portalfiscal.inf.br/nfe";

                var infAdic = xmldoc.Descendants(ns + "infAdic").FirstOrDefault();

                if(infAdic != null)
                {
                    string? infCpl = infAdic.Element(ns + "infCpl")?.Value;
                    var nomeArquivo = VerificarPlacas(infCpl);
                    if(nomeArquivo != null)
                    {
                        if(!Directory.Exists(Path.Combine(Path.GetDirectoryName(filepath), "Veiculos")))
                        {
                            Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(filepath), "Veiculos"));
                        }

                        File.Move(filepath ,Path.GetDirectoryName(filepath) + @$"\Veiculos\"  + nomeArquivo + " - " +  Path.GetFileName(filepath));                        
                    }
                    
                }

            }catch (Exception)
            {
                throw;
            }
        }

        private static string VerificarPlacas(string texto)
        {
            string[] placas = { "BWV-0I24", "ETB-5D61", "GUG-7H68","FHH-7I86","ECU-3A67","GAK-1567", "DMK-3C99", "FPK-7A88", "GJY-9755", "RNK-7B98",
                                    "SIZ-2J86", "GIL-0962", "SIG-9C92", "CUG-7H68", "SST-6A48", "SHS-3G04", "FFQ-9J27","FZH-2C11", "SIG-9C93", "MTZ-3D69",
                                    "DZV-4286", "KWW-8892", "SHU-1C35", "RUN-8C05", "SUC-1D60", "SIS-0E68", "GHK-9J60", "SJG-1C06", "ENN-0170", " CUG-7G68",
                                    "CUR-0E00", "DDF-2H83", "ETB-5661"};
            var placasEncontradas = placas.Where(placa => texto.Contains(placa)).ToList();

            if (placasEncontradas.Any())
            {
                return string.Join(", ", placasEncontradas);
            }
            else
            {
                return null;
            }
        }
    }
}