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
        [SerializeField] Projectile projectile = null;

        public bool HasProjectile => projectile != null;
        public float GetWeaponRange => weaponRange;
        public float GetWeaponDamage => weaponDamage;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if(weaponPrefab != null)
            {
                Transform handTransform = ChooseHand(rightHand, leftHand);
                Instantiate(weaponPrefab, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, ChooseHand(rightHand, leftHand).position, projectile.transform.rotation);
            projectileInstance.SetTarget(target, weaponDamage);
        }

        private Transform ChooseHand(Transform rightHand, Transform leftHand)
        {
            Transform handTransform = null;
            if (isRightHanded) { handTransform = rightHand; }
            else { handTransform = leftHand; }

            return handTransform;
        }
    }
}