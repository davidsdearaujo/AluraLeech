using AluraBot.Browser.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraBot.ViewConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite o email:");
            var email =  Console.ReadLine();

            Console.WriteLine("Digite a senha:");
            var password = Console.ReadLine();

            //DownloadCursosBruno(email, password);
            Console.WriteLine("Digite a URL do curso [Ex. 'https://cursos.alura.com.br/course/csharp-eventos-delegates-lambda']:");
            Console.Write("> ");
            var urlCurso = Console.ReadLine();

            Console.Clear();

            Console.WriteLine("Buscando informações...");

            var bot = new Browser.BOT();
            bot.OnVideoDownload += (object sender, OnVideoDownloadedEventArgs e) =>
            {
                Console.Clear();
                Console.WriteLine("Baixando...\n");
                Console.WriteLine($"[{e.Downloaded}/{e.Count}] {e.Path}");
            };

            bot.BaixarCurso(email, password, urlCurso, $@"{System.Environment.CurrentDirectory}\Cursos");

            Console.WriteLine("\nDownload Finalizado!\n");
            Console.WriteLine("Pressione qualquer tecla para finalizar . . .");
            Console.ReadKey();
        }

        private static void DownloadCursosBruno(string email, string password)
        {
            var listaCursos = new List<string>()
            {
                
                "https://cursos.alura.com.br/course/marketing-digital-canais-nao-pagos",
                "https://cursos.alura.com.br/course/marketing-digital-canais-pagos",
                "https://cursos.alura.com.br/course/negocios-no-youtube",
                "https://cursos.alura.com.br/course/branding",
                "https://cursos.alura.com.br/course/curadoria-de-conteudo",
                "https://cursos.alura.com.br/course/marketing-viral",
                "https://cursos.alura.com.br/course/people-marketing-okr-audiencia",
                "https://cursos.alura.com.br/course/seo-parte-1",
                "https://cursos.alura.com.br/course/seo-parte-2",
                "https://cursos.alura.com.br/course/metricas-e-relatorios-seo",
                "https://cursos.alura.com.br/course/introducao-ao-google-adwords",
                "https://cursos.alura.com.br/course/google-adwords-otimizando-campanhas",
                "https://cursos.alura.com.br/course/seo-wordpress",
                "https://cursos.alura.com.br/course/google-analytics",
                "https://cursos.alura.com.br/course/marketing-conteudo",
                "https://cursos.alura.com.br/course/social-media-marketing",
                "https://cursos.alura.com.br/course/facebook-marketing",
                "https://cursos.alura.com.br/course/instagram-marketing",
                "https://cursos.alura.com.br/course/twitter",
                "https://cursos.alura.com.br/course/facebook-ads",
                "https://cursos.alura.com.br/course/personal-branding",
                "https://cursos.alura.com.br/course/principios-influencia",
                "https://cursos.alura.com.br/course/linkedin-ads",
                "https://cursos.alura.com.br/course/blog-corporativo-gerando-leads-e-valor",
                "https://cursos.alura.com.br/course/blog-corporativo-parte-2-metricas-growth-marketing",
                "https://cursos.alura.com.br/course/lean",
                "https://cursos.alura.com.br/course/lean-inbound-na-pratica",
                "https://cursos.alura.com.br/course/lean-inbound-fundamentos",
                "https://cursos.alura.com.br/course/empreendedorismo",
                "https://cursos.alura.com.br/course/empreendedorismo-abra-sua-empresa",
                "https://cursos.alura.com.br/course/pitch",
                "https://cursos.alura.com.br/course/business-model-canvas",
                "https://cursos.alura.com.br/course/avance-na-construcao-de-negocios",
                "https://cursos.alura.com.br/course/ciclo-de-vida-do-produto",
                "https://cursos.alura.com.br/course/chatbot-watson-conversation",
                "https://cursos.alura.com.br/course/chatbot-parte-2-comunicando-sua-app-com-o-bot",
                "https://cursos.alura.com.br/course/arduino",
                "https://cursos.alura.com.br/course/arduino-robotica",
                "https://cursos.alura.com.br/course/raspberrypi-carro-espiao-com-camera-wifi-e-sensor",
                "https://cursos.alura.com.br/course/raspberrypi-controlando-o-mundo-com-gpio",
                "https://cursos.alura.com.br/course/raspberrypi-da-instalacao-ao-media-center",
                "https://cursos.alura.com.br/course/raspberrypi-servidor",
                "https://cursos.alura.com.br/course/robotica-brinquedo-interativo",
                "https://cursos.alura.com.br/course/csharp-paralelismo-no-mundo-real",
                "https://cursos.alura.com.br/course/reflection-parte-1",
                "https://cursos.alura.com.br/course/reflection-parte-2"
            };

            foreach (string link in listaCursos)
            {
                Console.WriteLine("Buscando informações...");

                var bot = new Browser.BOT();
                bot.OnVideoDownload += (object sender, OnVideoDownloadedEventArgs e) =>
                {
                    Console.Clear();
                    Console.WriteLine("Baixando...\n");
                    Console.WriteLine($"[{e.Downloaded}/{e.Count}] {e.Path}");
                };

                bot.BaixarCurso(email, password, link, $@"{System.Environment.CurrentDirectory}\Cursos");

                Console.WriteLine("\nDownload do curso Finalizado!\n");
                //System.Threading.Thread.Sleep(1000); //Tempo de espera para baixar o próximo curso
            }
            
            Console.WriteLine("\nFim dos Downloads!\n");
            Console.ReadKey();
           
        }
    }
}
