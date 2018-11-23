using MimeKit;
using Plataforma.Domain.Entities.NotMapped;
using System;
using MailKit.Net.Smtp;

namespace Plataforma.InfraEstrutura.Helpers
{
    public static class EmailEnvio
    {
        ///public static bool Email(string email, string assunto, string corpoEmail)
        public static bool Email(string email, string assunto, string corpoEmail, Configuracao_Sistema _Configuracao_Sistema)
        {
            //Configuracao_Sistema _Configuracao_Sistema = new Configuracao_Sistema();

            bool enviado = false;
            DadosConexaoEmail dadosEmail = _Configuracao_Sistema.DadosConexaoEmail;
            try
            {
                if (dadosEmail != null)
                {
                    //From Address 
                    string FromAddress = dadosEmail.FromAddress;
                    string FromAdressTitle = assunto;
                    //To Address 
                    string ToAddress = email;
                    string ToAdressTitle = assunto;
                    string Subject = assunto;
                    string BodyContent = String.Format("<div style=\"width:550px; margin:auto; padding-top:20px;\"> <img src=\"http://orgtsweb.azurewebsites.net/content/imagens/topo-email.jpg\" alt=\"Sistemas Diocesanos\" /> <h2 style=\"font-family:Arial, sans-serif; font-size:21px; color:#333;\">Olá você tem uma nova mensagem.</h2><table width=\"500\" style=\"font-family:Verdana, Geneva, Tahoma, sans-serif; color:#666; font-size:16px;\"><tbody><td style=\"font-size:14px;\">{0}</td></tr></tbody></table><br /><hr style=\"border:0px; height:1px; background-color:#333;\" /><br /><div style=\"width:500px;\"><img src=\"http://orgtsweb.azurewebsites.net/content/imagens/logo-mobile.gif\" alt=\"OrgSystem\" /></div></div>", corpoEmail); 

                    //Smtp Server 
                    string SmtpServer = dadosEmail.SmtpServer;
                    //Smtp Port Number 
                    int SmtpPortNumber = dadosEmail.SmtpPortNumber;

                    var mimeMessage = new MimeMessage();                    
                    mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                    mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
                    mimeMessage.Subject = Subject;                   
                    mimeMessage.Body = new TextPart("html")
                    {
                        Text = BodyContent
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect(SmtpServer, SmtpPortNumber, false);
                        client.Authenticate(dadosEmail.UserEmail, dadosEmail.PassEmail);
                        client.Send(mimeMessage);
                        client.Disconnect(true);
                        enviado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return enviado;
        }       
    }

}
