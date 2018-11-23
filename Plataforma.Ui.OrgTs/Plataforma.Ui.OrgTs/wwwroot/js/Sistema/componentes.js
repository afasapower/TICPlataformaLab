$(document).ready(function ()
{
    // chamada do metodo "listasModulo" resposavel por carregar os dados do arquivo json para o DOM, recebendo o  
    // endereço do arquivo json responsavel por montar as listas
    listas.listasModulo('../Sources/Sistema/Componentes.json');  
    hljs.initHighlightingOnLoad();
    //$('pre code').each(function (i, block)
    //{
    //    hljs.highlightBlock(block);
    //});
    // Guid Gerado aleatoriamente para liberação das abas dos modulos somente neste caso
    var idItemSecao = 'e235f08c-5892-4956-bae8-388489440e09';
    navegacao.gravaSessaoNavegacao('iditem', idItemSecao, 'titanModulo');
    navegacao.gravaSessaoNavegacao('idaba', idItemSecao, 'titanModulo');
    navegacao.gravaSessaoNavegacao('idlistaabas', idItemSecao, 'titanModulo');
});