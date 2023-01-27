﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundImageToSprite : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
    }
}
