using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GraundSettings
{
    public GameObject graundBase;
    public List<Sprite> graundSprits;
    public int size;
    public SpriteRenderer[] theGraund;
}

[System.Serializable]
public class PerlinNoiseSettings
{
    public float seed, scale, resolution;

    public float CalculePerlin(float x)
    {
        float xCoord = seed + x / resolution * scale;
        float yCoord = seed / resolution * scale;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}

public class GraundController : MonoBehaviour
{
    public static List<GraundController> controllers;
    public GraundSettings graundSettings;
    public PerlinNoiseSettings noiseSettings;
    public float minPerlinCont;
    public AnimationCurve curve;
    // Start is called before the first frame update
    void Start()
    {
        if (controllers == null)
            controllers = new List<GraundController>();
        controllers.Add(this);
        graundSettings.theGraund = new SpriteRenderer[graundSettings.size];
        for(int i = 0; i < graundSettings.size; i++)
        {
            GameObject obj = Instantiate(graundSettings.graundBase, transform);
            obj.transform.localPosition = new Vector3(i, 0);
            graundSettings.theGraund[i] = obj.GetComponent<SpriteRenderer>();
        }
    }

    public void Move(int posX)
    {
        Keyframe[] keys = new Keyframe[graundSettings.size];
        for (int i = 0; i < graundSettings.size; i++)
        {
            float noise = noiseSettings.CalculePerlin(posX + i);
            int index = 0;
            if (noise > minPerlinCont)
            {
                index = Mathf.FloorToInt(noise * graundSettings.graundSprits.Count);
            }

            keys[i] = new Keyframe(i, index);

            graundSettings.theGraund[i].sprite = graundSettings.graundSprits[index % graundSettings.graundSprits.Count];
        }
        curve = new AnimationCurve(keys);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
