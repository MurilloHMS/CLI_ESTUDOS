namespace CLI_Estudos;

public class CadastroUsuario
{
    private static string delimitadorInicio;
    private static string delimitadorFim;
    private static string tagNome;
    private static string tagDataNascimento;
    private static string tagNomeDaRua;
    private static string tagNumeroDaCasa;
    private static string tagNumeroDoDocumento;
    private static string caminhoArquivo;

    public struct DadosCadastraisStruct
    {
        public string Nome;
        public DateTime DataDeNascimento;
        public string NomeDaRua;
        public UInt32 NumeroDaCasa;
        public string NumeroDoDocumento;
    }

    public CadastroUsuario()
    {
        List<DadosCadastraisStruct> ListaDeUsuarios = new List<DadosCadastraisStruct>();
        string opcao;

        delimitadorInicio = "##### INICIO #####";
        delimitadorFim = "##### Fim #####";
        tagNome = "NOME : ";
        tagDataNascimento = "DATA_DE_NASCIMENTO : ";
        tagNomeDaRua = "NOME_DA_RUA : ";
        tagNumeroDaCasa = "NUMERO_DA_CASA : ";
        tagNumeroDoDocumento = "NUMERO_DO_DOCUMENTO : ";

        caminhoArquivo = @"BaseDeDados.txt";

        CarregaDados(caminhoArquivo, ref ListaDeUsuarios);

        do
        {
            Console.Clear();
            Console.WriteLine("Pressione C para cadastrar um novo usuário");
            Console.WriteLine("Pressione B para Buscar um usuário");
            Console.WriteLine("Pressione E para excluir um usuário");
            Console.WriteLine("Pressione S para sair");

            opcao = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (opcao == "c")
            {
                //Cadastrar um novo usuario
                CadastrarUsuario(ref ListaDeUsuarios);
                        
            }
            else if (opcao == "b")
            {
                //buscar usuário
                BuscaUsuarioPeloDoc(ListaDeUsuarios);
            }
            else if (opcao == "e")
            {
                //excluir usuário
                ExcluiUsuarioPeloDoc(ref ListaDeUsuarios);
            }
            else if (opcao == "s")
            {
                //sair da aplicacao
                MostrarMensagem("Encerrando o programa");
            }
            else
            {
                //opcao desconhecida
                MostrarMensagem("opcao desconhecida");
            }

        } while (opcao != "s");


    }

    private static void MostrarMensagem(string mensagem)
    {
        Console.WriteLine(mensagem);
        Excecao();
    }

    private static void Excecao()
    {
        Console.WriteLine("Pressione qualquer tecla para continuar");
        Console.ReadKey();
        Console.Clear();
    }

