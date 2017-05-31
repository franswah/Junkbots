﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableIsa : Damageable {

    public SpriteRenderer LEyeSprite;
    public SpriteRenderer REyeSprite;

    public override void OnHealthDecreased(int amount)
    {
        if (health < maxHealth / 2)
        {
            LEyeSprite.color = Color.yellow;
            REyeSprite.color = Color.yellow;
        }
        if (health < maxHealth / 4)
        {
            LEyeSprite.color = Color.red;
            REyeSprite.color = Color.red;
        }
    }

    public override void OnHealthZero()
    {
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}