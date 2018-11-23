$(document).ready(function ()
{
    // chamada do metodo "listasModulo" resposavel por carregar os dados do arquivo json para o DOM, recebendo o  
    // endereço do arquivo json responsavel por montar as listas
    listas.listasModulo('../sources/tribunais/movimentacaoDocumentos.json');
});
function customizacaoEditorTexto(secaoAtual)
{
    //'editor-titan'
    if ($('.note-editor').length > 0)
    {
        $('#atributosDocumento').show();
        formularios.buscaItensLista("pesquisa-lista", ".titan-lista-exemplo");
    }      
    else
        $('#atributosDocumento').hide();
    
    $('.titan-rolagem-vertical').height(1250);
}