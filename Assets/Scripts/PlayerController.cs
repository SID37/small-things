using System.Collections;
using System.Collections.Generic;
using System.Net.Cache;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float reloadInterval = 0.5f;
    public int hp = 10;
    public float spread = 15.0f;
    public int bullets = 3;

    public Transform gun;
    public Transform gunFirePoint;
    public GameObject bullet;
    public GameObject explosion;
    public Transform scaledSprite;

    public Camera mainCamera;
    public Image deadScreen;
    public Image hintText;
    public Animator animator;

    private Rigidbody2D body;
    private float reloadTime = 0;
    private float deadAlpha = 0;
    private Vector3 scaledSpriteScale;
    private Vector3 scaledGunScale;


    void Start()
    {
        Hint(false);
        body = GetComponent<Rigidbody2D>();
        scaledSpriteScale = scaledSprite.localScale;
        scaledGunScale = gun.localScale;
    }

    void Update()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 65536);
        gun.LookAt(mousePos, new Vector3(0, 0, 1));
        gun.localScale = mousePos.x < gun.position.x ? scaledGunScale : new Vector3(-scaledGunScale.x, scaledGunScale.y, scaledGunScale.z);
        if (Input.GetButtonDown("Fire1") && reloadTime <= 0)
        {
            var fireDelta = mousePos - gun.position;
            for (int i = 0; i < bullets; ++i)
            {
                float angle = Random.Range(-1.0f, 1.0f) * spread;
                Fire(Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)) * fireDelta);
            }
            gun.position -= new Vector3(fireDelta.normalized.x, fireDelta.normalized.y, 0).normalized * 0.5f;
            reloadTime = reloadInterval;
        }

        deadScreen.color = new Color(0, 0, 0, deadAlpha);

        reloadTime -= Time.deltaTime;
        deadAlpha -= Time.deltaTime;
        gun.position += (transform.position - gun.position) * Time.deltaTime * 5;
    }

    void FixedUpdate()
    {
        var horisontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horisontal) > 0.1)
        {
            scaledSprite.localScale = horisontal < 0 ? scaledSpriteScale : new Vector3(-scaledSpriteScale.x, scaledSpriteScale.y, scaledSpriteScale.z);
            animator.SetBool("walk", true);
        }
        else
            animator.SetBool("walk", false);
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
    }

    void Fire(Vector3 delta)
    {
        float maxDistance = 5;
        var vector = new Vector2(delta.x, delta.y).normalized;
        Physics2D.queriesHitTriggers = false;
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

    public void Hit(Vector2 point)
    {
        deadAlpha = 1;
        deadScreen.color = new Color(0, 0, 0, deadAlpha);

        Vector2 hitCamera = 0.5f * (
            ((Vector2)(new Vector3(point.x, point.y, 0) - transform.position)).normalized +
            new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f))
        ).normalized;
        mainCamera.transform.position -= new Vector3(hitCamera.x, hitCamera.y, 0);

        hp -= 1;
        if (hp <= 0)
        {
            StartCoroutine(RestartLevel(3));
            body.velocity = new Vector2(0, 0);
            animator.speed = 0;
            enabled = false;
        }
    }
    
    public IEnumerator RestartLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Hint(bool status)
    {
        if (status)
            hintText.color = Color.white;
        else
            hintText.color = new Color(0, 0, 0, 0);
    }
}
