using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public bool leftClickPressed = false;
    public float movementSpeed = 5f;
    //public bool isMoving = true;

    public Vector2 lastClickedPos;
    private SpriteRenderer spriteRenderer;
    public ItemViewController itemViewController;

    [HideInInspector]
    public float deplacement;
    
    [HideInInspector]
    public Rigidbody2D rb2d;
    ItemClicked itemClicked;
    Animator animator;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {

        if(ItemClicked.usingItem){
            leftClickPressed = false;
            lastClickedPos.x = transform.position.x;
        }
        if(!ItemClicked.usingItem){
            if(leftClickPressed) {
            
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (lastClickedPos.x != transform.position.x) {
                animator.SetBool("Moving", true);
                if(lastClickedPos.x < transform.position.x && spriteRenderer.flipX == false) {
                    spriteRenderer.flipX = true;
                }
                else if (lastClickedPos.x > transform.position.x && spriteRenderer.flipX == true) {
                    spriteRenderer.flipX = false;
                }
                
                deplacement = movementSpeed * Time.deltaTime;
                lastClickedPos.y = 1.58f;
                transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, deplacement);
                
                //print("test move");
            }
            else
            {
                animator.SetBool("Moving", false);
            }
            //isMoving = false;
            
            leftClickPressed = false;
        }
        
    }
    public void Move(InputAction.CallbackContext context) {
        if (context.performed) {
            //print("click to move");
            leftClickPressed = true;
        }
    }
}
//pos + (click - pos).normalised * mov
//pour calculer la dernière distance il faut prendre min de mouv et différence entre (pos - click)

//Vector2 pos
//Vector2 dir = pos.normalized