using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movements;
using RPG.Combat;

namespace RPG.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;
        Health health;

        private void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if(health.IsDead()) 
            {
                fighter.Stop();
                return; 
            }
            if(TryAttack()) { return; }
            if(TryMove()) { return; }
            //Debug.Log("Maybe insert new function that you cannot go there");
        }

        bool TryMove()
        {
            // Keep checking if ray hits a location to move
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            // Move if clicked
            if (hasHit)
            {
                if(Input.GetMouseButton(0))
                {
                    // Cancel any ongoing attack
                    fighter.Stop();

                    mover.MoveTo(hit.point);
                }
                return true;
            }
            return false;
        }

        bool TryAttack()
        {
            // Keep checking if ray hits an enemy
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) { continue; }

                if(!fighter.CanAttack(target.gameObject)) { continue; }

                // Attack if enemy
                if(Input.GetMouseButton(0))
                {
                    fighter.SetTarget(target.gameObject);
                }
                return true;
            }
            return false;
        }

        Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
