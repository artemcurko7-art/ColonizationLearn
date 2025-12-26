using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _delay;

    private Collider[] _colliders = new Collider[32];

    public event Action<List<Resource>> Changed;

    private void Start()
    {
        StartCoroutine(Scan());
    }

    private IEnumerator Scan()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;

            Changed?.Invoke(GetFoundResources());

            yield return null;
        }
    }

    private List<Resource> GetFoundResources()
    {
        List<Resource> resources = new List<Resource>();

        int size = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders);

        for (int i = 0; i < size; i++)
        {
            if (_colliders[i].TryGetComponent<Resource>(out Resource resource))
            {
                resources.Add(resource);
            }
        }

        return resources;
    }
}