using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Raycaster _raycaster;

    private void OnEnable()
    {
        _inputReader.Clicked += _raycaster.Press;
    }

    private void OnDisable()
    {
        _inputReader.Clicked -= _raycaster.Press;
    }
}
