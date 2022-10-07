using System;
using RPG.Saving;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] private float m_ExperiencePoints = 0;

        // public delegate void ExperienceGainedDelegate(float value);
        // public event ExperienceGainedDelegate onExperienceGained;

        public event Action onExperienceGained;

        public void GainExperience(float experience)
        {
            m_ExperiencePoints += experience;
            onExperienceGained();
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