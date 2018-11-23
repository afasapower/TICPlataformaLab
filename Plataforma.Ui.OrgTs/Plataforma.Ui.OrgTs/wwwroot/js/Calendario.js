function Calendario()
{
    // declaração da variavel de escopo responsavel por realizar a comunicação entre metodos 
    // e acessos a outras variaveis da classe
    var modCalendario = this;
        
    // declaração do metodo IniciaCalendario
    this.iniciaCalendario = IniciaCalendario;
    function IniciaCalendario()
    {
        if(document.getElementsByClassName('calendario').length > 0)
        {
            $('.calendario').fullCalendar({
                lang: 'pt',
                height: 275,
                header: {
                    left: 'prev, next',
                    center: 'title',
                    right: 'month'
                },
                buttonIcons: {
                    prev: 'none fa fa-arrow-left',
                    next: 'none fa fa-arrow-right',
                    prevYear: 'none fa fa-arrow-left',
                    nextYear: 'none fa fa-arrow-right'
                },
                defaultDate: new Date().toISOString().slice(0, 10).toString(),
                editable: true,
                eventLimit: true,               
                eventSources: [                   
                    {
                        url: document.baseURI + 'Sistema_AgendaAtendimento/CalendarioAgendamento',                        
                        data: { id_empresa: navegacao.buscaSessaoNavegacao('grempid', 'titan') },
                        error: function (xhr, ajaxOptions, thrownError)
                        {
                            erros.chamaErro(xhr.status);
                        }
                    }
                ],
                eventClick: function (calEvent, jsEvent, view)
                {                    
                    modCalendario.montaInfoCalendario(calEvent);                   

                    if (!$(this).hasClass('event-clicked'))
                    {
                        $('.fc-event').removeClass('event-clicked');
                        $(this).addClass('event-clicked');
                    }
                },
                eventAfterAllRender: function (view)
                {
                    // Verifica se há alguma função de customização do JS da página
                    if (typeof customizacaoCalendario === "function")
                    {
                        // Chama função de customização encaminhando dos parametros                        
                        customizacaoCalendario();
                    } 
                },
                eventDrop: function (event, delta, revertFunc)
                {
                    var data = new FormData();
                    data.append('id', event.id);
                    data.append('data', event.start.format());
                    data.append('__RequestVerificationToken', $('input[name="__RequestVerificationToken"]').val());
                    data.append('acao', 'U');
                    data.append('usuario', navegacao.buscaSessaoNavegacao('login', 'titan'));
                    data.append('id_modulo', event.id);
                    data.append('id_empresa', navegacao.buscaSessaoNavegacao('grempid', 'titan'));          

                    swal({
                        title: "Alterar o agendamento!",
                        text: "Deseja realmete alterar a data do agendamento?",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Sim, eu quero!",
                        closeOnConfirm: false
                    },
                    function ()
                    {         
                        $.ajax({
                            type: 'POST',
                            url: document.baseURI + 'Sistema_AgendaAtendimento/CalendarioAgendamentoOperacoes',
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
                                if (data.status)
                                {
                                    swal({
                                        html: true,
                                        title: 'Alteração efetuada!',
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
                                            if (isConfirm) {

                                                // Verifica se há alguma função de customização do JS da página
                                                if (typeof customizacaoCalendario === "function")
                                                {
                                                    // Chama função de customização encaminhando dos parametros
                                                    // secaoAtual: seção aberta no momento
                                                    customizacaoCalendario();
                                                }
                                            }
                                        });
                                } else
                                {
                                    var erros = formularios.trataErrosCampos(data, null);
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
            });
        }
    }



    // declaração do metodo IniciaCalendario
    this.montaInfoCalendario = MontaInfoCalendario;
    function MontaInfoCalendario(dados)
    {
        var conteudo = '<dl>';
        conteudo += '<dt>Nome do solicitante: </dt>';
        conteudo += '<dd">' + dados.title + '</dd>';

        conteudo += '<dt>Horário: </dt>';
        conteudo += '<dd>' + dados.hora + '</dd>';

        conteudo += '<dt>Agendado com: </dt>';
        conteudo += '<dd>' + dados.nomeResponsavel + '</dd>';

        conteudo += '<dt>Motivo do agendamento: </dt>';
        conteudo += '<dd>' + dados.descricao + '</dd>';

        conteudo += '<dt>Observações: </dt>';
        conteudo += '<dd>' + dados.observacao + '</dd>';

        conteudo += '</dl>';

        $('#modal-agendamento-info-dia').find('.info-agenda-dia').html(conteudo);

        var btAlteracao = $('#modal-agendamento-info-dia .bt-altera-agendamento');

        if (btAlteracao.length > 0)
        {            
            btAlteracao.data('id-agendamento', dados.id);
            btAlteracao.on('click', function (evt)
            {
                evt.preventDefault();
                var idAgendamento = $(this).data('id-agendamento');
                // Id do item selecionado para edição e visualização na primeira listagem
                navegacao.gravaSessaoNavegacao('iditem', idAgendamento, 'titanModulo');
                // Id responsavel por popular as listagens internas das abas
                navegacao.gravaSessaoNavegacao('idlistaabas', idAgendamento, 'titanModulo');
                // variavel responsavel por capturar todos os valores dos botões         

                window.location.href = document.head.baseURI +'Sistema_AgendaAtendimento/interno';
            });
        }
        $('#modal-agendamento-info-dia').modal();
    }   
}