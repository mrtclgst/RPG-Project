using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup fader;

        private void Awake()
        {
            fader = GetComponent<CanvasGroup>();
        }
        public void FadeOutImmediate()
        {
            fader.alpha = 1;
        }

        public IEnumerator FadeOut(float time)
        {
            while (fader.alpha < 1)
            {
                fader.alpha += Time.deltaTime / time; //sahne gecisi arasinin alpha degeri
                yield return null;
            }
        }
        public IEnumerator FadeIn(float time)
        {
            while (fader.alpha > 0)
            {
                fader.alpha -= Time.deltaTime / time; //sahne gecisi arasinin alpha degeri
                yield return null;
            }
        }
    }
}
