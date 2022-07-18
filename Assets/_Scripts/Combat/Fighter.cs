using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1;
        [SerializeField] float weaponDamage = 5f;

        float timePastSinceLastAttack = Mathf.Infinity;
        Health target;

        private void Update()
        {
            timePastSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position,1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        //We will add damage may be particle system that's why we extract method like this.
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timePastSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttackAnimation();
                timePastSinceLastAttack = 0;
                //this will trigger hit event
            }
        }

        private void TriggerAttackAnimation()
        {
            GetComponent<Animator>().SetTrigger("attack");
            GetComponent<Animator>().ResetTrigger("stopAttack");
        }

        internal void Attack(GameObject combatTarget)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        //This is Animation Event
        void Hit()
        {
            if (target == null) { return; }
            target.TakeDamage(weaponDamage);
        }

        public void Cancel()
        {
            StopAttackAnimation();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttackAnimation()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        private bool GetIsInRange()
        {
            //bool isInRange = Vector3.Distance(transform.position, target.position) < weaponRange ? true : false;
            return (Vector3.Distance(transform.position, target.transform.position) < weaponRange);
        }
    }
}