using System;
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

        public Coroutine FadeOut(float time)
        {
            return Fade(1, time);
        }

        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);
        }

        public Coroutine Fade(float target, float time)
        {
            if (m_CurrentActiveFade != null)
            {
                StopCoroutine(m_CurrentActiveFade);
            }

            m_CurrentActiveFade = StartCoroutine(FadeRoutine(target, time));
            return m_CurrentActiveFade;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(m_Fader.alpha, target))
            {
                m_Fader.alpha =
                    Mathf.MoveTowards(m_Fader.alpha, target,
                        Time.deltaTime / time); //sahne gecisi arasinin alpha degeri
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