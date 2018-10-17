using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption.ParticipationHistoryExceptions
{
    public class ParticipationHistoryIdNotSetException : Exception
    {
        const string ParticipationHistoryIdNotSetMessage = "Не установлен идентификатор истории участия в проекте";

        public ParticipationHistoryIdNotSetException() : base(ParticipationHistoryIdNotSetMessage) { }
    }
}
