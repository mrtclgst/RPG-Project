using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health m_Health = null;
        [SerializeField] private RectTransform m_Foreground = null;
        [SerializeField] private Canvas m_RootCanvas = null;

        void Update()
        {
            if (Mathf.Approximately(m_Health.GetFraction(), 1) || Mathf.Approximately(m_Health.GetFraction(), 0))
            {
                m_RootCanvas.enabled = false;
                return;
            }

            m_RootCanvas.enabled = true;
            m_Foreground.localScale = new Vector3(m_Health.GetFraction(), 1, 1);
        }
    }
}