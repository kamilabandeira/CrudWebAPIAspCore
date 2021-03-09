using CrudWebAPIAspCore.Models;
using CrudWebAPIAspCore.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrudWebAPIAspCore.Controllers
{
    [Route("filmes")]
    public class FilmesController : ApiController
    {
        private readonly IFilmeService service;
        public FilmesController(IFilmeService service)
        {
            this.service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var model = service.GetFilmes();
            var outputModel = ToOutputModel(model);
            return (IActionResult)Ok(outputModel);
        }
        [HttpGet("{id}", Name = "GetFilme")]
        public IActionResult Get(int id)
        {
            var model = service.GetFilme(id);
            if (model == null)
                return (IActionResult)NotFound();
            var outputModel = ToOutputModel(model);
            return (IActionResult)Ok(outputModel);
        }
        [HttpPost]
        public IActionResult Create([FromBody] FilmeInputModel inputModel)
        {
            if (inputModel == null)
                return (IActionResult)BadRequest();
            var model = ToDomainModel(inputModel);
            service.AddFilme(model);
            var outputModel = ToOutputModel(model);
            return (IActionResult)CreatedAtRoute("GetFilme", new { id = outputModel.Id }, outputModel);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FilmeInputModel inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
                return (IActionResult)BadRequest();
            if (!service.FilmeExists(id))
                return (IActionResult)NotFound();
            var model = ToDomainModel(inputModel);
            service.UpdateFilme(model);
            return NoContent();
        }

        private IActionResult NoContent()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!service.FilmeExists(id))
                return (IActionResult)NotFound();
                service.DeleteFilme(id);
                return NoContent();
        }
        //--------------------------------------------------
        //Mapeamentos : modelos envia/recebe dados via API
        private FilmeOutputModel ToOutputModel(Filme model)
        {
            return new FilmeOutputModel
            {
                Id = model.Id,
                Titulo = model.Titulo,
                AnoLancamento = model.AnoLancamento,
                Resumo = model.Resumo,
                UltimoAcesso = DateTime.Now
            };
        }
        private List<FilmeOutputModel> ToOutputModel(List<Filme> model)
        {
            return model.Select(item => ToOutputModel(item)).ToList();
        }
        private Filme ToDomainModel(FilmeInputModel inputModel)
        {
            return new Filme
            {
                Id = inputModel.Id,
                Titulo = inputModel.Titulo,
                AnoLancamento = inputModel.AnoLancamento,
                Resumo = inputModel.Resumo
            };
        }
        private FilmeInputModel ToInputModel(Filme model)
        {
            return new FilmeInputModel
            {
                Id = model.Id,
                Titulo = model.Titulo,
                AnoLancamento = model.AnoLancamento,
                Resumo = model.Resumo
            };
        }
    }
}
