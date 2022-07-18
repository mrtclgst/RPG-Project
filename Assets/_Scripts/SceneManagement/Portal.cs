using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 2f, fadeInTime = 1f, fadeWaitTime = 0.5f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }
        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;    //return null ile ayni anlama geliyor.
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);

            //Save Old Level
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            //Load current level
            wrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayerPosition(otherPortal);

            //transitiondan sonraki konumu kaydetmek icin yazdik tekrardan
            wrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayerPosition(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != this.destination) continue;

                return portal;
            }
            return null;
        }
    }
}