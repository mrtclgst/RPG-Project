using TMPro;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        private Health m_Health;

        private void Awake()
        {
            m_Health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            GetComponent<TextMeshProUGUI>().text = $"{m_Health.GetHealthPoints():0}/{m_Health.GetMaxHealthPoints():0}";
        }
    }
}