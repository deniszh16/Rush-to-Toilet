using Data;

namespace Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public UserProgress GetUserProgress { get; set; }
    }
}