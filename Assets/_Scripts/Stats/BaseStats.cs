using System;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)] [SerializeField] private int m_StartingLevel = 1;
        [SerializeField] CharacterClass m_CharacterClass;
        [SerializeField] private Progression m_Progression;
        [SerializeField] private GameObject m_LevelUpParticleEffect = null;

        [SerializeField] bool m_ShouldUseModifiers = false;
        private int m_CurrentLevel = 0;

        public event Action onLevelUp;

        private void Start()
        {
            m_CurrentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > m_CurrentLevel)
            {
                m_CurrentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(m_LevelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return GetBaseStat(stat) + GetAdditiveModifier(stat) * (1 + GetModifierPercentage(stat) / 100);
        }


        private float GetBaseStat(Stat stat)
        {
            return m_Progression.GetStat(stat, m_CharacterClass, GetLevel());
        }


        public int GetLevel()
        {
            if (m_CurrentLevel < 1)
            {
                m_CurrentLevel = CalculateLevel();
            }

            return m_CurrentLevel;
        }

        private int CalculateLevel()
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

        private float GetAdditiveModifier(Stat stat)
        {
            if (!m_ShouldUseModifiers)
            {
                return 0;
            }

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }

            return total;
        }

        private float GetModifierPercentage(Stat stat)
        {
            if (!m_ShouldUseModifiers)
            {
                return 0;
            }

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }

            return total;
        }

        //artik gerek yok yukaridan cekecegiz
        // public float GetExperienceReward()
        // {
        //     return 10;
        // }
    }
}