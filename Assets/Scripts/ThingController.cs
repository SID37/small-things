using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ThingController : MonoBehaviour
{
    public float speed = 1.0f;
    public float look_radius = 10;
    public PlayerController player;
    public Animator animator;

    public Transform scaled;

    Rigidbody2D body;
    Vector3 deltaPos;

    class CollisionPoint
    {
        public Vector3 point;
        public float radius;
    }

    List<CollisionPoint> collisionPoints = new List<CollisionPoint>();

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        UpdateDeltaPos();
    }

    void FixedUpdate()
    {
        if (!player.enabled) {
            body.velocity = new Vector2(0, 0);
            animator.speed = 0;
            return;
        }

        Vector3 velocity = new Vector3(0, 0, 0);

        Vector3 targetVector = deltaPos + player.transform.position - transform.position;
        if (targetVector.magnitude < look_radius ) {
            velocity += targetVector.normalized;
        }

        for (int i = 0; i < collisionPoints.Count; ++i)
        {
            if (collisionPoints[i].radius <= 0) continue;
            velocity += (transform.position - collisionPoints[i].point).normalized * collisionPoints[i].radius;
            collisionPoints[i].radius -= Time.deltaTime;
        }


        scaled.localScale = velocity.x < 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

        body.velocity = velocity.normalized * speed;
        transform.LookAt(transform.position + new Vector3(body.velocity.x, body.velocity.y, 65536), new Vector3(0, 0, 1));
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    void UpdateDeltaPos()
    {
        deltaPos = new Vector3(Random.Range(-1.0f, 1.0f) * 0.5f, Random.Range(-1.0f, 1.0f) * 1, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D contact in collision.contacts) {
            collisionPoints.Add(new CollisionPoint() {
                point = contact.point,
                radius = 2.0f
            });
            UpdateDeltaPos();

            contact.collider.GetComponent<PlayerController>()?.Hit(contact.point);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        collisionPoints.Add(new CollisionPoint() {
            point = collision.transform.position,
            radius = 2.0f
        });
        UpdateDeltaPos();
    }
}
