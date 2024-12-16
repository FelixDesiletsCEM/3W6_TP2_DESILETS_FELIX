using JuliePro.Models;

namespace JuliePro.Services
{
    public interface IRecordService : IServiceBaseAsync<RecordViewModel>
    {
        public Task<Record> CreateAsync(Record model, IFormCollection form);
        public Task EditAsync(Record model, IFormCollection form);
    }
}
