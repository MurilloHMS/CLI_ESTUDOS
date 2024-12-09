using System;
using System.IO;
using UglyToad.PdfPig;
using ClosedXML.Excel;

namespace CLI_Estudos;

public class PdfToExcelConversor
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
            Console.WriteLine("1: Selecionar arquivo");
            Console.WriteLine("2: Subir um nível");
            Console.WriteLine("3: Navegar para um diretório");
            Console.WriteLine("S: Sair");
            Console.WriteLine();

            string[] directories = Directory.GetDirectories(currentDirectory);
            string[] files = Directory.GetFiles(currentDirectory);
          
            Console.WriteLine("Diretórios");
            for(int i = 0; i < directories.Length; i++)
            {
                Console.WriteLine($"{i + 4}: {Path.GetFileName(directories[i])}");
            }

            Console.WriteLine("\nArquivos:");
            for(int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i + 4 + directories.Length}:  {Path.GetFileName(files[i])}");
            }

            Console.Write("\nEscolha uma opção:");
            string input = Console.ReadLine();

            if (input == "0")
            {
              Console.WriteLine($"Você selecionou o diretório: {currentDirectory}");
            }
            else if (input == "1")
            {
                Console.Write("Digite o número do arquivo para selecionar: ");
                if (int.TryParse(Console.ReadLine(), out int fileIndex) && fileIndex > 4 + directories.Length && fileIndex < 4 + directories.Length + files.Length )
                {
                    string selectedFile = files[fileIndex - 4 - directories.Length];
                    Conversor(selectedFile);
                }
            }
            else if(input == "2")
            {
                currentDirectory = Directory.GetParent(currentDirectory)?.FullName ?? currentDirectory;
            }
            else if(input == "3")
            {
                Console.WriteLine("Digite o nome do diretório para navegar: ");
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
            else if(input == "S" || input == "s")
            {
                break;
            }
            else
            {
                int index;
                if(int.TryParse(input, out index) && index >= 4 && index < directories.Length + 4)
                {
                    currentDirectory = directories[index -4];
                }else
                {
                  Console.WriteLine("Opção invalida!");
                  Console.ReadKey();
                }
            }
       }
    }


    private static void Conversor(string file)
    {
        try
        {
            using(var pdf = PdfDocument.Open(file))
            {
              using(var workbook = new XLWorkbook())
              {
                  var worksheet = workbook.Worksheets.Add("Sheet1");

                  int rowIndex = 1;
                  foreach(var page in pdf.GetPages())
                  {
                      string text = page.Text;

                      string[] lines = text.Split(new[]{"\r\n","\r", "\n"}, StringSplitOptions.None);
                      foreach(var line in lines)
                      {
                          worksheet.Cell(rowIndex, 1).Value = line;
                          rowIndex++;
                      }
                  }
                  
                  Console.Write("Digite o nome da planilha final: ");
                  string nome = Console.ReadLine();
                  string output = @$"{Path.GetDirectoryName(file)}\{nome}.xlsx";
                  workbook.SaveAs(output);

              }
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }    
}
