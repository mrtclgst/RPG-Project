using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] private GameObject m_TargetToDestroy = null;

        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (m_TargetToDestroy != null)
                {
                    Destroy(m_TargetToDestroy);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}