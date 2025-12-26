using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public event Action Touching;

    public void Touch() =>
        Touching?.Invoke();
}
