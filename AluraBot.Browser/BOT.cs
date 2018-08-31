using AluraBot.Browser.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraBot.Browser
{
    public class BOT
    {
        private readonly Browser _browser = new Browser();
        private string Email { get; set; }
        private string Password { get; set; }
        private string PathSave { get; set; }

        public delegate void OnVideoDownloadVideoEventHandler(object sender, OnVideoDownloadedEventArgs e);
        public event OnVideoDownloadVideoEventHandler OnVideoDownload;


        public CursoDTO BaixarCurso(string email, string password, string urlCurso, string pathSave)
        {
            Email = email;
            Password = password;
            PathSave = pathSave;

            //Validação
            if (!urlCurso.Contains("https://cursos.alura.com.br/course/"))
                throw new ArgumentException("O link do curso deve ser no seguinte formato: https://cursos.alura.com.br/course/[nome do curso]");

            //Faz o login caso esteja deslogado
            if (!Logado()) { Logar(); }
            string response = _browser.Navigate($"{urlCurso.Replace("course", "courses")}/tryToEnroll");

            var curso = new CursoDTO();
            curso.Url = urlCurso;
            curso.Nome = response.ValorEntre("<h2 class=\"task-menu-header-info-title-text\">", "</h2>");
            curso.Aulas = PopularAulas(curso, response);

            BaixarVideos(curso);

            return curso;
        }

        private List<AulaDTO> PopularAulas(CursoDTO cursoDto, string response)
        {
            var aulas = new List<AulaDTO>();

            //Pega Organizacao Aula
            string listaPagina = response.ValorEntre("<select class=\"task-menu-sections-select", "</select>");
            string complementoUrl = listaPagina.ValorEntre("onchange=\"location.href='", ";\">").Replace("'+this.value", "");

            List<string> aulasConteudo = listaPagina.ValorEntreTodos("<option value=\"", "</option>");

            //Percorre as aulas
            foreach (string aulaConteudo in aulasConteudo)
            {
                if (!Logado()) { Logar(); }

                var idAula = aulaConteudo.Substring(0, 1);
                var responseAula = _browser.Navigate("https://cursos.alura.com.br" + complementoUrl + idAula);

                AulaDTO aulaAtual = new AulaDTO()
                {
                    Nome = aulaConteudo.Split('>')[1],
                    Etapas = new List<EtapaDTO>()
                };

                //Pegar Pelo Menu
                if (!Logado()) { Logar(); }
                var menu = responseAula.ValorEntre("<ul class=\"task-menu-nav-list\">", "</ul>");
                List<string> etapasConteudo = menu.ValorEntreTodos("<a href=", "</a>");

                //Percorre as etapas
                foreach (var li in etapasConteudo)
                {
                    //Se tiver vídeo nessa aula
                    if (li.Contains("item-link-VIDEO"))
                    {
                        var etapaAtual = new EtapaDTO();

                        etapaAtual.Nome =
                            li.ValorEntreTodos("class=\"task-menu-nav-item-link task-menu-nav-item-link-VIDEO\"", "class=\"task-menu-nav-item-infos")[0]
                                .ValorEntre("<span class=\"task-menu-nav-item-title\"", "</span>")
                                .Split('>')[0]
                                .Replace("title=\"", "")
                                .Replace("\"", "");

                        var codVideo = li.ValorEntreTodos("/course/", "class=\"task-menu-nav-item-link task-menu-nav-item-link-VIDEO\"")[0].ValorEntre("task/", "\"").ToString();

                        var urlSolicitarVideo = "https://cursos.alura.com.br/course/" + cursoDto.Url.Replace("https://cursos.alura.com.br/course/", "") + $"/task/{codVideo}/video";
                        if (!Logado()) { Logar(); }
                        var responseJsonUrlVideo = _browser.RequisicaoVideo(urlSolicitarVideo);
                        etapaAtual.urlVideo = System.Text.RegularExpressions.Regex.Unescape(responseJsonUrlVideo.ValorEntre("[{\"link\":\"", "\""));

                        aulaAtual.Etapas.Add(etapaAtual);
                    }
                }
                aulas.Add(aulaAtual);
            }

            return aulas;
        }

        private void BaixarVideos(CursoDTO curso)
        {
            using (var webClient = new System.Net.WebClient())
            {
                int count = curso.Aulas.Select(a => a.Etapas.Count).Sum();
                int downloaded = 0;
                foreach (AulaDTO aula in curso.Aulas)
                {
                    foreach (EtapaDTO etapa in aula.Etapas)
                    {
                        string path = $"{curso.Nome}\\{aula.Nome}\\{etapa.Nome}.mp4";
                        path = $"{PathSave}\\{path.RemoveAcentos().Replace(":", "-")}";

                        var di = new DirectoryInfo(Path.GetDirectoryName(path));

                        if (!di.Exists) { di.Create(); }
                        
                        if(!Logado()) { Logar(); }

                        webClient.DownloadFile(etapa.urlVideo, path);

                        downloaded++;
                        OnVideoDownload(this, new OnVideoDownloadedEventArgs()
                        {
                            Count = count,
                            Downloaded = downloaded,
                            Path = path
                        });

                        //Tempo de espera para download de cada vídeo
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
        }


        private bool Logar()
        {
            var postData = new Dictionary<string, string>()
                {
                    { "email", Email},
                    { "password", Password},
                    { "uriOnError", ""}
                };

            var response2 = _browser.Navigate("https://cursos.alura.com.br/signin", postData);

            return response2.Contains("/signout") && response2.Contains("Sair");
        }

        private bool Logado()
        {
            var response = _browser.Navigate("https://cursos.alura.com.br/courses");
            return response.Contains("/signout");
        }
    }
}
