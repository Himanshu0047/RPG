using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Combat
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 20f;

        bool isDead;
        Animator animator;
        Fighter fighter;

        public bool IsDead() => isDead;

        private void Start()
        {
            animator = GetComponent<Animator>();
            fighter = GetComponent<Fighter>();
        }

        public void TakeDamage(float damage)
        {
            if(isDead) { return; }

            healthPoints = Mathf.Max(healthPoints - damage, 0);

            // Die
            if(healthPoints == 0)
            {
                isDead = true;
                DeathBehaviour();
            }
        }

        void DeathBehaviour()
        {
            animator.SetTrigger("Die");
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints == 0)
            {
                isDead = true;
                DeathBehaviour();
            }
        }
    }
}

