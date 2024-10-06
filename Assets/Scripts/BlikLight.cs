using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlikLight : MonoBehaviour
{
    public Color color1 = Color.white;
    public Color color2 = Color.gray;

    public float range1 = 1;
    public float range2 = 1;

    SpriteRenderer sprite;
    float timer = 0;
    bool state1 = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            state1 = !state1;
            sprite.color = state1 ? color1 : color2;
            timer = Random.Range(0, state1 ? range1 : range2);
        }
    }
}
