using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption.ParticipationHistoryExceptions
{
    public class ParticipationHistoriesNotFoundException : Exception
    {
        const string ParticipationHistoriesNotFoundMessage = "Истории участия в проекте не найдены";

        public ParticipationHistoriesNotFoundException() : base(ParticipationHistoriesNotFoundMessage) { }
    }
}
