using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite buriedGrave;
    private bool buried = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buried){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = buriedGrave;
        }
    }

    public void bury(){
        if(!buried){
            buried = true;
        }
    }
}
