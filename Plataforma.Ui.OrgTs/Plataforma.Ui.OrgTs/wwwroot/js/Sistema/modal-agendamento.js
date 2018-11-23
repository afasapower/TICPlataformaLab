$(document).ready(function ()
{   
    $('#modalAgendamento').on('show.bs.modal', function (e)
    {
        formularios.iniciaFormulario('#modalAgendamento');  
    });

      
    $("#modalAgendamento .modal-footer").on('click', '#salva-agendamento-modal', function ()
    {         
        enviaAgendamento();
    });   
});

function enviaAgendamento()
{
    console.log('enviaAgendamento');
    var formulario = $('#modalAgendamento form');
    // Recupera os dados do formulário ativo
    var inputs = formulario.find('input:not([type="file"]), select:not(.select-lista-titan), textarea, input[type=radio]:checked, input[type=checkbox]:checked').serializeArray();
    //var inputs = formulario.serializeArray();
    // Armazena os valores capturados dos inputs e sessões do navegador
    var data = new FormData();
    // Popula o FormData com os valores dos inputs
    for (var item = 0; item < inputs.length; item++)
    {
        data.append(inputs[item].name, inputs[item].value);
    }
    //--> envia o usuario logado para futuras auditorias 
    data.append('usuario', navegacao.buscaSessaoNavegacao('login', 'titan'));
    // Adiciona o tipo de ação que o usuário deseja realizar
    data.append('acao', 'I');
    // Adiciona a empresa ativa
    var grempid = navegacao.buscaSessaoNavegacao('grempid', 'titan');
    data.append('id_empresa', grempid);
    data.append('id_pessoa_empresa', grempid);

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
                $('#modalAgendamento').modal('hide');
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
                    function (isConfirm)
                    {
                        if (isConfirm)
                        {                    
                            formulario.find('textarea, input:not([name="__RequestVerificationToken"], [type="radio"], [type="checkbox"])').val('');
                            formulario.find('input, select, textarea').removeClass('titan-erro-input');               
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
            console.log(thrownError);
            console.log(ajaxOptions);
            console.log(xhr);
            erros.chamaErro(xhr.status);
        }
    });
}