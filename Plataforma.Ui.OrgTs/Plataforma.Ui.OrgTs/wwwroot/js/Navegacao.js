function Navegacao() {
    // declaração da variavel de escopo responsavel por realizar a comunicação entre metodos 
    // e acessos a outras variaveis da classe
    var mod = this;

    this.edicaoAtiva = false;
    this.idItemModulosAtivo = true;

    // Libera página para navegação após o carregamento
    $("#envolucroCarregador, #carregadorGeral").stop().fadeOut("slow");

    // Clique do logo da orgsystem
    $('#topo-sistema a.navbar-brand').click(function (evt) {
        // Responsável por limpar todos os dados dos modulos para a voltar para dashboard
        sessionStorage.removeItem('titanModulo');
    });

    //
    $('.menu-user-block a.log-out-titan').click(function (evt) {
        mod.logOutUsuario();
        return false;
    });




    // Menus abas formulários    
    // declaração do metodo CarregaAbasForms
    this.carregaAbasForms = CarregaAbasForms;
    // declaração do metodo BtsMenuAbasForms abaixo do metodo CarregaAbasForms
    this.btsMenuAbasForms = BtsMenuAbasForms;

    // Metodo responsavel carregar páginas das abas e listas internas das abas
    function CarregaAbasForms(qualbotao) {
        // variavael secaoAtual armazena e mantem o valor da posição (id) do repositorio, mantendo o endereçamento
        // mesmo depois da atualização (refresh) dos botões do menu detalhes
        var secaoAtual = qualbotao.data('target');
        // Filtra dos botões permitindo o acesso somente aos botões da lista com valores (undefined, null)
        // e para botões não clicados no menu abas 

        //--> Zera o tipo de ação que a aba iria realizar (Salvar, editar...)
        mod.gravaSessaoNavegacao('tipoAcao', '', 'titanModulo');

        //--> Armazena qual a aba ativa no momento
        mod.gravaSessaoNavegacao('abaAtual', secaoAtual, 'titanModulo');

        if ((!qualbotao.data('carregado') || qualbotao.data('carregado') === undefined || qualbotao.data('carregado') === null) && (secaoAtual !== null && secaoAtual !== '' && secaoAtual !== undefined)) {
            // HtmlLoader GIF de carregamento
            $("#envolucroCarregador, #carregadorGeral").stop().fadeIn();

            $(secaoAtual).load(qualbotao.data('url') + '?id=' + mod.buscaSessaoNavegacao('idaba', 'titanModulo') + '&id_empresa=' + mod.buscaSessaoNavegacao('grempid', 'titan') + '&id_modulo=' + mod.buscaSessaoNavegacao('iditem', 'titanModulo') + '&usuario=' + mod.buscaSessaoNavegacao('login', 'titan') + '&id_pagina=' + mod.buscaSessaoNavegacao('idpagina', 'titan') + '&id_menu=' + mod.buscaSessaoNavegacao('idmenu', 'titan') + '&id_usuario_logado=' + mod.buscaSessaoNavegacao('idus', 'titan'), function (responseTxt, statusTxt, xhr) {
                if (xhr.status === 200) {
                    menuEdicaoAbas.iniciarMenu(secaoAtual);
                    // Adiciona a classe active para expandir a área de visualização 
                    $(secaoAtual).addClass('active').data('aria-expanded', true);
                    // Marca o botão da aba que foi clicado com a classe active e altera o valor do data-set "carregado" para true 
                    // a fim de anular um novo carregamento, para manter os dados alterados no formulário  
                    qualbotao.data('carregado', true).addClass('active');

                    // verifica se há um menu de edição no formulário (incluir, anexar, remover...)
                    if ($("[data-toggle=tooltip]").length) {
                        // Habilita o tooltip para cada botão (incluir, anexar, remover...)
                        $("[data-toggle=tooltip]").tooltip();
                    }

                    // Editor Texto
                    editorTexto.iniciaEditorTexto(secaoAtual.replace('#', ''), false, false);

                    // Formulario
                    formularios.iniciaFormulario(secaoAtual, false);

                    // verificar qual botão antes de chamar o metodo abaixo === cancelar

                    if (qualbotao.data('tipo-bt') === 'cancelar') {
                        formularios.halitaConteudoOcultoSelectLista();
                    }

                } else {
                    erros.chamaErro(xhr.status);
                }

                // Libera página para navegação após o carregamento
                $("#envolucroCarregador, #carregadorGeral").stop().fadeOut("slow");
            });
        }
    }


    // Metodo responsavel popular com evento clique todos os botões do menu abas
    function BtsMenuAbasForms() {
        var idItemSecao = mod.buscaSessaoNavegacao('iditem', 'titanModulo');

        $(".container-menu a.nav-link").click(function (evt) {
            // Chamada o metodo responsavel por criar a requisição de abertura das abas, caso a resposta for TRUE será aberta a aba
            if (!mod.configAtualizacaoAbas($(this))) {
                evt.preventDefault();
                return false;
            }
        });

        // grava no idaba os valores iniciais para execução do primeiro formulário
        mod.gravaSessaoNavegacao('idaba', idItemSecao, 'titanModulo');

        // verifica se há uma chamada externa para esta página
        if (window.location.hash === '' || window.location.hash === undefined) {
            // busca as informção do primeiro botão das abas do modulo para iniciar o carregamento inicial pagina
            mod.carregaAbasForms($(".container-menu a.nav-link:first"));
        }

        // inicia o algoritimo de rolagem dos botões
        if (document.getSelection('ul.lista-menu').length) {
            // algoritimo resposavel por efetuar a rolagem do menu abas caso ela exceda o limite da página
            menuAbaScroll('.container-menu');
        }
        mod.montaMenuModulos();
    }

    // declaração do metodo ConfigAtualizacaoAbas
    this.configAtualizacaoAbas = ConfigAtualizacaoAbas;
    // Metodo responsavel por criar a requisição de abertura das abas
    function ConfigAtualizacaoAbas(qualBt) {
        idItemSecao = mod.buscaSessaoNavegacao('iditem', 'titanModulo');
        navegacao.gravaSessaoNavegacao('idaba', idItemSecao, 'titanModulo');
        navegacao.gravaSessaoNavegacao('idlistaabas', idItemSecao, 'titanModulo');

        if (idItemSecao === '00000000-0000-0000-0000-000000000000' || idItemSecao === '-1' || idItemSecao === '' || idItemSecao === undefined) mod.idItemModulosAtivo = false; else mod.idItemModulosAtivo = true;

        // Variavel indica se há uma edição em andamento para que não haja inserções em tabelas erradas
        // e evações sem salvamento por parte do cliente
        if (mod.edicaoAtiva === false && mod.idItemModulosAtivo === true) {
            // Capta todos os dados referente ao botão clicado
            var este = qualBt;

            formularios.verificaStatusSelectLista(este.data('lista'), este.data('target'));

            // Chama o metodo listaAbasModulo da classe ListasSistema passando os parametros (qualAba, qualSecao)
            // qualAba = indica para qual item no objeto Json (abasModulo) é responsável por carregar as colunas e botões da lista
            // qualSecao = passa para o botão visualizar da lista em qual seção deverá ser carregada as informção da lista para visualização completa ou edição
            listas.listaAbasModulo(este.data('lista'), este.data('target'));

            // responsável por carregar o item da aba escolhido recebendo como parametro os dados do botão clicado.
            mod.carregaAbasForms(este);

            return true;

        } else {
            // confirma se realmente a uma edição em andamento
            if (mod.edicaoAtiva === true) {
                // Gera um aviso ao cliente sobre a edição em aberto
                swal("Você tem uma edição em aberto, cancele ou salve para contiunar!");
            }

            if (mod.idItemModulosAtivo === false) {
                if (window.location.hash === '' || window.location.hash === undefined) {
                    // Gera um aviso ao cliente sobre a ausencia do cadastro
                    swal("Será necessário realizar o cadastro abaixo para continuar");
                } else {
                    window.location.href = window.location.origin + '/' + window.location.pathname;
                }
            }
            return false;
        }
    }

    // declaração do metodo ChamadaExterna
    this.chamadaExterna = ChamadaExterna;
    // Metodo responsável por verificar a autenticidade da chamada
    function ChamadaExterna() {
        // Captura a url da pagina
        var linkSessao = window.location;
        // Verifica se há algum hash no link
        if (linkSessao.hash) {
            // Compara se a url é compativel com a url da aplicação
            if (linkSessao.origin + '/' === document.head.baseURI) {
                // remove a classe ativa do botão atual da página requisitada
                $('.nav-tabs a.nav-link active').removeClass('active');
                // Captura o botão referente ao chamado externo
                var qualBtAba = $('.nav-tabs a.nav-link[href="' + linkSessao.hash + '"]');
                // Marca o novo botão da chamada externa como ativo
                qualBtAba.tab('show');
                // Requisita a montagem da aba referente ao chamado externo                    
                mod.configAtualizacaoAbas(qualBtAba);
            } else {
                // caso não seja compativel o link externo com a url da aplicação
                // o usuario é redirecionado a pagina inicial da aplicação 
                window.location.href = document.head.baseURI;
            }
        }
    }

    // Menu principal
    // declaração do metodo BtsMenuPrincipal
    this.btsMenuPrincipal = BtsMenuPrincipal;

    // Metodo responsavel por ativar o menu principal do sistema atraves do evento clique
    function BtsMenuPrincipal() {
        // propaga o vento clique na página
        $(document).on('click', function (e) {
            // remove as classes ativas do menu geral
            $(document).find('li.bt-ativo-menu-geral').removeClass('bt-ativo-menu-geral');
            $(document).find('div.ativo-menu-geral').removeClass('ativo-menu-geral');
            // localiza se a area clicada é um botão da navegação
            var qualMenu = $(e.target).parents('ul li').find('.menu-acao');
            // localiza o primeiro antecessor da classe .bt-modulo no caso o ul do id #menu-geral
            if (e.target.closest('.bt-modulo')) {
                // Adiciona a classe ('ativo-menu-geral') na div ".menu-acao" logo abaixo ao ".bt-modulo" e libera a expansão do ".menu-acao "
                qualMenu.addClass('ativo-menu-geral');
                // Adiciona a classe ('bt-ativo-menu-geral') na li clicada do ul do id #menu-geral pintando ele da cor referente
                $(e.target.parentElement.parentElement).addClass('bt-ativo-menu-geral');
            }
        });

        // bloqueia o evento do link nos botões dos modulos
        $('#menu-geral .bt-modulo > a').click(function (evt) { evt.preventDefault(); });

        // bloqueia o evento do link nos botões ações (Cadastros, movimentações, relatorios)
        $('#menu-geral .bt-acoes > a').click(function (evt) { evt.preventDefault(); return false; });

        // bloqueia o evento do link nos botões de divisão dos itens dos modulos
        $('#menu-geral .bt-itens-modulos > a').click(function (evt) { evt.preventDefault(); return false; });

        // bloqueia o evento do link nos botões das funções do menu
        $('#menu-geral .bt-itens-funcoes > a').click(function (evt) { evt.preventDefault(); });

        // Responsavel por direcionar para as páginas
        $('#menu-geral .bt-itens-funcoes').click(function (evt) {
            var tagA = $(this).find('a');
            mod.gravaSessaoNavegacao('idpagina', tagA.data('titan-idpagina'), 'titan');
            mod.gravaSessaoNavegacao('idmenu', tagA.data('titan-idmenu'), 'titan');

            window.location.href = tagA.attr('href');
            mod.menuModulos(tagA.attr('href'), tagA.text());
        });
    }


    // declaração do metodo gravaSessaoNavegacao
    this.gravaSessaoNavegacao = GravaSessaoNavegacao;
    function GravaSessaoNavegacao(qualVar, valor, sessao) {
        var titan = {};
        if (sessionStorage.getItem(sessao) !== null && sessionStorage.getItem(sessao) !== 'null') {
            titan = JSON.parse(sessionStorage[sessao]);
            titan[qualVar] = valor;
        } else {
            titan[qualVar] = valor;
        }
        sessionStorage.setItem(sessao, JSON.stringify(titan));
    }

    // declaração do metodo buscaSessaoNavegacao
    this.buscaSessaoNavegacao = BuscaSessaoNavegacao;
    function BuscaSessaoNavegacao(qualVar, sessao) {
        var returno = "";
        if (sessionStorage.getItem(sessao) !== null && sessionStorage.getItem(sessao) !== 'null') {
            var titan = JSON.parse(sessionStorage[sessao]);
            returno = titan[qualVar];
        }
        return returno;
    }

    // declaração do metodo buscaSessaoNavegacao
    this.alteraEmpresaUsuario = AlteraEmpresaUsuario;
    // Metodo responsável por verificar e atualizar os dados da session storage 'titan'' 
    function AlteraEmpresaUsuario() {
        // Função responsável por atualizar a session storage
        function acaoSelectEmpresa() {
            // Busca o Id do usuário na session storage
            var id_usuario = mod.buscaSessaoNavegacao('idus', 'titan');
            // Verifica se o Id do usuário é válido
            if (id_usuario !== null && id_usuario !== undefined && id_usuario !== '00000000-0000-0000-0000-000000000000' && id_usuario !== '') {
                // Caso for valido é chamado metodo abaixo na controller Contas
                $.ajax({
                    type: 'GET',
                    url: '../Sistema_Contas/AlteraUsuarioEmpresa',
                    data: { id_usuario: id_usuario, id_empresa: $('#select-topo-menu select').val() },
                    success: function (data, status) {
                        if (data.dados !== null) {
                            navegacao.gravaSessaoNavegacao('grempid', data.dados.id_pessoa_empresa, 'titan');
                            window.location.href = document.head.baseURI;
                        } else {
                            console.log('passar msg de erro');
                        }

                    }, error: function (xhr, ajaxOptions, thrownError) {
                        erros.chamaErro(xhr.status);
                    }
                });
            } else {
                // Caso o Id do usuário não exista é dado 3 segundos a fim de esperar o carregamento da aplicação e a rotina de verificação é reiniciada
                setTimeout(function () {
                    mod.alteraEmpresaUsuario();
                }, 3000);
            }
        }
        $('#select-topo-menu select').change(acaoSelectEmpresa);
        // Verifica se o Id da empresa atual corresponde ao Id da empresa na ssession Storage
        if (mod.buscaSessaoNavegacao('grempid', 'titan') !== $('#select-topo-menu select').val()) {
            // Caso não corresponda será chamada a função para a atualização do Id da empresa na sessionstorage
            acaoSelectEmpresa();
        }
    }
    mod.alteraEmpresaUsuario();


    // declaração do metodo buscaSessaoNavegacao
    this.logOutUsuario = LogOutUsuario;
    // Metodo responsável por deslogar usuário e remover os dados da session storage 'titan'' 
    function LogOutUsuario() {
        // Verifica se há dados no Session Storage, caso existir ele será removido para evitar loop com acesso de outro usuário.
        if (sessionStorage.getItem('titan') !== null && sessionStorage.getItem('titan') !== 'null') {
            sessionStorage.clear();
            location.href = $(evt.currentTarget).attr('href');
        }
    }

    // declaração do metodo carregaAreasAbasForms
    this.carregaAreasAbasForms = CarregaAreasAbasForms;

    // Metodo responsavel carregar áreas customizadas dentro das abas
    function CarregaAreasAbasForms(areasAbasForms, limparCampos) {
        var secaoAtual = areasAbasForms[0]; //qualbotao.data('target');
        var idAbaAtual = areasAbasForms[2]; // mod.buscaSessaoNavegacao('idaba', 'titanModulo');
        var menuDetalhes = $(secaoAtual).closest(".tab-pane.active").find('.menu-detalhes');


        console.log('idAbaAtual: ' + idAbaAtual);
        //--> Zera o tipo de ação que a aba iria realizar (Salvar, editar...)
        //--> verificar pois deu um problema no cadastro do MENU ÁREAS dentro do cadastro de MODULOS |-- navegacao.gravaSessaoNavegacao('tipoAcao', '', 'titanModulo');

        // HtmlLoader GIF de carregamento
        var htmlLoader = '<div class="row"><div class="col-lg-12 col-md-12 col-sm-12"><div class="height-150"><div class="vertical-align-middle center-block"><img src="../images/ajax-loader.gif" /></div></div></div></div>';

        $(secaoAtual).html(htmlLoader).load(areasAbasForms[1] + '?id=' + idAbaAtual, function (responseTxt, statusTxt, xhr) {
            if (xhr.status === 200) {
                menuDetalhes.find('button.excluir-bt-detalhes, button.editar-bt-detalhes').data('idaba', idAbaAtual);
                menuDetalhes.show();

                // Formulario
                formularios.iniciaFormulario('#' + $(secaoAtual).closest(".tab-pane.active").attr('id'), limparCampos);

                // Ativa botões editar e excluir
                menuDetalhes.find('button.editar-bt-detalhes, button.excluir-bt-detalhes').attr("disabled", false).removeClass('inativo');

            } else
                erros.chamaErro(xhr.status);
        });
    }

    // Menu utilitários
    // declaração do metodo BtsMenuUtilitarios
    this.btsMenuUtilitarios = BtsMenuUtilitarios;

    // Metodo responsavel por ativar o menu de utilitários do sistema através do evento clique
    function BtsMenuUtilitarios() {
        var BtsMenu = $('.menu-utilitario-topo a');
        var idPagina = BtsMenu.attr('href') + '?id_pagina=' + navegacao.buscaSessaoNavegacao('idpagina', 'titan');
        BtsMenu.click(function () {
            var valorInicial = ($(this).data('titan-tipo-id') === 'Guid') ? '00000000-0000-0000-0000-000000000000' : '-1';
            mod.gravaSessaoNavegacao('idaba', valorInicial, 'titanModulo');
            mod.gravaSessaoNavegacao('iditem', valorInicial, 'titanModulo');
            mod.gravaSessaoNavegacao('idlistaabas', valorInicial, 'titanModulo');
            mod.gravaSessaoNavegacao('tipoAcao', '', 'titanModulo');
        }).attr('href', idPagina);
    }

    // Menu Modulos
    // declaração do metodo MenuModulos
    this.menuModulos = MenuModulos;
    function MenuModulos(url, titulo) {
        var menuAbas = mod.buscaSessaoNavegacao('menuAbas', 'titanModulo');
        if (menuAbas !== '' && menuAbas !== undefined) {
            var posTitulo = -1;
            var posUrl = -1;
            for (var b = 0; b < menuAbas.length; b++) {
                menuAbas[b][2] = false;
                if (menuAbas[b].indexOf(titulo) > -1) posTitulo = b;
                if (menuAbas[b].indexOf(url) > -1) posUrl = b;
            }
            if (posTitulo === -1 && posUrl === -1) menuAbas.push([titulo, url, true]);
        } else {
            menuAbas = [];
            menuAbas.push([titulo, url, true]);
        }
        mod.gravaSessaoNavegacao('menuAbas', menuAbas, 'titanModulo');
    }

    this.montaMenuModulos = MontaMenuModulos;
    function MontaMenuModulos() {
        // Verifica todos os botões salvos no session location do navegador (modulos acessessados pelo usuário)
        var menuAbas = mod.buscaSessaoNavegacao('menuAbas', 'titanModulo');
        if (menuAbas !== '' && menuAbas !== undefined) {
            var btMenu = '';
            // Cria os botões com base nos dados recolhidos do navegador
            for (var i = 0; i < menuAbas.length; i++) {
                var ativo = '';
                var ativoLink = false;
                if (menuAbas[i][2] === true) ativo = 'ativo';
                if (ativo !== '') ativoLink = true;
                btMenu += '<div class="aba-modulo-ativo ' + ativo + ' nav-item"><a href="' + menuAbas[i][1] + '" data-pos-botao="' + i + '" target="_self" data-ativo="' + ativoLink + '">' + menuAbas[i][0] + '<span class="bt-fechar-modulo-ativo">&times;</span></a></div>';
            }

            // Insere todos os botão de modulo no menu
            $('#menu-abas-modulos .lista-menu').html(btMenu);

            // remove o botão de fechar do modulo ativo no momento
            $('#menu-abas-modulos .lista-menu > .nav-item.ativo span.bt-fechar-modulo-ativo').css('display', 'none');

            // Seleciona todos os botões do menu da interface
            var linkBts = $('#menu-abas-modulos div.aba-modulo-ativo a');
            // 
            linkBts.click(function (evt) {
                var menuAbas = mod.buscaSessaoNavegacao('menuAbas', 'titanModulo');
                for (var b = 0; b < menuAbas.length; b++) menuAbas[b][2] = false;
                menuAbas[$(this).data('pos-botao')][2] = true;
                mod.gravaSessaoNavegacao('menuAbas', menuAbas, 'titanModulo');
                // Zera todas as condições de paginas realizadas pelo modulo anterior
                mod.gravaSessaoNavegacao('tipoAcao', '', 'titanModulo');
                mod.gravaSessaoNavegacao('iditem', '', 'titanModulo');
                mod.gravaSessaoNavegacao('idlistaabas', '', 'titanModulo');
                mod.gravaSessaoNavegacao('idaba', '', 'titanModulo');

                mod.gravaSessaoNavegacao('idpagina', mod.urlParametros('id_pagina', $(this)[0].href), 'titan');
            });

            // Remove botão do modulo do menu
            $('#menu-abas-modulos div.aba-modulo-ativo a span.bt-fechar-modulo-ativo').click(function (evt) {
                if (!$(this).parent().data('ativo')) mod.removeBtMenuModulos($(this).parent().attr('href'), $(this).parent().text());
                evt.preventDefault();
            });

            // inicia o algoritimo de rolagem dos botões
            if ($('div.aba-modulo-ativo').length) {
                // algoritimo resposavel por efetuar a rolagem do menu abas caso ela exceda o limite da página
                menuAbaScroll('#menu-abas-modulos');
            }
        }
    }


    this.removeBtMenuModulos = RemoveBtMenuModulos;
    function RemoveBtMenuModulos(url, titulo) {
        var menuAbas = mod.buscaSessaoNavegacao('menuAbas', 'titanModulo');
        var posTitulo = -1;
        var posUrl = -1;
        for (var b = 0; b < menuAbas.length; b++) {
            if (menuAbas[b].indexOf(titulo) > -1) posTitulo = b;
            if (menuAbas[b].indexOf(url) > -1) posUrl = b;
        }
        if (posUrl !== -1) menuAbas.splice(posUrl, 1);

        mod.gravaSessaoNavegacao('menuAbas', menuAbas, 'titanModulo');
        mod.montaMenuModulos();
    }


    this.limparMenuModulos = LimparMenuModulos;
    function LimparMenuModulos() {
        var menuAbas = [];
        mod.gravaSessaoNavegacao('menuAbas', menuAbas, 'titanModulo');
    }


    this.verificaConexao = VerificaConexao;
    function VerificaConexao() {
        var xhr = new XMLHttpRequest();
        var file = document.head.baseURI + "images/blank-sem-internet.png";
        var randomNum = Math.round(Math.random() * 10000);
        xhr.open('HEAD', file + "?rand=" + randomNum, true);
        xhr.send();
        xhr.addEventListener("readystatechange", function (e) {
            if (xhr.readyState === 4) {
                if (document.getElementById('aviso-sem-internet')) {
                    if (xhr.status >= 200 && xhr.status < 304) {
                        document.getElementById('aviso-sem-internet').style.display = "none";
                    } else {
                        document.getElementById('aviso-sem-internet').style.display = "block";
                    }
                }
            }
        }, false);
    }
    window.addEventListener('online', mod.verificaConexao);
    window.addEventListener('offline', mod.verificaConexao);
    mod.verificaConexao();

    this.logOutSistema = LogOutSistema;
    function LogOutSistema() {
        $('#logOutSistema').click(function (evt) {
            evt.preventDefault();
            sessionStorage.clear();
            window.location.href = document.head.baseURI + evt.currentTarget.pathname;
            return false;
        });
    }
    mod.logOutSistema();


    // Responsável por filtra os paramentos das urls
    this.urlParametros = UrlParametros;
    function UrlParametros(_nome, _url) {
        var resultado = new RegExp('[\?&]' + _nome + '=([^&#]*)').exec(_url);
        return resultado[1] || 0;
    }
}  