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

        public bool IsDead() => isDead;

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
            GetComponent<Animator>().SetTrigger("Die");
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

