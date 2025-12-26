using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayResources : MonoBehaviour
{
    [SerializeField] private Warehouse _warehouse;
    [SerializeField] private TMP_Text _amountText;

    private void OnEnable() =>
        _warehouse.Changed += View;

    private void OnDisable() =>
        _warehouse.Changed -= View;

    private void View(List<Resource> resources) =>
        _amountText.text = resources.Count.ToString();
}
