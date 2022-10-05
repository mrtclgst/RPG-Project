using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using TMPro;
using UnityEngine;

public class ExperienceDisplay : MonoBehaviour
{
    private Experience m_Experience;

    private void Awake()
    {
        m_Experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
    }

    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = $"{m_Experience.GetPoints():0}";
    }
}