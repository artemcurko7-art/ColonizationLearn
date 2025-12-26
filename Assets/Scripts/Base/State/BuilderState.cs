using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuilderState : MonoBehaviour
{
    [SerializeField] private BuilderBase _builderBase;

    private Unit _unit;

    public IEnumerator Action(List<Unit> units, Vector3 target)
    {
        yield return new WaitUntil(() => units.Count > 1 && IsFreeUnit(units));

        units.Remove(_unit);
        _unit.Initialize(target);

        _unit.BaseBuildRequested += _builderBase.Build;

        yield break;
    }

    private bool IsFreeUnit(List<Unit> units)
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i].IsBusy == false)
            {
                _unit = units[i];
                return true;
            }
        }

        return false;
    }
}