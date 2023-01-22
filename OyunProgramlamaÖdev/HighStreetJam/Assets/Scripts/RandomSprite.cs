using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;
    public Sprite sprite7;
    public Sprite sprite8;

    int key = 0;

    // Start is called before the first frame update
    void Start()
    {
        key = Random.Range(0, 9);

        switch (key)
        {
            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite3;
                break;
            case 4:
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite4;
                break;
            case 5:
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite5;
                break;
            case 6:
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite6;
                break;
            case 7:
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite7;
                break;
            case 8:
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite8;
                break;
            default:
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
                break;

        }
    }

}
