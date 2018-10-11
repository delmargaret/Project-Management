using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Repository.Interfaces
{
    public interface IParticipationHistoryRepository
    {
        IEnumerable<ParticipationHistory> GetAll();
        ParticipationHistory Get(int id);
        IEnumerable<ParticipationHistory> Find(Func<ParticipationHistory, Boolean> predicate);
        void Create(ParticipationHistory item);
        void Update(ParticipationHistory item);
        void Delete(int id);
    }
}
