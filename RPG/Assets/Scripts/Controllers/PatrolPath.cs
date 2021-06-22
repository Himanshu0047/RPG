using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controllers
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float waypointGizmoRadius = 0.5f;

        private void OnDrawGizmos()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);

                Gizmos.DrawSphere(GetWaypointPosition(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(j));
            }
        }

        public int GetNextIndex(int i)
        {
            if(i == transform.childCount - 1)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWaypointPosition(int i)
        {
            return transform.GetChild(i).position;
        }


    }
}
