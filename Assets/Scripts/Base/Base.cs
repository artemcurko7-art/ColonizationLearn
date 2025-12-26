using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Base : MonoBehaviour
{
    [SerializeField] private ServiceResources _serviceResources;
    [SerializeField] private BuilderState _builderState;
    [SerializeField] private Warehouse _warehouse;
    [SerializeField] private Sender _sender;
    [SerializeField] private UnitFactory _unitFactory;
    [SerializeField] private Transform _unitPoint;
    [SerializeField] private float _delaySendUnits;
    [SerializeField] private int _amountResorces;
    [SerializeField] private int _amountBuildResources;

    private List<Unit> _units = new List<Unit>();
    private Unit _unit;

    private bool _isStateBuild;

    private void Start()
    {
        StartCoroutine(_sender.Send(_serviceResources, _units, _delaySendUnits));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Unit>(out Unit unit))
        {
            if (_units.Contains(unit) == false)
            {
                _units.Add(unit);
                return;
            }

            TakeResource(unit.UnloadResource());

            if (CanCreateUnit() && (_units.Count == 1 || _isStateBuild == false))
                CreateUnit();
        }
    }

    public void Initialize(ServiceResources serviceResources, BuilderState builderState)
    {
        _serviceResources = serviceResources;
        _builderState = builderState;
    }

    public void Initialize(Vector3 position)
    {
        StartCoroutine(HoldToBuilderState(position));
        _isStateBuild = true;
    }

    public void AddUnits(Unit unit) =>
        _units.Add(unit);

    public void DisableStateBuild() =>
        _isStateBuild = false;

    private void TakeResource(Resource resource)
    {
        if (resource == null)
            return;

        resource.transform.parent = transform.parent;
        _serviceResources.RemoveBusy(resource);
        _warehouse.Add(resource);
        resource.Take();
    }

    private void CreateUnit()
    {
        _unit = _unitFactory.Create(_unitPoint.position);

        _units.Add(_unit);
        _warehouse.Removes(_amountResorces);
    }

    private bool CanCreateUnit() =>
        _warehouse.Resources.Count >= _amountResorces;

    private IEnumerator HoldToBuilderState(Vector3 position)
    {
        yield return new WaitUntil(() => _warehouse.Resources.Count >= _amountBuildResources);

        _warehouse.Removes(_amountBuildResources);
        StartCoroutine(_builderState.Action(_units, position));
        _isStateBuild = false;
    }
}