using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movements;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHand = null;
        [SerializeField] Transform leftHand = null;
        [SerializeField] Weapon defaultWeapon = null;

        Health target;
        Health health;
        Animator animator;
        Mover mover;
        Weapon currentWeapon = null;
        bool isInRange;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();

            EquipWeapon(defaultWeapon);
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
            isInRange = (target.transform.position - transform.position).sqrMagnitude <= currentWeapon.GetWeaponDamage * currentWeapon.GetWeaponRange;

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
            target.TakeDamage(currentWeapon.GetWeaponDamage);
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHand, leftHand, animator);
        }
    }
}
