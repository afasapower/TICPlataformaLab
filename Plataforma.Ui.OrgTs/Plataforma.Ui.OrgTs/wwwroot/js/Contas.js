function Contas() {
    // declaração da variavel de escopo responsavel por realizar a comunicação entre metodos 
    // e acessos a outras variaveis da classe
    modContas = this;

    // declaração do metodo IniciaDadosConta
    this.iniciaDadosConta = IniciaDadosConta;
    // Metodo responsavel carregar dados de login do usuario
    function IniciaDadosConta()
    {      
        // Verifica se há alguma sessão iniciada 
        //caso haja ele impede uma nova requisição ao servidor       
        if (sessionStorage.getItem('titan') === null || sessionStorage.getItem('titan') === 'null')
        {
           $.ajax({
                url: 'Sistema_Contas/dadosUsuario',
                dataType: 'json',
                success: function (dados, status, xhr)
                {
                    // objeto resposnsavel por inserir dados na sessão "Titan"
                    var titan = {
                        idus: dados.dados[0].id,
                        idpessoa: dados.dados[0].id_pessoa,
                        nome: dados.dados[0].nome,
                        email: dados.dados[0].email,
                        grid: dados.dados[0].id_grupo,
                        grempid: dados.dados[0].id_pessoa_empresa,
                        login: dados.dados[0].login
                    };                 
                    // Metodo nativo de inserção de sessão
                    sessionStorage.setItem('titan', JSON.stringify(titan));
                },
                error: function (xhr, status, error)
                {
                    console.log('aqui 2222');
                    erros.chamaErro(xhr.status);
                }
            });
        }
    }
}