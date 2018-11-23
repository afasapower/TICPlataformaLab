$(document).ready(function ()
{
    // chamada do metodo "listasModulo" resposavel por carregar os dados do arquivo json para o DOM, recebendo o  
    // endereço do arquivo json responsavel por montar as listas
    listas.listasModulo('../sources/tribunais/inicioProcesso.json');
});

function customizacaoFormulario(secaoAtual) {
    if ($(secaoAtual).find('#bt-encerrar').length > 0) {
        $(secaoAtual).find('#bt-encerrar').click(function () {
            // grava na seção titanModulo o tipo de ação que será executado
            navegacao.gravaSessaoNavegacao('tipoAcao', 'I', 'titanModulo');
            menuEdicaoAbas.salvar(this, true);
        });
    }

    if ($(secaoAtual).find('#bt-encerrar-update').length > 0) {
        $(secaoAtual).find('#bt-encerrar-update').click(function () {
            // grava na seção titanModulo o tipo de ação que será executado
            navegacao.gravaSessaoNavegacao('tipoAcao', 'U', 'titanModulo');
            menuEdicaoAbas.salvar(this, true);
        });
    }
}