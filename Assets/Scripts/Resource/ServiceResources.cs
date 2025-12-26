using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ServiceResources : MonoBehaviour
{
    [SerializeField] private Scaner _scanning;

    private List<Resource> _resources = new List<Resource>();
    private List<Resource> _busyResources = new List<Resource>();

    public IReadOnlyList<Resource> Resources => _resources.ToList().AsReadOnly();
    public IReadOnlyList<Resource> BusyResources => _busyResources.ToList().AsReadOnly();

    private void OnEnable() =>
        _scanning.Changed += ChangeAmount;

    private void OnDisable() =>
        _scanning.Changed -= ChangeAmount;

    public void AddBusy(Resource resource) =>
        _busyResources.Add(resource);

    public void RemoveBusy(Resource resource) =>
        _busyResources.Remove(resource);

    public Resource GetFree() =>
        _resources.FirstOrDefault(resource => _busyResources.Contains(resource) == false);

    private void ChangeAmount(List<Resource> resources) =>
        _resources = resources;
}