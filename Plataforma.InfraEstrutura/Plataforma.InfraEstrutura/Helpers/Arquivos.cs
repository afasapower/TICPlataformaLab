using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Plataforma.InfraEstrutura.Helpers
{
    public class Arquivos
    {
        private IHostingEnvironment hostingEnv;

        public Arquivos(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }

        public DadosArquivoUpload UploadArquivos(HttpRequest itensForm, string nomePasta, List<string> extensoes = null, bool pastaSegura = true, string acao = "I")
        {
            string pasta = (pastaSegura ? "App_Data\\" : "");
            DadosArquivoUpload retorno = new DadosArquivoUpload();
            retorno.status = false;
            retorno.msgerro = string.Empty;          

            List<string> teste = new List<string>() { };
            try
            {
                // Lista de extensões permitidas (pegar do banco)
                List<string> arqExt = extensoes ?? new List<string>() { ".xls", ".xlsx", ".jpg", ".png", ".doc", ".docx", ".pdf", ".rtf", ".odt" };

                // Metodo responsável por criar pasta caso não tenha
                CriarPasta(pasta + nomePasta);

                // verifica se há pelo menos um arquivo selecionado
                // caso não tenha será lançada uma mensagem de erro
                if (itensForm.Form.Files.Count > 0)
                {
                    // captura todos os arquivos selecionados pelo usuário
                    var arquivosUsuario = itensForm.Form.Files;
                    // loop para divisão dos arquivos para upload
                    foreach (var itens in arquivosUsuario)
                    {
                        // Separa o nome do arquivo do caminho de origem
                        string nomeArquivo = Path.GetFileName(itens.FileName.Trim('"'));                      

                        // Concatena o nome com um guid mais a extensão
                        StringBuilder nomeArqGuid = new StringBuilder();

                        // captura extensão do arquivo para analise e montagem do novo nome
                        string extensao = Path.GetExtension(nomeArquivo).ToLower().Trim();

                        // Verifica se há este arquivo na lista de extensões permitidas
                        // caso não tenha será lançada uma mensagem de erro
                        if (arqExt.Contains(extensao))
                        {
                            // Verifica o tamanho no arquivo limite (5Mb)
                            // caso for maior será lançada uma mensagem de erro
                            if (itens.Length < 5000000)
                            {
                                // Monta novo nome para ser gravado na pasta                
                                nomeArquivo = Path.GetFileNameWithoutExtension(nomeArquivo);
                                nomeArqGuid.Append(nomeArquivo);
                                nomeArqGuid.Append("-");
                                nomeArqGuid.Append(Guid.NewGuid().ToString().Split('-')[0].ToString());
                                nomeArqGuid.Append(extensao);
                                // var analiseConteudo = ContentDispositionHeaderValue.Parse(itens.ContentDisposition);  
                                // adiciona nome dos arquivos para o retorno                              
                                retorno.enviados.Add(nomeArqGuid.ToString());

                                // Gera o caminho de gravação do arquivo incluindo nome do arquivo, pasta e rota (wwwroot)
                                string[] caminhosPastas = new string[] 
                                {
                                    this.hostingEnv.WebRootPath.ToString(),
                                    pasta,
                                    nomePasta,
                                    nomeArqGuid.ToString()
                                };

                                var caminhoArquivo = Path.Combine(caminhosPastas);

                                // Inicia a gravação do arquivo na pasta de destino
                                using (var gravacao = File.OpenWrite(caminhoArquivo))
                                {
                                    try
                                    {
                                        // Gravação do arquivo na pasta de destino
                                        itens.CopyTo(gravacao);
                                        // Limpa os buffers desse fluxo e faz com que todos os dados armazenados em buffer sejam gravados no arquivo                        
                                        gravacao.Flush();
                                        // Retorno  de conclusão do procedimento de gravação foi realizada com sucesso                                  
                                        retorno.status = true;
                                    }
                                    catch (Exception)
                                    {
                                        retorno.msgerro = "Desculpe, ocorreu um problema no envio do(s) arquivo(s).";
                                        retorno.recusados.Add(nomeArquivo);
                                    }                                    
                                }
                            }
                            else
                            {
                                retorno.msgerro = "O arquivo excedeu o tamanho maximo permitido (5Mb).";
                                retorno.recusados.Add(nomeArquivo);
                            }
                        }
                        else
                        {
                            retorno.msgerro = "Não é permitido o envio de arquivos no formato (" + extensao + ")";
                            retorno.recusados.Add(nomeArquivo);
                        }
                    }
                }
                else
                {
                    if(acao == "U")                    
                        retorno.status = true;
                    else                    
                        retorno.msgerro = "É obrigatório o envio de 1 (um) ou mais arquivos.";                                       
                }
            }
            catch (Exception)
            {
                retorno.msgerro = "Desculpe, ocorreu um problema no envio do(s) arquivo(s).";
            }
            return retorno;
        }


        public bool CriarPasta(string nomePasta)
        {
            var caminhoPasta = Path.Combine(this.hostingEnv.WebRootPath, nomePasta);
            bool pastaExiste = Directory.Exists(caminhoPasta);
            if (!pastaExiste)
            {
                Directory.CreateDirectory(caminhoPasta);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeletaPasta(string caminhoPasta)
        {
            if (Directory.Exists(caminhoPasta))
            {
                // Delete all files from the Directory
                foreach (string arquivo in Directory.GetFiles(caminhoPasta))
                {
                    File.Delete(arquivo);
                }
                // Check all child Directories and delete files
                foreach (string subPasta in Directory.GetDirectories(caminhoPasta))
                {
                    DeletaPasta(subPasta);
                }
                Directory.Delete(caminhoPasta);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeletaArquivo(string caminhoArquivo)
        {
            var caminhoAbsoluto = Path.Combine(this.hostingEnv.WebRootPath, caminhoArquivo);
            if (File.Exists(caminhoAbsoluto))
            {
                File.Delete(caminhoAbsoluto);
                return true;
            }
            else
            {
                return false;
            }
        }


        public string GetContentType(string _oArquivo)
        {
            var extensao = Path.GetExtension(_oArquivo).ToLower();
            switch (extensao)
            {
                case ".ai": return "application/postscript";
                case ".aif": return "audio/x-aiff";
                case ".aifc": return "audio/x-aiff";
                case ".aiff": return "audio/x-aiff";
                case ".asc": return "text/plain";
                case ".au": return "audio/basic";
                case ".avi": return "video/x-msvideo";
                case ".bcpio": return "application/x-bcpio";
                case ".bin": return "application/octet-stream";
                case ".c": return "text/plain";
                case ".cc": return "text/plain";
                case ".ccad": return "application/clariscad";
                case ".cdf": return "application/x-netcdf";
                case ".class": return "application/octet-stream";
                case ".cpio": return "application/x-cpio";
                case ".cpp": return "text/plain";
                case ".cpt": return "application/mac-compactpro";
                case ".cs": return "text/plain";
                case ".csh": return "application/x-csh";
                case ".css": return "text/css";
                case ".dcr": return "application/x-director";
                case ".dir": return "application/x-director";
                case ".dms": return "application/octet-stream";
                case ".doc": return "application/msword";
                case ".drw": return "application/drafting";
                case ".dvi": return "application/x-dvi";
                case ".dwg": return "application/acad";
                case ".dxf": return "application/dxf";
                case ".dxr": return "application/x-director";
                case ".eps": return "application/postscript";
                case ".etx": return "text/x-setext";
                case ".exe": return "application/octet-stream";
                case ".ez": return "application/andrew-inset";
                case ".f": return "text/plain";
                case ".f90": return "text/plain";
                case ".fli": return "video/x-fli";
                case ".flv": return "video/x-flv";
                case ".gif": return "image/gif";
                case ".gtar": return "application/x-gtar";
                case ".gz": return "application/x-gzip";
                case ".h": return "text/plain";
                case ".hdf": return "application/x-hdf";
                case ".hh": return "text/plain";
                case ".hqx": return "application/mac-binhex40";
                case ".htm": return "text/html";
                case ".html": return "text/html";
                case ".ice": return "x-conference/x-cooltalk";
                case ".ief": return "image/ief";
                case ".iges": return "model/iges";
                case ".igs": return "model/iges";
                case ".ips": return "application/x-ipscript";
                case ".ipx": return "application/x-ipix";
                case ".jpe": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".jpg": return "image/jpeg";
                case ".js": return "application/x-javascript";
                case ".kar": return "audio/midi";
                case ".latex": return "application/x-latex";
                case ".lha": return "application/octet-stream";
                case ".lsp": return "application/x-lisp";
                case ".lzh": return "application/octet-stream";
                case ".m": return "text/plain";
                case ".man": return "application/x-troff-man";
                case ".me": return "application/x-troff-me";
                case ".mesh": return "model/mesh";
                case ".mid": return "audio/midi";
                case ".midi": return "audio/midi";
                case ".mime": return "www/mime";
                case ".mov": return "video/quicktime";
                case ".movie": return "video/x-sgi-movie";
                case ".mp2": return "audio/mpeg";
                case ".mp3": return "audio/mpeg";
                case ".mpe": return "video/mpeg";
                case ".mpeg": return "video/mpeg";
                case ".mpg": return "video/mpeg";
                case ".mpga": return "audio/mpeg";
                case ".ms": return "application/x-troff-ms";
                case ".msh": return "model/mesh";
                case ".nc": return "application/x-netcdf";
                case ".oda": return "application/oda";
                case ".pbm": return "image/x-portable-bitmap";
                case ".pdb": return "chemical/x-pdb";
                case ".pdf": return "application/pdf";
                case ".pgm": return "image/x-portable-graymap";
                case ".pgn": return "application/x-chess-pgn";
                case ".png": return "image/png";
                case ".pnm": return "image/x-portable-anymap";
                case ".pot": return "application/mspowerpoint";
                case ".ppm": return "image/x-portable-pixmap";
                case ".pps": return "application/mspowerpoint";
                case ".ppt": return "application/mspowerpoint";
                case ".ppz": return "application/mspowerpoint";
                case ".pre": return "application/x-freelance";
                case ".prt": return "application/pro_eng";
                case ".ps": return "application/postscript";
                case ".qt": return "video/quicktime";
                case ".ra": return "audio/x-realaudio";
                case ".ram": return "audio/x-pn-realaudio";
                case ".ras": return "image/cmu-raster";
                case ".rgb": return "image/x-rgb";
                case ".rm": return "audio/x-pn-realaudio";
                case ".roff": return "application/x-troff";
                case ".rpm": return "audio/x-pn-realaudio-plugin";
                case ".rtf": return "text/rtf";
                case ".rtx": return "text/richtext";
                case ".scm": return "application/x-lotusscreencam";
                case ".set": return "application/set";
                case ".sgm": return "text/sgml";
                case ".sgml": return "text/sgml";
                case ".sh": return "application/x-sh";
                case ".shar": return "application/x-shar";
                case ".silo": return "model/mesh";
                case ".sit": return "application/x-stuffit";
                case ".skd": return "application/x-koan";
                case ".skm": return "application/x-koan";
                case ".skp": return "application/x-koan";
                case ".skt": return "application/x-koan";
                case ".smi": return "application/smil";
                case ".smil": return "application/smil";
                case ".snd": return "audio/basic";
                case ".sol": return "application/solids";
                case ".spl": return "application/x-futuresplash";
                case ".src": return "application/x-wais-source";
                case ".step": return "application/STEP";
                case ".stl": return "application/SLA";
                case ".stp": return "application/STEP";
                case ".sv4cpio": return "application/x-sv4cpio";
                case ".sv4crc": return "application/x-sv4crc";
                case ".swf": return "application/x-shockwave-flash";
                case ".t": return "application/x-troff";
                case ".tar": return "application/x-tar";
                case ".tcl": return "application/x-tcl";
                case ".tex": return "application/x-tex";
                case ".tif": return "image/tiff";
                case ".tiff": return "image/tiff";
                case ".tr": return "application/x-troff";
                case ".tsi": return "audio/TSP-audio";
                case ".tsp": return "application/dsptype";
                case ".tsv": return "text/tab-separated-values";
                case ".txt": return "text/plain";
                case ".unv": return "application/i-deas";
                case ".ustar": return "application/x-ustar";
                case ".vcd": return "application/x-cdlink";
                case ".vda": return "application/vda";
                case ".vrml": return "model/vrml";
                case ".wav": return "audio/x-wav";
                case ".wrl": return "model/vrml";
                case ".xbm": return "image/x-xbitmap";
                case ".xlc": return "application/vnd.ms-excel";
                case ".xll": return "application/vnd.ms-excel";
                case ".xlm": return "application/vnd.ms-excel";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlw": return "application/vnd.ms-excel";
                case ".xml": return "text/xml";
                case ".xpm": return "image/x-xpixmap";
                case ".xwd": return "image/x-xwindowdump";
                case ".xyz": return "chemical/x-pdb";
                case ".zip": return "application/zip";
                default: return string.Format("application/{0}", extensao);
            }
        }

        public FileContentResult DownloadArquivos(string nomePasta, string nomeArq, HttpContext context, bool pastaSegura = true)
        {
            FileContentResult fileContentResult;
            context.Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            try
            {               
                string pasta = (pastaSegura ? "App_Data\\" : "");
                // Gera o caminho de gravação do arquivo incluindo nome do arquivo, pasta e rota (wwwroot)
                string[] caminhosPastas = new string[]
                {
                    this.hostingEnv.WebRootPath.ToString(),
                    pasta,
                    nomePasta,
                    nomeArq
                };

                string caminhoArquivo = Path.Combine(caminhosPastas);
                var filename = caminhoArquivo;
                fileContentResult = new FileContentResult(System.IO.File.ReadAllBytes(filename), "application/octet-stream")
                {
                    FileDownloadName = nomeArq                    
                };
                context.Response.Headers.Add("Set-Cookie", "fileDownload=true; path=/");
            }
            catch (FileNotFoundException)
            {
                fileContentResult = null;
                context.Response.Headers.Add("Set-Cookie", "fileDownload=false; path=/");
            } 
            return fileContentResult;
        }
    }

    public class DadosArquivoUpload
    {
        public bool status { get; set; }       
        public string msgerro { get; set; }
        public List<string> enviados = new List<string>();
        public List<string> recusados = new List<string>();
    }
}