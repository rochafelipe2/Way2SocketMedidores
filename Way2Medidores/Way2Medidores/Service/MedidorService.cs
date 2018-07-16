using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Way2Medidores.Service
{
    public class MedidorService
    {


        public void conectarSocket()
        {
            //alimentarCampos();
            TcpClient cliente = new TcpClient();

            cliente.Connect("40.121.85.141", 20008);
            var conected = cliente.Connected;
            var result = cliente.GetStream();

            byte[] msg = Encoding.UTF8.GetBytes("0x81");
            cliente.Client.Send(msg);

            byte[] bytes = new byte[256];
            cliente.Client.Receive(bytes);

            var retorno = Encoding.UTF8.GetString(bytes);

            NetworkStream serverStream = cliente.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("0x01" + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, (int)cliente.ReceiveBufferSize);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
        }


        public void lerTodaMedicao()
        {
            string medidor = "medidor1";
            string[] linhas = System.IO.File.ReadAllLines(@"C:\ProjetosWay2\Way2Medidores\Way2Medidores\ArquivoMock\mock_"+ medidor+".txt");


            FileService fileService = new FileService();

            for(int i =0; i < linhas.Length; i++)
            {
                linhas[i] = linhas[i].Replace('\t', ';');
                linhas[i] = arredondarValorMedicao(linhas[i]);
            }
            fileService.escreverArquivoDestino(linhas);

            if (linhas.Length == 0)
                Console.WriteLine("Erro: sem registros no arquivo de origem");

        }

        public void lerMedicaoPorRange(int inicial, int final)
        {

        }

        public string arredondarValorMedicao(string linha)
        {

            var split = linha.Split(';');
            var valorMedicao = Math.Round(decimal.Parse(split[3]),2);
            split[3] = valorMedicao.ToString();
            var novaLinha = split[0] +";" +split[1] +";"+ split[2] +";"+ split[3];


            return novaLinha;
        }


       
    }
}
