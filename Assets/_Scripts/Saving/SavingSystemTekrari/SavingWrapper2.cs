using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingWrapper2 : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<SavingSystem2>().Save(defaultSaveFile);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                GetComponent<SavingSystem2>().Load(defaultSaveFile);
            }
        }
    }
}
