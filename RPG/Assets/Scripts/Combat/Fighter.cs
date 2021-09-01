using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movements;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon = null;
   

        Health target;
        Health health;
        Animator animator;
        Mover mover;
        bool isInRange;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();

            SpawnWeapon();
        }

        private void Update()
        {
            if (health.IsDead()) { return; }

            TryAttack();
        }

        public void SetTarget(GameObject target)
        {
            this.target = target.GetComponent<Health>();
        }

        void TryAttack()
        {
            timeSinceLastAttack += Time.deltaTime;
            if(target == null) { return; }
            if(target.IsDead()) { return; }

            // Faster than Vector3.Distance()
            isInRange = (target.transform.position - transform.position).sqrMagnitude <= weaponRange * weaponRange;

            if (isInRange)
            {
                // Attack
                mover.Stop();
                if(timeSinceLastAttack >= timeBetweenAttacks)
                {
                    AttackBehaviour();
                    timeSinceLastAttack = 0f;
                }
            }
            else
            {
                mover.MoveTo(target.transform.position);
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(!combatTarget.GetComponent<Health>().IsDead())
            {
                return true;
            }
            return false;
        }

        void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            animator.ResetTrigger("StopAttack");

            // This will trigger Hit() event
            animator.SetTrigger("Attack");
        }

        public void Stop()
        {
            target = null;
            mover.Stop();

            animator.ResetTrigger("Attack");
            animator.SetTrigger("StopAttack");
        }

        // Animation Event
        void Hit()
        {
            if(target == null) { return; }
            target.TakeDamage(weaponDamage);
        }

        void SpawnWeapon()
        {
            if(weapon == null) { return; }

            Animator animator = GetComponent<Animator>();
            weapon.Spawn(handTransform, animator);
        }
    }
}
