using RPG.Stats;
using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    private BaseStats m_BaseStats;

    private void Awake()
    {
        m_BaseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
    }

    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = $"{m_BaseStats.GetLevel():0}";
    }
}