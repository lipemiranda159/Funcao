using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        private readonly BoBeneficiario _boBeneficiario;

        public BeneficiarioController(BoBeneficiario boBeneficiario)
        {
            _boBeneficiario = boBeneficiario;
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {

                try
                {
                    model.ModalId = _boBeneficiario.Incluir(GetBeneficiarioByModel(model));
                    return Json("Cadastro efetuado com sucesso");
                }
                catch (CpfJaExisteException ex)
                {
                    Response.StatusCode = 400;
                    return Json(ex.Message);
                }
            }
        }

        private static Beneficiario GetBeneficiarioByModel(BeneficiarioModel model)
        {
            return new Beneficiario()
            {
                Nome = model.ModalNome,
                CPF = model.ModalCPF
            };
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
        {
            var bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                var beneficiario = GetBeneficiarioByModel(model);
                beneficiario.Id = model.ModalId;
                _boBeneficiario.Alterar(beneficiario);

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            var bo = new BoBeneficiario();
            Beneficiario beneficiario = bo.Consultar(id);
            BeneficiarioModel model = null;

            if (beneficiario != null)
            {
                model = new BeneficiarioModel()
                {
                    ModalNome = beneficiario.Nome,
                    ModalCPF = beneficiario.Nome
                };


            }

            return View(model);
        }

        [HttpPost]
        public JsonResult BeneficiarioList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Beneficiario> beneficiarios = _boBeneficiario.Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = beneficiarios, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
