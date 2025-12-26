using UnityEngine;

public abstract class Factory <T>: MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    public T Create(Vector3 position) =>
        Instantiate(_prefab, position, Quaternion.identity);
}
