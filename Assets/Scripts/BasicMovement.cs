using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicMovement : MonoBehaviour
{
    public float speed;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    private Rigidbody2D body;
    // for checking if attack has already been registered, so attacks are hit mutliple times with one bite
    public bool hit = false;
    private bool gray = false;
    public bool has_bone = false;
    private bool Gray_Wall_Flag = false;
    public bool Grave_Flag = false;
    private bool Sign_Flag = false;
    private GameObject Gray_Wall_Hold;
    private GameObject Grave_Hold;
    private string Sign_Hold;
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Dogs attack(Bite) controlls and animation
        if (Input.GetKeyDown("space") && !has_bone){
            animator.SetTrigger("Attack");
        }

        // For Breaking gray walls with gray bone power.
        if (Input.GetKeyDown("space") && Gray_Wall_Flag){
            Debug.Log("Broken");
            Gray_Wall_Hold.GetComponent<Breakable_Wall>().destroy();
            animator.SetBool("Grabbed_Gray_Bone", false);
            has_bone = false;
        }

        // If press space on grave with bone, "Bury" bone, set grave to buried state, open gate
        if (Input.GetKeyDown("space") && Grave_Flag){
            Debug.Log("here");
            animator.SetBool("Grabbed_Gray_Bone", false);
            has_bone = false;
            Grave_Flag = false;
            Grave_Hold.GetComponent<Grave>().bury();
            GameObject.FindGameObjectWithTag("Exit_Gate").GetComponent<Gate>().remove(Grave_Hold);
        }

        // If in front of sign and press space, Call pop animation on sign from PopupSystem Class if not already opened, close it if it is.
        if (Input.GetKeyDown("space") && Sign_Flag){
            PopupSystem pop = GameObject.FindGameObjectWithTag("PopUp_System").GetComponent<PopupSystem>();
            if(pop.animator.GetCurrentAnimatorStateInfo(0).IsName("PopUp_Pop")){
                pop.Close();
            }
            else{
                pop.PopUp(Sign_Hold);
            }
        }

        // Dog cant have attacked if in idle
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dog_Idle")){
            hit = false;
        }
    }

    // Used with Rigidbody. Instead of every frame update, fixedframe runs anywhere between 0 and several times a frame depending on physics frames per second are set in settings and the framerate.
    // Use with physics to keep everything in sync(I.E. character going outofbounds and then being shot in bounds on next frame. with rigidbody.)
    void FixedUpdate(){
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // These ifs == 0 make it so we only move horzontally when not moving vertically and vice-versa
        if (verticalInput == 0){
            Vector3 horizontal = new Vector3(horizontalInput, 0.0f, 0.0f);
            // body.MovePosition((transform.position + horizontal * Time.deltaTime * speed));
            transform.position = transform.position + horizontal * Time.deltaTime * speed;
        }

        if (horizontalInput == 0){
            Vector3 vertical = new Vector3(0.0f, verticalInput, 0.0f);
            transform.position = transform.position + vertical * Time.deltaTime * speed;
        }
        
        //Debug.Log(Mathf.Ceil(Input.GetAxisRaw("Horizontal")).ToString());
        if (horizontalInput < 0) 
        {
            spriteRenderer.flipX = true;
        }
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // If in attack state and in skeleton collision and not already have hit in attack state.
        if (collision.gameObject.tag == "Skeleton" && animator.GetCurrentAnimatorStateInfo(0).IsName("Dog_Bite") && !hit){
            Debug.Log("hit");
            hit = true;
            GameObject enemy = collision.gameObject;
            enemy.GetComponent<AI_Skeleton>().die();
            if (enemy.GetComponent<AI_Skeleton>().isdead()){
                animator.SetBool("Grabbed_Gray_Bone", true);
                has_bone = true;
            }
        }

        if (collision.gameObject.tag == "Break_Gray_Wall" && animator.GetCurrentAnimatorStateInfo(0).IsName("Dog_Gray_Bone") && has_bone){
            Gray_Wall_Flag = true;
            Gray_Wall_Hold = collision.gameObject;
        }

        if (collision.gameObject.tag == "Sign"){
            Sign_Flag = true;
            Sign_Hold = collision.GetComponent<UnityEngine.UI.Text>().text;
        }

        if (collision.gameObject.tag == "Button"){
            //Play noise to signal button pressed
            collision.GetComponent<Button>().destroy();
        }

        if (collision.gameObject.tag == "Grave" && has_bone){
            Grave_Flag = true;
            Grave_Hold = collision.gameObject;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Break_Gray_Wall" && animator.GetCurrentAnimatorStateInfo(0).IsName("Dog_Gray_Bone") && has_bone){
            Gray_Wall_Flag = false;
            Gray_Wall_Hold = null;
        }

        if (other.gameObject.tag == "Grave"){
            Grave_Flag = false;
        }

        if (other.gameObject.tag == "Sign"){
            Sign_Flag = false;
            PopupSystem pop = GameObject.FindGameObjectWithTag("PopUp_System").GetComponent<PopupSystem>();
            if (pop.animator.GetCurrentAnimatorStateInfo(0).IsName("PopUp_Pop")){
                pop.Close();
            }
            
        }
    }

}
