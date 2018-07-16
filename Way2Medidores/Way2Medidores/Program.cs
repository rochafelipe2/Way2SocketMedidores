using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Way2Medidores.Service;

namespace Way2Medidores
{
    class Program
    {

        private static string ip;
        private static int port;
        private static int initialIndex;
        private static int finalIndex;
         
        static void Main(string[] args)
        {
            FileService fileService = new FileService();
            MedidorService medidorService = new MedidorService();

            var linhas = fileService.lerArquivoOrigem("arquivoEntrada.txt");
            
            if(linhas.Length > 0)
            {
                foreach(var linha in linhas)
                {
                    medidorService.lerTodaMedicao();
                }
            }
            
         
            try
            {
                

            }
            catch (SocketException exp)
            {

            }
            finally
            {
               // cliente.Close();
            }



        }

        public static void alimentarCampos()
        {
            Console.WriteLine("Entre com o IP:");
            ip = Console.ReadLine();

            Console.WriteLine("Entre com o Port:");
            port = int.Parse(Console.ReadLine());

            Console.WriteLine("Entre com o Indice inicial:");
            initialIndex = int.Parse(Console.ReadLine());

            Console.WriteLine("Entre com o Indice final:");
            finalIndex = int.Parse(Console.ReadLine());

            Console.WriteLine(ip);
            Console.ReadLine();
        }

      
   

    }
}
