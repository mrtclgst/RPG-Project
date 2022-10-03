using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] private ProgressionCharacterClass[] m_CharacterClasses = null;

        public float GetHealth(CharacterClass character, int level)
        {
            foreach (ProgressionCharacterClass item in m_CharacterClasses)
            {
                if (item.m_CharacterClass == character)
                {
                    //return item.m_Health[level - 1];
                }
            }
            return 30;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass m_CharacterClass;

            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stats stats;
            public float[] levels;
        }
    }
}