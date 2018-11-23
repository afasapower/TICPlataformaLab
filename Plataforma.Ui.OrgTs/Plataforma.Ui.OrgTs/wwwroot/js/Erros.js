function ErrosSistema()
{
    // declaração da variavel de escopo responsavel por realizar a comunicação entre metodos 
    // e acessos a outras variaveis da classe
    modErros = this;

    this.urlBaseTitan = window.location.origin;

    this.erroAtual = -1;

    this.erroHttp500 = ErroHttp500;
    function ErroHttp500()
    {
        modErros.erroAtual = 500;
        swal({
            title: "Erro interno no servidor",
            text: "Ocorreu um erro interno no sistema ao tentar processar sua requisição, atualize a página e tente novamente.  \n\nCaso o problema persista entre em contato com nosso suporte. ",
            type: "info",
            showCancelButton: true,
            confirmButtonClass: "btn-info",
            confirmButtonText: "Atualizar página",
            cancelButtonText: "Contatar suporte",
            closeOnConfirm: false,
            closeOnCancel: false
        },function (isConfirm)
        {
            if (isConfirm)
            {
                window.location.reload();
            } else
            {
                window.location.href = modErros.urlBaseTitan;
            }
        });
    }

    this.erroHttp502 = ErroHttp502;

    function ErroHttp502()
    {
        modErros.erroAtual = 502;
        swal({
            title: "Tempo limite excedido",
            text: "O tempo de conexão com servidor excedeu o tempo limite, atualize a página e tente novamente.  \n\nCaso o problema persista entre em contato com nosso suporte. ",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-warning",
            confirmButtonText: "Atualizar página",
            cancelButtonText: "Contatar suporte",
            closeOnConfirm: false,
            closeOnCancel: false
        }, function (isConfirm)
        {
            if (isConfirm)
            {
                window.location.reload();
            } else
            {
                window.location.href = modErros.urlBaseTitan;
            }
        });
    }


    this.erroHttp400 = ErroHttp400;
    function ErroHttp400()
    {
        modErros.erroAtual = 400;
        swal({
            title: "Requisição inválida",
            text: "Ocorreu um problema no envio da requisição para o servidor, atualize a página e tente novamente. \n\nCaso o problema persista entre em contato com nosso suporte.",
            type: "info",
            showCancelButton: true,
            confirmButtonClass: "btn-info",
            confirmButtonText: "Atualizar página",
            cancelButtonText: "Contatar suporte",
            closeOnConfirm: false,
            closeOnCancel: false
        }, function (isConfirm)
        {
            if (isConfirm)
            {
                window.location.reload();
            } else
            {
                window.location.href = modErros.urlBaseTitan;
            }
        });
    }


    this.erroHttp401 = ErroHttp401;
    function ErroHttp401()
    {       
        modErros.erroAtual = 401;
        swal({
            title: "Autenticação obrigatória",
            text: "O tempo de conexão com o sistema expirou, será necessário se autenticar novamente.",
            type: "info",
            showCancelButton: false,
            confirmButtonClass: "btn-info",
            confirmButtonText: "Ir, para autenticação",
            cancelButtonText: "",
            closeOnConfirm: false,
            closeOnCancel: false
        },
        function (isConfirm)
        {
            if (isConfirm)
            {
                window.location.href = modErros.urlBaseTitan;
            } 
        });       
    }

    this.erroHttp404 = ErroHttp404;
    function ErroHttp404()
    {
        modErros.erroAtual = 404;
        swal({
            title: "Página não encontrada",
            text: "Ocorreu um problema no carregamento da página, atualize a página e tente novamente. \n\nCaso o problema persista entre em contato com nosso suporte.",
            type: "danger",
            showCancelButton: true,
            confirmButtonClass: "btn-info",
            confirmButtonText: "Atualizar página",
            cancelButtonText: "Contatar suporte",
            closeOnConfirm: false,
            closeOnCancel: false
        },
        function (isConfirm) {
            if (isConfirm)
            {
                window.location.reload();
            }else
            {
                window.location.href = modErros.urlBaseTitan;
            }
        });
    }

    this.chamaErro = ChamaErro;

    function ChamaErro(qualErro)
    {
        if (modErros.erroAtual === parseInt(qualErro))
        {
            return false;
        }

        switch(parseInt(qualErro))
        {
            case 400:
                modErros.erroHttp400();
                break;
            case 401:
                modErros.erroHttp401();
                break;
            case 404:
                modErros.erroHttp404();
                break;
            case 500:
                modErros.erroHttp500();
                break;
            case 502:
                modErros.erroHttp502();
            case 0:
                modErros.erroHttp404();
                break;
            default:
                break;
        }
        modErros.erroAtual = parseInt(qualErro);
    }
}