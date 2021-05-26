using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] List<Collectible> gatherables;
    [SerializeField] UnityEvent OnCompletemEvent;
    List<Collectible> collectiblesRemaining;

    private void OnEnable()
    {
        collectiblesRemaining = new List<Collectible>(gatherables);

        foreach (var collectible in collectiblesRemaining)
        {
            collectible.OnPickup += HandlePickup;
        }

        UpdateText();
    }
    void HandlePickup(Collectible collectible)
    {
        collectiblesRemaining.Remove(collectible);
        UpdateText();

        if (collectiblesRemaining.Count == 0)
        {
            OnCompletemEvent.Invoke();
        }
    }

    void UpdateText()
    {
        text.SetText($"{collectiblesRemaining.Count} more..");
        if (collectiblesRemaining.Count == 0 || collectiblesRemaining.Count == gatherables.Count)
        {
            text.enabled = false;
        }
        else
        {
            text.enabled = true;
        }
    }

    [ContextMenu("AutoFill Collectibles")]
    void AutoFillCollectibles()
    {
        gatherables = FindObjectsOfType<Collectible>()
            .Where(t => t.name.ToLower().Contains("coin"))
            .ToList();
    }
}
