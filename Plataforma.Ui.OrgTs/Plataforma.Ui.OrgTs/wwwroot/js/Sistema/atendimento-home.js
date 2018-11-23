//--> Form contato |----------------------------------------
$(document).ready(function () {
    $('#salva-agendamento-modal').click(function ()
    {
        $('#modal-atendimento-home').modal({ show: true }).find('.modal-title').text('Processando!');
        $('#modal-atendimento-home').modal({ show: true }).find('.modal-body').html('<h4>Aguarde enviando agendamento...</h4>');

        var form = new FormData();
        form.append('descricao_chamado', $('#descricao_chamado').val());
        form.append('usuario', navegacao.buscaSessaoNavegacao('login', 'titan'));
        form.append('id_empresa', navegacao.buscaSessaoNavegacao('grempid', 'titan'));  

        $('#assunto').find('option:selected').each(function (index, e) {form.append('assunto', e.value);});               
        var headers = {};
        headers['__RequestVerificationToken'] = $('#form-atendimento input[name="__RequestVerificationToken"]').val();
        
        $.ajax({
            url: $('#form-atendimento').attr('action'),
            contentType: false,
            processData: false,
            cache: false,
            type: 'POST',
            headers: headers,
            data: form,
            success: function (result)
            {
                var conteudo = "<h4>Seu agendamento foi enviado com sucesso!</h4><br /><p>Em breve entraremos em contato.</p>";
                switch (result.status)
                {
                    case "ok":
                        $("#form-atendimento input").each(function () { $(this).val(''); });
                        $('#form-atendimento select').prop('selectedIndex', 0);
                        $('#modal-atendimento-home').modal({ show: true }).find('.modal-title').text('Sucesso!');
                        break;
                    case "erro":
                        var errosContato = $.parseJSON(result.itens);
                        conteudo = "<h4>Alguns campos estão sem preenchimento.</h4><ul>";
                        for (var erros in errosContato)
                        {
                            conteudo += '<li>' + errosContato[erros] + '</li>';
                        }
                        conteudo += "</ul>";
                        $('#modal-atendimento-home').modal({ show: true }).find('.modal-title').text('Atenção!');
                        break;
                    case "erroenvio":
                        $('#modal-atendimento-home').modal({ show: true }).find('.modal-title').text('Atenção!');
                        conteudo = result.itens;
                        break;
                    default:
                        break;
                }
                $('#modal-atendimento-home').modal({ show: true }).find('.modal-body').html(conteudo);
            },error: function (xhr, ajaxOptions, thrownError)
            {
                erros.chamaErro(xhr.status);
            }
        });
        return false;
    });
});