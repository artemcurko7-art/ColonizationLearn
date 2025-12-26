using UnityEngine;

public class BuilderBase : MonoBehaviour
{
    [SerializeField] private BaseFactory _baseFactory;
    [SerializeField] private ServiceResources _serviceResources;
    [SerializeField] private BuilderState _builderState;

    public void Build(Unit unit, Vector3 position)
    {
        _baseFactory.Initialize(_serviceResources, _builderState, position);
        unit.BaseBuildRequested -= Build;
    }
}