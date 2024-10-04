using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;

namespace Hackaton.Application.Services
{
    public class MedicoService
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoService(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<IEnumerable<Medico>> GetAllMedicosAsync()
        {
            return await _medicoRepository.ListAllAsync();
        }

        public async Task<Medico> GetMedicoByIdAsync(Guid id)
        {
            return await _medicoRepository.GetByIdAsync(id);
        }

        public async Task AddMedicoAsync(Medico medico)
        {
            await _medicoRepository.AddAsync(medico);
        }

        public async Task UpdateMedicoAsync(Medico medico)
        {
            await _medicoRepository.UpdateAsync(medico);
        }

        public async Task DeleteMedicoAsync(Guid id)
        {
            var medico = await _medicoRepository.GetByIdAsync(id);
            if (medico != null)
            {
                await _medicoRepository.DeleteAsync(medico);
            }
        }
    }
}
