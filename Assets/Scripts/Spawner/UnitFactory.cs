using UnityEngine;

public class UnitFactory : Factory<Unit>
{
    public void Initialize(Vector3 position, Vector3 target)
    {
        Create(position).Initialize(target);
    }
}