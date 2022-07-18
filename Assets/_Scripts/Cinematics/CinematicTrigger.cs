using UnityEngine;
using UnityEngine.Playables;    //timeline icin gerekli olan library

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyTriggered && other.CompareTag("Player"))
            {
                GetComponent<PlayableDirector>().Play();
                alreadyTriggered = true;
            }
        }
    }
}
