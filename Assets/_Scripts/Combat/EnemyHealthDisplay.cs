using RPG.Attributes;
using RPG.Combat;
using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        private Fighter m_Fighter;

        private void Awake()
        {
            m_Fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if (m_Fighter.GetTarget() == null)
            {
                GetComponent<TextMeshProUGUI>().text = "N/A";
                return;
            }

            Health health = m_Fighter.GetTarget();
            GetComponent<TextMeshProUGUI>().text = $"{health.GetPercentage():0}%";
        }
    }
}