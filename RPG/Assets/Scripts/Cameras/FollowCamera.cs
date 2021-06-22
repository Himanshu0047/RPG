using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Cameras
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target = null;
        [SerializeField] float cameraMoveSpeed = 5f;

        private void LateUpdate()
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position, cameraMoveSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
