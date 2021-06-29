﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}