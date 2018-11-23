using Microsoft.AspNetCore.Mvc;
using Plataforma.Domain.Entities.NotMapped;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Plataforma.InfraEstrutura.Helpers
{
    public class Seguranca: Controller
    {
        public static string MD5Hash(string senha)
        {
            using (var md5 = MD5.Create())
            {              
                var resultadoMD5 = md5.ComputeHash(Encoding.ASCII.GetBytes(senha));
                StringBuilder resultadoStringMD5 = new StringBuilder(resultadoMD5.Length * 2);
                for (int i = 0; i < resultadoMD5.Length; i++) resultadoStringMD5.Append(resultadoMD5[i].ToString("x2"));
                return resultadoStringMD5.ToString();               
            }
        }


        private const string SenhaCaracteresValidos = "abcdefghijklmnopqrstuvwxyz1234567890@#!?";
        public static string GeradorDeSenha()
        {
            //Aqui eu defino o número de caracteres que a senha terá
            int tamanho = 8;

            //Aqui pego o valor máximo de caracteres para gerar a senha
            int valormaximo = SenhaCaracteresValidos.Length;

            //Criamos um objeto do tipo randon
            Random random = new Random(DateTime.Now.Millisecond);

            //Criamos a string que montaremos a senha
            StringBuilder senha = new StringBuilder(tamanho);

            //Fazemos um for adicionando os caracteres a senha
            for (int i = 0; i < tamanho; i++)
                senha.Append(SenhaCaracteresValidos[random.Next(0, valormaximo)]);

            //retorna a senha
            return senha.ToString();
        }
    }
}