
function MenuEdicaoAbas()
{
    modEdicao = this;
    this.secaoMenuEdicaoAbas;
    this.salvar = Salvar;
    function Salvar(qualBt, avulso)
    {               
        // Inicialização de variaveis
        var abaAtual, qualIdSecaoAtual;

        // Caso avulso for TRUE foi utilizado um botão externo aos botões padrões do menu detalhes
        if (!avulso)        
            // Localiza qual menu de edição está ativo no momento
            abaAtual = qualBt.closest('section.conteudo-aba').find('.menu-detalhes');        
        else        
            // localiza qual seção está ativa no momento
            abaAtual = qualBt.closest('section.conteudo-aba');       

        // Localiza o valor do ID da aba ativa no momento para repassar para o metodo de captura do editor html
        qualIdSecaoAtual = $(qualBt).closest('div.tab-content > .active').attr('id');
       
        // Captura o login do usuário logado
        var login = navegacao.buscaSessaoNavegacao('login', 'titan');  
        // Captura o idpessoa do usuário logado
        var idpessoa = navegacao.buscaSessaoNavegacao('idpessoa', 'titan');  
        // Chama metodo responsável pela captura dos dados de conteúdo dos editores
        editorTexto.captaValores(qualIdSecaoAtual);
        // O Tipo de ação que o usuário pretende realizar (salvar, editar) 
        var tipoAcao = navegacao.buscaSessaoNavegacao('tipoAcao', 'titanModulo');
        // Captura o id da empresa em que o usuario tem acesso atualmente
        var grempid = navegacao.buscaSessaoNavegacao('grempid', 'titan').toString();
        // Captura o id da informação da aba ativa
        var id = (tipoAcao === 'I') ? '' : navegacao.buscaSessaoNavegacao('idaba', 'titanModulo');
        // Captura o id do modulo ativo 
        var idModulo = navegacao.buscaSessaoNavegacao('iditem', 'titanModulo');
              

        // Busca pelo formulario da aba ativa
        var formulario = $(abaAtual).closest('section.conteudo-aba').find('form');

        // Recupera os dados do formulário ativo
        var inputs = formulario.find('input:not([type="file"]), select:not(.select-lista-titan), textarea, input[type=radio]:checked, input[type=checkbox]:checked').serializeArray();
        //var inputs = formulario.serializeArray();
        // Armazena os valores capturados dos inputs e sessões do navegador
        var data = new FormData();
        // Popula o FormData com os valores dos inputs
        for (var item = 0; item < inputs.length; item++) 
        {            
            data.append(inputs[item].name, inputs[item].value);    
          //  console.log(inputs[item].name + ': ' + inputs[item].value);
        }


        // Localiza os selects select-lista-titan
        formulario.find('select.select-lista-titan[name]').each(function (index, selectListaFor)
        {
            // Coleta dados de tags do tipo select multiple
            $(selectListaFor).find('option').each(function (index, e)
            {               
                data.append(selectListaFor.name, e.value);
             //   console.log(selectListaFor.name+': '+e.value);
            });           
        });
     
        //--> envia o usuario logado para futuras auditorias 
        data.append('usuario', login);
        //--> envia o id do usuario logado
        data.append('id_usuario_logado', navegacao.buscaSessaoNavegacao('idus', 'titan'));
        //--> envia o id pessoa do usuario logado para futuras auditorias 
        data.append('id_pessoa', idpessoa);        
        // Adiciona o tipo de ação que o usuário deseja realizar
        data.append('acao', tipoAcao);
        // Caso a ação seja de edição ('U') é adicionado ao FormData o do item a ser editado       
        var menuDetalhes;
        if (tipoAcao === 'U')
        {
            if (!avulso)
            {
                menuDetalhes = abaAtual;
                data.append('id', menuDetalhes.find('.editar-bt-detalhes').data('idaba'));
            } else
                data.append('id', $(qualBt).data('idaba'));            
        }

        // Recupera o id do item selecionado na primeira grid (não é o id da aba)
        data.append('id_modulo', idModulo);       

        // Loop responsável por coletar os dados dos inputs do tipo "file"
        $(".upload-titan").each(function (index, e)
        {         
            // Localiza o input file
            var inputFileUpload = $(this).get(0);
            // Coleta os valores de cada input file
            var files = inputFileUpload.files;
            // Adiciona os valores coletados para o formdata
            for (var i = 0; i < files.length; i++) data.append(inputFileUpload.name, files[i]);
        });      

        // Adiciona o id da empresa logada atualmente
        data.append('id_pessoa_empresa', grempid);
        // Adiciona a empresa ativa
        data.append('id_empresa', grempid);

        var submeteDadosServidor = function ()
        {
            // Adiciona o alerta de status do carregmento dos dados 
            swal({ html: true, closeOnConfirm: false, closeOnCancel: false, showConfirmButton: false, showCancelButton: false, text: '<progress class="titan-progresso progress progress-primary" value="0" max="0">0%</progress>', title: "Aguarde processando..." });

            $.ajax({
                type: 'POST',
                url: formulario.attr('action'),
                contentType: false,
                processData: false,
                cache: false,
                data: data,
                xhr: function ()
                {
                    // Inicio da requisão http
                    var iniXhr = $.ajaxSettings.xhr();
                    // verifica a exixtencia do upload de dados
                    if (iniXhr.upload)
                    {
                        // Inicia a leitura dos dados
                        iniXhr.upload.addEventListener('progress', function (e)
                        {
                            // Verifica se existe dados a serem enviados
                            if (e.lengthComputable)
                            {
                                // Grava os valores nos atributos da tag progreess
                                $('.titan-progresso').attr({ value: e.loaded, max: e.total });
                            }
                        }, false);
                    }
                    // Retorna os valores iniciados
                    return iniXhr;
                },
                success: function (data, status)
                {                
                    if (data.status === true)
                    {
                        if (data.id !== '' && data.id !== undefined && data.id !== null)
                        {
                            menuDetalhes = abaAtual;
                            if (!avulso)
                            {
                                menuDetalhes.find('.editar-bt-detalhes').data('idaba', data.id);
                                menuDetalhes.find('.excluir-bt-detalhes').data('idaba', data.id);
                            } else
                            {
                                $(qualBt).data('idaba', data.id);
                            }
                           
                            navegacao.gravaSessaoNavegacao('idaba', data.id, 'titanModulo');
                            // Se for o primeiro item das abas o resultado (ID) será gravado como ID principal das abas
                            if ($(abaAtual).closest('.tab-pane').attr('id') === "secao-0" && idModulo === '00000000-0000-0000-0000-000000000000')
                            {
                                navegacao.gravaSessaoNavegacao('iditem', data.id, 'titanModulo');
                            }
                        }

                        var tituloSwal = (tipoAcao === 'I') ? 'Cadastro efetuado!' : 'Alteração efetuada!';

                        swal({
                            html: true,
                            title: tituloSwal,
                            text: "",
                            type: "success",
                            showCancelButton: false,
                            confirmButtonClass: "btn-success",
                            confirmButtonText: "ok",
                            cancelButtonText: "",
                            closeOnConfirm: true,
                            closeOnCancel: false
                        },
                            function (isConfirm)
                            {                         
                                if (isConfirm)
                                {
                                    $(abaAtual).find('button.editar-bt-detalhes, button.incluir-bt-detalhes, button.excluir-bt-detalhes').attr("disabled", false).removeClass('inativo remove');
                                    $(abaAtual).find('button.cancelar-bt-detalhes, button.salvar-bt-detalhes').addClass('remove');
                                    $(abaAtual).closest('section.conteudo-aba').find('form input, form textarea').attr('readonly', true);

                                  

                                    if (tipoAcao === 'I' && $('.dataTable').length === 0)
                                    {                                        
                                        $(abaAtual).find('button.incluir-bt-detalhes').attr("disabled", true).addClass('inativo remove');
                                    }
                                    


                                    // Seleciona todos os selects da aba (exceto o que possui o dataset = data-titan-lista)
                                    var selectTitan = $(abaAtual).closest('section.conteudo-aba').find('form select.select-titan:not([data-titan-lista])');
                                    // desabilita todos os selects selecionados acima
                                    selectTitan.prop('disabled', true).selectpicker('refresh');                            

                                    // Verifica se há alguma função de customização do JS da página
                                    if (typeof customizacaoGravacao === "function")
                                    {
                                        // Chama função de customização encaminhando dos parametros
                                        // secaoAtual: seção aberta no momento
                                        customizacaoGravacao(abaAtual, data);
                                    }

                                    editorTexto.iniciaEditorTexto(qualIdSecaoAtual, false, false);

                                    // Retorna a tela inicial do modulo
                                    if ($(qualBt).data('titan-retorna-inicio') || ($('.lista-menu a.nav-link').length === 1 && $('.dataTable').length === 0)) { window.history.back(); }

                                    formulario.find('input, select, textarea').removeClass('titan-erro-input');
                                    listas.atualizaTabelaAbas();                                   
                                }
                            });
                    } else
                    {
                        var erros = formularios.trataErrosCampos(data, formulario);
                        swal({
                            title: erros[0],
                            text: erros[1],
                            type: "warning",
                            html: true,
                            showCancelButton: false,
                            confirmButtonClass: "btn-warning",
                            confirmButtonText: "ok",
                            closeOnConfirm: false,
                            closeOnCancel: false
                        });

                    }
                }, error: function (xhr, ajaxOptions, thrownError) {
                    console.log(thrownError);
                    console.log(ajaxOptions);
                    console.log(xhr);
                    erros.chamaErro(xhr.status);
                }
            });
        };

        var confirmacao = null;
        if (!avulso)
        {
            if (tipoAcao === 'U')            
                confirmacao = abaAtual.find('.editar-bt-detalhes').data('titan-bt-confirmacao');            
            else
                confirmacao = $(qualBt).data('titan-bt-confirmacao');
        }            
        else
            confirmacao = $(qualBt).data('titan-bt-confirmacao');
        
        if (confirmacao !== undefined && confirmacao !== null)
        {
            var dadosConfirmacao = confirmacao.split(';');
            swal({
                title: dadosConfirmacao[0],
                text: dadosConfirmacao[2],
                type: dadosConfirmacao[1],
                showCancelButton: true,
                confirmButtonClass: "btn-success",
                confirmButtonText: dadosConfirmacao[3],
                cancelButtonText: "Cancelar",
                closeOnConfirm: false
            }, function ()
            {
                submeteDadosServidor();
            });
        } else
            submeteDadosServidor();               
    }
 

  
    this.editar = Editar;
    function Editar(qualBt)
    {
        // navegacao.edicaoAtiva = true;
        navegacao.gravaSessaoNavegacao('tipoAcao', 'U', 'titanModulo');
        // seleciona o menu detalhes da aba atual
        var abaAtual = qualBt.closest('section.conteudo-aba').find('.menu-detalhes');
        // desabilita os botões do menu detalhes da aba atual
        abaAtual.find('button.editar-bt-detalhes, button.incluir-bt-detalhes, button.excluir-bt-detalhes').attr("disabled", true).addClass('inativo');
        // habilita os botões do menu detalhes da aba atual
        abaAtual.find('button.cancelar-bt-detalhes, button.salvar-bt-detalhes').removeClass('remove');
        // habilita os inputs e textareas para inserção de conteudo
        abaAtual.closest('section.conteudo-aba').find('form input, form textarea').attr('readonly', false);
        // seleciona todos selects da classe .select-titan
        var selectTitan = abaAtual.closest('section.conteudo-aba').find('form select.select-titan');
        // verifica a existencia dos atributos dataset abaixo
        var parceiro = selectTitan.data('titan-parceiro');
        var url = selectTitan.data('titan-url');
        var destino = selectTitan.data('titan-destino');
        var lista = selectTitan.data('titan-lista');
        var editavel = selectTitan.data('titan-editavel');
        // Caso for um select responsavel por carregar uma nova partial view dentro do menu ele será desabilitado até a conclusão da edição ou inclusão
        if ((url !== '' && url !== undefined) && (destino !== '' || destino !== undefined) && (parceiro === '' || parceiro === undefined) && (lista === '' || lista === undefined) && (editavel === '' || editavel === undefined || editavel === false))
        {            
            // Desabilita o select
            selectTitan.attr('disabled','disabled');
        } else
        {
            // Habilita todos os selects
            selectTitan.removeAttr('disabled').selectpicker('refresh');
        }

        // Recupera as informações do formulários atual
        var formAtual = abaAtual.closest('section.conteudo-aba').find('form');
        formAtual.find('select.select-lista-titan, button[type="button"].bt-select-lista-titan').attr('disabled', false);
        // habilita os inputs dos tipos (radio e checkbox) para inserção de conteudo
        abaAtual.closest('section.conteudo-aba').find('form input[type="radio"], form input[type="checkbox"]').attr('disabled', false);
        // habilita o campo de arquivo, caso haja
        formAtual.find('input[type="file"]').attr('disabled', false);
        // chama o metodo para iniciar o editor de texto caso haja
        editorTexto.iniciaEditorTexto(qualBt.closest('div.tab-content > .active').attr('id'), true, false);    

        // chama o metodo para iniciar o plugin do input file caso exista
        formularios.ativaInputFileEstilos(qualBt.closest('div.tab-content > .active').attr('id'));

        // chama o metodo para iniciar o plugin de seletor de cores caso exista
        formularios.ativaColorPicker(formAtual);
    }

    this.cancelar = Cancelar;
    function Cancelar(qualBt)
    {
        navegacao.gravaSessaoNavegacao('tipoAcao', '', 'titanModulo');       
        navegacao.edicaoAtiva = false;
        qualBt.data('target','#'+ qualBt.closest('section.conteudo-aba').parent().attr('id'));          
        qualBt.closest('section.conteudo-aba').find('.menu-detalhes button').removeClass('inativo').attr("disabled", false);      
        navegacao.carregaAbasForms(qualBt);        
    }


    this.incluir = Incluir;
    function Incluir(qualBt)
    {
        // grava na seção titanModulo o tipo de ação que será executado
        navegacao.gravaSessaoNavegacao('tipoAcao', 'I', 'titanModulo');        
        //  navegacao.edicaoAtiva = true;     
        // Recupera as informações do menu detalhes
        var abaAtual = qualBt.closest('section.conteudo-aba').find('.menu-detalhes');             
        // Recupera as informações do menu detalhes
        var formAtual = abaAtual.closest('section.conteudo-aba').find('form');
        // Recupera as informações do select-titan do tipo lista-titan
        var selectOptionTitan = formAtual.find('select.select-titan option:selected');     

        // Verifica se há alguma função de customização do JS da página
        if (typeof customizacaoFormulario === "function")
        {
            // Chama função de customização encaminhando dos parametros
            // secaoAtual: seção aberta no momento
            customizacaoFormulario(abaAtual);
        }   

        // Verifica se o select-titan é do tipo lista-titan
        if (formAtual.find('select.select-titan').data('titan-lista') !== '' && formAtual.find('select.select-titan').data('titan-lista') !== undefined && formAtual.find('select.select-titan').data('titan-lista') !== null)
        {
            // Verifica se o select-titan contém o dataset (url)
            if (selectOptionTitan.data('url') !== '' && selectOptionTitan.data('url') !== undefined && selectOptionTitan.data('url') !== null)
            {
                // Verifica se o select-titan contém o dataset (target)
                if (selectOptionTitan.data('target') !== '' && selectOptionTitan.data('target') !== undefined)
                {
                    // Armazena os dados do botão clicado no sessionStorage para abertura do metodo "carregaAreasAbasForms"
                    var areasAbasForms = [selectOptionTitan.data('target'), selectOptionTitan.data('url'), ''];
                    navegacao.gravaSessaoNavegacao('areasAbasForms', areasAbasForms, 'titanModulo');
                    // Aciona o carregamento da partial view indicada no dataset (target)
                    navegacao.carregaAreasAbasForms(areasAbasForms, true);
                }
                else
                {
                    alert('Certifique-se de que os <option> do select-titan do tipo (titan-lista) contém os datasets (url e target)');
                }
            };
        }

        // Recupera as informações do select-titan
        var selectTitan = formAtual.find('select.select-titan');     
        if (selectTitan.data('titan-parceiro') === undefined && selectTitan.data('titan-url') === undefined && selectTitan.data('titan-destino') === undefined && selectTitan.data('titan-lista') === undefined && selectTitan.data('titan-menu-ativo') === undefined && selectTitan.data('titan-id-edicao') === undefined)
            selectTitan.val('');       


        if (selectTitan.data('titan-editavel') === true)
        {
            selectTitan.prop('selectedIndex', 0).selectpicker('refresh');
        }

        // desabilita os botões (incluir, editar e excluir) do menu detalhes para iniciar uma nova inclusão
        abaAtual.find('button.incluir-bt-detalhes, button.editar-bt-detalhes, button.excluir-bt-detalhes').attr("disabled", true).addClass('inativo');
        // habilita os botões (cancelar e salvar) do menu detalhes para iniciar uma nova inclusão
        abaAtual.find('button.cancelar-bt-detalhes, button.salvar-bt-detalhes').removeClass('remove');
        // limpa os campos do formulario para uma nova inserção (exceto o token)
        formAtual.find('textarea, input:not([name="__RequestVerificationToken"], [type="radio"], [type="checkbox"])').attr('readonly', false).val('');
        formAtual.find('input[type="radio"], input[type="checkbox"]').attr('disabled', false);
        // habilita o campo de arquivo, caso haja
        formAtual.find('input[type="file"]').attr('disabled', false).val('');
        // habilita o select
        selectTitan.removeAttr('disabled').selectpicker('refresh');
        // Habilita os select listas, botões listas
        formAtual.find('select.select-lista-titan, button[type="button"].bt-select-lista-titan').attr('disabled', false);        
        // Inicia o editor de texto 
        editorTexto.iniciaEditorTexto(qualBt.closest('div.tab-content > .active').attr('id'), true, true);
        // Ativa os inputs de upload de arquivos
        formularios.ativaInputFileEstilos(qualBt.closest('div.tab-content > .active').attr('id'));          

        // chama o metodo para iniciar o plugin de seletor de cores caso exista
        formularios.ativaColorPicker(formAtual);
    }

    this.excluir = Excluir;
    function Excluir(qualBt)
    {
        var abaAtual = qualBt.closest('section.conteudo-aba').find('.menu-detalhes');
        // Busca pelo formulario da aba ativa
        var formulario = abaAtual.closest('section.conteudo-aba').find('form');

        navegacao.gravaSessaoNavegacao('tipoAcao', 'D', 'titanModulo');

        // Captura o login do usuário logado
        var login = navegacao.buscaSessaoNavegacao('login', 'titan');

        // Habilita os select e select listas, para exclusão
        formulario.find('select.select-lista-titan, select.select-titan').attr('disabled', false); 
        // Recupera os dados do formulário ativo 
        var inputs = formulario.find('input:not([type="file"]), select, textarea').serializeArray();
        // Armazena os valores capturados dos inputs e sessões do navegador
        var dados = new FormData();
        // Popula o FormData com os valores dos inputs
        for (var item = 0; item < inputs.length; item++)
        {
            dados.append(inputs[item].name, inputs[item].value);            
        }

        var menuDetalhes = abaAtual.closest('section.conteudo-aba').find('.menu-detalhes');        
        dados.append('id', menuDetalhes.find('.editar-bt-detalhes, .excluir-bt-detalhes').data('idaba'));

        dados.append('id_empresa',navegacao.buscaSessaoNavegacao('grempid', 'titan'));
        dados.append('id_modulo', navegacao.buscaSessaoNavegacao('iditem', 'titanModulo'));
        dados.append('acao', 'D');

        //--> envia o usuario logado para futuras auditorias 
        dados.append('usuario', login);      

        var dataType = 'application/x-www-form-urlencoded; charset=utf-8';
        swal({
            title: "Você tem certeza?",
            text: "Você não será capaz de recuperar esta informação!",
            type: "warning",
            showCancelButton: true,
            cancelButtonClass: "btn-default",
            confirmButtonClass: "btn-warning",
            confirmButtonText: "Remover",
            closeOnConfirm: false,
            showLoaderOnConfirm: false
        },
        function ()
        {          
            $.ajax({
                type: 'POST',
                url: formulario.attr('action'),
                dataType: 'json',            
                contentType: false,
                processData: false,
                data: dados,
                success: function (data, status)
                {
                    if (data.status === true)
                    {
                        swal({
                            title: "Item removido!",
                            text: "",
                            type: "success",
                            showCancelButton: false,
                            confirmButtonClass: "btn-success",
                            confirmButtonText: "ok",
                            cancelButtonText: "",
                            closeOnConfirm: true,
                            closeOnCancel: false,
                            html: true
                        },
                            function (isConfirm)
                            {
                                if (isConfirm)
                                {
                                    // Recupera as informações do select-titan
                                    var selectTitan = formulario.find('select.select-titan');
                                    if (selectTitan.data('titan-parceiro') === undefined && selectTitan.data('titan-url') === undefined && selectTitan.data('titan-destino') === undefined && selectTitan.data('titan-lista') === undefined && selectTitan.data('titan-menu-ativo') === undefined && selectTitan.data('titan-id-edicao') === undefined)
                                    {                                        
                                        selectTitan.val('');                                       
                                        selectTitan.attr('disabled', true);
                                        selectTitan.selectpicker('refresh');
                                    }
                                    menuDetalhes.find('.editar-bt-detalhes, .excluir-bt-detalhes').data('idaba', '');
                                    abaAtual.find('button.incluir-bt-detalhes').attr("disabled", false).removeClass('inativo');
                                    abaAtual.find('button.cancelar-bt-detalhes, .menu-detalhes button.salvar-bt-detalhes').addClass('remove');

                                    abaAtual.closest('section.conteudo-aba').find('textarea, input:not([name="__RequestVerificationToken"], [type="radio"], [type="checkbox"])').attr('readonly', true).val('');
                                    abaAtual.closest('section.conteudo-aba').find('input[type="radio"], input[type="checkbox"]').attr('disabled', true);

                                    abaAtual.find('button.editar-bt-detalhes, button.excluir-bt-detalhes').addClass('remove');  
                                    navegacao.gravaSessaoNavegacao('idaba', '', 'titanModulo');
                                    navegacao.gravaSessaoNavegacao('tipoAcao', '', 'titanModulo');                                                                    
                                    if ($(qualBt).data('titan-retorna-inicio') || ($('.lista-menu a.nav-link').length === 1 && $('.dataTable').length === 0)) window.history.back();
                                    listas.atualizaTabelaAbas();
                                }
                            });
                    } else
                    {
                        var erros = formularios.trataErrosCampos(data, formulario);
                        swal({
                            title: erros[0],
                            text: erros[1],
                            type: "warning",
                            html: true,
                            showCancelButton: false,
                            confirmButtonClass: "btn-warning",
                            confirmButtonText: "ok",
                            closeOnConfirm: false,
                            closeOnCancel: false
                        });
                    }

                }, error: function (xhr, ajaxOptions, thrownError)
                {
                    erros.chamaErro(xhr.status);                   
                }
            });
           
        });
    }


    

    this.iniciarMenu = IniciarMenu;
    function IniciarMenu(qualSecao)
    {        
        $(qualSecao).find('.menu-detalhes button').click(function (evt)
        {
            // Verifica se o elemento clicado é mesmo o button, pois existe um elemento interno <i> que também pode ser clicado
            // caso o eleemento <i> for clicado ele sobe para o pai que no caso é o elemento button
            var este = ($(evt.target).is("i")) ? $(evt.target).parent() : $(evt.target);
            este.tooltip('hide');
                      
            switch ($(this).data('tipo-bt'))
            {
                case 'incluir':
                    modEdicao.incluir(este);
                    break;
                case 'excluir':
                    modEdicao.excluir(este);
                    break;
                case 'salvar':
                    modEdicao.salvar(este, false);
                    break;
                case 'anexar':
                    modEdicao.anexar(este);
                    break;
                case 'editar':
                    modEdicao.editar(este);
                    break;
                case 'cancelar':
                    modEdicao.cancelar(este);
                    break;               
                default:
                    break;
            }          
        });
    }
}
