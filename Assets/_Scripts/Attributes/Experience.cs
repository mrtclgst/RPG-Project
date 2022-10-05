using RPG.Saving;
using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour , ISaveable
    {
        [SerializeField] private float m_ExperiencePoints = 0;

        public void GainExperience(float experience)
        {
            m_ExperiencePoints += experience;
        }

        public float GetPoints()
        {
            return m_ExperiencePoints;
        }
        
        public object CaptureState()
        {
            return m_ExperiencePoints;
        }

        public void RestoreState(object state)
        {
            m_ExperiencePoints = (float)state;
        }
    }
}