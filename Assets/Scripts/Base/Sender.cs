using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sender : MonoBehaviour
{
    public IEnumerator Send(ServiceResources serviceResources, List<Unit> units, float delaySendUnits)
    {
        List<Resource> foundResources = new List<Resource>();

        var wait = new WaitForSeconds(delaySendUnits);

        while (enabled)
        {
            yield return wait;

            for (int i = 0; i < units.Count; i++)
            {
                var resource = serviceResources.GetFree();

                if (units[i].IsBusy == false && resource != null)
                {
                    units[i].Initialize(resource, transform.position, resource.transform.position);
                    serviceResources.AddBusy(resource);
                }
            }

            yield return null;
        }
    }
}