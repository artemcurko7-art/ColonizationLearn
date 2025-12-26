using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Resource : MonoBehaviour
{
    private Collider _collider;

    public event Action<Resource> Taken;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void Initialize(Vector3 position)
    {
        transform.position = position;
    }

    public void ResetSettings()
    {
        _collider.enabled = true;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void Take() =>
        Taken?.Invoke(this);
}
