using System.Collections;
using UnityEngine;

public class SpawnerResources : MonoBehaviour
{
    [SerializeField] private PoolResources _poolResources;
    [SerializeField] private float _delay;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;

            _poolResources.GetResource();

            yield return null;
        }
    }
}
