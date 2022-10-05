using UnityEngine;
using System.Collections.Generic;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] private ProgressionCharacterClass[] m_CharacterClasses = null;

        private Dictionary<CharacterClass, Dictionary<Stat, float[]>> m_LookUpTable = null;

        public float GetStat(Stat stat, CharacterClass character, int level)
        {
            BuildLookUp();

            float[] levels = m_LookUpTable[character][stat];

            if (levels.Length < level)
            {
                return 0;
            }

            return levels[level - 1];

            // foreach (ProgressionCharacterClass item in m_CharacterClasses)
            // {
            //     if (item.m_CharacterClass != character)
            //     {
            //         continue;
            //     }
            //
            //     foreach (var item2 in item.stats)
            //     {
            //         if (item2.stats != stats)
            //         {
            //             continue;
            //         }
            //
            //         if (item2.levels.Length < level)
            //         {
            //             continue;
            //         }
            //
            //         return item2.levels[level - 1];
            //     }
            // }
            //
            // return 100;
        }

        private void BuildLookUp()
        {
            if (m_LookUpTable != null)
            {
                return;
            }

            m_LookUpTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach (ProgressionCharacterClass progressionCharacterClass in m_CharacterClasses)
            {
                var statLookUpTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat progressionStat in progressionCharacterClass.stats)
                {
                    statLookUpTable[progressionStat.stats] = progressionStat.levels;
                }

                m_LookUpTable[progressionCharacterClass.m_CharacterClass] = statLookUpTable;
            }
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
            public Stat stats;
            public float[] levels;
        }
    }
}