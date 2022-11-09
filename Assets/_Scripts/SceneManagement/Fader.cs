using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup m_Fader;
        private Coroutine m_CurrentActiveFade = null;

        private void Awake()
        {
            m_Fader = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            m_Fader.alpha = 1;
        }

        public IEnumerator FadeOut(float time)
        {
            if (m_CurrentActiveFade != null)
            {
                StopCoroutine(m_CurrentActiveFade);
            }

            m_CurrentActiveFade = StartCoroutine(FadeOutRoutine(time));
            yield return m_CurrentActiveFade;
        }

        public IEnumerator FadeIn(float time)
        {
            if (m_CurrentActiveFade != null)
            {
                StopCoroutine(m_CurrentActiveFade);
            }

            m_CurrentActiveFade = StartCoroutine(FadeInRoutine(time));
            yield return m_CurrentActiveFade;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (m_Fader.alpha > 0)
            {
                m_Fader.alpha -= Time.deltaTime / time; //sahne gecisi arasinin alpha degeri
                yield return null;
            }
        }
        
        // private IEnumerator FadeOutRoutine(float time)
        // {
        //     while (m_Fader.alpha < 1)
        //     {
        //         m_Fader.alpha += Time.deltaTime / time; //sahne gecisi arasinin alpha degeri
        //         yield return null;
        //     }
        // }
    }
}