#pragma checksum "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Shared\menus\MenuAbasPaginas.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f17e3e32b1fa55fa25e21c6c7ff5c8b57f83dd94"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_menus_MenuAbasPaginas), @"mvc.1.0.view", @"/Views/Shared/menus/MenuAbasPaginas.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/menus/MenuAbasPaginas.cshtml", typeof(AspNetCore.Views_Shared_menus_MenuAbasPaginas))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f17e3e32b1fa55fa25e21c6c7ff5c8b57f83dd94", @"/Views/Shared/menus/MenuAbasPaginas.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f31401908884db9e4a24e19d48fae8f000a7a976", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_menus_MenuAbasPaginas : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<string[,]>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(43, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(841, 289, true);
            WriteLiteral(@"

<div class=""container-menu"">
    <div class=""scroller scroller-left""><i class=""icmn-arrow-left""></i></div>
    <div class=""scroller scroller-right""><i class=""icmn-arrow-right""></i></div>
    <div class=""involucro-menu"">
        <ul class=""nav nav-tabs lista-menu"" role=""tablist"">
");
            EndContext();
#line 29 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Shared\menus\MenuAbasPaginas.cshtml"
             for (int i = 0; i < Model.GetLength(0); i++)
            {

#line default
#line hidden
            BeginContext(1204, 78, true);
            WriteLiteral("                <li class=\"nav-item\">\r\n                    <a class=\"nav-link\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1282, "\"", 1301, 2);
            WriteAttributeValue("", 1289, "#", 1289, 1, true);
#line 32 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Shared\menus\MenuAbasPaginas.cshtml"
WriteAttributeValue("", 1290, Model[i,1], 1290, 11, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1302, 11, true);
            WriteLiteral(" data-url=\"");
            EndContext();
            BeginContext(1314, 10, false);
#line 32 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Shared\menus\MenuAbasPaginas.cshtml"
                                                                 Write(Model[i,1]);

#line default
#line hidden
            EndContext();
            BeginContext(1324, 14, true);
            WriteLiteral("\" data-lista=\"");
            EndContext();
            BeginContext(1339, 10, false);
#line 32 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Shared\menus\MenuAbasPaginas.cshtml"
                                                                                          Write(Model[i,2]);

#line default
#line hidden
            EndContext();
            BeginContext(1349, 63, true);
            WriteLiteral("\" data-toggle=\"tab\" data-carregado=\"false\" data-target=\"#secao-");
            EndContext();
            BeginContext(1413, 1, false);
#line 32 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Shared\menus\MenuAbasPaginas.cshtml"
                                                                                                                                                                    Write(i);

#line default
#line hidden
            EndContext();
            BeginContext(1414, 35, true);
            WriteLiteral("\" role=\"tab\" aria-expanded=\"false\">");
            EndContext();
            BeginContext(1450, 21, false);
#line 32 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Shared\menus\MenuAbasPaginas.cshtml"
                                                                                                                                                                                                         Write(Model[i, 0].ToUpper());

#line default
#line hidden
            EndContext();
            BeginContext(1471, 29, true);
            WriteLiteral("</a>\r\n                </li>\r\n");
            EndContext();
#line 34 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Shared\menus\MenuAbasPaginas.cshtml"
            }

#line default
#line hidden
            BeginContext(1515, 33, true);
            WriteLiteral("        </ul>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<string[,]> Html { get; private set; }
    }
}
#pragma warning restore 1591
