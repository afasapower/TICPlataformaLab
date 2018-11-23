var testesss = 0;
function Formulario() {
    // declaração da variavel de escopo responsavel por realizar a comunicação entre metodos 
    // e acessos a outras variaveis da classe
    var modForm = this;
    this.secaoAtualForm = null;

    // declaração do metodo IniciaFormulario
    this.iniciaFormulario = IniciaFormulario;
    this.limparCamposForm = false;
    function IniciaFormulario(secaoAtual, limparCampos) {
        // Recebe qual seção está aberta no momento
        var qualSecao = $(secaoAtual);
        modForm.secaoAtualForm = qualSecao;
        // Seleciona selects da aba aberta
        var selectsAbas = qualSecao.find('form .select-titan');
        // Inicia o plugin "selectpicker" com no máximo 7 itens a mostra após aberto
        selectsAbas.selectpicker({ size: 7 });
        // Chama o metodo responsável pelos eventos (ao mudar)
        modForm.acaoSelect(selectsAbas, qualSecao);
        // Chama o metodo responsável por estilizar os inputs do tipo file
        modForm.iniciaInputFileEstilos(qualSecao);
        // Chama o metodo responsável por gerar o calendário
        modForm.iniciaDatePicker(qualSecao);
        // Chama o metodo responsável pelas mascaras
        modForm.iniciaMascaras(qualSecao);
        // Chama o metodo responsável pela consulta na base dos correios
        modForm.buscaCep(qualSecao);

        modForm.selectLista(qualSecao);
        // Chama o metodo responsável por ativar os eventos dos botões Avulsos
        modForm.btsAvulsos(qualSecao);
        // Chama o metodo responsável por gerar o seletor de cores
        modForm.iniciaColorPicker(qualSecao);
        // Chama o metodo responsável por gerar os botões de downloads
        modForm.downloadArquivos(qualSecao);
        // Chama o metodo responsável por gerar os checkbox efeitos 
        modForm.ativaBootstrapToggleCheckBox(qualSecao);

        // Verifica se há alguma função de customização do JS da página
        if (typeof customizacaoFormulario === "function") {
            // Chama função de customização encaminhando dos parametros
            // secaoAtual: seção aberta no momento
            customizacaoFormulario(secaoAtual);
        }

        modForm.limparCamposForm = limparCampos;
        if (limparCampos === true) {
            modForm.resetarCampos(qualSecao);
        }
    }

    // Declaração do metodo reposanvel por carregar a grid com os dados de um "select-titan data-titan-lista' já selecionado anteriormente
    this.carregaGridSelectLista = CarregaGridSelectLista;
    function CarregaGridSelectLista() {
        if (modForm.secaoAtualForm === null || modForm.secaoAtualForm === undefined) {
            modForm.secaoAtualForm = $(navegacao.buscaSessaoNavegacao('abaAtual', 'titanModulo'));
        }
        // Verifica se não é um formulário já novo
        if (!modForm.limparCamposForm) {
            // Seleciona todos os ".select-titan" da aba atual
            modForm.secaoAtualForm.find('form .select-titan').each(function (evt) {
                // Verifica a presença de um select do tipo lista
                var tipoSelectlista = $(this).data('titan-lista');

                // Verifica se o select do tipo lista é válido
                if ((tipoSelectlista !== '' && tipoSelectlista !== undefined)) {
                    // Verifica se exixte uma grid ativa na aba
                    if ($.fn.DataTable.isDataTable(listas.dataTablesModulo.nomeLista)) {
                        // Seleciona a grid atual
                        var tabelaAtual = $(listas.dataTablesModulo.nomeLista).DataTable();
                        // Caso não haja dados carregados nesta lista ela será recarregada a através da trigger abaixo
                        if (!tabelaAtual.data().count()) {
                            // Chama o evento do select do tipo lista da aba
                            $(this).trigger('hidden.bs.select');
                        }
                    }
                }
            });
        }
    }


    this.resetarCampos = ResetarCampos;
    function ResetarCampos(secaoAtual) {
        console.log('resetarCampos');
        // limpa os campos do formulario para uma nova inserção (exceto o token)
        $(secaoAtual).find('textarea, input:not([name="__RequestVerificationToken"])').attr('readonly', false).val('');
        // habilita o campo de arquivo, caso haja
        $(secaoAtual).find('input[type="file"]').attr('disabled', false).val('');
        // habilita o select
        $(secaoAtual).find('select.select-titan').removeAttr('disabled').selectpicker('refresh');
    }


    // declaração do metodo AcaoSelect
    this.acaoSelect = AcaoSelect;
    function AcaoSelect(selectsAbas, qualSecao) {
        // remove o evento hidden.bs.select adicionado anteriormente
        $(selectsAbas.selector).off('hidden.bs.select');
        var idEmpresa = navegacao.buscaSessaoNavegacao('grempid', 'titan');
        var idModulo = navegacao.buscaSessaoNavegacao('iditem', 'titanModulo');
        var tipoAcao = navegacao.buscaSessaoNavegacao('tipoAcao', 'titanModulo');


        // varre o formulário em busca de selects com a classe select-titan
        selectsAbas.each(function (evt) {
            var parceiro = $(this).data('titan-parceiro');
            var url = $(this).data('titan-url');
            var destino = $(this).data('titan-destino');
            var lista = $(this).data('titan-lista');
            var menuAtivo = $(this).data('titan-menu-ativo');
            var idEdicao = $(this).data('titan-id-edicao');

            if ((parceiro !== '' && parceiro !== undefined) && (url !== '' && url !== undefined) && (destino === '' || destino === undefined) && (lista === '' || lista === undefined)) {
                var idSelecionado = $(this).val();
                if (idSelecionado !== "") {
                    modForm.populaSelects(selectsAbas, parceiro, url, idSelecionado, qualSecao);
                }

                $(this).on('hidden.bs.select', function (e) {
                    var atual = $(e.currentTarget);
                    modForm.populaSelects(selectsAbas, atual.data('titan-parceiro'), atual.data('titan-url'), atual.val(), qualSecao);
                })
            }

            // Responsável por carregar uma partial view na página
            // Pre-requisitos - declarar os data-sets (url, destino, menuAtivo) e NÃO declarar os data-sets (lista, parceiro)
            if ((parceiro === '' || parceiro === undefined) && (url !== '' && url !== undefined) && (destino !== '' && destino !== undefined) && (lista === '' || lista === undefined)) {
                // Verifica a existencia do data-set "titan-menu-ativo"
                if (menuAtivo === undefined || menuAtivo === null) alert('É obrigatorio a inserção do atributo dataset (titan-menu-ativo) com o valor "true" para habilitar o menu e false para desabilitalos no select: ' + $(this).attr('id'));

                var abaAtual = selectsAbas.closest('section.conteudo-aba').find('.menu-detalhes');
                // Caso o menu ativo for igual a FALSE e o valor selecionado for em branco
                // O menu detalhes será desativado e será ativado o select responsável por carregar a partial view       
                if (($(this).val() === '' || $(this).val() === undefined) && menuAtivo === false) {
                    // Desabilita o menu detalhes
                    abaAtual.find('button.editar-bt-detalhes, button.incluir-bt-detalhes, button.excluir-bt-detalhes').attr("disabled", true).addClass('inativo');
                    // Ativa o select na tela para carregar a partial view
                    $(this).attr("disabled", false);
                } else {
                    if (tipoAcao === "U" || tipoAcao === "I") {
                        // Recupera as informações do formulários atual
                        var formAtual = $(this).closest('section.conteudo-aba').find('form');
                        formAtual.find('select.select-lista-titan, button[type="button"].bt-select-lista-titan').attr('disabled', false);
                        // habilita os inputs dos tipos (radio e checkbox) para inserção de conteudo
                        abaAtual.closest('section.conteudo-aba').find('form input[type="radio"], form input[type="checkbox"]').attr('disabled', false);
                        // habilita o campo de arquivo, caso haja
                        formAtual.find('input[type="file"]').attr('disabled', false);
                        // chama o metodo para iniciar o editor de texto caso haja
                        editorTexto.iniciaEditorTexto($(this).closest('div.tab-content > .active').attr('id'), true, false);
                        // habilita os inputs e textareas para inserção de conteudo
                        abaAtual.closest('section.conteudo-aba').find('form input, form textarea').attr('readonly', false);
                        // chama o metodo para iniciar o plugin do input file caso exista
                        formularios.ativaInputFileEstilos($(this).closest('div.tab-content > .active').attr('id'));
                        // chama o metodo para iniciar o plugin de seletor de cores caso exista
                        formularios.ativaColorPicker(formAtual);

                        // seleciona todos selects da classe .select-titan
                        var selectTitan = abaAtual.closest('section.conteudo-aba').find('form select.select-titan');
                        // verifica a existencia dos atributos dataset abaixo
                        var parceiro = selectTitan.data('titan-parceiro');
                        var url = selectTitan.data('titan-url');
                        var destino = selectTitan.data('titan-destino');
                        var lista = selectTitan.data('titan-lista');
                        var editavel = selectTitan.data('titan-editavel');
                        // Caso for um select responsavel por carregar uma nova partial view dentro do menu ele será desabilitado até a conclusão da edição ou inclusão
                        if ((url !== '' && url !== undefined) && (destino !== '' || destino !== undefined) && (parceiro === '' || parceiro === undefined) && (lista === '' || lista === undefined) && (editavel === '' || editavel === undefined || editavel === false)) {
                            // Desabilita o select
                            selectTitan.attr('disabled', 'disabled');
                        } else {
                            // Habilita todos os selects
                            selectTitan.removeAttr('disabled').selectpicker('refresh');
                        }

                        if (tipoAcao === "I") {
                            // limpa os campos do formulario para uma nova inserção (exceto o token)
                            formAtual.find('textarea, input:not([name="__RequestVerificationToken"], [type="radio"], [type="checkbox"])').attr('readonly', false).val('');
                            // habilita o campo de arquivo, caso haja
                            formAtual.find('input[type="file"]').attr('disabled', false).val('');
                        }
                    }
                }


                // Evento responsável por verificar mudanças no select
                $(this).on('hidden.bs.select', function (e) {
                    // Caso o menu ativo for igual a FALSE e o valor selecionado for em branco
                    // O menu detalhes será ativado 
                    if (($(this).val() !== '' && $(this).val() !== undefined) && $(this).data('titan-menu-ativo') === false) {
                        // Ativa o menu detalhes
                        abaAtual.find('button.editar-bt-detalhes, button.incluir-bt-detalhes, button.excluir-bt-detalhes').attr("disabled", false).removeClass('inativo');

                        abaAtual.find('button.cancelar-bt-detalhes, button.salvar-bt-detalhes').addClass('remove');
                    }
                    var atual = $(e.currentTarget);
                    modForm.carregaAreaSelects($(e.currentTarget).data('titan-destino'), $(e.currentTarget).data('titan-url') + '?id=' + atual.val() + '&id_empresa=' + idEmpresa + '&id_modulo=' + idModulo, $(selectsAbas.selector).closest('.tab-pane.active'));

                    if ($(this).data('titan-id-edicao') !== $(this).val()) {
                        $(this).data('titan-id-edicao', $(this).val());
                    }
                });

                if (idEdicao !== '' && idEdicao !== undefined && $('#' + destino).html() === '') {
                    modForm.carregaAreaSelects(destino, url + '?id=' + idEdicao + '&id_empresa=' + idEmpresa + '&id_modulo=' + idModulo, $(selectsAbas.selector).closest('.tab-pane.active'));
                }
            }

            // select tipo 'lista' especializado em popular a lista existente da aba com novas informações 
            if ((lista !== '' && lista !== undefined)) {
                var idAbaAtual = navegacao.buscaSessaoNavegacao('idaba', 'titanModulo');
                // Caso a opção selecionada não conter valores o menu detalhes será removido
                if ($(this).val() === '') {
                    $(qualSecao).closest(".tab-pane.active").find('.menu-detalhes').hide();
                }


                // evento dos selects
                function acaoSelect(e) {
                    e.preventDefault();
                    // Retorna valor da opação selecionada
                    var valorSelecionado = $(e.currentTarget).val();
                    navegacao.gravaSessaoNavegacao('idlistaabas', valorSelecionado, 'titanModulo');
                    // Após selecionado um item com valores o menu detalhes será novamente mostrado
                    var menuDetalhes = $(qualSecao).closest(".tab-pane.active").find('.menu-detalhes');
                    // desabilita os botões (editar e excluir) do menu detalhes para iniciar uma nova inclusão
                    menuDetalhes.find('button.editar-bt-detalhes, button.excluir-bt-detalhes').attr("disabled", true).addClass('inativo');
                    menuDetalhes.show();

                    // Caso a opção selecionada não conter valores o menu detalhes será removido
                    // Se conter será gravado o valor seleciondo na session Storage
                    if (valorSelecionado === '') {
                        $('.menu-detalhes').hide();
                    } else {
                        var idSession = navegacao.buscaSessaoNavegacao('selectTitanLista', 'titanModulo');
                        if (idSession !== valorSelecionado) {
                            // Armazena os dados do botão clicado no sessionStorage para abertura do metodo "carregaAreasAbasForms"
                            var areasAbasForms = [];
                            navegacao.gravaSessaoNavegacao('areasAbasForms', areasAbasForms, 'titanModulo');
                        }

                        navegacao.gravaSessaoNavegacao('selectTitanLista', $(this).val(), 'titanModulo');
                    }
                    // Chama o metodo listaAbasModulo da classe ListasSistema passando os parametros (qualAba, qualSecao)
                    // qualAba = indica para qual item no objeto Json (abasModulo) é responsável por carregar as colunas e botões da lista
                    // qualSecao = passa para o botão visualizar da lista em qual seção deverá ser carregada as informção da lista para visualização completa ou edição
                    listas.listaAbasModulo(lista, '');

                    console.log('acaoSelect');
                }
                $(this).on('hidden.bs.select', acaoSelect);



                // Se conter um valor gravado na session Storage do tipo (selectTitanLista), será automaticamente selecionado o option correspondente
                var valorSelectListaSession = navegacao.buscaSessaoNavegacao('selectTitanLista', 'titanModulo');
                if (valorSelectListaSession !== undefined && valorSelectListaSession !== null && valorSelectListaSession !== '') {
                    // Atualiza o combobox
                    $(this).selectpicker('val', valorSelectListaSession);
                }
            }
        });
    }

    // declaração do metodo PopulaSelects
    this.populaSelects = PopulaSelects;
    function PopulaSelects(selectsAbas, selectName, url, valorParceiro, qualSecao) {
        var selectAtual = qualSecao.find('form select[name=' + selectName + ']');
        var idEdicao = selectAtual.data('titan-id-edicao');
        if (idEdicao === undefined || idEdicao === null) alert('É obrigatorio a inserção do atributo dataset (titan-id-edicao) no select: ' + selectName);

        $.ajax({
            url: url,
            dataType: 'json',
            data: { valor: valorParceiro, id_empresa: navegacao.buscaSessaoNavegacao('grempid', 'titan') },
            success: function (dados, status, xhr) {
                var conteudo = "";
                for (var i = 0; i < dados.valores.length; i++) {
                    var selecionado = "";
                    if (idEdicao === dados.valores[i]) selecionado = "selected";
                    conteudo += '<option data-tokens="' + dados.descricao[i] + '" value="' + dados.valores[i] + '"' + selecionado + '>' + dados.descricao[i] + '</option>';
                }
                qualSecao.find('form select[name=' + selectName + ']').html(conteudo).selectpicker('refresh').data('titan-id-edicao');
            },
            error: function (xhr, status, error) {
                erros.chamaErro(xhr.status);
            }
        });
    }

    // declaração do metodo CarregaAreaSelects
    this.carregaAreaSelects = CarregaAreaSelects;
    function CarregaAreaSelects(areaAbas, url, secaoAtual) {
        $('#' + areaAbas).load(url, function (responseTxt, statusTxt, xhr) {
            if (statusTxt === "success") {
                var tipoAcao = navegacao.buscaSessaoNavegacao('tipoAcao', 'titanModulo');
                // Chama metodo responsavel por habilitar o editor de texto caso exista
                editorTexto.iniciaEditorTexto($(secaoAtual).attr('id'), (tipoAcao === 'U' || tipoAcao === 'I') ? true : false, false);
                // Chama metodo responsavel por habilitar os inputs do formulário
                modForm.iniciaFormulario(secaoAtual, false);
            }

            if (statusTxt === "error") erros.chamaErro(xhr.status);
        });
    }



    this.iniciaInputFileEstilos = IniciaInputFileEstilos;
    function IniciaInputFileEstilos(qualSecao) {
        qualSecao.find('.upload-titan').each(function (index, element) {
            var desabilitado = ($(element).data('titan-disabled') !== undefined && $(element).data('titan-disabled') !== '') ? $(element).data('titan-disabled') : true;
            //var btRemover = qualSecao.find('.btn-remover-upload-titan');
            //btRemover.hide();


            $(element).filestyle({ 'buttonText': 'Selecione um arquivo', 'disabled': desabilitado, buttonName: "btn-success", placeholder: element.dataset.placeholder });

            //var btMaisUploadTitan = qualSecao.find('button.mais-btn-titan');
            //if (parseInt(btMaisUploadTitan.data('titan-max-arquivos')) === 1) {
            //    btMaisUploadTitan.hide();
            //}

        });

    }

    this.ativaInputFileEstilos = AtivaInputFileEstilos;
    function AtivaInputFileEstilos(qualSecao) {
        // O Tipo de ação que o usuário pretende realizar (salvar, editar) 
        var tipoAcao = navegacao.buscaSessaoNavegacao('tipoAcao', 'titanModulo');

        var qualSecaoAtiva = (qualSecao.indexOf('#') > -1) ? $(qualSecao) : $('#' + qualSecao);
        // modForm.componenteUpload(qualSecaoAtiva);
        qualSecaoAtiva.find('.upload-titan').filestyle('destroy');
        qualSecaoAtiva.find('.upload-titan').each(function (index, element) {
            $(element).filestyle({ 'buttonText': 'Selecione um arquivo', 'disabled': false, buttonName: "btn-success", placeholder: (tipoAcao === 'U') ? element.dataset.placeholder : '' });
        });
        console.log('AtivaInputFileEstilos');
        testesss = qualSecao;

        //qualSecaoAtiva.find('button.mais-btn-titan').click(() =>
        //{
        //    alert('testes');
        //    $('.involucro-input-file-titan').appendTo('.panel-file-titan');
        //});




        //var panelFileTitan = $('.panel-file-titan');
        //panelFileTitan.css({ 'overflow-y': "auto", 'max-height': '385px' });


        //var btRemover = qualSecaoAtiva.find('.btn-remover-upload-titan');
        //btRemover.removeAttr('disabled');

        //var btMaisUploadTitan = qualSecaoAtiva.find('button.mais-btn-titan');
        //btMaisUploadTitan.removeAttr('disabled');


        //btMaisUploadTitan.click((e) => {

        //    if (parseInt(e.currentTarget.dataset.titanMaxArquivos) > parseInt(qualSecaoAtiva.find('.involucro-input-file-titan').length)) {
        //        qualSecaoAtiva.find('.involucro-input-file-titan:first').clone().appendTo(panelFileTitan);
        //        var elementoCriado = qualSecaoAtiva.find('.involucro-input-file-titan:last .upload-titan');

        //        var novoInvolucro = qualSecaoAtiva.find('div.involucro-input-file-titan:last');
        //        var qtdInvolucro = qualSecaoAtiva.find('div.involucro-input-file-titan').length;

        //        novoInvolucro.find('.btn-remover-upload-titan').hide();
        //        novoInvolucro.find('select.select-titan').attr('id', novoInvolucro.find('select.select-titan').attr('id') + qtdInvolucro);

        //        novoInvolucro.find('.bootstrap-select > button.btn-default')[0].dataset.id = novoInvolucro.find('select.select-titan').attr('id');

        //        novoInvolucro.find('label > span').text('');  
        //        novoInvolucro.find('input').val('');

        //        testesss = novoInvolucro;

        //        console.log(novoInvolucro);

        //        var novoId = elementoCriado.attr('id') + novoInvolucro.length;
        //        elementoCriado.attr('id', novoId);
        //        var labelNovoElemento = elementoCriado[0].nextElementSibling;
        //        $(labelNovoElemento).attr('for', novoId);
        //        panelFileTitan.stop().animate({ scrollTop: '10000px' }, 'slow');





        //        btRemover = qualSecaoAtiva.find('.btn-remover-upload-titan');


        //        btRemover.click((e) => {
        //            var involucro = e.currentTarget.closest('div.involucro-input-file-titan');
        //            if (qualSecaoAtiva.find('div.involucro-input-file-titan').length > 1)
        //                involucro.remove();
        //            else {
        //                $(involucro).find('input').val('');
        //                $(involucro).find('.input-group > label > span').text('');
        //                $(e.currentTarget).hide();
        //            }
        //        });
        //        modForm.componenteUpload(qualSecaoAtiva);
        //    } else {
        //        swal("Quantidade maxima excedida!", "Você não tem permissão de inserir novos arquivos", "warning");
        //    }
        //});
    }

    this.componenteUpload = ComponenteUpload;

    function ComponenteUpload(qualSecaoAtiva) {
        qualSecaoAtiva.find('.upload-titan').each(function (index, element) {

            var label = element.nextElementSibling;
            var labelVal = label;
            element.addEventListener('change', function (e) {
                var involucro = e.currentTarget.closest('div.involucro-input-file-titan');
                var fileName = '';
                if (this.files)
                    fileName = e.target.value.split('\\').pop();

                if (fileName)
                    label.querySelector('span').innerHTML = fileName;
                else
                    label.innerHTML = labelVal;

                $(involucro).find('.btn-remover-upload-titan').show();
            });
            // Firefox bug fix
            element.addEventListener('focus', function () { element.classList.add('has-focus'); });
            element.addEventListener('blur', function () { element.classList.remove('has-focus'); });
        });
    }




    // declaração do metodo VerificaStatusSelectLista
    this.verificaStatusSelectLista = VerificaStatusSelectLista;
    // Metodo responsável por verificar a existência de um select de lista no formulário e atualizar o id da lista caso exista 
    function VerificaStatusSelectLista(lista, secaoAtual) {
        // Recebe qual seção está aberta no momento
        var qualSecao = $(secaoAtual);
        // Seleciona selects da aba aberta
        var selectsAbas = qualSecao.find('form .select-titan');
        // Varre o formulário a busca de selects com a classe .select-titan
        selectsAbas.each(function () {
            // verifica a existencia do atributo data-titan-lista
            var lista = $(this).data('titan-lista');
            // Caso haja é verificado se há um valor definido
            if ((lista !== '' && lista !== undefined)) {
                // Se o select ter a classe .select-titan e tiver o atributo data-titan-lista com um valor definido
                // então será regatado o valor do option selecionado e posteriormente a substituição do idlistaabas do session storage titanModulo
                navegacao.gravaSessaoNavegacao('idlistaabas', $(this).val(), 'titanModulo');
            }
        });
    }

    this.halitaConteudoOcultoSelectLista = HalitaConteudoOcultoSelectLista;
    function HalitaConteudoOcultoSelectLista() {
        // Verifica se contém um valor gravado na session Storage do tipo (selectTitanLista)
        var valorSelectListaSession = navegacao.buscaSessaoNavegacao('selectTitanLista', 'titanModulo');
        if (valorSelectListaSession !== undefined && valorSelectListaSession !== null && valorSelectListaSession !== '') {
            // Verifica se também contem dados na session Storage do tipo (areasAbasForms), para abertura dos formulários
            var areasAbasForms = navegacao.buscaSessaoNavegacao('areasAbasForms', 'titanModulo');
            if (areasAbasForms !== '' && areasAbasForms !== undefined) {
                // Verifica se tem algum id garvado, para que não abre um formulario em branco
                if (areasAbasForms[2] !== '' && areasAbasForms[2] !== undefined & areasAbasForms[2] !== null) {
                    // Verifica se há algum elemento html que será o destino do formulário 
                    if ($(areasAbasForms[0]).length > 0) {
                        // Verifica se já existe um conteúdo aberto no elemento pai
                        if ($(areasAbasForms[0]).html().length === 0) {
                            // Chama o metodo responsável pelo carregamento do formulário
                            navegacao.carregaAreasAbasForms(areasAbasForms, false);
                        }
                    }
                }
            }
        }
    }



    this.iniciaDatePicker = IniciaDatePicker;
    function IniciaDatePicker(qualSecao) {
        $('.titan-date-picker').datetimepicker({
            format: 'DD/MM/YYYY',
            locale: 'pt-br',
            widgetPositioning: {
                horizontal: 'left'
            },
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-arrow-up",
                down: "fa fa-arrow-down"
            }
        });

        $('.titan-hour-picker').datetimepicker({
            widgetPositioning: {
                horizontal: 'left'
            },
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-arrow-up",
                down: "fa fa-arrow-down"
            },
            format: 'HH:mm'
        });
    }


    this.iniciaColorPicker = IniciaColorPicker;
    function IniciaColorPicker(qualSecao) {
        var inputColor = qualSecao.find('form input.titan-color-picker');
        var idEnvolucro = 'titan-color-component' + inputColor.attr('id');
        inputColor.after('<div class="input-group colorpicker-component" id="' + idEnvolucro + '"></div>');
        $('#' + idEnvolucro).append('<span class="input-group-addon"><i></i></span>');
        inputColor.prependTo('#' + idEnvolucro);
        $('#' + idEnvolucro).colorpicker('disable');
    }

    this.ativaColorPicker = AtivaColorPicker;
    function AtivaColorPicker(qualSecao) {
        var componente = qualSecao.find('div.colorpicker-component');
        componente.colorpicker('enable');
    }

    this.ativaBootstrapToggleCheckBox = AtivaBootstrapToggleCheckBox;
    function AtivaBootstrapToggleCheckBox(qualSecao) {
        var checkBox = $('.titan-checkbox');
        checkBox.bootstrapToggle();
        checkBox.data('toggle', 'toggle');
    }






    this.trataErrosCampos = TrataErrosCampos;
    function TrataErrosCampos(data, formulario) {
        var erros = [];
        var textValidacao = '<ol style="list-style-type:none; margin:10px 10px; padding:7px; border:#f00 dashed 2px;">';
        console.log('TrataErrosCampos');
        console.log(data);
        if (!data.data.excecao && formulario !== null) {

            erros[0] = "Alguns itens do formulário são de preenchimento obrigatório.";

            for (var v = 0; v < data.data.length; v++) {
                // Localiza os campos com problema
                var inputErro = formulario.find('input[name="' + data.data[v].name + '"], textarea[name="' + data.data[v].name + '"], select[name="' + data.data[v].name + '"]');

                // Grava a classe de erro nos campos, para o efeito de borda pontilhada vermelha 
                // e verifica se o elemeto é um select para a aplica do efeito no elemento pai               
                (inputErro.is("select")) ? inputErro.parent().addClass('titan-erro-input').click(function () { $(this).removeClass('titan-erro-input'); $(this).unbind(); }) : inputErro.addClass('titan-erro-input').click(function () { $(this).removeClass('titan-erro-input'); $(this).unbind(); });

                // Verifica o campo input parceiro
                var qualLabel = '';
                if (inputErro.is("select")) {
                    if (inputErro.attr("id") === undefined || inputErro.attr("id") === null || inputErro.attr("id") === '')
                        if (inputErro.data('titan-parceiro') === undefined || inputErro.data('titan-parceiro') === null || inputErro.data('titan-parceiro') === '')
                            alert('É obrigatorio a inserção do atributo dataset (titan-parceiro) no input: ' + inputErro.attr("name"));
                        else
                            qualLabel = inputErro.data('titan-parceiro');
                    else
                        qualLabel = inputErro.attr("id");
                } else
                    qualLabel = inputErro.attr("id");
                // Monta os itens do erro
                textValidacao += "<li><strong>" + formulario.find('label[for="' + qualLabel + '"]').text() + ": </strong>" + data.data[v].errors + "</li>";
            }
            textValidacao += '</ol>';
        } else {
            erros[0] = "Não foi possivel realizar a ação desejada, verifique o motivo abaixo.";
            textValidacao += "<li>" + data.data.errors[0].errorMessage + "</li></ol>";
        }
        erros[1] = textValidacao;
        return erros;
    }

    this.iniciaMascaras = IniciaMascaras;
    function IniciaMascaras(qualSecao) {
        qualSecao.find('form input.titan-telefone').mask("(99) 9999-99999")
            .focusout(function (event) {
                var target, phone, element;
                target = (event.currentTarget) ? event.currentTarget : event.srcElement;
                phone = target.value.replace(/\D/g, '');
                element = $(target);
                element.unmask();
                if (phone.length > 10) {
                    element.mask("(99) 99999-9999");
                } else {
                    element.mask("(99) 9999-99999");
                }
            });

        qualSecao.find('form input.titan-cpf').mask('000.000.000-00');
        qualSecao.find('form input.titan-cnpj').mask('00.000.000/0000-00');
        qualSecao.find('form input.titan-date-picker').mask('00/00/0000');
    }

    this.buscaCep = BuscaCep;
    function BuscaCep(qualSecao) {
        // localiza todos os campos cep da aba
        qualSecao.find('form input.titan-cep').each(function () {
            $(this).parent().append('<div class="form-input-icon form-input-icon-left titan-loading-input"><i class="icmn-spinner2 util-spin" ></i></div>');
            qualSecao.find('.titan-loading-input').append($(this)).find('i').hide();
            // Injeta o evento ouvinte de teclado neles
            $(this).change(function (e) {
                // captura o valor do id do campo, para ser utilizado com link para os outros campos
                var qualCepForm = e.currentTarget.id;
                // captura que está sendo digitado no campo
                var valorInput = e.currentTarget.value.replace(/[^\w\s]/gi, '');
                e.currentTarget.value = valorInput;

                // Remove mensagens de retorno da API dos Correios
                qualSecao.find('form input#' + e.currentTarget.id).parent().find('small').remove();

                // Caso a quantidade de caracteres sejam iguais a 10, é disparado a busca do cep digitado
                if (valorInput.length === 8) {
                    // Captura dados básicos da aplicação para busca do cep
                    var data = new FormData();
                    data.append('cep', valorInput);
                    data.append('usuario', navegacao.buscaSessaoNavegacao('login', 'titan'));
                    data.append('id_empresa', navegacao.buscaSessaoNavegacao('grempid', 'titan'));
                    $('#' + qualCepForm).parent().find('i').show();

                    // Efetua um POST para actions responsavel pela pesquisa na base dos correios
                    $.ajax({
                        type: 'POST',
                        url: '../Componente/BuscaCep',
                        contentType: false,
                        processData: false,
                        cache: false,
                        data: data,
                        success: function (data, status) {
                            // Caso NÃO haja erros no backend será returnado com o valor verdadeiro a variavel "status"
                            if (data.status === true) {
                                $('#' + qualCepForm).parent().find('i').hide();
                                // Verifica se há retorno dos correios para prosseguir o andamento
                                if (data.data.return !== null && data.data.return !== undefined) {
                                    // Captura o retorno da cidade da api dos correios
                                    var qualCidade = data.data.return.cidade;
                                    // Captura o retorno do bairro da api dos correios e grava no input referente
                                    qualSecao.find('form input[data-titan-bairro="' + qualCepForm + '"]').val(data.data.return.bairro);
                                    // Captura o retorno do endereço da api dos correios e grava no input referente
                                    qualSecao.find('form input[data-titan-end="' + qualCepForm + '"]').val(data.data.return.end);
                                    // Captura o retorno do complemento da api dos correios e grava no input referente
                                    qualSecao.find('form input[data-titan-complemento="' + qualCepForm + '"]').val(data.data.return.complemento);
                                    // Captura o retorno do segundo complemento da api dos correios e grava no input referente
                                    qualSecao.find('form input[data-titan-complemento2="' + qualCepForm + '"]').val(data.data.return.complemento2);
                                    // Localiza o select estado parceiro do campo cep
                                    var estadoSelect = qualSecao.find('form select[data-titan-estado="' + qualCepForm + '"]');
                                    // Caso não encontre o endereço completo (Cep com final 000)
                                    if (data.data.return.bairro === '' && data.data.return.end === '') {
                                        qualSecao.find('form input#' + e.currentTarget.id).after('<small class="color-danger">Somente cidade e estado encontrados!</small>');
                                    }

                                    // Caso não haja o select estado será disparado uma mensagem de alerta
                                    if (estadoSelect.length !== 0) {
                                        // busca o option do select com o mesmo valor do retorno dos correios
                                        var inputValue = qualSecao.find('form select[data-titan-estado="' + qualCepForm + '"] option[data-tokens="' + data.data.return.uf + '"]').attr('value');
                                        // Aciona o evento do componente para atualizar o option selecionado
                                        estadoSelect.selectpicker('val', inputValue);
                                        // Aguarda o evento acima concluir
                                        estadoSelect.on('refreshed.bs.select', function (e) {
                                            // Captura o select de estado atual para ser utilizado pelas funções abaixo
                                            var selectAtual = qualSecao.find('#' + e.currentTarget.id);
                                            // Captura o valor do ID do select da cidade parceira do select do estado
                                            var selectCidade = selectAtual.data('titan-parceiro');
                                            // Aguarda o select de cidade ser finalizado
                                            qualSecao.find('#' + selectCidade).on('refreshed.bs.select', function (e) {
                                                // Aciona o evento do componente para atualizar o option selecionado no select da cidade
                                                $(this).selectpicker('val', $(this).find('option[data-tokens="' + qualCidade + '"]').attr('value'));
                                            });
                                            // Chama o metodo para popular o select da cidade
                                            modForm.populaSelects(qualSecao.find('form .select-titan'), selectCidade, selectAtual.data('titan-url'), selectAtual.val(), qualSecao);
                                        });
                                        // Atualiza o select do estado
                                        estadoSelect.selectpicker('refresh');
                                    } else
                                        // Aviso de preenchimento
                                        alert('É obrigatorio a inserção do atributo dataset (titan-estado) no select do estado com o id do input do cep como valor');
                                }
                                else {
                                    qualSecao.find('form input#' + e.currentTarget.id).after('<small class="color-danger">Endereço não encontrado!</small>');
                                }
                            }
                        }, error: function (xhr, ajaxOptions, thrownError) {
                            erros.chamaErro(xhr.status);
                        }
                    });
                }
            });
        });
    }


    this.selectLista = SelectLista;
    function SelectLista(qualSecao) {
        // O Tipo de ação que o usuário pretende realizar (salvar, editar) 
        var tipoAcao = navegacao.buscaSessaoNavegacao('tipoAcao', 'titanModulo');

        var selectsBtListasAbas = qualSecao.find('form .bt-select-lista-titan, form .select-lista-titan');
        selectsBtListasAbas.attr('disabled', ((tipoAcao === 'U' || tipoAcao === 'I') && (tipoAcao !== '' || tipoAcao !== undefined)) ? false : true);

        var selectsAbas = qualSecao.find('form .select-lista-titan');

        selectsAbas.dblclick(function () {
            var parceiro = $(this).data('titan-parceiro');
            $(this).find('option:selected').remove().appendTo('#' + parceiro);
        });

        qualSecao.find("form .bt-select-lista-titan").click(function () {
            var listaOrigem = $(this).data("titan-lista-origem");
            var listaparceiro = $(this).data("titan-parceiro");

            if ($("#" + listaOrigem).val() === null) {
                swal({
                    title: "Ops!",
                    text: "É necessário selecionar um item para mover",
                    type: "warning",
                    confirmButtonClass: "btn-info"
                });
            } else {
                $("#" + listaOrigem + " option:selected").remove().appendTo("#" + listaparceiro);
            }
        });
    }

    this.btsAvulsos = BtsAvulsos;
    function BtsAvulsos(qualSecao) {
        var formAtual = qualSecao.find('form');

        if (formAtual.find('.bt-titan-avulso-salvar').length > 0) {
            formAtual.find('.bt-titan-avulso-salvar').click(function () {
                // grava na seção titanModulo o tipo de ação que será executado
                navegacao.gravaSessaoNavegacao('tipoAcao', 'I', 'titanModulo');
                menuEdicaoAbas.salvar(this, true);
            });
        }

        if (formAtual.find('.bt-titan-avulso-atualizar').length > 0) {
            formAtual.find('.bt-titan-avulso-atualizar').click(function () {
                // grava na seção titanModulo o tipo de ação que será executado
                navegacao.gravaSessaoNavegacao('tipoAcao', 'U', 'titanModulo');
                menuEdicaoAbas.salvar(this, true);
            });
        }
    }


    this.buscaItensLista = BuscaItensLista;
    function BuscaItensLista(qualForm, lista) {
        $('#' + qualForm).off('keydown');
        $('#' + qualForm).keydown(function (evt) {
            console.log('aqui');
            var input, filtro, dl, dt, a, i, dd;
            input = document.getElementById(qualForm);
            filtro = input.value.toUpperCase();
            dl = document.querySelector(lista);
            dt = dl.getElementsByTagName('dt');
            dd = dl.getElementsByTagName('dd');
            for (i = 0; i < dt.length; i++) {
                a = dt[i];
                if (a.innerHTML.toUpperCase().indexOf(filtro) > -1) {
                    dt[i].style.display = "";
                    dd[i].style.display = "";
                } else {
                    dd[i].style.display = "none";
                    dt[i].style.display = "none";
                }
            }
            if (filtro === '') {
                dl.getElementsByTagName('dt').style.display = "";
            }
        });
    }

    this.downloadArquivos = DownloadArquivos;
    function DownloadArquivos(qualSecao) {
        // Seleciona os botões de download da aba aberta
        var ababotoesDownloads = qualSecao.find('form');
        ababotoesDownloads.on("click", "a.titan-bt-download", function () {
            $.fileDownload($(this).attr('href'),
                {
                    successCallback: function (url) {
                        swal("Arquivo baixado!", "Verifique na sua pasta de downloads.", "success");
                    },
                    failCallback: function (responseHtml, url) {
                        erros.chamaErro(400);
                    }
                });
            return false;
        });
    }
}