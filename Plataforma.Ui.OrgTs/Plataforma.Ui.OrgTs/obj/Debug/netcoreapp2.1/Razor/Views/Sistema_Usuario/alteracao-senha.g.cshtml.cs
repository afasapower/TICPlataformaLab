#pragma checksum "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Usuario\alteracao-senha.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "24c1ddb5420f01d0aa05d75100eccbe56b939a81"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sistema_Usuario_alteracao_senha), @"mvc.1.0.view", @"/Views/Sistema_Usuario/alteracao-senha.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Sistema_Usuario/alteracao-senha.cshtml", typeof(AspNetCore.Views_Sistema_Usuario_alteracao_senha))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\_ViewImports.cshtml"
using Plataforma.Ui.OrgTs;

#line default
#line hidden
#line 2 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\_ViewImports.cshtml"
using Plataforma.Domain.Entities.NotMapped;

#line default
#line hidden
#line 3 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\_ViewImports.cshtml"
using Plataforma.Domain.Entities.Sistema;

#line default
#line hidden
#line 4 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\_ViewImports.cshtml"
using Plataforma.Ui.OrgTs.ViewModel;

#line default
#line hidden
#line 5 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\_ViewImports.cshtml"
using Plataforma.Ui.OrgTs.ViewComponents;

#line default
#line hidden
#line 6 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\_ViewImports.cshtml"
using Plataforma.Ui.OrgTs.ViewModel.Components;

#line default
#line hidden
#line 7 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\_ViewImports.cshtml"
using Plataforma.Repository.Interfaces;

#line default
#line hidden
#line 8 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\_ViewImports.cshtml"
using System.Text.RegularExpressions;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"24c1ddb5420f01d0aa05d75100eccbe56b939a81", @"/Views/Sistema_Usuario/alteracao-senha.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f31401908884db9e4a24e19d48fae8f000a7a976", @"/Views/_ViewImports.cshtml")]
    public class Views_Sistema_Usuario_alteracao_senha : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Plataforma.Ui.OrgTs.ViewModel.Sistema.UsuarioViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "menus/MenuDetalhes", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("col-lg-12 col-md-12 col-sm-12"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("Sistema_Usuario/alteracao-senha"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Usuario\alteracao-senha.cshtml"
  
    Layout = null;

    Usuario dadosusuario = Model.usuario ?? new Usuario();
    Pessoa dadospessoa = Model.pessoa ?? new Pessoa();

    // Botões para o menu detalhes da aba solicitada   
    List<MenuDetalhes> dadosMenu = new List<MenuDetalhes>()
    {
        new MenuDetalhes(){Id = dadosusuario.id, Tipo = "salvar", Visivel = true},
        new MenuDetalhes(){Id = dadosusuario.id, Tipo = "cancelar", Visivel = true, Url = "Sistema_Usuario/usuario-aba"},
        new MenuDetalhes(){Id = dadosusuario.id, Tipo = "incluir", Visivel = true},
        new MenuDetalhes(){Id = dadosusuario.id, Tipo = "deletar", Visivel = true},
        new MenuDetalhes(){Id = dadosusuario.id, Tipo = "atualizar", Visivel = true}
    };

#line default
#line hidden
            BeginContext(806, 234, true);
            WriteLiteral("<section class=\"conteudo-aba\">\r\n    <div class=\"row\">\r\n        <div class=\"col-lg-8 col-md-8 col-sm-8\">\r\n            <div class=\"col-lg-4 col-md-4 col-sm-4\">\r\n                <h1>Inclusão Usuario</h1>\r\n            </div>\r\n            ");
            EndContext();
            BeginContext(1040, 55, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "7c425198067349a4b786c7da5d4c00a1", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 24 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Usuario\alteracao-senha.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = dadosMenu;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1095, 121, true);
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <div></div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        ");
            EndContext();
            BeginContext(1216, 2160, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae9890e938884bd28a27eac53222996e", async() => {
                BeginContext(1301, 14, true);
                WriteLiteral("\r\n            ");
                EndContext();
                BeginContext(1316, 23, false);
#line 34 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Usuario\alteracao-senha.cshtml"
       Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(1339, 377, true);
                WriteLiteral(@"
            <div class=""row"">
                <div class=""col-md-8"">                
                    <div class=""form-group col-md-12"">
                        <label for=""razao_social_nome"">Nome</label>
                        <input type=""text"" class=""form-control"" name=""razao_social_nome"" tabindex=""1"" readonly placeholder=""Insira seu nome"" id=""razao_social_nome""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1716, "\"", 1754, 1);
#line 39 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Usuario\alteracao-senha.cshtml"
WriteAttributeValue("", 1724, dadospessoa.razao_social_nome, 1724, 30, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1755, 287, true);
                WriteLiteral(@">
                    </div>

                    <div class=""form-group col-md-12"">
                        <label for=""login"">Login</label>
                        <input type=""text"" class=""form-control"" name=""login"" tabindex=""2"" readonly placeholder=""Insira seu login"" id=""login""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2042, "\"", 2069, 1);
#line 44 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Usuario\alteracao-senha.cshtml"
WriteAttributeValue("", 2050, dadosusuario.login, 2050, 19, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2070, 390, true);
                WriteLiteral(@">
                    </div>

                    <div class=""form-group col-lg-6 col-md-6 col-sm-6"">
                        <label for=""senha"">Senha</label>
                        <div class=""input-group"">
                            <input type=""password"" class=""form-control"" name=""senha"" tabindex=""4"" readonly placeholder=""Insira sua senha com no máximo 8 caracteres"" id=""senha""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2460, "\"", 2487, 1);
#line 50 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Usuario\alteracao-senha.cshtml"
WriteAttributeValue("", 2468, dadosusuario.senha, 2468, 19, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2488, 581, true);
                WriteLiteral(@">
                            <span class=""input-group-addon"">
                                <i class=""icmn-key""></i>
                            </span>
                        </div>
                    </div>
                    
                    <div class=""form-group col-lg-6 col-md-6 col-sm-6"">
                        <label for=""confirmar_senha"">Confirmar senha</label>
                        <div class=""input-group"">
                            <input type=""password"" class=""form-control"" name=""confirmar_senha"" tabindex=""5"" readonly id=""confirmar_senha""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 3069, "\"", 3096, 1);
#line 60 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Usuario\alteracao-senha.cshtml"
WriteAttributeValue("", 3077, dadosusuario.senha, 3077, 19, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3097, 272, true);
                WriteLiteral(@">
                            <span class=""input-group-addon"">
                                <i class=""icmn-key""></i>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3376, 24, true);
            WriteLiteral("\r\n    </div>\r\n</section>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Plataforma.Ui.OrgTs.ViewModel.Sistema.UsuarioViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
