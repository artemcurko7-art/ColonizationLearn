using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolResources : MonoBehaviour
{
    [SerializeField] private Resource _resourse;
    [SerializeField] private Transform _spawnPoint;

    private ObjectPool<Resource> _pool;
    private int _index;

    private void Awake()
    {
        _pool = new ObjectPool<Resource>(
            createFunc: () => Instantiate(_resourse),
            actionOnGet: (resource) => ActionOnGet(resource),
            actionOnRelease: (resource) => ActionOnRelease(resource));
    }

    public Resource GetResource() =>
        _pool.Get();

    private void ActionOnGet(Resource resource)
    {
        resource.gameObject.SetActive(true);
        resource.Initialize(GetConsistentPosition());
        resource.Taken += OnRelease;
    }

    private void ActionOnRelease(Resource resource)
    {
        resource.gameObject.SetActive(false);
        resource.ResetSettings();
    }

    private void OnRelease(Resource resource)
    {
        _pool.Release(resource);
        resource.Taken -= OnRelease;
    }

    private Vector3 GetConsistentPosition()
    {
        _index++;

        if (_index == _spawnPoint.childCount)
            _index = 0;

        var position = _spawnPoint.GetChild(_index).position;

        return position;
    }
}
