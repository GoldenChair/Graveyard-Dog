using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Skeleton : MonoBehaviour
{
    // For flipping sprite
    public SpriteRenderer spriteRenderer;
    // Speed of skeleton
    public float speed = 2.0f;
    // Wanted distance travled for AI skeleton horizontantly
    public float distanceX = 1.0f;
    // For controlling direction skelton is going on horizontanl
    private float direction = 1.0f;
    // Holds starting position of skeleton
    float startPosition = 0.0f;

    // For holding animator to control animations
    public Animator animator;

    public bool killed = false;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead){
            if(!killed){
                Vector3 horizontal = new Vector3(direction, 0.0f, 0.0f);
                transform.position = transform.position + horizontal * Time.deltaTime * speed;

                // if skeleton has traveled distanceX, turn it around and flip sprite, if's are for left and right
                if (transform.position.x > startPosition + distanceX){
                    direction = -1.0f;
                    spriteRenderer.flipX = true;
                }
                if (transform.position.x < startPosition - distanceX){
                    direction = 1.0f;
                    spriteRenderer.flipX = false;
                } 
            }
            if (killed){
                animator.SetTrigger("Killed");
                dead = true;
            }
        }
    }

    public void die(){
        killed = true;
    }

    public bool isdead(){
        return dead;
    }
}
