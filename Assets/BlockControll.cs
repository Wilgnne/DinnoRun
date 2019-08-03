using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class BlockControll : MonoBehaviour
{
    BoxCollider2D box;
    SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer.sprite == null)
            box.enabled = false;
        else
            box.enabled = true;
    }
}
