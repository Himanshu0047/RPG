using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] bool isRightHanded = true;

        public float GetWeaponRange => weaponRange;
        public float GetWeaponDamage => weaponDamage;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if(weaponPrefab != null)
            {
                Transform handTransform = null;
                if(isRightHanded) { handTransform = rightHand; }
                else { handTransform = leftHand; }
                Instantiate(weaponPrefab, handTransform);
            }
            if(animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
    }
}