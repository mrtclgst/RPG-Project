using System;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)] [SerializeField] private int m_StartingLevel;
        [SerializeField] CharacterClass m_CharacterClass;
        [SerializeField] private Progression m_Progression;

        public float GetStat(Stat stat)
        {
            return m_Progression.GetStat(stat, m_CharacterClass, m_StartingLevel);
        }

        private void Update()
        {
            if (gameObject.tag == "Player")
            {
                print(GetLevel());
            }
        }

        public int GetLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null)
            {
                return m_StartingLevel;
            }


            float currentExp = experience.GetPoints();
            int penultimateLevel = m_Progression.GetLevels(Stat.ExperienceToLevelUp, m_CharacterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float EXPtoLevelUp = m_Progression.GetStat(Stat.ExperienceToLevelUp, m_CharacterClass, level);
                if (EXPtoLevelUp > currentExp)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }

        //artik gerek yok yukaridan cekecegiz
        // public float GetExperienceReward()
        // {
        //     return 10;
        // }
    }
}