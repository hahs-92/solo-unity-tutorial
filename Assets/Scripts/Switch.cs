using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject objectToSwitch;

    private SpriteRenderer sr;
    public Sprite downSprite;

    private bool hasSwitched;
    // lo seteamos true, si queremos que cuando el palyer
    // toque el swith aparesca el objeto(door), o false,
    // si queremos que desaparesca
    public bool deactiveOnSwitch;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !hasSwitched)
        {
            if(deactiveOnSwitch)
            {
                objectToSwitch.SetActive(false);
            } else
            {
                objectToSwitch.SetActive(true);
            }
            sr.sprite = downSprite;
            hasSwitched= true;
        }

    }
}
