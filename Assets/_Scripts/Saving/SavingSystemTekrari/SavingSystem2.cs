using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem2 : MonoBehaviour
    {

        internal void Save(string saveFile)
        {
            print("Saving to " + saveFile);
        }
        internal void Load(string saveFile)
        {
            print("Loading from " + saveFile);
        }
    }
}
