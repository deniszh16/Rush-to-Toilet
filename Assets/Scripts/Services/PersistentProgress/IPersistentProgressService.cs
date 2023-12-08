using Data;

namespace Services.PersistentProgress
{
    public interface IPersistentProgressService
    {
        public UserProgress GetUserProgress { get; set; }
    }
}