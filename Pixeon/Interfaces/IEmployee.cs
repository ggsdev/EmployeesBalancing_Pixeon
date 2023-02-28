using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixeon.Interfaces
{
    public interface IEmployee
    {
        string Name { get; }
        byte PLevel { get; }
        int BirthYear { get; }
        int AdmissionYear { get; }
        int LastProgressionYear { get; }
        float TotalScoreForPromotion { get; }
    }
}
