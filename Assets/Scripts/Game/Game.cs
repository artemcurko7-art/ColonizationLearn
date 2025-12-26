using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private BaseFactory _baseFactory;
    [SerializeField] private UnitFactory _unitFactory;
    [SerializeField] private ServiceResources _serviceResources;
    [SerializeField] private BuilderState _builderState;
    [SerializeField] private Transform _basePoint;
    [SerializeField] private Transform _unitPoint;

    private Unit _unit;

    private void Awake()
    {
        Base newBase = _baseFactory.Create(_basePoint.position);
        newBase.Initialize(_serviceResources, _builderState);
        newBase.AddUnits(_unitFactory.Create(_unitPoint.position));
    }
}
