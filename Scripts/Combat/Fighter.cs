using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System.Collections.Generic;
using RPG.Attributes;


namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target != null && target.IsDead()) target = null;
            if (target == null) return;

            if (!GetIsInRange())
            {
                GetComponent<PMove>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<PMove>().Cancel();
                AttackBehaviour();
            }
        }

        public Health GetTarget()
        {
            return target;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //trigger Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;
                Health healthComponent = target.GetComponent<Health>();
                float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
                healthComponent.TakeDamage(gameObject, damage);
            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation Event
        void Hit()
        {
            if (target == null)
                return;
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            target.TakeDamage(gameObject, damage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
                return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<PMove>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

    }
}