using UnityEngine;

public class BaseFactory : Factory<Base>
{
    public void Initialize(ServiceResources serviceResources, BuilderState builderState, Vector3 position) =>
        Create(position).Initialize(serviceResources, builderState);
}