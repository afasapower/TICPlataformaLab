$(document).ready(function ()
{
    // chamada do metodo "listasModulo" resposavel por carregar os dados do arquivo json para o DOM, recebendo o  
    // endereço do arquivo json responsavel por montar as listas
    listas.listasModulo('../sources/sistema/grupo.json');
});

function customizacaoFormulario(secaoAtual)
{
    if (navegacao.buscaSessaoNavegacao('tipoAcao', 'titanModulo') === 'I')
    {        
        var formAtual = secaoAtual.closest('section.conteudo-aba').find('form');
        formAtual.find('select.select-titan').val('').removeAttr('disabled').selectpicker('refresh');
        if ($('#menu_select').val() === '00000000-0000-0000-0000-000000000000')
        {
            $('#paginas_disponiveis, #paginas_selecionadas').html('');
            $('#paginas_selecionadas').data('titan-id-edicao', '00000000-0000-0000-0000-000000000000');           
        }        
    }

    $('select#paginas_selecionadas').click(function (e)
    {        
        var idPagina = e.currentTarget.selectedOptions[0].value;
        var idModulo = navegacao.buscaSessaoNavegacao('iditem', 'titanModulo');
        var idEmpresa = navegacao.buscaSessaoNavegacao('grempid', 'titan');
        var login = navegacao.buscaSessaoNavegacao('login', 'titan');
        
        $('#area-permissoes').load('Sistema_Grupo/grupo-permissao-area-permissoes/?id=' + idPagina + '&id_modulo=' + idModulo + '&id_empresa=' + idEmpresa, function (responseTxt, statusTxt, xhr)
        {
            console.log(statusTxt);
            if (statusTxt == "success")
            {
                $("#area-permissoes").off("click", 'input[type="radio"]');
                $('#area-permissoes input[type="radio"]').click(function ()
                {                   
                   EniviaPermissoes(idPagina, idModulo, idEmpresa, login);
                });
            }
            if (statusTxt == "error") { erros.chamaErro(xhr.status); }
        });
    });    
}

function EniviaPermissoes(idPagina, idModulo, idEmpresa, login)
{
    var dados = new FormData();
    dados.append('id_empresa', idEmpresa);
    dados.append('id_modulo', idModulo);
    dados.append('usuario', login);
    dados.append('id_pagina', idPagina);

    var inputs = $('#area-permissoes input[type=radio]:checked').serializeArray();
    for (var item = 0; item < inputs.length; item++)
    {
        dados.append(inputs[item].name, inputs[item].value);
    }   

    for (var key of dados)
    {
        console.log(key);
    }

    $.ajax({
        type: 'POST',
        url: 'Sistema_Grupo/grupo-permissao-area-permissoes',
        contentType: false,
        processData: false,
        cache: false,
        data: dados,
        success: function (data, status)
        {
            console.log(data);
            if (data.status === true)
            {
                     
            } else
            {
                
            }
        }, error: function (xhr, ajaxOptions, thrownError)
        {
            console.log(thrownError);
            console.log(ajaxOptions);
            console.log(xhr);
            erros.chamaErro(xhr.status);
        }
    });
}


function customizacaoGravacao(abaAtual)
{
    abaAtual.closest('section.conteudo-aba').find('form .select-titan, form .select-lista-titan, form button[type="button"].bt-select-lista-titan').attr("disabled", true);
}