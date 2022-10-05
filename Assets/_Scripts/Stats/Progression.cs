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

                foreach (var item2 in item.stats)
                {
                    if (item2.stats != stats)
                    {
                        continue;
                    }

                    if (item2.levels.Length < level)
                    {
                        continue;
                    }

                    return item2.levels[level - 1];
                }
            }

            return 100;
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