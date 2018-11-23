$(document).ready(function () {
 
    ImagemPerfil();
    var idModulo = navegacao.buscaSessaoNavegacao('idus', 'titan');
    navegacao.gravaSessaoNavegacao('id_modulo', idModulo, 'titanModulo');
    navegacao.gravaSessaoNavegacao('iditem', idModulo, 'titanModulo');

    listas.listasModulo('../sources/sistema/perfil.json');
});

function ImagemPerfil()
{
    $(".imagem-perfil-involucro .img-thumbnail > div").click(() => {
        $('#modalImagemPerfil').modal('show').on('shown.bs.modal', function () {
            formularios.iniciaFormulario('#modalImagemPerfil', true);
            formularios.ativaInputFileEstilos('#modalImagemPerfil');
        })

    }).css({ 'cursor': 'pointer' });
    $("#modalImagemPerfil .modal-footer").on('click', '#salva-imagem-modal', function () {
        enviaImagem();
    });
}


function enviaImagem()
{   
    var formulario = $('#modalImagemPerfil form');
    // Recupera os dados do formulário ativo
    var inputs = formulario.find('input:not([type="file"]), select:not(.select-lista-titan), textarea, input[type=radio]:checked, input[type=checkbox]:checked').serializeArray();
    //var inputs = formulario.serializeArray();
    // Armazena os valores capturados dos inputs e sessões do navegador
    var data = new FormData();
    //--> envia o usuario logado para futuras auditorias 
    data.append('usuario', navegacao.buscaSessaoNavegacao('login', 'titan'));
    // Adiciona o tipo de ação que o usuário deseja realizar
    data.append('acao', 'U');
    // Adiciona a empresa ativa
    var grempid = navegacao.buscaSessaoNavegacao('grempid', 'titan');
    data.append('id_empresa', grempid);
    data.append('id_pessoa_empresa', grempid);
    data.append('id_modulo', navegacao.buscaSessaoNavegacao('iditem', 'titanModulo'));



    // Loop responsável por coletar os dados dos inputs do tipo "file"
    $(".upload-titan").each(function (index, e) {
        // Localiza o input file
        var inputFileUpload = $(this).get(0);
        // Coleta os valores de cada input file
        var files = inputFileUpload.files;
        // Adiciona os valores coletados para o formdata
        for (var i = 0; i < files.length; i++) data.append(inputFileUpload.name, files[i]);
    });  

    // Adiciona o alerta de status do carregmento dos dados 
    swal({ html: true, closeOnConfirm: false, closeOnCancel: false, showConfirmButton: false, showCancelButton: false, text: '<progress class="titan-progresso progress progress-primary" value="0" max="0">0%</progress>', title: "Aguarde processando..." });



    $.ajax({
        type: 'POST',
        url: formulario.attr('action'),
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        xhr: function () {
            // Inicio da requisão http
            var iniXhr = $.ajaxSettings.xhr();
            // verifica a exixtencia do upload de dados
            if (iniXhr.upload) {
                // Inicia a leitura dos dados
                iniXhr.upload.addEventListener('progress', function (e) {
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
        success: function (data, status)
        {
            if (data.status === true)
            {
                console.log(data.data.caminho_logotipo);
                $('#modalImagemPerfil').modal('hide');
                swal({
                    html: true,
                    title: 'Cadastro efetuado!',
                    text: "",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonClass: "btn-success",
                    confirmButtonText: "ok",
                    cancelButtonText: "",
                    closeOnConfirm: true,
                    closeOnCancel: false
                },
                    function (isConfirm) {
                        if (isConfirm)
                        {
                            formulario.find('textarea, input:not([name="__RequestVerificationToken"], [type="radio"], [type="checkbox"])').val('');
                            formulario.find('input, select, textarea').removeClass('titan-erro-input');
                            $('#img-usuario').html('<img class="objectImage" src= "' + data.data.caminho_logotipo + '/' + data.data.logotipo+'" id= "imagem-perfil" >');
                            ImagemPerfil();
                        }
                    });

            } else {
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
}

function customizacaoGravacao(qualSecao, data)
{
    if ($(qualSecao).find('.titan-panel-perfil input[name="nome"]').length > 0)
    {
        $(qualSecao).find('.titan-panel-perfil input[name="nome"]').val(data.data.pessoa.razao_social_nome);
        $(qualSecao).find('.titan-panel-perfil input[name="email"]').val(data.data.emailUsuario.email);
        $(qualSecao).find('.titan-panel-perfil input[name="telefone"]').val(data.data.telefone.telefone);    
    }
   
}