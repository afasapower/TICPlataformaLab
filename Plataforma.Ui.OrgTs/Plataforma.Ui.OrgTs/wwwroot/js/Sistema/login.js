$(function () {
    
    $('body').addClass('single-page single-page-inverse');

    $('#form-validation').submit(function (event)
    {       
        var formData = {
            'username': $('input[name=username]').val(),
            'password': $.md5($('input[name=password]').val()),
            '__RequestVerificationToken': $('input[name=__RequestVerificationToken]').val()
        };
              
        $.ajax({
            type: 'POST', 
            url: 'Login', 
            data: formData,
            dataType: 'json', 
            encode: true
        }).done(function (data)
        {
            if (data.autenticado === false)
            {
                swal({
                    title: "Acesso negado",
                    text: "Senha ou usuário inválidos.",
                    type: "warning"
                });
            } else
            {
                window.location.href = data.urlAutenticacao;
                }
            });        
        event.preventDefault();
    });

});