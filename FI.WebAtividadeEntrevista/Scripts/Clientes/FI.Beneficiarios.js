$('#formBeneficiario').submit(function (e) {
    e.preventDefault();

    if (!$('#formBeneficiario').valid())
        return;

    const _cpf = $('#CPFBeneficiario').val();
    const _nome = $('#NomeBeneficiario').val();
    const _idCliente = obj != null ? obj.Id : 0;

    try {


        VerificaBeneficiarioCliente(_cpf, _idCliente);

        // Recupera os beneficiários da sessionStorage
        let beneficiarios = sessionStorage.getItem("beneficiarios") == "" ? [] : JSON.parse(sessionStorage.getItem("beneficiarios"));

        // Cria um novo beneficiário com os dados informados
        let beneficiario = {
            "Id": 0,
            "CPFBeneficiario": _cpf,
            "NomeBeneficiario": _nome,
            "IdCliente": _idCliente
        };

        // Acrescenta o novo beneficiário junto dos demais recuperados da sessionStorage
        beneficiarios.push(beneficiario);

        // Recoloca na sessionStorage o JSON contendo o novo beneficiário
        sessionStorage.setItem("beneficiarios", JSON.stringify(beneficiarios));

        // Monta o HTML que será acrescentado na tabela
        let contentRow = `<tr><td>${beneficiario.CPFBeneficiario}</td><td>${beneficiario.NomeBeneficiario}</td><td class='text-center mx-auto'><button name='alterarBeneficiario' class='btn btn-primary' data-identifier='${beneficiario.CPFBeneficiario}' data-command='alterar'>Alterar</button> <button name='excluirBeneficiario' class='btn btn-primary' data-identifier='${beneficiario.CPFBeneficiario}' data-command='excluir'>Excluir</button></td></tr>`;

        // Insere o HTML na linha
        $('#tabelaBeneficiarios tbody').append(contentRow);

    } catch (e) {
        ModalDialog("Atenção!", e.message);
    }
})

function VerificaBeneficiarioCliente(cpf, idCliente) {
    cpf = cpf.replace(/[^0-9]/g, '');

    let beneficiarios = sessionStorage.getItem("beneficiarios") == "" ? [] : JSON.parse(sessionStorage.getItem("beneficiarios"));

    // Se encontrou uma lista, primeiro verifica na própria lista, pois o beneficiáio
    // pode já ter sido informado mas não estar gravado em banco
    if (beneficiarios != null && beneficiarios.length > 0) {
        beneficiarios.some(function (e) {
            if (e.CPFBeneficiario == cpf && e.IdCliente == idCliente) {
                throw new DOMException("Já existe um beneficiário com o CPF informado para esse cliente.");
            }
        })
    }

    $.get("/Cliente/VerificaBeneficiarioCliente", { cpf, idCliente }, function (result) {
        if (result === false) {
            throw new DOMException("Já existe um beneficiário com o CPF informado para esse cliente.");
        }
    })
}
