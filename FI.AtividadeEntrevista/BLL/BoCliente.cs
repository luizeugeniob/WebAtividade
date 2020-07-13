using FI.AtividadeEntrevista.DML;
using Helpers;
using System;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(DML.Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();

            if (!ValidacaoHelper.ValidaCPF(cliente.CPF))
                throw new Exception("Informe um CPF válido!");

            if (VerificarExistencia(cliente.CPF, cliente.Id))
                throw new Exception("O CPF informado já está cadastrado!");

            foreach (Beneficiario beneficiario in cliente.Beneficiarios)
            {
                if (!ValidacaoHelper.ValidaCPF(beneficiario.CPF))
                    throw new Exception($"Informe um CPF válido para o beneficiário [{beneficiario.Nome}]!");
            }

            return cli.Incluir(cliente);
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(DML.Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();

            if (!ValidacaoHelper.ValidaCPF(cliente.CPF))
                throw new Exception($"O CPF do cliente [{cliente.Nome.ToUpper()}] não está correto!");

            if (VerificarExistencia(cliente.CPF, cliente.Id))
                throw new Exception($"Já existe um cliente com o CPF informado!");

            cli.Alterar(cliente);
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();

            Cliente cliente = cli.Consultar(id);
            cliente.Beneficiarios = ben.Consultar(cliente.Id);

            return cliente;
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Listar()
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// Verifica se o CPF já está gravado.
        /// </summary>
        /// <param name="cpf">CPF que será testado.</param>
        /// <param name="idCliente">ID do cliente.</param>
        /// <returns>Retorna <c><see langword="true"/></c> se já existe um cliente gravado em banco com este CPF.</returns>
        public bool VerificarExistencia(string cpf, long idCliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarExistencia(cpf, idCliente);
        }

        /// <summary>
        /// Verifica se o beneficiário já foi informado para o atual cliente.
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public bool VerificaBeneficiarioCliente(string cpf, long idCliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificaBeneficiarioCliente(cpf, idCliente);
        }
    }
}
