using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public PlayerController player;
    public BaseActivator winActivator;

    public float winStart = 0;
    public float winEnd = 1;
    public float thanksStart = 5;
    public float thanksEnd = 6;

    public Image winScreen;
    public Image thanksScreen;

    float timer = 0;

    void Start()
    {
    }

    void Update()
    {
        if (!winActivator.activated) return;
        if (player.enabled)
        {
            player.StopGame();
        }
        timer += Time.deltaTime;
        if (timer > winStart && timer <= winEnd)
            winScreen.color = new Color(1, 1, 1, (timer - winStart) / (winEnd - winStart));
        if (timer > thanksStart && timer <= thanksEnd)
            thanksScreen.color = new Color(1, 1, 1, (timer - thanksStart) / (thanksEnd - thanksStart));
    }
}
