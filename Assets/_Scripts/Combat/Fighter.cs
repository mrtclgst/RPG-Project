using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;
using RPG.Stats;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] float timeBetweenAttacks = 1;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        float timePastSinceLastAttack = Mathf.Infinity;
        Health target;
        Weapon currentWeapon = null;

        private void Start()
        {
            if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
        }

        private void Update()
        {
            timePastSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
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

        public Health GetTarget()
        {
            return target;
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
            if (combatTarget == null)
            {
                return false;
            }

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        //This is Animation Event
        void Hit()
        {
            if (target == null)
            {
                return;
            }

            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);

            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
            else
            {
                target.TakeDamage(gameObject, damage);
            }
        }

        //This is Animation Event
        void Shoot()
        {
            Hit();
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

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.GetDamage();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.GetPercentageBonus();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            //null check yapmamiza gerek kalmadi c�nk� eline biz silah verecegiz artik
            //if (weapon == null) return;

            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.SpawnWeapon(rightHandTransform, leftHandTransform, animator);
        }

        private bool GetIsInRange()
        {
            //bool isInRange = Vector3.Distance(transform.position, target.position) < weaponRange ? true : false;
            return (Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange());
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}