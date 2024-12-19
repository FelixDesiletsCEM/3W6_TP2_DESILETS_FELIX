using Microsoft.AspNetCore.Mvc.Rendering;

namespace JuliePro.Models
{
    public class RecordViewModel
    {
        public RecordViewModel()
        {

        }
        public Trainer trainer { get; set; }

        public IEnumerable<Record> TrainerRecords { get; set; }

        public Record record { get; set; }

        public SelectList trainers { get; set; }
        public SelectList disciplines { get; set; }
    }
}