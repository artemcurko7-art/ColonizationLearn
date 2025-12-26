using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode LeftMouseButton = KeyCode.Mouse0;

    public event Action Clicked;

    private void Update()
    {
        if (Input.GetKeyDown(LeftMouseButton))
            Clicked?.Invoke();
    }
}
