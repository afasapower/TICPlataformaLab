function Graficos()
{
    // declaração da variavel de escopo responsavel por realizar a comunicação entre metodos 
    // e acessos a outras variaveis da classe
    var modGraficos = this;

    // declaração do metodo InicioGrafico
    this.inicioGrafico = InicioGrafico;
    function InicioGrafico()
    {
                 
        if (document.getElementsByClassName('grafico').length > 0)
        {
            var data = new FormData();
            data.append('id_empresa', navegacao.buscaSessaoNavegacao('grempid', 'titan'));
            data.append('id_pessoa', navegacao.buscaSessaoNavegacao('idpessoa', 'titan'));
            data.append('usuario', navegacao.buscaSessaoNavegacao('login', 'titan'));
            
            var nomesMeses, valorMeses = [];

            $.ajax({
                url: "Home/GraficoProcessos",
                type: "POST",      
                contentType: false,
                processData: false,
                cache: false,
                data: data,
                success: function (result)
                {
                    nomesMeses = result.nomeMeses;
                    valorMeses = result.listaNumMeses;

                    if (result.nomeMeses.length > 1)
                    {
                        new Chartist.Line(".grafico-linha", { labels: nomesMeses, series: valorMeses }, { fullWidth: !0, chartPadding: { right: 40 }, plugins: [Chartist.plugins.tooltip()] });
                    } else
                    {
                        $(".grafico-linha").html('<h4 class="color-default padding-top-100 text-center"><span class="icmn-stats-dots margin-right-15 font-size-20"></span>Dados insuficientes</h4>');
                    }
                },
                error: function (xhr, status, error)
                {
                    console.log(result.xhr);
                }
            });           
        }
         
    }

    window.addEventListener('storage', (e) =>
    {
        if (e.storageArea === sessionStorage)
        {
            modGraficos.inicioGrafico();
        }
    });
}