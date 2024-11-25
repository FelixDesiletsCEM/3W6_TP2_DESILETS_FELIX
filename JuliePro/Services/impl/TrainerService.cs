using JuliePro.Data;
using JuliePro.Models;
using JuliePro.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JuliePro.Services.impl
{
    public class TrainerService : ServiceBaseEF<Trainer>, ITrainerService
    {
        private IImageFileManager fileManager;

        public TrainerService(JulieProDbContext dbContext, IImageFileManager fileManager) : base(dbContext)
        {
            this.fileManager = fileManager;
        }

        public async Task<Trainer> CreateAsync(Trainer model, IFormCollection form)
        {
            model.Photo = await fileManager.UploadImage(form, false, null);

            return await base.CreateAsync(model);
        }

        public async Task EditAsync(Trainer model, IFormCollection form)
        {
            var old = await _dbContext.Trainers.Where(x=>x.Id == model.Id).Select(x=>x.Photo).FirstOrDefaultAsync();
            model.Photo = await fileManager.UploadImage(form, true, old);
            await this.EditAsync(model);
        }

        public async Task<TrainerSearchViewModel> GetAllAsync(TrainerSearchViewModelFilter filter)
        {
            filter.VerifyProperties();//mets à null les éléments qui sont vides. 

            var result = new TrainerSearchViewModel(filter);

            //TODO Faire les filtres et utilisez les paramètres de pagination.

            result.Items = await this._dbContext.Trainers.ToPaginatedAsync(0,10000);

            //TODO ajouter les éléments dans selectList 
            result.AvailablePageSizes = new SelectList(new List<int>() { 9, 12, 18, 21 });
            result.Disciplines = new SelectList(new List<Discipline>(), "Id", "Name");
            result.Certifications = new SelectList(new List<Certification>(), "Id", "FullTitle");
            result.CertificationCenters = new SelectList(new List<string>());

            return result;
        }
    }
}
