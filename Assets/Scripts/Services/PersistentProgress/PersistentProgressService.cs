using Data;

namespace Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public UserProgress UserProgress { get; set; }
    }
}