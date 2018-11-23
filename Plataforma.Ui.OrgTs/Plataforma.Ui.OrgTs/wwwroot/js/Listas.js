function ListasSistema()
{
    // declaração da variavel de escopo responsavel por realizar a comunicação entre metodos 
    // e acessos a outras variaveis da classe
    modLista = this;

    // declaração do metodo ListasModulo
    this.listasModulo = ListasModulo;

    // declaração da variavel dadosJson responsavel por receber os dados do arquivo json
    this.dadosJson = null;

    // declaração da variavel urlArquivo responsavel por receber o endereço do arquivo json
    this.urlArquivo = null;

    // Metodo responsável por carregar o arquivo Json responsável por montar as colunas e botões das lista
    function ListasModulo(urlArquivo)
    {
        $.ajax({
            url: urlArquivo,
            dataType: 'json',
            success: function (dados, status, xhr)
            {           
                
                // Tranforma o Json recebido em string, devido ao bug do push quando das arrays responsaveis por adicionar botões as lista
                modLista.dadosJson = JSON.stringify(dados);

                console.log(dados.listaInicial);

                // verfica se está na página principal do modListaulo
                if (document.querySelectorAll(dados.listaInicial.nomeLista).length > 0)
                    // inicia o carremento da lista principal chamando o metodo "listaInicialModulo"
                    modLista.listaInicialModulo(dados.listaInicial);
                else
                {
                    // captura as informações do primeiro botão do menu abas
                    var primeiroBt = $('ul.nav-tabs.lista-menu li:first a');
                    // caso esteja dentro do modulo, inicia o carregamento da primeira lista caso exista
                    modLista.listaAbasModulo(primeiroBt.data('lista'), primeiroBt.data('target'));
                }

                // verifica se há uma chamada externa para esta página
                if (window.location.hash !== '' && window.location.hash !== undefined)
                {
                    // Chama o metodo responsável por verificar a autenticidade da chamada
                    navegacao.chamadaExterna();
                }
            },
            error: function (xhr, status, error)
            {
                erros.chamaErro(xhr.status);             
            }
        });        
    }   

    // Lista Geral
    // declaração do metodo ListaInicialModulo
    this.listaInicialModulo = ListaInicialModulo;
    // Declarações das informações passadas ou não pelo combobox responsalvel por alterar a grid inicial
    modLista.comboFiltroListaInicial = false;
    modLista.IdDomboFiltroListaInicial = '';
    // Metodo responsavel carregar a listas da página principal do modulo recebendo o parametro (itens) 
    // com o nome do id da tabela onde será carregada as informações do banco
    function ListaInicialModulo(itens)
    {
        
        // verifica se há o id da tabela na página
        if (document.querySelectorAll(itens.nomeLista).length > 0)
        {
             // Combo altera empresa usuario Orgsystem inicia invisivel
            $('.comboAlterDadosGridInicial').hide();           

            // Adiciona o botão de visualizaçao de itens por páginas
            var BtsFuncoes = itens.botoes;
            BtsFuncoes.push('pageLength');

            // Recebe os titulos e nomes das colunas vindas do arquivo json
            var colunas = itens.colunas;

            // Monta a quantidade de colunas da lista para converção das valores
            var targetColunas = new Array();
            for (var c = 0; c < colunas.length; c++) targetColunas[c] = c;
           
            // verifica se será inserido o botão visualizar
            
            if (itens.btVisualizarLista[0] !== '')
            {
                var urlBtvisualizar = itens.btVisualizarLista + "?id_pagina=" + navegacao.buscaSessaoNavegacao('idpagina', 'titan');

                // adiciona o botão visualizar na lista 
                itens.colunas.push(modLista.btVisualizarInicio(urlBtvisualizar));
            }
            
            // plugin responsavel pela montagem das lista
            var tabelaLista = $(itens.nomeLista).DataTable({
                contentType: 'application/json; charset=utf-8', 
                // Salva o estado da tabela (paginação atual)
                stateSave: false,
                // permite busca
                searching: true,
                // permite se adaptar a tela removendo colunas de acordo com espaço disponivel
                responsive: true,
                // gera o aviso de carregamento
                processing: true,
                // melhora a performance da grid para execução sem server-side
                deferRender: true,
                // habilita a possibilidade de solicitção por ajax
                //serverSide: true,
                //scrollX: true,
                // aguarda a conclusão do carregamento da lista
                initComplete: function (settings, json)
                {
                    var colunasSelects = '<option value="">Selecione uma opção</option>';
                    this.api().columns().every(function (index)
                    {
                        var colunaAtual = this.context[0].aoColumns[index];
                        if (colunaAtual.title !== '' && colunaAtual.title != undefined)
                        {
                            colunasSelects += '<option value="' + index + '">' + colunaAtual.title + '</option>';
                        }                        
                    });
                    
                    $('#colunas-grid').html(colunasSelects).selectpicker('refresh');
                    // remove manipuladores de eventos do elemento.
                    $("#busca-interna").unbind();

                    // adiciona o evento clique no botão "#bt-busca-lista"
                    $('#bt-busca-lista').on('click', function ()
                    {
                        var txtBusca = $('#busca-interna').val();
                       
                        if (txtBusca === '')
                        {
                            swal("Digite o que deseja pesquisar.");
                            return false; 
                        } 
                        var coluna = $('#colunas-grid').val();

                        if (coluna === '') {
                            swal("Selecione uma categoria ao lado!");
                            return false;
                        } 

                         // Comando responsavel por enviar a pesquisa e atualizar a lista
                        $(itens.nomeLista).dataTable().api().column($('#colunas-grid').val()).search(txtBusca).draw();
                       // console.log($(itens.nomeLista).dataTable().api().column($('#colunas-grid').val()));
                    });

                    // Verifica se há alguma função de customização do JS da página
                    if (typeof customizacaoListaModulo === "function")
                    {
                        // Chama função de customização encaminhando dos parametros
                        // itens: dados preenchidos no Json do modulo
                        // tabelaLista: dados da tabela ativa
                        customizacaoListaModulo(itens, tabelaLista);                        
                    }   


                    // Combo altera empresa usuario Orgsystem
                    $('.comboAlterDadosGridInicial').selectpicker().on('rendered.bs.select', function (e) {
                        // Coleta os dados da combo selecionada e passará para a variavel responsável por mandar o valor por paramentro para uma controller
                        modLista.IdDomboFiltroListaInicial = $(this).val();
                        // Altera o valor da variavel 'comboFiltroListaInicial' responsável por direcionar os valores do combo até a controller
                        modLista.comboFiltroListaInicial = true;
                        // Captura o id da grid atual
                        var tabelaAtual = $('table.dataTable').attr('id');
                        // Comando responsável por recarregar a grid
                        $('#' + tabelaAtual).dataTable().api().ajax.reload(null, false);
                        $(this).show();
                    });
                    // Após concluido o processo de solicitação de recarregamento da grid a variavel passa para o valor padrão false assim orientando o uso do id_empresa da session storage 
                    modLista.comboFiltroListaInicial = false;
                },
                ajax:
                {
                    url:  itens.url,
                    dataSrc: 'data',
                    type: "POST",
                    datatype:"json",
                    data: function (d)
                    {
                        var info = $(itens.nomeLista).DataTable().page.info();
                        d.page = info.page;                     
                        // parametro enviado para a pesquisa
                        //d.search = d.search.value;
                        // Passar todo parametro padrão por aqui
                        // Id Responsavel por carregar os itens da grid de acordo com a empresa atual
                        // Caso seja selecionado um item em uma combo o valor da variavel "comboFiltroListaInicial" será true e o valor buscado será o da variavel 'modLista.IdDomboFiltroListaInicial'
                        d.id_empresa = (!modLista.comboFiltroListaInicial) ? navegacao.buscaSessaoNavegacao('grempid', 'titan') : modLista.IdDomboFiltroListaInicial;
                        // Id do usuário logado no momento
                        d.id_usuario_logado = navegacao.buscaSessaoNavegacao('idus', 'titan');
                        //
                        d.id_modulo = navegacao.buscaSessaoNavegacao('iditem', 'titanModulo');
                    },
                    error: function (reason)
                    {
                        erros.chamaErro(reason.status);
                    }, complete: function (data) {
                        console.log(data);
                    }
                },
                pagingType: 'full_numbers',
                paging: true,
                ordering: true,
                // recebe e monta as colunas de acordo com os parametros recebidos no arquivo Json
                columns: colunas,                
                // tradução do plugin para o portugues
                language: { url: "/lib/datatables/media/js/Portuguese-Brasil.json" },
                // habilita o botão de paginação e ações (csv, excel...)
                dom: 'Brtip',
                // recebe e monta os botões do menu de ações (csv, excel...) de acordo com os parametros recebidos no arquivo Json
                buttons: BtsFuncoes,
                lengthMenu: [[25, 50, 100], ['25 linhas', '50 linhas', '100 linhas']],
               // columnDefs: [{"targets": targetColunas }]

                columnDefs: [{ "render": modLista.converteValorColunas, "targets": targetColunas }]
               
            }).buttons().container().appendTo(itens.nomeLista + '_wrapper .col-md-6:eq(0)');

           

            // verifica se houve alteração na lista exemplo (clique nos botões de paginação)
            $(itens.nomeLista).on('draw.dt', function ()
            {
                modLista.btsListasInicio(itens.nomeLista);
            });
        }
    }

    // Botão visualizar abas item
    // declaração do metodo BtVisualizarInicio
    this.btVisualizarInicio = BtVisualizarInicio;
    
    // Metodo responsavel por montar a função do botão visualizar da lista 
    function BtVisualizarInicio(_url)
    {             
        return {
            "data": "id",
            "mData": null,
            "bSortable": false,
            "mRender": function (data, type, row) {
                return '<a role="button" href="' + _url + '" class="btn btn-sm btn-success pull-right btn-lista-aba"><i class="icmn-eye" aria-hidden="true"></i> Visualizar </a>';
            }
        };        
    }


    // Botões lista
    // Declaração do metodo BtsListasAbas
    this.btsListasInicio = BtsListasInicio;
    // Metodo responsavel por popular os botões com os eventos
    function BtsListasInicio(qualLista)
    {
        // Recebe os dados de "listaInicial" oriundo do arquivo Json
        var dadosLista = JSON.parse(modLista.dadosJson).listaInicial;     
        // Verifica de qual ID do banco serão resgatados as informações para as abas
        var idListabanco = dadosLista.colunas[dadosLista.colunas.length - 1].data;

        // popula os botões visualizar da lista com o evento click
        $(qualLista).find('a.btn-lista-aba').unbind().click(function ()
        {            
            // busca na lista o retorno do banco com todos os valores 
            var data = $(qualLista).dataTable().api().row($(this).parents('tr')).data();
            // Id do item selecionado para edição e visualização na primeira listagem
            navegacao.gravaSessaoNavegacao('iditem', data[idListabanco], 'titanModulo');
            // Id responsavel por popular as listagens internas das abas
            navegacao.gravaSessaoNavegacao('idlistaabas', data[idListabanco], 'titanModulo');
            // Id da página principal (modulo - index)
           // navegacao.gravaSessaoNavegacao('idpagina', location.search.split('id_pagina='), 'titan');
            // variavel responsavel por capturar todos os valores dos botões
            var este = $(this);

            window.location.href = este.attr('href');
            // mata a propagação do evento clique do html
            return false;
        });
    }
    
    // Lista Abas
    this.listaAbasModulo = ListaAbasModulo;
    this.dataTablesModulo = null;
    // Metodo responsavel carregar a listas internas das abas recebendo como parametros (qualAba, qualSecao)
    function ListaAbasModulo(qualAba, qualSecao)
    {
        // Recebe os dados de "abasModulo" oriundo do arquivo Json
        var dadosAba = JSON.parse(modLista.dadosJson).abasModulo;

        modLista.dataTablesModulo = dadosAba;
        
        // verifica a existencia do id da tabela para iniciar o carregamento da lista
        if (document.querySelectorAll(dadosAba.nomeLista).length > 0)
        {
            // verifica se a lista já tenha sido carregada anteriormente
            if ($.fn.dataTable.isDataTable(dadosAba.nomeLista))
            {
                // caso a lista já tenha sido carregada anteriormente será necessario matar o plugin e reinicia-lo
                // os metodos abaixo localiza e destroi o plugin
                $(dadosAba.nomeLista).dataTable().fnClearTable();
                $(dadosAba.nomeLista).dataTable().fnDestroy();
                $(dadosAba.nomeLista).empty();
            }
                       

            // verifica se a aba tem o data-set chamado "data-lista", caso tenha ele verifica o valor 
            // a fim de localizar no arquivo json e executar o plugin caso o valor for -1 ele ignora e não monta e oculta a lista
            if (qualAba !== -1)
            {
                // caso a lista esteja ocultada é removida a classe 'inativo' responsavel por ocultar a lista
                $('.lista-abas').removeClass('inativo');
                // recebe os botões ações que irão estar ativos na lista oriundos do arquivo json
                BtsFuncoes = dadosAba.data[qualAba].botoes;
                // inicia o array que receberá os valores que gerarão as colunas das listas 
                var colunasTabela = new Array();
                // recebe as colunas que irão estar ativos na lista oriundos do arquivo json
                colunasTabela = dadosAba.data[qualAba].colunas;
                // verifica se terá o botão visualiza na lista

                // Monta a quantidade de colunas da lista para converção das valores
                var targetColunas = new Array();
                for (var c = 0; c < colunasTabela.length; c++) { targetColunas[c] = c; }
                                
                var qtdArrayBtVisualizar = dadosAba.data[qualAba].btVisualizarLista.length;
                if (qtdArrayBtVisualizar > 0)
                {
                    if (qtdArrayBtVisualizar > 1)
                    {
                        qualSecao = dadosAba.data[qualAba].btVisualizarLista[1];
                        $(qualSecao).html('');
                    }

                    // insere o botão visualizar na lista
                    colunasTabela.push(modLista.btVisualizarAbas(dadosAba.data[qualAba].btVisualizarLista[0], qualSecao, qualAba));
                }         
                
                // plugin responsavel pela montagem das lista
                $(dadosAba.nomeLista).DataTable({
                    // Salva o estado da tabela (paginação atual)
                    stateSave: false,
                    // permite busca
                    searching: true,
                    // permite se adaptar a tela removendo colunas de acordo com espaço disponivel
                    responsive: true,
                    // gera o aviso de carregamento
                    processing: true,
                    // melhora a performance da grid para execução sem server-side
                    deferRender: true,
                    // habilita a possibilidade de solicitção por ajax
                    //serverSide: true,
                    //scrollX: true,
                    //scrollCollapse: true,
                    //scroller: true,
                    // aguarda a conclusão do carregamento da lista
                    initComplete: function (settings, json)
                    {
                        // chama o metodo "btsListasAbas" responsavel por popular os botões com os eventos
                        modLista.btsListasAbas(dadosAba.nomeLista);

                        // Chama o metodo "carregaGridSelectLista" reposanvel por carregar os dados de um "select-titan data-titan-lista' já selecionado anteriormente
                        formularios.carregaGridSelectLista();
                                                
                        formularios.halitaConteudoOcultoSelectLista();
                    },
                    ajax:
                    {
                        url: dadosAba.data[qualAba].url,
                        type: "POST",
                        dataSrc: 'data',
                        data: function (d)
                        {
                            var info = $(dadosAba.nomeLista).DataTable().page.info();
                            d.page = info.page;
                            //d.search = d.search.value;
                            // Passar todo parametro padrão por aqui
                            d.iditem = navegacao.buscaSessaoNavegacao('idlistaabas', 'titanModulo');        
                            d.id_empresa = navegacao.buscaSessaoNavegacao('grempid', 'titan');
                        },
                        error: function (reason)
                        {
                            console.log(reason);
                            erros.chamaErro(reason.status);
                        },
                        complete: function (data)
                        {
                            console.log(data);
                        }
                    },                    
                    // recebe e monta as colunas de acordo com os parametros recebidos no arquivo Json
                    columns: colunasTabela,
                    // tradução do plugin para o portugues
                    language: { url: "/lib/datatables/media/js/Portuguese-Brasil.json" },
                    // habilita o botão de paginação e ações (csv, excel...)
                    dom: 'Brftip',
                    // recebe e monta os botões do menu de ações (csv, excel...) de acordo com os parametros recebidos no arquivo Json
                    buttons: BtsFuncoes,
                    lengthMenu: [[25, 50, 100], ['25 linhas', '50 linhas', '100 linhas']],
                    columnDefs: [{"targets": targetColunas }]
                    //columnDefs: [{ "render": modLista.converteValorColunas, "targets": targetColunas }]
                }).buttons().container().appendTo(dadosAba.nomeLista + '_wrapper .col-md-6:eq(0)');
            } else
            {
                // caso o valor do parametro "qualAba" for -1 ele adiciona a classe 'inativo' e oculta a lista
                $('.lista-abas').addClass('inativo');
            }

            // verifica se houve alteração na lista exemplo (clique nos botões de paginação)
            $(dadosAba.nomeLista).on('draw.dt', function ()
            {
                modLista.btsListasAbas(dadosAba.nomeLista);
            });
        }                  
    }

    this.atualizaTabelaAbas = AtualizaTabelaAbas;

    function AtualizaTabelaAbas()
    {
        var tabelaAtual = $('.lista-abas table.dataTable').attr('id');
        $('#'+tabelaAtual).dataTable().api().ajax.reload(null, false);
    }


    // Botões lista
    // declaração do metodo BtsListasAbas
    this.btsListasAbas = BtsListasAbas;
    // metodo responsavel por popular os botões com os eventos
    function BtsListasAbas(qualLista)
    {
        // Recebe os dados de "abasModulo" oriundo do arquivo Json
        var dadosAba = JSON.parse(modLista.dadosJson).abasModulo;

        // popula os botões visualizar da lista com o evento click
        $(qualLista).find('a.btn-lista-aba').unbind().click(function ()
        {
            // variavel responsavel por capturar todos os valores dos botões
            var este = $(this);
            // busca na lista o retorno do banco com todos os valores 
            var data = $(qualLista).dataTable().api().row(este.parents('tr')).data();           

            // Localiza as colunas da grid para busca de informações das tabelas do banco
            var colunasBanco = dadosAba.data[parseInt(este.data('aba-ativa'))].colunas;
            // Verifica de qual ID do banco serão resgatados as informações para a aba
            var idListabanco = colunasBanco[colunasBanco.length -1].data;           

            // idAba = será igual ao do item que está sendo editado no momento
            navegacao.gravaSessaoNavegacao('idaba', data[idListabanco], 'titanModulo');
            navegacao.gravaSessaoNavegacao('tipoAcao', '', 'titanModulo');

            var idAbaAtual = navegacao.buscaSessaoNavegacao('abaAtual', 'titanModulo');

            // Recupera as informações do menu detalhes
            var abaAtual = $(idAbaAtual).find('.menu-detalhes');           
            // Desabilita os botões (cancelar e salvar) do menu detalhes para iniciar uma nova inclusão
            abaAtual.find('button.cancelar-bt-detalhes, button.salvar-bt-detalhes').addClass('remove');
            abaAtual.find('button.incluir-bt-detalhes').attr("disabled", false).removeClass('inativo');

            // insere no data-set url do botão visualizar da lista a url com o id referencia para o carregamento
            // da aba com os valores do banco          
            // este.data('url', este.data('url'));
            este.data('carregado', false);  

            // Posiciona a página novamente para o formulário
            window.scrollTo(0, 0);

            if (dadosAba.data[parseInt(este.data('aba-ativa'))].btVisualizarLista.length === 1)
            {
                // chama o metodo carregaAbasForms da classe navegação passando como parametro os dados do botão
                navegacao.carregaAbasForms(este);      
            }
            else
            {               
                // Armazena os dados do botão clicado no sessionStorage para abertura do metodo "carregaAreasAbasForms"
                var areasAbasForms = [este.data('target'), este.data('url'), data[idListabanco]];
                navegacao.gravaSessaoNavegacao('areasAbasForms', areasAbasForms, 'titanModulo');          
                // chama o metodo carregaAbasForms da classe navegação passando como parametro os dados do botão
                navegacao.carregaAreasAbasForms(areasAbasForms, false);
            }
            // mata a propagação do evento clique do html
            return false;
        });
    }
    // Botão visualizar abas item
    // declaração do metodo BtVisualizarAbas
    this.btVisualizarAbas = BtVisualizarAbas;
    // Metodo responsavel por montar a função do botão visualizar da lista nas abas recebendo os seguintes paramentros:
    // _url = endereço da pagina a ser carregada
    // _secao = local no html onde será carregada a página
    function BtVisualizarAbas(_url, _secao, _abaAtiva)
    {        
        return {
            "data": "id",
            "mData": null,
            "bSortable": false,
            "mRender": function (ev) {
                return '<a role="button" href="#' + _url + '" data-target="' + _secao + '" data-url="' + _url + '" data-aba-ativa="' + _abaAtiva + '" class="btn btn-sm btn-success pull-right btn-lista-aba"><i class="icmn-eye" aria-hidden="true"></i> Visualizar </a>';
            }
        };
    }

    // declaração do metodo ConverteValorColunas
    this.converteValorColunas = ConverteValorColunas;
    // Metodo responsavel por formatar os valores oriundos do banco de dados:
    function ConverteValorColunas(data, type, row)
    {
        //console.log(data);
        //console.log('type: ' + type);
        //console.log(typeof data);

        if (typeof data !== 'string') {
            if (typeof data === 'boolean')
                return (data) ? 'Ativo' : 'Inativo';
            else
                return data;      
        } else
        {

           // var checagem = Date.parse(data);
            if (/^[\d]{4}-[\d]{2}-[\d]{2}/.test(data))
                return moment(data).format('DD/MM/YYYY');
            else
                return data;

        }
    }
}