using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption.ParticipationHistoryExceptions
{
    public class ParticipationHistoryNotFoundException : Exception
    {
        const string ParticipationHistoryNotFoundMessage = "История участия в проекте не найдена";

        public ParticipationHistoryNotFoundException() : base(ParticipationHistoryNotFoundMessage) { }
    }
}
