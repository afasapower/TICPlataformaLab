function EditorTexto()
{
    // declaração da variavel de escopo responsavel por realizar a comunicação entre metodos 
    // e acessos a outras variaveis da classe
    modEditor = this;

    // declaração do metodo IniciaEditorTexto
    this.iniciaEditorTexto = IniciaEditorTexto;

    // metodo responsável por iniciar a montagem dos editores de texto
    // Recebe como parametros secaoAtual e ativo
    // secaoAtual: nome da aba ativo no momento (secao1, secao2...)
    // ativo: confirmação de ativação do editor, sendo que "true" libera o conteúdo para edição e false transforma o conteúdo em HTML
    // limparConteudo : Responsável por limpar o conteudo do editor
    function IniciaEditorTexto(secaoAtual, ativo, limparConteudo)
    {        
        // localiza o form dentro da aba ativa no momento
        var formularioAbaAtual = $('#' + secaoAtual).find('form');
        // localiza 1 ou mais editores na aba ativa
        var summernoteEditores = formularioAbaAtual.find('.editor-titan');
        // verifica se existe algum editor na aba, caso não haja será ignorado a rotina abaixo
        // e o a função encerra sem a montagem do editor
        if (summernoteEditores.length)
        {
            // Customização da barra de edição do summernote (editor) 
            var barraFerramentas = [['misc', ['print']], ['cleaner', ['cleaner']], ['style', ['style']], ['font', ['bold', 'italic', 'underline', 'clear']], ['fontname', ['fontname']], ['fontsize', ['fontsize']], ['color', ['color']], ['para', ['ul', 'ol', 'paragraph']], ['height', ['height']], ['table', ['table']], ['insert', ['link', 'hr', 'picture']], ['view', ['fullscreen','codeview']], ['help', ['help']]];
            // Remove codigos do word
            var cleaner = {
                notTime: 2400, // Time to display Notifications.
                action: 'both', // both|button|paste 'button' only cleans via toolbar button, 'paste' only clean when pasting content, both does both options.
                newline: '<br>', // Summernote's default is to use '<p><br></p>'
                notStyle: 'position:absolute;top:0;left:0;right:0', // Position of Notification
                icon: '<i class="note-icon">Limpar formatação do word</i>',
                keepHtml: false, // Remove all Html formats
                keepOnlyTags: ['<p>', '<br>', '<ul>', '<li>', '<b>', '<strong>', '<i>', '<a>'], // If keepHtml is true, remove all tags except these
                keepClasses: false, // Remove Classes
                badTags: ['style', 'script', 'applet', 'embed', 'noframes', 'noscript', 'html'], // Remove full tags with contents
                badAttributes: ['style', 'start'] // Remove attributes from remaining tags
            };

            // loop para montagem dos editores
            for (var i = 0; i < summernoteEditores.length; i++)
            {
                // localiza todos os editores através do ID da div
                var refEditores = $(summernoteEditores[i]).attr('id');
                var editorAtual = $('#' + refEditores);               
                // verfica se há alguma classe de formatação do margens inserida
               //// var conteudo = (editorAtual.find('.editor-titan-editor').length > 1) ? editorAtual.find('.editor-titan-editor:last').html() : editorAtual.html();

                // inserção dos parametros do editor de texto caso, for true
                if (ativo)
                {
                    editorAtual.summernote({
                        lang: 'pt-BR',
                        toolbar: barraFerramentas,
                        cleaner: cleaner,
                        minHeight: 600,                       
                        disableDragAndDrop: true,
                        focus: true,                        
                        callbacks:
                        {
                            onImageUpload: function (files, editor, welEditable)
                            {                             
                                modEditor.enviaImagemEditor(files[0], editor, welEditable, editorAtual);                                
                            }
                        }
                    });
                    $('[data-titan-bt-print-editor="' + refEditores + '"]').remove();

                    if (limparConteudo)
                    {
                        editorAtual.summernote('code', '');

                    }
                }
                else   
                {
                    var btVisualizar = ($('#' + refEditores + '.editor-titan-ocultar').length == 1) ? '<button type="button" class="btn bt-editor-titan-visualizar" data-titan-bt-view-editor="' + refEditores + '"><span class="icmn-eye"></span> &nbsp;visualizar</button>':"";
                    editorAtual.summernote('destroy');
                    
                    var btEdicao = ($('#' + refEditores).data('titan-editor-habilitar-edicao') === true) ? '<button type="button" class="btn btn-warning bt-editor-titan-editar margin-left-5" data-titan-bt-editar-documento-editor="' + refEditores + '"><span class="icmn-pencil3"></span> &nbsp;editar documento</button>' : "";
                    

                    var formGroup = editorAtual.closest('div.form-group');  
                    if (formGroup.find('.involucro-bt-print-editor').length == 0)
                    {
                        editorAtual.before('<div class="involucro-bt-print-editor"><div class="row"><div class="col-md-6">' + btVisualizar + btEdicao + '</div><div class="col-md-6 text-right"><button type="button" data-titan-bt-print-editor="' + refEditores + '" class="btn btn-primary">Imprimir</button></div></div></div>');
                    }                    
                                              
                    $('[data-titan-bt-print-editor="' + refEditores + '"]').click(function () { modEditor.imprimirValores(secaoAtual, $(this).data('titan-bt-print-editor')); });
                    $('[data-titan-bt-view-editor="' + refEditores + '"]').click(function ()
                    {
                        var esteBt = $(this);   
                        var formGroupBt = esteBt.closest('.form-group').find('div.note-editor.note-frame');
                        formGroupBt.toggleClass("editor-titan-ocultar");

                        $("#" + esteBt.data('titan-bt-view-editor')).toggleClass("editor-titan-ocultar");                       
                        var titulo = esteBt.text();                        
                        esteBt.html((titulo.trim() == 'visualizar') ? '<span class="icmn-eye-blocked" ></span > &nbsp;ocultar' : '<span class="icmn-eye"></span> &nbsp;visualizar');
                        
                        if (formGroupBt.length == 0)
                        {
                            var editorAtual = esteBt.closest('.form-group').find('div.editor-titan');                            
                            if (titulo.trim() == 'visualizar')
                                editorAtual.addClass('margem-edicao-inativo');
                            else
                                editorAtual.removeClass('margem-edicao-inativo');                            
                        }
                    });


                    $('[data-titan-bt-editar-documento-editor="' + refEditores + '"]').click(function ()
                    {
                        var esteBt = $(this);
                        var formGroupBt = esteBt.closest('.form-group').find('div.note-editor.note-frame');
                        formGroupBt.toggleClass("editor-titan-ocultar");
                        var editor = esteBt.data('titan-bt-editar-documento-editor');                       

                        $("#" + esteBt.data('titan-bt-view-editor')).toggleClass("editor-titan-ocultar");
                        var titulo = esteBt.text();
                        esteBt.html((titulo.trim() === 'editar documento') ? '<span class="icmn-checkmark4" ></span > &nbsp;finalizar' : '<span class="icmn-eye"></span> &nbsp;editar documento');
                                            
                        var editorAtual = esteBt.closest('.form-group').find('div.editor-titan');
                       
                        if (titulo.trim() === 'editar documento')
                        {                           
                            esteBt.addClass('btn-success').removeClass('btn-warning');
                            esteBt.closest('.involucro-bt-print-editor').find('[data-titan-bt-print-editor="' + editor + '"]').hide();
                            esteBt.closest('.involucro-bt-print-editor').find('[data-titan-bt-view-editor="' + editor + '"]').hide();
                            $('#'+editor).summernote({ focus: true });
                            editorAtual.addClass('margem-edicao-inativo');
                        }
                        else
                        {                           
                            esteBt.addClass('btn-warning').removeClass('btn-success');
                            esteBt.closest('.involucro-bt-print-editor').find('[data-titan-bt-print-editor="' + editor + '"]').show();
                            esteBt.closest('.involucro-bt-print-editor').find('[data-titan-bt-view-editor="' + editor + '"]').show();
                            editorAtual.removeClass('margem-edicao-inativo');      
                            $('#' + editor).summernote('destroy');      


                            swal({
                                title: "Lembre-se!",
                                text: "Para que está edição seja salva, será necessário clicar no botão no final desta página.",
                                type: "warning",
                                showCancelButton: false,
                                confirmButtonClass: "btn-danger",
                                confirmButtonText: "Ok, eu entendi!",                               
                                closeOnConfirm: false,
                                closeOnCancel: false
                            });
                        }
                        
                    });
                }                                

                // Insere classe de formação de margens
                // editorAtual.html('').append('<div class="editor-titan-editor"><div>');
                // editorAtual.find('.editor-titan-editor').html(conteudo);  
                // será inserida pra cada editor um input que receberá os dados dos editores para postagem na aplicação
                var nomeInput = "";
                // Verifica se o editor faz parte de um GRUPO de editores que irão popular uma mesma tabela com varios valores
                var grupoEditores = formularioAbaAtual.find('#' + refEditores).data('titan-editor-grupo');
                // Caso o editor não faça parte de um grupo o input irá adotar o ID do editor senão, ele irá adotar o valor do dataSet (data-titan-editor-grupo)
                var grupoAtivo = (grupoEditores !== undefined && grupoEditores !== null && grupoEditores !== '');
                (grupoAtivo) ? nomeInput = grupoEditores + '[]' : nomeInput = refEditores;
                // Verifica se já existe os inputs para não popular eles novamnete
                if (formularioAbaAtual.find('input[name="' + nomeInput + '"]').length == 0 || (grupoAtivo && formularioAbaAtual.find('input[name="' + nomeInput + '"]').data('titan-editor-grupo') !== refEditores))
                {                    
                    // Verifica se há grupos a inserir no input
                    if (grupoAtivo)
                    {
                        // Tipos de inputs gerados para o sistema
                        var tiposDados = ['id', 'titulo', 'conteudo'];
                        for (var j = 0; j < 3; j++) {
                            formularioAbaAtual.append('<input type="hidden" name="' + nomeInput + '" data-titan-editor-valor="' + tiposDados[j] + '" data-titan-editor-grupo="' + refEditores + '">');
                        }
                    } else
                    {
                        // Caso não seja de um grupo ele irá criar somente um input para cada editor
                        formularioAbaAtual.append('<input type="hidden" name="' + nomeInput + '">');
                    }                   
                }                          
            }

            // Verifica se há alguma função de customização do JS da página
            if (typeof customizacaoEditorTexto === "function")
            {
                // Chama função de customização encaminhando dos parametros
                // secaoAtual: seção aberta no momento
                customizacaoEditorTexto(secaoAtual);
            }
        }
    }

    // Declaração do metodo CaptaValores
    this.captaValores = CaptaValores;

    // Metodo responsável por capturar o conteudo dos editores da aba ativa para postagem na aplicação
    // Recebe como paramentro o nome da seção ativa no momento 
    function CaptaValores(secaoAtual)
    {
        // Localiza o form dentro da aba ativa no momento
        var formularioAbaAtual = $('#' + secaoAtual).find('form');
        // Localiza 1 ou mais editores na aba ativa
        var summernoteEditores = formularioAbaAtual.find('.editor-titan');
        // Verifica se existe algum editor na aba, caso não haja será ignorado a rotina abaixo
        // e o a função encerra sem a captura dos dados dos editores
        if (summernoteEditores.length)
        {           
            // Loop para o processo de captura dos editores
            for (var i = 0; i < summernoteEditores.length; i++)
            {                
                // localiza todos os editores através do ID da div 
                var refEditores = $(summernoteEditores[i]).attr('id');
                // Editor atual 
                var editorAtual = $("#" + refEditores);
                // localiza os inputs referentes aos editores e captura e insere os valores dos conteúdos (com as tags html, css...)                
                var nomeInput = "";
                var grupoEditores = editorAtual.data('titan-editor-grupo');
                // Caso o editor não faça parte de um grupo o input irá adotar o ID do editor senão, ele irá adotar o valor do dataSet (data-titan-editor-grupo)
                (grupoEditores !== undefined && grupoEditores !== null && grupoEditores !== '') ? nomeInput = grupoEditores + '[]' : nomeInput = refEditores;
                // Localiza os inputs para gravação do valores
                var inputs = formularioAbaAtual.find('input[name="' + nomeInput +'"]');                
                for (var j = 0; j < inputs.length; j++)
                {                   
                    // Verifica se o editor faz parte de um grupo
                    if ($(inputs[j]).data('titan-editor-grupo') === refEditores)
                    {
                        switch ($(inputs[j]).data('titan-editor-valor')) {
                            case "id":
                                $(inputs[j]).val(editorAtual.data('titan-editor-id'));
                                break;
                            case "titulo":
                                $(inputs[j]).val(editorAtual.data('titan-editor-titulo'));
                                break;
                            case "conteudo":
                                $(inputs[j]).val(editorAtual.summernote('code'));
                                break;
                        }
                    } else
                    {
                        // Caso não faça parte de um grupo, ele populará o input com o valor dos dados do summernote (editor)
                        if ((grupoEditores === undefined || grupoEditores === null || grupoEditores === ''))
                        {
                            $(inputs[j]).val(editorAtual.summernote('code'));
                        }                       
                    }
                }               

                if (editorAtual.data('titan-editor-inativo')) editorAtual.summernote('destroy');                
            }
        }        
    }


    this.enviaImagemEditor = EnviaImagemEditor;
    function EnviaImagemEditor(file, editor, welEditable, editorAtual)
    {
        var enderecoBase = document.head.baseURI;
        var dadosImg = new FormData();
        dadosImg.append("file", file);       
        // Adiciona o alerta de status do carregmento dos dados 
        swal({ html: true, closeOnConfirm: false, closeOnCancel: false, showConfirmButton: false, showCancelButton: false, text: '<progress class="titan-progresso progress progress-primary" value="0" max="0">0%</progress>', title: "Aguarde processando..." });

        $.ajax({
            data: dadosImg,
            type: "POST",
            url: editorAtual.data('titan-url'),
            cache: false,
            contentType: false,
            processData: false,
            xhr: function ()
            {
                // Inicio da requisão http
                var iniXhr = $.ajaxSettings.xhr();
                // Verifica a exixtencia do upload de dados
                if (iniXhr.upload)
                {
                    // Inicia a leitura dos dados
                    iniXhr.upload.addEventListener('progress', function (e)
                    {
                        // Verifica se existe dados a serem enviados
                        if (e.lengthComputable) {
                            // Grava os valores nos atributos da tag progreess
                            $('.titan-progresso').attr({ value: e.loaded, max: e.total });
                        }
                    }, false);
                }
                // Retorna os valores iniciados
                return iniXhr;
            },
            success: function (dados)
            {
                if (dados.status)
                {                   
                    for (var i = 0; i < dados.data.imgs.length; i++)
                    {                                 
                        editorAtual.summernote('insertImage', enderecoBase + dados.data.url + dados.data.imgs[i]);
                    }    
                    swal("", "Arquivo carregado com sucesso!", "success");
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


    this.imprimirValores = ImprimirValores;
    function ImprimirValores(secaoAtual, editor)
    {
        var $frame = $('<iframe name="summernotePrintFrame"' + 'width="0" height="0" frameborder="0" src="about:blank" style="visibility:hidden">' + '</iframe>');
        $frame.appendTo($('#' + editor).parent());

        var $head = $frame.contents().find('head');        
        $('style, link[rel=stylesheet]', document).each(function ()
        {            
            $head.append($(this).clone());
        });

        console.log('teste');
        
        $frame.contents().find('body').html('<div class="titan-impressao-documento">'+$('#'+editor).html()+'</div>');

        setTimeout(function ()
        {
            $frame[0].contentWindow.focus();
            $frame[0].contentWindow.print();
            $frame.remove();
            $frame = null;
        }, 250);        
    }
}

