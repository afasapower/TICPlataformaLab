#pragma checksum "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d95a4ac6a77cbe91f3391f0af2a666ff1f71ec0a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sistema_AgendaAtendimento_AgendaAtendimento_aba), @"mvc.1.0.view", @"/Views/Sistema_AgendaAtendimento/AgendaAtendimento-aba.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Sistema_AgendaAtendimento/AgendaAtendimento-aba.cshtml", typeof(AspNetCore.Views_Sistema_AgendaAtendimento_AgendaAtendimento_aba))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d95a4ac6a77cbe91f3391f0af2a666ff1f71ec0a", @"/Views/Sistema_AgendaAtendimento/AgendaAtendimento-aba.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f31401908884db9e4a24e19d48fae8f000a7a976", @"/Views/_ViewImports.cshtml")]
    public class Views_Sistema_AgendaAtendimento_AgendaAtendimento_aba : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Plataforma.Ui.OrgTs.ViewModel.Sistema.AgendaAtendimentoViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "menus/MenuDetalhes", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("col-lg-12 col-md-12 col-sm-12"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("Sistema_AgendaAtendimento/AgendaAtendimento-aba"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
  
    Layout = null;

    Agenda_Atendimento dadosAgendaAtendimento = Model.agendaAtendimento ?? new Agenda_Atendimento();
    List<Pessoa> dadosPessoa = Model.listaPessoa;

    // Botões para o menu detalhes da aba  
    BtMenuDetalhes dadosMenu = new BtMenuDetalhes()
    {
        listaPermissoesMenuUtilitarios = new List<Retorno_Permissao_Grupo_Usuario>(),
        ListaMenuDetalhes = new List<MenuDetalhes>()
        {
        new MenuDetalhes(){Id = dadosAgendaAtendimento.id, Tipo = "salvar", Visivel = true},
        new MenuDetalhes(){Id = dadosAgendaAtendimento.id, Tipo = "cancelar", Visivel = true, Url = "Sistema_AgendaAtendimento/AgendaAtendimento-aba"},
        new MenuDetalhes(){Id = dadosAgendaAtendimento.id, Tipo = "incluir", Visivel = true},
        new MenuDetalhes(){Id = dadosAgendaAtendimento.id, Tipo = "deletar", Visivel = true},
        new MenuDetalhes(){Id = dadosAgendaAtendimento.id, Tipo = "atualizar", Visivel = true }
        }
    };


#line default
#line hidden
            BeginContext(1069, 241, true);
            WriteLiteral("<section class=\"conteudo-aba\">\r\n\r\n    <div class=\"row\">\r\n        <div class=\"col-lg-6 col-md-6 col-sm-6\">\r\n            <div class=\"col-lg-4 col-md-4 col-sm-4\">\r\n                <h1>Agenda de Atendimento</h1>\r\n            </div>\r\n            ");
            EndContext();
            BeginContext(1310, 55, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "dfbbb080e5924e8f8327d5ed39e2b0be", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 30 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
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
            BeginContext(1365, 121, true);
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <div></div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        ");
            EndContext();
            BeginContext(1486, 3573, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7fdf8f9aa1d04a36b70fa9c8225c85fc", async() => {
                BeginContext(1587, 14, true);
                WriteLiteral("\r\n            ");
                EndContext();
                BeginContext(1602, 23, false);
#line 40 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
       Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(1625, 419, true);
                WriteLiteral(@"
            <div class=""col-md-6"">
                <div class=""row"">
                    <div class=""form-group col-md-12"">
                        <label for=""id_tipo_agendamento"">Tipo de Agendamento</label><br />
                        <select class=""select-titan"" name=""id_tipo_agendamento"" tabindex=""1"" data-width=""50%"" data-live-search=""true"" id=""id_tipo_agendamento"" disabled>
                            ");
                EndContext();
                BeginContext(2044, 58, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7205575a30eb4144af93ca99e4bd4d79", async() => {
                    BeginContext(2061, 32, true);
                    WriteLiteral("Selecione um tipo de agendamento");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2102, 468, true);
                WriteLiteral(@"                            
                        </select>
                    </div>
                </div>

                <div class=""row"">
                    <div class=""form-group col-md-12"">
                        <label for=""nome_solicitante"">Nome do Solicitante</label>
                        <input type=""text"" class=""form-control"" name=""nome_solicitante"" tabindex=""2"" readonly placeholder=""Informe o Nome do Solicitante"" id=""nome_solicitante""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2570, "\"", 2618, 1);
#line 54 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
WriteAttributeValue("", 2578, dadosAgendaAtendimento.nome_solicitante, 2578, 40, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2619, 369, true);
                WriteLiteral(@">
                    </div>
                </div>

                <div class=""row"">
                    <div class=""form-group col-md-12"">
                        <label for=""data"">Data</label>
                        <input type=""text"" class=""form-control width-200 titan-date-picker"" name=""data"" tabindex=""3"" readonly placeholder=""Insira uma Data"" id=""data""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2988, "\"", 3024, 1);
#line 61 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
WriteAttributeValue("", 2996, dadosAgendaAtendimento.data, 2996, 28, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3025, 369, true);
                WriteLiteral(@">
                    </div>
                </div>

                <div class=""row"">
                    <div class=""form-group col-md-12"">
                        <label for=""hora"">Hora</label>
                        <input type=""text"" class=""form-control titan-hour-picker width-150"" name=""hora"" tabindex=""4"" readonly placeholder=""Insira uma Hora"" id=""hora""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 3394, "\"", 3430, 1);
#line 68 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
WriteAttributeValue("", 3402, dadosAgendaAtendimento.hora, 3402, 28, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3431, 427, true);
                WriteLiteral(@">
                    </div>
                </div>

                <div class=""row"">
                    <div class=""form-group col-md-12"">
                        <label for=""id_pessoa_responsavel"">Nome do Responsável</label><br />
                        <select class=""select-titan"" name=""id_pessoa_responsavel"" tabindex=""5"" data-live-search=""true"" id=""id_pessoa_responsavel"" disabled>
                            ");
                EndContext();
                BeginContext(3858, 43, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c4382f605caa4dd8b99a8addab5fa78a", async() => {
                    BeginContext(3875, 17, true);
                    WriteLiteral("Selecione um nome");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3901, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 77 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
                             foreach (var itens in dadosPessoa)
                             {
                                if (itens.id == dadosAgendaAtendimento.id_pessoa_responsavel)
                                {

#line default
#line hidden
                BeginContext(4130, 36, true);
                WriteLiteral("                                    ");
                EndContext();
                BeginContext(4166, 68, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "12ac35ba827340008b1acc7080fd9547", async() => {
                    BeginContext(4202, 23, false);
#line 81 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
                                                                  Write(itens.razao_social_nome);

#line default
#line hidden
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#line 81 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
                                       WriteLiteral(itens.id);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(4234, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 82 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
                                }
                                else
                                {

#line default
#line hidden
                BeginContext(4344, 36, true);
                WriteLiteral("                                    ");
                EndContext();
                BeginContext(4380, 59, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2390105eb5d54d3586604af37b7fde96", async() => {
                    BeginContext(4407, 23, false);
#line 85 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
                                                         Write(itens.razao_social_nome);

#line default
#line hidden
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#line 85 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
                                       WriteLiteral(itens.id);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(4439, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 86 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
                                }
                             }

#line default
#line hidden
                BeginContext(4508, 417, true);
                WriteLiteral(@"                        </select>
                    </div>
                </div>

                <div class=""row"">
                    <div class=""form-group col-md-12"">
                        <label for=""observacao"">Observação</label>
                        <textarea class=""form-control height-300"" name=""observacao"" tabindex=""6"" readonly placeholder=""Insira uma observação (opcional)"" id=""observacao"">");
                EndContext();
                BeginContext(4926, 33, false);
#line 95 "C:\Users\joaop\Desktop\plataformaBiblioteco\Plataforma.Ui.OrgTs\Plataforma.Ui.OrgTs\Views\Sistema_AgendaAtendimento\AgendaAtendimento-aba.cshtml"
                                                                                                                                                                    Write(dadosAgendaAtendimento.observacao);

#line default
#line hidden
                EndContext();
                BeginContext(4959, 93, true);
                WriteLiteral("</textarea>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5059, 24, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Plataforma.Ui.OrgTs.ViewModel.Sistema.AgendaAtendimentoViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
