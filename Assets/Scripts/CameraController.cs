using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject dog;
    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.Find("Dog");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Clamps camera to follow dog but not move past edge
        // Clamps are based of initail dog position and free aspect on moniter 1
        // Changes must likely needed later
        float xPos = Mathf.Clamp(dog.transform.position.x,-6.62f,2.62f);
        float yPos = Mathf.Clamp(dog.transform.position.y,-3.58f,1.58f);
        Vector3 bound = new Vector3(xPos, yPos, -10.0f);
        // if (dog.transform.position.x < transform.position.x){
        //     xPos = Mathf.Clamp(xPos, -5.62f,transform.position.x);
        //     Debug.Log("test");
        // }
        transform.position = bound;

    }
}
