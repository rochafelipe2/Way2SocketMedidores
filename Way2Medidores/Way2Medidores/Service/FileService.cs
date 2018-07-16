using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Way2Medidores.Service
{
    public class FileService
    {

        public FileService()
        {

        }

        public string[] lerArquivoOrigem(string nomeArquivo)
        {


            string[] linhas = System.IO.File.ReadAllLines(@"C:\ProjetosWay2\Way2Medidores\Way2Medidores\ArquivoOrigem\" + nomeArquivo);

            if (linhas.Length == 0)
                Console.WriteLine("Erro: sem registros no arquivo de origem");

            return linhas;
        }

        public bool escreverArquivoDestino(string[] linhas)
        {
            bool verificador = false;

            try
            {


                string fileName = "arquivo_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + ".csv";
                string destino = @"C:\ProjetosWay2\Way2Medidores\Way2Medidores\ArquivoDestino\";
                string path = destino + fileName;

                //garente um unico arquivo por hora
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);


                System.IO.File.Create(path).Close();
                System.IO.File.AppendAllLines(path, linhas);
                verificador = true;
            }
            catch (Exception exp)
            {

                string linhalog = DateTime.Now.ToLongDateString() + " " + exp.Message + " " + exp.StackTrace.ToString();
                this.escreverLog(linhalog);
             }

            return verificador;
        }


        private bool verificarExtencaoArquivo(string nomeArquivo)
        {
            bool verificador = false;
            if (nomeArquivo.EndsWith(".txt"))
            {
                verificador = true;
            }

            return verificador;
        }

        private bool verificarLinha(string linha)
        {
            bool verificador = false;
            var linhaSplitada = linha.Split(' ');

            //Se a linha tiver 4 valores ela é valida
            if (linhaSplitada.Length == 4)
            {
                verificador = true;
            }

            return verificador;
        }


        public void escreverLog(string linha)
    {
        string fileName = "arquivo_erro.txt";
        string destino = @"C:\ProjetosWay2\Way2Medidores\Way2Medidores\ArquivoLog\";
        string pathLog = destino + fileName;
        if (!System.IO.File.Exists(pathLog))
            System.IO.File.Create(pathLog);

        using (TextWriter tw = new StreamWriter(pathLog))
        {
            tw.WriteLine(linha);
        }

    }
}
}
