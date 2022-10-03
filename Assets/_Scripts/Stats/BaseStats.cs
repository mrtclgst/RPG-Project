using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] private int m_StartingLevel;
        [SerializeField] CharacterClass m_CharacterClass;
        [SerializeField] private Progression m_Progression;

        public float GetHealth()
        {
            return m_Progression.GetHealth(m_CharacterClass, m_StartingLevel);
        }

        public float GetExperienceReward()
        {
            return 10;
        }
    }
}