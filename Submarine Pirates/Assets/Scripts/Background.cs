using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Sprite background1;
    public Sprite background2;
    public Sprite background3;
    public Sprite background4;
    public Sprite background5;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void SetBackground(int num)
    {
        if (num == 1) spriteRenderer.sprite = background1;
        if (num == 2) spriteRenderer.sprite = background2;
        if (num == 3) spriteRenderer.sprite = background3;
        if (num == 4) spriteRenderer.sprite = background4;
        if (num == 5) spriteRenderer.sprite = background5;
    }
}
