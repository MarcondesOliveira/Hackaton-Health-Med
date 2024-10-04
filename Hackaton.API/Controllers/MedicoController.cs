using AutoMapper;
using Hackaton.Application.Features.Commands.CreateMedico;
using Hackaton.Application.Features.Queries.GetMedicoById;
using Hackaton.Application.Features.Queries.GetMedicos;
using Hackaton.Application.Services;
using Hackaton.Domain.Dto;
using Hackaton.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hackaton.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MedicoController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedico([FromBody] CreateMedicoCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var medicoId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetMedicoById), new { id = medicoId }, medicoId);
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicoDto>>> GetAllMedicos()
        {
            var query = new GetAllMedicosQuery();
            var medicos = await _mediator.Send(query);
            return Ok(medicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoDto>> GetMedicoById(Guid id)
        {
            var medico = await _mediator.Send(new GetMedicoByIdQuery { Id = id });
            if (medico == null)
            {
                return NotFound();
            }
            return Ok(medico);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateMedico(Guid id, [FromBody] UpdateMedicoCommand command)
        //{
        //    if (id != command.MedicoId)
        //    {
        //        return BadRequest();
        //    }

        //    await _mediator.Send(command);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMedico(Guid id)
        //{
        //    await _mediator.Send(new DeleteMedicoCommand { MedicoId = id });
        //    return NoContent();
        //}
    }
}
