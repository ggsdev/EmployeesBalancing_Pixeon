using Pixeon.Interfaces;

namespace Pixeon.Classes
{
    public class Employee : IEmployee
    {
        public string Name { get; set; }
        public byte PLevel { get; set; }
        public int BirthYear { get;  set; }
        public int AdmissionYear { get;  set; }
        public int LastProgressionYear { get; set; }
        public float TotalScoreForPromotion { get; set; }
        public Employee(string name, byte pLevel, int birthYear, int admissionYear, int lastProgressionYear)
        {
            Name = name;
            PLevel = pLevel;
            BirthYear = birthYear;
            AdmissionYear = admissionYear;
            LastProgressionYear = lastProgressionYear;
            TotalScoreForPromotion = (float) GetTotalPromotionScore(admissionYear, lastProgressionYear, birthYear);
        }

        static double GetTotalPromotionScore(int admissionYear, int lastProgressionYear, int birthYear)
        {
            int currentYear = DateTime.Now.Year;
            int timeWithoutProgression = currentYear - lastProgressionYear;
            int companyTime = currentYear - admissionYear;
            int ageEmployee = currentYear - birthYear;
            float totalScoreForPromotion = companyTime * 2 + timeWithoutProgression * 3 + ageEmployee / 5;

            return totalScoreForPromotion;
        }
    }
}
