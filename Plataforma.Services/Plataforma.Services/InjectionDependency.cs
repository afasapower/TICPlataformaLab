using Microsoft.Extensions.DependencyInjection;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Repository.Repository.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using Plataforma.Services.Services.Sistema;

namespace Plataforma.Services
{
    /// <summary>
    /// Classe que implementa a injeção de dependência.
    /// </summary>
    public class InjectionDependency
    {
        /// <summary>
        /// Adiciona a injeção de dependência entre os repositorios e suas interfaces.
        /// </summary>
        /// <param name="services"></param>
        public static void InjecaoDependenciaRepositorios(ref IServiceCollection services)
        {
            #region Sistema

            services.AddSingleton<ILaboratorioRepository, LaboratorioRepository>();
            services.AddSingleton<IComputadorRepository, ComputadorRepository>();
            services.AddSingleton<ISoftwareRepository, SoftwareRepository>();

            services.AddSingleton<IAgenda_AtendimentoRepository, Agenda_AtendimentoRepository>();        
            services.AddSingleton<IGrupoRepository, GrupoRepository>();
            services.AddSingleton<IGrupo_UsuarioRepository, Grupo_UsuarioRepository>();
            services.AddSingleton<ILog_Erro_AplicacaoRepository, Log_Erro_AplicacaoRepository>();
            services.AddSingleton<IMenuRepository, MenuRepository>();
            services.AddSingleton<IMenu_SubRepository, Menu_SubRepository>();
            services.AddSingleton<IModuloRepository, ModuloRepository>();
            services.AddSingleton<IModulo_EmpresaRepository, Modulo_EmpresaRepository>();
            services.AddSingleton<IPaginaRepository, PaginaRepository>();       
            services.AddSingleton<IPermissao_GrupoRepository, Permissao_GrupoRepository>();
            services.AddSingleton<IPermissao_Grupo_EtapaRepository, Permissao_Grupo_EtapaRepository>();
            services.AddSingleton<IPermissao_UsuarioRepository, Permissao_UsuarioRepository>();
            services.AddSingleton<IPessoaRepository, PessoaRepository>();            
            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();            
            services.AddSingleton<IUsuario_EmpresaRepository, Usuario_EmpresaRepository>();
            services.AddSingleton<IUsuario_Empresa_AtivoRepository, Usuario_Empresa_AtivoRepository>();

            #endregion

        }

        /// <summary>
        /// Adiciona a injeção de dependência entre os serviços e suas interfaces.
        /// </summary>
        /// <param name="services"></param>
        public static void InjecaoDependenciaServicos(ref IServiceCollection services)
        {
            #region Sistema

            services.AddSingleton<ILaboratorioService, LaboratorioService>();
            services.AddSingleton<IComputadorService, ComputadorService>();
            services.AddSingleton<ISoftwareService, SoftwareService>();

            services.AddSingleton<IAgenda_AtendimentoService, Agenda_AtendimentoService>();
            services.AddSingleton<IGrupoService, GrupoService>();
            services.AddSingleton<IGrupo_UsuarioService, Grupo_UsuarioService>();
            services.AddSingleton<ILog_Erro_AplicacaoService, Log_Erro_AplicacaoService>();
            services.AddSingleton<IMenuService, MenuService>();
            services.AddSingleton<IMenu_SubService, Menu_SubService>();
            services.AddSingleton<IModuloService, ModuloService>();
            services.AddSingleton<IModulo_EmpresaService, Modulo_EmpresaService>();
            services.AddSingleton<IPaginaService, PaginaService>();
            services.AddSingleton<IPermissao_GrupoService, Permissao_GrupoService>();
            services.AddSingleton<IPermissao_Grupo_EtapaService, Permissao_Grupo_EtapaService>();
            services.AddSingleton<IPermissao_UsuarioService, Permissao_UsuarioService>();
            services.AddSingleton<IPessoaService, PessoaService>();
            services.AddSingleton<IUsuarioService, UsuarioService>();            
            services.AddSingleton<IUsuario_EmpresaService, Usuario_EmpresaService>();
            services.AddSingleton<IUsuario_Empresa_AtivoService, Usuario_Empresa_AtivoService>();
            services.AddSingleton<IEmpresa_UsuarioService, Empresa_UsuarioService>();

            #endregion            
        }
    }
}