    private static Resultado_e PegaData(ref DateTime data, string mensagem)
    {
        Resultado_e retorno;
        do
        {
            try
            {
                Console.WriteLine(mensagem);
                string temp = Console.ReadLine();
                if (temp.ToLower() == "s")
                {
                    retorno = Resultado_e.Sair;
                }
                else
                {
                    data = Convert.ToDateTime(temp);
                    retorno = Resultado_e.Sucesso;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exceção: {e.Message}");
                Excecao();
                retorno = Resultado_e.Excecao;

            }
        } while (retorno == Resultado_e.Excecao);

        Console.Clear();
        return retorno;
    }

    private static Resultado_e PegaString(ref string minhaString, string mensagem)
    {
        Resultado_e retorno;
        Console.WriteLine(mensagem);
        string temp = Console.ReadLine();
        if (temp.ToLower() == "s")
        {
            retorno = Resultado_e.Sair;
        }
        else
        {
            minhaString = temp;
            retorno = Resultado_e.Sucesso;
        }

        Console.Clear();
        return retorno;
    }

    private static Resultado_e PegaUint32(ref UInt32 numero, string mensagem)
    {
        Resultado_e retorno;
        do
        {
            try
            {
                Console.WriteLine(mensagem);
                string temp = Console.ReadLine();
                if (temp.ToLower() == "s")
                {
                    retorno = Resultado_e.Sair;
                }
                else
                {
                    numero = Convert.ToUInt32(temp);
                    retorno = Resultado_e.Sucesso;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exceção: {e.Message}");
                Excecao();
                retorno = Resultado_e.Excecao;
            }
        } while (retorno == Resultado_e.Excecao);

        Console.Clear();
        return retorno;
    }
    public static void GravaDados(string caminho, List<DadosCadastraisStruct> ListaDeUsuarios)
    {
        try
        {
            string conteudoArquivo = "";
            foreach (DadosCadastraisStruct cadastro in ListaDeUsuarios)
            {
                conteudoArquivo += delimitadorInicio + "\r\n";
                conteudoArquivo += tagNome + cadastro.Nome + "\r\n";
                conteudoArquivo += tagDataNascimento + cadastro.DataDeNascimento.ToString("dd/MM/yyyy") + "\r\n";
                conteudoArquivo += tagNumeroDoDocumento + cadastro.NumeroDoDocumento + "\r\n";
                conteudoArquivo += tagNomeDaRua + cadastro.NomeDaRua + "\r\n";
                conteudoArquivo += tagNumeroDaCasa + cadastro.NumeroDaCasa + "\r\n";
                conteudoArquivo += delimitadorFim + "\r\n";
            }
            File.WriteAllText(caminho, conteudoArquivo);

        }
        catch (Exception e)
        {
            Console.WriteLine("Excecao : " + e.Message);
        }
    }
    public static void CarregaDados(string caminho, ref List<DadosCadastraisStruct> ListaDeUsuarios)
    {
        try
        {
            if (File.Exists(caminho))
            {
                string[] conteudoArquivo = File.ReadAllLines(caminho);
                DadosCadastraisStruct dadosCadastrais;
                dadosCadastrais.Nome = "";
                dadosCadastrais.DataDeNascimento = new DateTime();
                dadosCadastrais.NomeDaRua = "";
                dadosCadastrais.NumeroDaCasa = 0;
                dadosCadastrais.NumeroDoDocumento = ""; 

                foreach (string linha in conteudoArquivo)
                {
                    if (linha.Contains(delimitadorInicio))
                        continue;
                    if (linha.Contains(delimitadorFim))
                        ListaDeUsuarios.Add(dadosCadastrais);
                    if (linha.Contains(tagNome))
                        dadosCadastrais.Nome = linha.Replace(tagNome, "");
                    if (linha.Contains(tagDataNascimento))
                        dadosCadastrais.DataDeNascimento = Convert.ToDateTime(linha.Replace(tagDataNascimento, ""));
                    if (linha.Contains(tagNumeroDoDocumento))
                        dadosCadastrais.NumeroDoDocumento = linha.Replace(tagNumeroDoDocumento, "");
                    if (linha.Contains(tagNomeDaRua))
                        dadosCadastrais.NomeDaRua = linha.Replace(tagNomeDaRua, "");
                    if (linha.Contains(tagNumeroDaCasa))
                        dadosCadastrais.NumeroDaCasa = Convert.ToUInt32(linha.Replace(tagNumeroDaCasa, ""));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Excecao : " + e.Message);
        }
    }
            public static void BuscaUsuarioPeloDoc(List<DadosCadastraisStruct> ListaDeUsuarios)
        {
            Console.WriteLine("Digite o numero do documento para buscar o usuario ou digite S para sair");
            string temp = Console.ReadLine();
            if (temp.ToLower() == "s")
                return;
            else
            {
                List<DadosCadastraisStruct> ListaDeUsuariosTemp = ListaDeUsuarios.Where(x => x.NumeroDoDocumento == temp).ToList();
                if (ListaDeUsuariosTemp.Count > 0) 
                {
                    foreach(DadosCadastraisStruct usuario in ListaDeUsuariosTemp)
                    {
                        Console.WriteLine(tagNome + usuario.Nome);
                        Console.WriteLine(tagDataNascimento + usuario.DataDeNascimento.ToString("dd/MM/yyyy"));
                        Console.WriteLine(tagNumeroDoDocumento + usuario.NumeroDoDocumento);
                        Console.WriteLine(tagNomeDaRua + usuario.NomeDaRua);
                        Console.WriteLine(tagNumeroDaCasa + usuario.NumeroDaCasa);
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum usuario possui o documento : " + temp);
                }
                MostrarMensagem("");
            }
        }

        public static void ExcluiUsuarioPeloDoc(ref List<DadosCadastraisStruct> ListaDeUsuarios)
        {
            Console.WriteLine("Digite o numero do documento para excluir o usuario ou digite S para sair");
            string temp = Console.ReadLine();
            bool alguemFoiExcluido = false;
            if (temp.ToLower() == "s")
                return;
            else
            {
                List<DadosCadastraisStruct> ListaDeUsuariosTemp = ListaDeUsuarios.Where(x => x.NumeroDoDocumento == temp).ToList();
                if (ListaDeUsuariosTemp.Count > 0)
                {
                    foreach(DadosCadastraisStruct usuario in ListaDeUsuariosTemp)
                    {
                        ListaDeUsuarios.Remove(usuario);
                        alguemFoiExcluido = true;
                    }
                    if (alguemFoiExcluido)
                        GravaDados(caminhoArquivo, ListaDeUsuarios);
                    Console.WriteLine(ListaDeUsuariosTemp.Count + "usuario(s) com documento " + temp + " excluido(s)");
                }
                else
                {
                    Console.WriteLine("Nenhum usuario possui o documento : " + temp);
                }
            }
            MostrarMensagem("");
        }


    public static Resultado_e CadastrarUsuario(ref List<DadosCadastraisStruct> ListaUsuarios)
    {
        DadosCadastraisStruct cadastroUsuario;
        cadastroUsuario.Nome = string.Empty;
        cadastroUsuario.DataDeNascimento = new DateTime();
        cadastroUsuario.NomeDaRua = string.Empty;
        cadastroUsuario.NumeroDaCasa = 0;
        cadastroUsuario.NumeroDoDocumento = string.Empty;

        if (PegaString(ref cadastroUsuario.Nome, "Digite o Nome completo ou digite S para sair") == Resultado_e.Sair)
            return Resultado_e.Sair;
        if (PegaData(ref cadastroUsuario.DataDeNascimento, "Digite a Data de Nascimento ou digite S para sair") == Resultado_e.Sair)
            return Resultado_e.Sair;
        if (PegaString(ref cadastroUsuario.NumeroDoDocumento, "Digite o Número do documento ou digite S para sair") == Resultado_e.Sair)
            return Resultado_e.Sair;
        if (PegaString(ref cadastroUsuario.NomeDaRua, "Digite o Nome da rua ou digite S para sair") == Resultado_e.Sair)
            return Resultado_e.Sair;
        if (PegaUint32(ref cadastroUsuario.NumeroDaCasa, "Digite o Numero da Casa ou digite S para Sair") == Resultado_e.Sair)
            return Resultado_e.Sair;
        ListaUsuarios.Add(cadastroUsuario);
        GravaDados(caminhoArquivo, ListaUsuarios);
        return Resultado_e.Sucesso;
    }

}
