using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 1.0f;
    public Transform target;

    void Update()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 target_pos = new Vector2(target.position.x, target.position.y);
        Vector2 step = (target_pos - pos) * Time.deltaTime * speed;
        transform.position += new Vector3(step.x, step.y, 0);
    }
}
