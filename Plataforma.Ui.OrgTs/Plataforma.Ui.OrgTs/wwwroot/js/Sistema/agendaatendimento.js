$(document).ready(function ()
{
    // chamada do metodo "listasModulo" resposavel por carregar os dados do arquivo json para o DOM, recebendo o  
    // endereço do arquivo json responsavel por montar as listas
   // listas.listasModulo('../sources/sistema/agendaatendimento.json');    
});

function customizacaoCalendario()
{
    $('.calendario').fullCalendar('option', 'height', 650);
    var dataAtual = new Date().toISOString().slice(0, 10).toString();
    
    $.ajax({
        type: 'GET',
        url: '../Sistema_AgendaAtendimento/CalendarioAgendamento',
        contentType: false,        
        cache: false,
        data: { id_empresa: navegacao.buscaSessaoNavegacao('grempid', 'titan'), end: dataAtual, start: dataAtual },
        success: function (data, status)
        {           
            var conteudoTag = '';
            data.forEach(function (item, index)
            {
                conteudoTag += '<a href="#" class="list-group-item" data-titan-agenda-id="' + item.id +'" data-titan-agenda-observacao="' + item.observacao + '" data-titan-agenda-title="' + item.title + '" data-titan-agenda-hora="' + item.hora + '" data-titan-agenda-nomeResponsavel="' + item.nomeResponsavel + '" data-titan-agenda-descricao="' + item.descricao + '"><h5 class="list-group-item-heading" style="color:' + item.color +'">' + item.hora + '</h5><p class="list-group-item-text">' + item.title +'</p></a >'
            });

            $('.lista-info-agenda-dia').html(conteudoTag);

            $('.lista-info-agenda-dia a').click(function (evt)
            {
                evt.preventDefault();
                var dados = {
                    observacao: $(this).data('titan-agenda-observacao'),
                    title: $(this).data('titan-agenda-title'),
                    hora: $(this).data('titan-agenda-hora'),
                    nomeResponsavel: $(this).data('titan-agenda-nomeresponsavel'),
                    descricao: $(this).data('titan-agenda-descricao'),
                    id: $(this).data('titan-agenda-id')
                };
                calendario.montaInfoCalendario(dados);                
            });
        }
    });
}