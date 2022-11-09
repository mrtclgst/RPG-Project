using System;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float m_HealthPoints = -1f;
        [SerializeField] private float m_RegenerationPercentage = 70;
        [SerializeField] private UnityEvent<float> m_TakeDamage;
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        private void Start()
        {
            if (m_HealthPoints < 0)
            {
                m_HealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        internal void TakeDamage(GameObject instigator, float damage)
        {
            m_HealthPoints = Mathf.Max(m_HealthPoints - damage, 0);
            if (m_HealthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
            else
            {
                m_TakeDamage.Invoke(damage);
            }

            // print(healthPoints);
        }

        public float GetHealthPoints()
        {
            return m_HealthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return m_HealthPoints / GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionSchedular>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        private void RegenerateHealth()
        {
            // float regeneratedHealthPoints = m_HealthPoints =
            //     GetComponent<BaseStats>().GetStat(Stat.Health) * (m_RegenerationPercentage / 100);
            // m_HealthPoints = Mathf.Max(m_HealthPoints, regeneratedHealthPoints);
            m_HealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public object CaptureState()
        {
            return m_HealthPoints;
        }

        public void RestoreState(object state)
        {
            m_HealthPoints = (float)state;
            if (m_HealthPoints == 0)
            {
                Die();
            }
        }
    }
}