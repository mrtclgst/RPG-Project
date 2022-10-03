using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 10f;
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float m_MaxLifeTime = 10f;
        [SerializeField] float m_LifeAfterImpact = 2f;
        [SerializeField] GameObject[] m_DestroyOnHit = null;
        Health target = null;
        private GameObject instigator = null;
        float damage;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        private void Update()
        {
            if (target == null) return;

            if (isHoming && !target.IsDead())
                transform.LookAt(GetAimLocation());

            transform.Translate(Time.deltaTime * projectileSpeed * Vector3.forward);
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;
            Destroy(gameObject, m_MaxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();
            if (targetCollider == null)
            {
                return target.transform.position + Vector3.up;
            }

            return target.transform.position + Vector3.up * targetCollider.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            {
                Health enemyHP = other.GetComponent<Health>();
                if (enemyHP.IsDead())
                    return;
                enemyHP.TakeDamage(instigator, damage);

                projectileSpeed = 0;

                if (hitEffect != null)
                {
                    Instantiate(hitEffect, GetAimLocation(), transform.rotation);
                }

                foreach (GameObject item in m_DestroyOnHit)
                {
                    Destroy(item, m_LifeAfterImpact);
                }

                Destroy(this.gameObject);
            }
        }
    }
}