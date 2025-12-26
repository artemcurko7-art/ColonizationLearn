using Unity.Burst.CompilerServices;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Flag _flag;

    private Base _base;

    private void OnEnable()
    {
        _flag.Touching += Touch;
    }

    private void OnDisable()
    {
        _flag.Touching -= Touch;
    }

    public void Press()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.transform.TryGetComponent<Base>(out Base currentBase))
            {
                if (_base == null)
                    _base = currentBase;

                if (_base != currentBase)
                {
                    _base.DisableStateBuild();
                    _base = currentBase;
                }
            }
            else if (_base != null)
            {
                ActivateFlag(_flag, hit.point);
                _base.Initialize(_flag.transform.position);
            }
        }
    }

    public void Touch() =>
        _flag.transform.gameObject.SetActive(false);
    
    private void ActivateFlag(Flag flag, Vector3 position)
    {
        flag.transform.gameObject.SetActive(true);
        flag.transform.position = position;
        flag.transform.rotation = Quaternion.identity;
    }
}
