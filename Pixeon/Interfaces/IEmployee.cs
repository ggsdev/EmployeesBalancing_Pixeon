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
