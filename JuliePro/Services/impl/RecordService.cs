using JuliePro.Data;
using JuliePro.Models;
using JuliePro.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JuliePro.Services.impl
{
    public class RecordService : ServiceBaseEF<Record>
    {
        public RecordService(JulieProDbContext dbContext) : base(dbContext)
        {}

        public async Task<RecordViewModel> GetTrainerRecords(int id)
        {

            var result = new RecordViewModel();


            result.trainer = this._dbContext.Trainers.Where(t => t.Id == id).First();
            result.TrainerRecords = this._dbContext.Records.Where(r => r.Trainer_Id == id).ToList();

            result.trainers = new SelectList(this._dbContext.Trainers, "Id", "Name");
            result.disciplines = new SelectList(this._dbContext.Disciplines, "Id", "Name");

            return result;
        }
    }
}
