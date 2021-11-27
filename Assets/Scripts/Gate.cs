using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Gate : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite openedGate;
    // Add all connected graves to graves so that when we bury bones in the grave,
    // the grave is removed from the list and when the list is empty, open the gate
    public ArrayList graves = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        // FindGameObjectsWithTag() returns a list of objects with that tag
        foreach(GameObject grave in GameObject.FindGameObjectsWithTag("Grave")){
            graves.Add(grave);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // When figure out collision
        if (graves.Count == 0){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = openedGate;
        }
        
    }

    // Returns true if all graves have been buried
    public void remove(GameObject grave){
        graves.Remove(grave);
    }
}
