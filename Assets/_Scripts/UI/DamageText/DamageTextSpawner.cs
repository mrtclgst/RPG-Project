using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private DamageText m_DamageTextPrefab = null;

        public void Spawn(float damageAmount)
        {
            DamageText instance = Instantiate(m_DamageTextPrefab, transform);
            instance.SetValue(damageAmount);
        }
    }
}