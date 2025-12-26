using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Transform _takePoint;
    [SerializeField] private float _speed;

    private Resource _resource;
    private Vector3 _target;
    private Vector3 _positionBase;
    private bool _isReached;
    private bool _canTakeResource;

    public event Action<Unit, Vector3> BaseBuildRequested;

    public bool IsBusy { get; private set; }

    private void Update()
    {
        if (IsBusy && _isReached == false)
            _mover.Move(_target, _speed);
        else if (_isReached)
            _mover.Move(_positionBase, _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Resource>(out Resource resource))
        {
            if (_canTakeResource == false)
            {
                if (_resource == null)
                    return;

                _resource.transform.position = _takePoint.transform.position;
                _resource.GetComponent<Collider>().enabled = false;
                _resource.transform.parent = _takePoint.transform.parent;
                _isReached = true;
                _canTakeResource = true;
            }
        }
        else if (other.transform.TryGetComponent<Flag>(out Flag flag))
        {
            BaseBuildRequested?.Invoke(this, flag.transform.position);
            flag.Touch();
            MarkFree();
        }
    }

    public void Initialize(Resource resource, Vector3 positionBase, Vector3 target)
    {
        _resource = resource;
        _positionBase = positionBase;
        _target = target;
        IsBusy = true;
    }

    public void Initialize(Vector3 target)
    {
        _positionBase = target;
        _target = target;
        IsBusy = true;
    }

    private void MarkFree()
    {
        IsBusy = false;
        _isReached = false;
        _canTakeResource = false;
    }

    public Resource UnloadResource()
    {
        MarkFree();

        var resource = _resource;
        _resource = null;

        return resource;
    }
}