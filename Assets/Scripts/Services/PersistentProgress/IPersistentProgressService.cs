using Data;

namespace Services.PersistentProgress
{
    public interface IPersistentProgressService
    {
        public UserProgress UserProgress { get; set; }
    }
}