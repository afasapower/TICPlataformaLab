function startTime()
{
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    m = checkTime(m);
    s = checkTime(s);
    document.getElementById('horario').innerHTML = h + ":" + m + ":" + s;
    var t = setTimeout(startTime, 500);
    var cumprimento = ["Bom dia!", "Boa tarde!", "Boa noite!"];
    var qualCumprimento = 0;
    if (h > 12)
    {
        qualCumprimento = 1;
    } else if(h > 17)
    {
        qualCumprimento = "Boa noite!";
    } else if (h > 6 && h < 11)
    {
        qualCumprimento = 0;
    }
    $('div.mensagem-home h3.usuario-msg em').text(cumprimento[qualCumprimento]);
}
function checkTime(i)
{
    if (i < 10) { i = "0" + i };  // add zero in front of numbers < 10
    return i;
}
$(document).ready(function ()
{
    // Select responsável por alterar o tipo de solicitação do chamado
    $('.select-titan').selectpicker({ size: 7 });

    startTime();

    $(".bt-pesquisa-rapida").keydown(function (evt)
    {
        AcessoRapido(evt.currentTarget.id, evt.currentTarget.dataset.titanParceiro);
    });   
});


//bt-pesquisa-rapida

function AcessoRapido(qualBt, lista)
{    

    var input, filtro, ul, li, a, i;
    input = document.getElementById(qualBt);
    filtro = input.value.toUpperCase();
    ul = document.getElementById(lista);
    li = ul.getElementsByTagName('li');
    
    for (i = 0; i < li.length; i++)
    {
        a = li[i];
        if (a.innerHTML.toUpperCase().indexOf(filtro) > -1)
        {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}