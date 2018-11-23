$(document).ready(function ()
{
    // chamada do metodo "listasModulo" resposavel por carregar os dados do arquivo json para o DOM, recebendo o  
    // endereço do arquivo json responsavel por montar as listas
    listas.listasModulo('../sources/tribunais/movimentacoes/etapasProcesso.json');   
});

function customizacaoListaModulo(conteudoJson, tabela)
{    
    $(conteudoJson.nomeLista + ' tbody').on('click', 'td.details-control', function ()
    {       
        var tr = $(this).closest('tr');
        var row = $(conteudoJson.nomeLista).dataTable().api().row(tr);
        
        if (row.child.isShown())
        {     
            tr.find('span').removeClass('color-warning icmn-eye-minus').addClass('icmn-eye-plus color-info');
           
              // Esta linha já está aberta - fechar
            $('div.slider', row.child()).slideUp(function ()
            {
                console.log('slideUp');
                row.child.hide();
                tr.removeClass('shown');
            });  
        }
        else
        {
            tr.find('span').removeClass('icmn-eye-plus color-info').addClass('color-warning icmn-eye-minus');
            // Abrir esta linha           
            MontaDetalheProcessoLista(row.data(), row, tr);           
        }
    });
}

function MontaDetalheProcessoLista(d, linha, tabela)
{
    
    var data = new FormData();
    data.append('id_tribunal', d.id_tribunal);
    data.append('iditem', d.id);
    data.append('id_empresa', navegacao.buscaSessaoNavegacao('grempid', 'titan').toString());
    navegacao.gravaSessaoNavegacao('iditem', d.id, 'titanModulo');

    var conteudo = '';
    $.ajax({
        type: 'POST',
        url: 'Tribunal_EtapasMovimentacoesProcesso/FasesProcesso',
        dataType: 'json',
        contentType: false,
        processData: false,
        data: data,
        success: function (data, status)
        {           
            if (data.status === true)
            {               
                var conteudo = '<div class="container-fluid padding-top-10 padding-bottom-10"><div class="row slider"><div class="col-md-12 padding-top-10 padding-bottom-15"><h1 class="color-secondary">Fases do processo</h1><hr /></div>';
                var linhas = '';
                var qualCor = 'btn-primary';
                var retorno = data.data;
                var totalRetorno = retorno.length;                
                for (var i = 0; i < totalRetorno; i++) {
                    var faseEncerrada = retorno[i].data_validade_fim;
                    var faseAberta = retorno[i].data_validade_inicio;
                    var faseAtiva = (faseAberta !== '') ? true : false;
                    var dataFase = 'Não iniciada';
                    var nomeFase = retorno[i].etapa;

                    if (faseAberta !== '' && faseEncerrada === '') {
                        dataFase = 'Aberta em  ' + faseAberta.slice(0, 10);
                        qualCor = 'btn-success';
                    }
                    else if (faseEncerrada !== '' && faseAberta !== '') {
                        console.log('Encerrada em ' + faseEncerrada);

                        dataFase = 'Encerrada em ' + faseEncerrada.slice(0, 10);
                        qualCor = 'btn-info';
                    }
                    else if (faseEncerrada === '' && faseAberta === '')
                    { 
                        qualCor = 'btn-default';
                    }
                    linhas += '<div class="col-md-1 col-lg-1 height-150 margin-bottom-15"><div class="row"><div class="col-md-12 text-center"><div class="height-40 avatar width-50 text-center btn ' + qualCor + '" data-target=".info-detalhes-lista-processo" style="padding:3px 8px 10px 0px; letter-spacing: -6px;" data-titan-url="' + retorno[i].url_destino +'/interno/?id_pagina=' + retorno[i].id_pagina +'" data-titan-fase-ativa="' + faseAtiva + '" data-titan-nome="' + nomeFase + '"><h1 class="font-size-40 color-white">' + retorno[i].ordem + '</h1></div></div><div class="col-md-12 margin-top-5 titulo-lista-etapas"><p class="text-center font-size-20 margin-bottom-0">' + nomeFase + '</p></div><div class="col-md-12 margin-top-5"><p class="text-center font-size-10 color-default">' + dataFase + '</p></div></div></div>';
                }

                conteudo += linhas + '</div>';
                linha.child(conteudo).show();
                tabela.addClass('shown');
                var trExt = $('div.slider', linha.child()).slideDown().closest('tr');
                trExt.addClass('info-linha-ativa');

                $('.info-linha-ativa .btn').click(function ()
                {           
                    // verifica se a fase está disponivel para acesso
                    if ($(this).data('titan-fase-ativa'))
                    {
                        // Id responsavel por popular as listagens internas das abas
                        navegacao.gravaSessaoNavegacao('idlistaabas', navegacao.buscaSessaoNavegacao('iditem', 'titanModulo'), 'titanModulo');
                        // Captura a url de destino
                        var endereco = $(this).data('titan-url');              
                        // Captura o nome da fase
                        var nomeFase = $(this).data('titan-nome');

                        if (endereco !== '' && endereco !== null)
                        {
                            navegacao.menuModulos(endereco, nomeFase);
                            $('.info-detalhes-lista-processo').modal('show');
                            setTimeout(function(){window.location.href = document.head.baseURI + endereco;}, 2000);
                        } else
                        {
                            swal("Requisição inválida", "Não foi possivel encontrar a fase cadastrada no sistema, por favor entre em contato com o suporte ténico.", "warning");
                        }
                    } else
                    {
                        swal("Fase indisponível", "O processo ainda não alcançou esta etapa, tente outra fase.", "warning");
                    }                                      
                });
            }
        }, error: function (xhr, ajaxOptions, thrownError)
        {
            erros.chamaErro(xhr.status);
        }
    });
}

function MontaDetalheEtapasProcessoLista()
{
    console.log('MontaDetalheEtapasProcessoLista');
    var info = '<ul class="list-group row">';        
    for (var i = 0; i < 20; i++)
    {
        info += '<li class="list-group-item col-xs-6 col-sm-4 col-md-3">teste '+ i +'</li >';
    }
    info += '</dl>';
    var colunas = '<div class="col-md-12">' + info +'</div>';
    return colunas;
}