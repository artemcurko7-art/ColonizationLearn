using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public void Move(Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.LookAt(target);
    }
}
