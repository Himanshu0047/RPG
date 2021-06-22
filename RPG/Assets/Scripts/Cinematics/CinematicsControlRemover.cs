using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Controllers;
using RPG.Combat;

namespace RPG.Cinematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {
        [SerializeField] GameObject player;

        // Subscribe to the events
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        // Unsubscribe from events
        private void OnDestroy()
        {
            GetComponent<PlayableDirector>().played -= DisableControl;
            GetComponent<PlayableDirector>().stopped -= EnableControl;
        }

        void DisableControl(PlayableDirector director)
        {
            player.GetComponent<Fighter>().Stop();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector director)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
