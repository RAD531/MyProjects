using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteRendererCustom : MonoBehaviour
{
    [SerializeField]
    private Sprite[] animatedImages;
    [SerializeField]
    private Image animatedImageObj;
    [SerializeField]
    private int updateInterval;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animatedImageObj.sprite = animatedImages[(int)(Time.time * updateInterval) % animatedImages.Length];
    }
}
