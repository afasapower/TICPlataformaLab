#pragma checksum "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Grupo\interno.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2b13db35ccf0bff97793c64a258e6aed540d81c6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sistema_Grupo_interno), @"mvc.1.0.view", @"/Views/Sistema_Grupo/interno.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Sistema_Grupo/interno.cshtml", typeof(AspNetCore.Views_Sistema_Grupo_interno))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2b13db35ccf0bff97793c64a258e6aed540d81c6", @"/Views/Sistema_Grupo/interno.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f31401908884db9e4a24e19d48fae8f000a7a976", @"/Views/_ViewImports.cshtml")]
    public class Views_Sistema_Grupo_interno : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/min/sistema/grupo.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "menus/MenuAbasAtivos", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "menus/MenuUtilitarios", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "menus/MenuAbaspaginas", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Grupo\interno.cshtml"
  
    ViewData["Title"] = "Grupo";
    string[,] urlsend = new string[,]
    {
       { "GRUPO", "Sistema_Grupo/grupo-aba","-1"},
       { "PERMISSÕES", "Sistema_Grupo/grupo-permissao-aba","0"},
       { "PERMISSÃO DE GRUPO POR ETAPA", "Sistema_Grupo/permissao-grupo-etapa-aba","1"}
    };

#line default
#line hidden
            DefineSection("scripts", async() => {
                BeginContext(321, 10, true);
                WriteLiteral("    \r\n    ");
                EndContext();
                BeginContext(331, 49, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0c464a1107344bca9f7e63034383e01d", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(380, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            BeginContext(385, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(387, 39, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c4c4c4cb30034f30877dbceb598834d5", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(426, 98, true);
            WriteLiteral("\r\n\r\n<section class=\"page-content\">    \r\n        <section class=\"page-content-inner\">\r\n            ");
            EndContext();
            BeginContext(524, 226, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "2706300a92eb4886aec8fa83d71e4ddd", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#line 19 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Grupo\interno.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = new MenuUtilitariosViewModel() { listaPermissoesMenuUtilitarios = new List<Retorno_Permissao_Grupo_Usuario>(), menuUtilitario = new string[,] { { "voltar", "Sistema_Grupo" } } };

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
            BeginContext(750, 75, true);
            WriteLiteral("\r\n        </section>\r\n        <!-- Menu abas paginas internas -->\r\n        ");
            EndContext();
            BeginContext(825, 56, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "46008a1bdc5543299efbc8dd7d4823bf", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
#line 22 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Grupo\interno.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = urlsend;

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
            BeginContext(881, 274, true);
            WriteLiteral(@"
        <!-- Fim Menu abas paginas internas -->
        <section class=""page-content-inner"">
            <section class=""panel"">
                <div class=""panel-body"">
                    <div>
                        <div class=""tab-content padding-vertical-20"">
");
            EndContext();
#line 29 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Grupo\interno.cshtml"
                             for (int i = 0; i < urlsend.GetLength(0); i++)
                            {

#line default
#line hidden
            BeginContext(1263, 53, true);
            WriteLiteral("                                <div class=\"tab-pane\"");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 1316, "\"", 1329, 2);
            WriteAttributeValue("", 1321, "secao-", 1321, 6, true);
#line 31 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Grupo\interno.cshtml"
WriteAttributeValue("", 1327, i, 1327, 2, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1330, 47, true);
            WriteLiteral(" role=\"tabpanel\" aria-expanded=\"false\"></div>\r\n");
            EndContext();
#line 32 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_Grupo\interno.cshtml"
                            }

#line default
#line hidden
            BeginContext(1408, 628, true);
            WriteLiteral(@"                        </div>
                    </div>
                </div>
            </section>
        </section>
        <section class=""page-content-inner lista-abas inativo"">
            <section class=""panel"">
                <div class=""panel-body"">
                    <div class=""row"">
                        <div class=""col-lg-12"">
                            <table id=""lista-abas"" class=""table table-hover nowrap"" cellspacing=""0"" style=""width:100%;""></table>
                        </div>
                    </div>
                </div>
            </section>
        </section>
</section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
