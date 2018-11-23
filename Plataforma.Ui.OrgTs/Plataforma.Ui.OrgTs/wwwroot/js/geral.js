// Classes

var navegacao = new Navegacao();
var menuEdicaoAbas = new MenuEdicaoAbas();
var listas = new ListasSistema();
var erros = new ErrosSistema();
var calendario = new Calendario();
var graficos = new Graficos();
var contas = new Contas();
var editorTexto = new EditorTexto();
var formularios = new Formulario();

// Menu geral
$(document).ready(function ()
{    
    // Inicia menus abas
    navegacao.btsMenuAbasForms();
    // Menu principal
    navegacao.btsMenuPrincipal();
    // Menu utilitario
    navegacao.btsMenuUtilitarios();
    // Calendario
    calendario.iniciaCalendario();   
    // Contas
    contas.iniciaDadosConta();
    // Gráficos
    graficos.inicioGrafico();
});