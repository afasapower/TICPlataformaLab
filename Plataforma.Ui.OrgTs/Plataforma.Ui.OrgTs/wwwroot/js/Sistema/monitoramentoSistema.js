var monitoramentoServers;
function customizacaoMonitoramento()
{
    var monitoramentoServers = new MapaMonitoramento();
    monitoramentoServers.monitoramento();
}


function MapaMonitoramento()
{
    var modMapa = this;
    this.marcadores = [];
    this.locations = [];
    this.map;
    this.geocoder = new google.maps.Geocoder();
    this.limites = new google.maps.LatLngBounds();
    this.enderecos = [];   
    var mapaAtivo = false;
    var carregadorGraficos = '<img src="../images/ajax-loader.gif" class="carregador" />';
    this.id = '';
    this.apis = [];
    

    this.monitoramento = Monitoramento;
    function Monitoramento()
    {       
        modMapa.map = new google.maps.Map(document.getElementById('map'), {
            zoom: 5,
            center: { lat: -16.290583952750126, lng: -54.07321085000001 }          
        });
               
        modMapa.chamadaMonitoramentoAjax();
    }

    this.chamadaMonitoramentoAjax = ChamadaMonitoramentoAjax;
    function ChamadaMonitoramentoAjax()
    {        
        $.ajax({ type: 'GET', url: '../MonitoraAplicacaoSig/Monitoramento', contentType: false, cache: false, success: function (data, status) {
                modMapa.enderecos = data;
                if (!mapaAtivo)
                {
                    $('#map').append('<div class="carregadorMapaFundo"><h1>Carregando informações no mapa...</h1></div>');
                    mapaAtivo = true;
                    modMapa.geocoder = new google.maps.Geocoder(); 
                    modMapa.enderecamentoReverso();                   
                }                

                if (modMapa.id !== '')
                {                   
                    modMapa.chamadaMonitoramento24hr(modMapa.id);
                    modMapa.chamadaDisponibilidadeBanco(modMapa.id);
                }
                modMapa.menuEsq();
                setTimeout(modMapa.chamadaMonitoramentoAjax, 30000);            
            }
        });
    }

    this.iniciaMapa = IniciaMapa;
    function IniciaMapa()
    {
        $('#map .carregadorMapaFundo').remove();
        modMapa.removeMarcadores();
        for (var i = 0; i < modMapa.locations.length; i++)
        {
            modMapa.adicionaMarcadores(modMapa.locations[i][0], i * 300, '', i);
        }     
    }    

    this.adicionaMarcadores = AdicionaMarcadores;
    function AdicionaMarcadores(position, timeout, label, indice)
    {
        window.setTimeout(function ()
        {
            var marcador = new google.maps.Marker({ position: position, map: modMapa.map, animation: google.maps.Animation.DROP, label: label });            
            modMapa.marcadores.push(marcador);
            modMapa.map.fitBounds(modMapa.limites);  
            modMapa.clickMarcador(marcador, indice);          
        }, timeout);
    }

    this.conteudoMarcador = ConteudoMarcador;
    function ConteudoMarcador(indice)
    {
        var dados = modMapa.enderecos[indice];
        return '<div id="content"><h6 id="firstHeading" class="firstHeading">' + dados.nome_servidor + '</h6><div id="bodyContent"><p><b>Uso CPU: </b>' + dados.uso_cpu + '%<br /><b>Uso Memoria RAM livre: </b>' + parseFloat(dados.uso_memoria) + 'Gb</p></div></div>';
    }


    this.clickMarcador = ClickMarcador;
    function ClickMarcador(marcador, indice)
    {        
        var infowindow = new google.maps.InfoWindow({ content: modMapa.conteudoMarcador(indice)});
        marcador.addListener('click', function ()
        {                
            infowindow.open(marcador.get('map'), marcador);
        });
    }


    this.removeMarcadores = RemoveMarcadores;
    function RemoveMarcadores() {
        for (var i = 0; i < modMapa.marcadores.length; i++)
        {
            modMapa.marcadores[i].setMap(null);
        }
        modMapa.marcadores = [];
    }
        

    this.enderecamentoReverso = EnderecamentoReverso;
    function EnderecamentoReverso()
    {        
        if (modMapa.locations.length > 0)
        {
            modMapa.locations = [];
            modMapa.limites = new google.maps.LatLngBounds();            
        }
        modMapa.qtdLoopMapa = 0;
        modMapa.loopMapa();
    }  

    var qtdLoopMapa = 0;
    this.loopMapa = LoopMapa;
    function LoopMapa()
    {        
        if (modMapa.qtdLoopMapa < modMapa.enderecos.length)
        {
            var enderecosAgrupado = modMapa.enderecos[modMapa.qtdLoopMapa].endereco + ' - ' + modMapa.enderecos[modMapa.qtdLoopMapa].bairro + ', ' + modMapa.enderecos[modMapa.qtdLoopMapa].nome_cidade + ' - ' + modMapa.enderecos[modMapa.qtdLoopMapa].sigla;

            modMapa.geocoder.geocode({ 'address': enderecosAgrupado }, function (results, status)
            {
                if (status === 'OK') {
                    modMapa.limites.extend(results[0].geometry.location);
                    modMapa.locations.push([{ lat: results[0].geometry.location.lat(), lng: results[0].geometry.location.lng() }]);
                    modMapa.qtdLoopMapa++;
                    setTimeout(modMapa.loopMapa, 400);
                } else {
                    alert('O Geocode não foi bem sucedido pelo seguinte motivo: ' + status);
                }
            });
        } else if (modMapa.qtdLoopMapa === modMapa.enderecos.length) {
            modMapa.iniciaMapa();
        }
    }


    this.menuEsq = MenuEsq;
    function MenuEsq()
    {        
        menuEsqHtml = '';
        for (var m = 0; m < modMapa.enderecos.length; m++)
        {
            menuEsqHtml += '<div class="list-group-item">' +
                '<a href= "#" data-toggle="collapse" data-target="#demo-' + m + '" class="bt-colapse-diocese">' +
                '<div class="row">' +
                '<div class="col-md-2">' +
                '<i class="icmn-cloud-check2 color-success font-size-26"></i></div><div class="col-md-10">' +
                '<h5 class="list-group-item-heading">' + modMapa.enderecos[m].nome_servidor + '</h5>' +
                '<h6 class="list-group-item-text">' + moment(modMapa.enderecos[m].data_inclusao).format('DD/ MM - HH:mm: ss') + '</h6>' +
                '</div></div></a>' +
                '<div id="demo-'+ m +'" class="collapse"><hr />' +
                '<p class="margin-0"><b>CPU:</b> ' + modMapa.enderecos[m].uso_cpu + '%</p>' +
                '<p class="margin-0"><b>RAM LIVRE:</b> ' + parseFloat(modMapa.enderecos[m].uso_memoria) + 'Gb</p>' +
                '<p><a href="#" class="bt-mais-info btn btn-primary-outline margin-inline margin-0 margin-top-10 padding-0 padding-left-5 padding-right-5" data-titan-id="' + modMapa.enderecos[m].id_pessoa + '" data-titan-diocese="' + modMapa.enderecos[m].nome_servidor +'">Mais informações</a></p></div></div>';
          
        }
        $('#menuLatEsqMonitoramento').html(menuEsqHtml).promise().done(function ()
        {         
            $('.bt-mais-info').click(function (evt)
            {
                $('.scroll-pane').height(840);
                $('.graficos-banco-server h1 span.diocese').text('(' + $(this).data('titan-diocese') + ')');

                $('.grafico-server, .grafico-banco').html(carregadorGraficos);
                var id = $(this).data('titan-id');
                evt.preventDefault();
                
                $('.mapa-server').height(520);
                $('#map').height(450);
                $('.graficos-banco-server').height(420);
                modMapa.map.fitBounds(modMapa.limites);
                setTimeout(function ()
                {
                    modMapa.id = id;
                    modMapa.chamadaMonitoramento24hr(id);
                    modMapa.chamadaDisponibilidadeBanco(id);                 
                }, 1500);               
            });
        }); 
    }

    this.chamadaMonitoramento24hr = ChamadaMonitoramento24hr;
    function ChamadaMonitoramento24hr(id)
    { 
        $.ajax({type: 'GET', url: '../MonitoraAplicacaoSig/Monitoramento24hrServer', data: { id: id }, contentType: false, cache: false, success: function (data, status) {             
                if (status ==='success')
                {               
                    $('.grafico-server img.carregador').remove();
                    modMapa.graficoUsoServer(data);                    
                }               
            }
        });
    }


    this.graficoUsoServer = GraficoUsoServer;
    function GraficoUsoServer(dados)
    {       
        var horarios = [];
        var usuario = [];
        var cpu = [];
        var memoria = [];

        for (var d = 0; d < dados.length; d++)
        {          
            horarios[d] = dados[d].horas;
            usuario[d] = dados[d].total_usuarios_logados;
            cpu[d] = dados[d].uso_cpu;
            memoria[d] = dados[d].uso_memoria;
        }
        var elemento = new Chartist.Line('.grafico-server', { labels: horarios, series: [usuario, cpu, memoria] },
        {
            fullWidth: true,
            chartPadding:
            {
                right: 10,
                left: 0,
                top: 10
            },
            height: 270,
            axisY: {
                ticks: usuario,
                divisor: 4
            },
            plugins: [                    
                Chartist.plugins.tooltip(),
                Chartist.plugins.ctAxisTitle({
                    axisX: {
                        axisTitle: 'Últimas 24hrs',
                        axisClass: 'ct-axis-title',
                        offset: {
                            x: 0,
                            y: 35
                        },
                        textAnchor: 'middle'
                    },
                    axisY: {
                        axisTitle: 'Usuários ativos',
                        axisClass: 'ct-axis-title',
                        offset: {
                            x: 0,
                            y: 0
                        },
                        textAnchor: 'middle',
                        flipTitle: false
                    }
                }),
                Chartist.plugins.legend({legendNames: ['Usuários conectados', 'Porcentagem do uso da CPU', 'Memoria RAM livre em GB']})
            ]
        });
        elemento.on('created', function ()
        {
            setTimeout(function ()
            {              
                var coresLegenda = ['#46be8a', '#f05b4f', '#f4c63d'];
                for (var c = 0; c < coresLegenda.length; c++)
                {
                    $('.ct-legend').find('li.ct-series-' + c).append('<span></span>');
                    $('.ct-legend').find('li.ct-series-' + c + ' > span').css('background-color', coresLegenda[c]);
                }
            }, 2000);
        });
    }


    this.chamadaDisponibilidadeBanco = ChamadaDisponibilidadeBanco;
    function ChamadaDisponibilidadeBanco(id)
    {
        $.ajax({type: 'GET', url: '../MonitoraAplicacaoSig/DisponibilidadeBanco',  data: { id: id },  contentType: false, cache: false, success: function (data, status) {
                if (status === 'success')
                {
                    $('.grafico-banco img.carregador').remove();
                    modMapa.graficoBanco(data);
                }
            }
        });
    }

    this.graficoBanco = GraficoBanco;
    function GraficoBanco(dados)
    {
        var disponibilidade = { ativo: 0, inativo: 0};    

        for (var d = 0; d < dados.length; d++)
        {
            (dados[d].situacao_banco === 'Ativos') ? disponibilidade.ativo = dados[d].total_conexao : disponibilidade.inativo = dados[d].total_conexao;        
        }

        var chart = new Chartist.Pie('.grafico-banco',
            {
                series: [{
                    name: 'done',
                    className: 'ct-done',
                    value: disponibilidade.ativo
                }, {
                    name: 'outstanding',
                    className: 'ct-outstanding',
                    value: disponibilidade.inativo
                }]
            },
            {
                donut: true,
                labelInterpolationFnc: function (value)
                {
                    var total = chart.data.series.reduce(function (prev, series)
                    {
                        return prev + series.value;
                    }, 0);
                    return Math.round(value / total * 100) + '%*';
                }
            });

        chart.on('draw', function (ctx)
        {
            if (ctx.type === 'label')
            {
                if (ctx.index === 0)
                {
                    ctx.element.attr({dx: ctx.element.root().width() / 2, dy: ctx.element.root().height() / 2 });
                } else {
                    ctx.element.remove();
                }
            }
        });
    }    
}