using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)] [SerializeField] private int m_StartingLevel;
        [SerializeField] CharacterClass m_CharacterClass;
        [SerializeField] private Progression m_Progression;

        public float GetStat(Stats stats)
        {
            return m_Progression.GetStat(stats, m_CharacterClass, m_StartingLevel);
        }
        
        //artik gerek yok yukaridan cekecegiz
        // public float GetExperienceReward()
        // {
        //     return 10;
        // }
    }
}