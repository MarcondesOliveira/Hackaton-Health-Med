﻿using AutoMapper;
using Hackaton.Application.Features.Commands.CreatePaciente;
using Hackaton.Application.Features.Commands.LoginPaciente;
using Hackaton.Application.Features.Commands.UpdateConsulta;
using Hackaton.Application.Features.Queries.GetMedicos;
using Hackaton.Application.Features.Queries.GetPacienteById;
using Hackaton.Domain.Dto;
using Hackaton.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hackaton.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PacienteController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarPaciente([FromBody] CreatePacienteCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var pacienteId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPacienteById), new { id = pacienteId }, pacienteId);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginPacienteCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var token = await _mediator.Send(command);
            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet("todos-medicos")]
        public async Task<ActionResult<List<MedicoDto>>> GetAllMedicos()
        {
            var query = new GetAllMedicosQuery();
            var medicos = await _mediator.Send(query);
            return Ok(medicos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDto>> GetPacienteById(Guid id)
        {
            var paciente = await _mediator.Send(new GetPacienteByIdQuery { Id = id });
            if (paciente == null)
            {
                return NotFound();
            }
            return Ok(paciente);
        }

        [Authorize(Roles = "Paciente")]
        [HttpPut("agendar-consulta")]
        public async Task<IActionResult> UpdateConsulta([FromBody] UpdateConsultaCommand command)
        {
            if (command == null || command.ConsultaId == Guid.Empty)
            {
                return BadRequest();
            }

            command.Status = Status.Agendada;

            var success = await _mediator.Send(command);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
