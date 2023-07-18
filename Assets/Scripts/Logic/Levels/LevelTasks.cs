using UnityEngine;

namespace Logic.Levels
{
    public class LevelTasks : MonoBehaviour
    {
        [Header("Персонажи уровня")]
        [SerializeField] private Character[] _characters;

        private int _linesDrawn;

        public void AddLine()
        {
            _linesDrawn++;

            if (_linesDrawn >= _characters.Length)
            {
                foreach (Character character in _characters)
                    character.StartMovement();
            }
        }
    }
}