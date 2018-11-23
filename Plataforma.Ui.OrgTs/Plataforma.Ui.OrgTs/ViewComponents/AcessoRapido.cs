using Microsoft.AspNetCore.Mvc;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using Plataforma.Ui.OrgTs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Plataforma.Ui.OrgTs.ViewComponents
{
    public class MenuGeral : ViewComponent
    {
        public IModuloService ModuloService { get; private set; }
        public IMenuService MenuService { get; private set; }
        public IMenu_SubService Menu_SubService { get; private set; }

        public MenuGeral(IModuloService moduloService,
                         IMenuService menuService,
                         IMenu_SubService menu_SubService)
        {
            ModuloService = moduloService;
            MenuService = menuService;
            Menu_SubService = menu_SubService;
        }

        public IViewComponentResult Invoke()
        {
            // modulo_dal menuModulo = new modulo_dal();
            // menu_dal menuAcao = new menu_dal();
            // menu_sub_dal menusub = new menu_sub_dal();

            // Captura ID do usuario logado
            string id = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "id").Value;

            // Recebe do banco os modulos permitidos para o usuario atual
            List<Modulo> selectMenuModulo = ModuloService.GetList_Modulo_Menu(new Guid(id)).ToList();

            // Recebe do banco os menus (cadastrar, relatorios..) através do id do modulo acima
            List<Menu> selectMenuAcao = new List<Menu>();

            // Retorna o menu retornado
            List<Menu> menus = new List<Menu>();

            foreach (var modulo in selectMenuModulo)
            {
                // Recupera os menus do módulo
                selectMenuAcao = MenuService.GetList((Guid)modulo.id, Guid.Empty, new Guid(id), new Domain.Entities.NotMapped.Busca_Generica.Parametros_Busca_Grid() { length = 200 }).ToList();

                // Percorre montado o menu do módulo
                foreach (var item in selectMenuAcao)
                {
                    if(item.id != Guid.Empty) { 
                        Menu _menu = new Menu();
                        _menu.id = item.id;
                        _menu.id_modulo = item.id_modulo;
                        _menu.id_situacao_cadastral = item.id_situacao_cadastral;
                        _menu.nome = item.nome;
                        _menu.descricao = item.descricao;
                        _menu.nome_imagem = item.nome_imagem;
                        _menu.ordem = item.ordem;
                        _menu.data_inclusao = item.data_inclusao;

                        menus.Add(_menu);
                    }
                }
            }            

            // Recebe do banco as páginas do menu (botões) através do id do usuário logado
            List<Menu_Sub> selectMenuSub = new List<Menu_Sub>();
            foreach (var idMenuAcao in menus)
            {
                selectMenuSub = Menu_SubService.GetListFront(new Guid(id)).ToList();
            }
            
            // Retorna na ViewModel
            var viewMenu = new LayoutViewModel()
            {
                menuModulos = selectMenuModulo,
                menuAcao = menus,
                menuSub = selectMenuSub
            };          
            return View(viewMenu);
        }
    }
}
