using Newtonsoft.Json.Schema;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] private ProgressionCharacterClass[] m_CharacterClasses = null;

        public float GetStat(Stats stats, CharacterClass character, int level)
        {
            foreach (ProgressionCharacterClass item in m_CharacterClasses)
            {
                if (item.m_CharacterClass != character)
                {
                    continue;
                }

                foreach (var VARIABLE in item.stats)
                {
                    if (VARIABLE.stats != stats)
                    {
                        continue;
                    }

                    if (VARIABLE.levels.Length < level)
                    {
                        continue;
                    }

                    return VARIABLE.levels[level - 1];
                }
            }

            return 0;
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