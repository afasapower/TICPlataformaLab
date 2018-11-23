using Plataforma.Domain.Entities.Sistema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace Plataforma.Repository.Context
{
    /// <summary>
    /// Classe que representa o contexto da aplicação.
    /// </summary>
    public class PlataformaContext : DbContext
    {       
        #region Objeto de Sistema

        public DbSet<Agenda_Atendimento> Agenda_Atendimento { get; set; }
        public DbSet<Computador> Computador { get; set; }
        public DbSet<Laboratorio> Laboratorio { get; set; }
        public DbSet<Software> Software { get; set; }
        public DbSet<Grupo_Usuario> Grupo_Usuario { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<Log_Erro_Aplicacao> Log_Erro_Aplicacao { get; set; }
        public DbSet<Menu_Sub> Menu_Sub { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Modulo_Empresa> Modulo_Empresa { get; set; }
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<Pagina> Pagina { get; set; }
        public DbSet<Permissao_Grupo_Etapa> Permissao_Grupo_Etapa { get; set; }
        public DbSet<Permissao_Grupo> Permissao_Grupo { get; set; }
        public DbSet<Permissao_Usuario> Permissao_Usuario { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Situacao_Cadastral> Situacao_Cadastral { get; set; }
        public DbSet<Usuario_Empresa_Ativo> Usuario_Empresa_Ativo { get; set; }
        public DbSet<Usuario_Empresa> Usuario_Empresa { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        #endregion        

        public PlataformaContext()
        {
        }

        /// <summary>
        /// Seta o tempo de resposta do banco de dados
        /// </summary>
        /// <param name="options"></param>
        public PlataformaContext(DbContextOptions<PlataformaContext> options) : base(options)
        {
            this.Database.SetCommandTimeout(180);
        }

        /// <summary>
        /// Recupera a conexão com banco de dados
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseNpgsql(config.GetSection("Data")["StringPostGres"]);

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Sobreescreve o método SaveChanges.
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            SetaDataInclusao();

            return base.SaveChanges();
        }

        /// <summary>
        /// Verifica se existe a propriedade Data_Inclusao, caso ela exista e se trate de uma inserção de registro o campo recebe a data e horários atuais.
        /// </summary>private void SetaDataInclusao()
        private void SetaDataInclusao()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("data_inclusao") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("data_inclusao").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("data_inclusao").IsModified = false;
                }
            }
        }

        /// <summary>
        /// Verifica se existe a propriedade Data_Atualizacao, caso ela exista e se trate de uma atualização o campo recebe a data e horários atuais.
        /// </summary>
        private void SetaDataAtualizacao()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("data_atualizacao") != null))
            {
                if ((entry.State == EntityState.Modified))
                {
                    entry.Property("data_atualizacao").CurrentValue = DateTime.Now;
                }
            }
        }
    }
}