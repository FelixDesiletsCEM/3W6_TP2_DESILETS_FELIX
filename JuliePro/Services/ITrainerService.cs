using JuliePro.Data;
using JuliePro.Models;
using Microsoft.EntityFrameworkCore;

namespace JuliePro.Services
{
    public interface ITrainerService : IServiceBaseAsync<Trainer>
    {
        public Task<TrainerSearchViewModel> GetAllAsync(TrainerSearchViewModelFilter filter);
        public Task<Trainer> CreateAsync(Trainer model, IFormCollection form);
        public Task EditAsync(Trainer model, IFormCollection form);
    }
}
