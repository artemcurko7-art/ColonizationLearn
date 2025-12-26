using System;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    private List<Resource> _resources = new List<Resource>();

    public event Action<List<Resource>> Changed;

    public IReadOnlyList<Resource> Resources => _resources;

    public void Add(Resource resource)
    {
        _resources.Add(resource);
        Changed?.Invoke(_resources);
    }

    public void Removes(int amountResources)
    {
        for (int i = amountResources - 1; i >= 0; i--)
            _resources.RemoveAt(i);

        Changed?.Invoke(_resources);
    }
}
