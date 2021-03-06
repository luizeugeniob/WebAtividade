﻿using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using Helpers;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            try
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    throw new Exception(string.Join(Environment.NewLine, erros));
                }
                else
                {
                    Cliente cliente = new Cliente()
                    {
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        CPF = model.CPF,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Telefone = model.Telefone,
                        Beneficiarios = new List<Beneficiario>()
                    };

                    if (model.Beneficiarios != null && model.Beneficiarios.Count > 0)
                    {
                        foreach (BeneficiarioModel beneficiario in model.Beneficiarios)
                        {
                            cliente.Beneficiarios.Add(new Beneficiario
                            {
                                Id = beneficiario.Id,
                                CPF = beneficiario.CPFBeneficiario,
                                Nome = beneficiario.NomeBeneficiario,
                                IdCliente = beneficiario.IdCliente
                            });
                        }
                    }

                    model.Id = bo.Incluir(cliente);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message);
            }

            return Json("Cadastro efetuado com sucesso");
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            try
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();
                    throw new Exception(string.Join(Environment.NewLine, erros));
                }
                else
                {
                    if (bo.VerificarExistencia(model.CPF, model.Id))
                        throw new Exception("O CPF informado já está cadastrado.");

                    Cliente cliente = new Cliente()
                    {
                        Id = model.Id,
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        CPF = model.CPF,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Telefone = model.Telefone,
                        Beneficiarios = new List<Beneficiario>()
                    };

                    if (model.Beneficiarios != null && model.Beneficiarios.Count > 0)
                    {
                        foreach (BeneficiarioModel beneficiario in model.Beneficiarios)
                        {
                            cliente.Beneficiarios.Add(new Beneficiario
                            {
                                Id = beneficiario.Id,
                                CPF = beneficiario.CPFBeneficiario,
                                Nome = beneficiario.NomeBeneficiario,
                                IdCliente = beneficiario.IdCliente
                            });
                        }
                    }

                    bo.Alterar(cliente);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message);
            }

            return Json("Cadastro alterado com sucesso");
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    CPF = cliente.CPF,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    Beneficiarios = (from ben in cliente.Beneficiarios
                                     select new BeneficiarioModel
                                     {
                                         Id = ben.Id,
                                         CPFBeneficiario = ben.CPF,
                                         NomeBeneficiario = ben.Nome,
                                         IdCliente = ben.IdCliente
                                     }).ToList()
                };
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public bool VerificaBeneficiarioCliente(string cpf, long idCliente)
        {
            return new BoCliente().VerificaBeneficiarioCliente(cpf, idCliente);
        }
    }
}