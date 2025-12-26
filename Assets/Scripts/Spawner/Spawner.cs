using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Base _base;

    private void Start()
    {
        Instantiate(_base, new Vector3(10, 0.5f, 0), Quaternion.identity);
        Instantiate(_base, new Vector3(0, 0.5f, 0), Quaternion.identity);
    }
}
