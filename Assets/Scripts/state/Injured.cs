using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injured : State
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = Color.yellow;
    }
}
