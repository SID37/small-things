using System.Collections;
using System.Collections.Generic;
using System.Net.Cache;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float reloadInterval = 0.5f;
    public int hp = 10;

    public Transform gun;
    public Transform gunFirePoint;
    public GameObject bullet;
    public GameObject explosion;

    public Camera mainCamera;

    private Rigidbody2D body;
    private float reloadTime = 0;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 65536);
        gun.LookAt(mousePos, new Vector3(0, 0, 1));
        if (Input.GetButtonDown("Fire1") && reloadTime <= 0)
        {
            Fire(mousePos - gun.position);
            reloadTime = reloadInterval;
        }
        reloadTime -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
    }

    void Fire(Vector3 delta)
    {
        float maxDistance = 10;
        var vector = new Vector2(delta.x, delta.y).normalized;
        RaycastHit2D hit = Physics2D.Raycast(gunFirePoint.position, new Vector2(delta.x, delta.y), maxDistance);
        float hit_distance = hit.distance > 0 ? hit.distance : maxDistance;
        var newBullet = Instantiate(bullet);

        newBullet.transform.position = gunFirePoint.position;
        newBullet.transform.LookAt(newBullet.transform.position + delta, new Vector3(0, 0, 1));
        newBullet.transform.localScale = new Vector3(1, hit_distance, 1);

        var target = vector * hit_distance;

        Instantiate(explosion).transform.position = gunFirePoint.position + new Vector3(target.x, target.y, 0);

        hit.collider?.GetComponent<ThingController>()?.Kill();
    }

    public void Hit()
    {
        hp -= 1;
        if (hp <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
