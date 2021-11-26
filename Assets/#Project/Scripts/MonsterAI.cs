using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterAI : MonoBehaviour
{
    //START OF CHANGES
    //temporary
    //public float agroRange;
    Animator animator;
    //[SerializeField]
    //float deathRange;
    public float hitRange;
    //public float moveSpeed;
    public Vector2 speed = Vector2.zero;
    //GameObject notice;
    public string sceneName;

    [SerializeField]
    GameObject player; //keep track of the player
    Rigidbody2D rb2d; //il faut pouvoir accéder au rigidBody
    SpriteRenderer spriteRenderer;
    //public UnityEvent whenTouchPlayer;
    public GameObject ignoreObjects;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ignoreObjects.GetComponent<Collider2D>());
    }

    void Update()
    {
        Vector2 start;
        Vector2 direction;
        
        
        //distance to player
        //float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        #region monstre se déplace à gauche ou à droite


        // if(moveSpeed.x < 0) {
        //     if(animator != null) {
        //         //flip
        //         animator.SetBool("Right", false);
        //     }
        //     else {
        //         spriteRenderer.flipX = false;
        //     }
        //     start = (Vector2)transform.position + Vector2.left * 0.51f;
        //     direction = Vector2.left;
        // }
        // //modifier pour que le monstre ne flippe pas (animation iddle ?)
        // else {
        //     if (animator != null) { //si pas d'animation, flippe
        //         animator.SetBool("Right", true);
        //     }
        //     else {
        //         spriteRenderer.flipX = true;
        //     }
        //     //change if problems occur
        //     spriteRenderer.flipX = false;
        //     start = (Vector2)transform.position + Vector2.right * 0.51f;
        //     direction = Vector2.right;
        // }

        #endregion


        // Vector2 deplacement = moveSpeed * Time.deltaTime;
        // transform.position += (Vector3)deplacement;

        //on va à droite
        if (speed.x < 0) {
            if (animator != null) {
                animator.SetBool("Right", false);
            }
            else {
                spriteRenderer.flipX = false;
            }
            start = (Vector2)transform.position + Vector2.left * 0.51f;
            direction = Vector2.left;
        }
        //on va à droite
        else {
            if (animator != null) { //s'il n'y a pas d'animator je flippe
                animator.SetBool("Right", true);
            }
            else {
                spriteRenderer.flipX = true;
            }
            spriteRenderer.flipX = false;
            start = (Vector2)transform.position + Vector2.right * 0.51f;
            direction = Vector2.right;
        }
        Debug.DrawRay(start, direction * hitRange, Color.blue); //permet de voir ce qu'il y a devant nous. On part de la position et on on va vers la droite
        RaycastHit2D hit = Physics2D.Raycast(start, direction, hitRange);

        if(hit.collider != null && hit.transform.CompareTag("Player")) {
            Debug.Log("player detected");
            ChasePlayer();
        }
        else {
            
        }
    }
    void ChasePlayer(){
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed.x * Time.deltaTime);
        //animator.SetBool("Notice", true);
    }
    // void StopChasingPlayer(){
    //     rb2d.velocity = Vector2.zero;
    //     animator.SetBool("Notice", false);
    // }
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            //lancer l'animation
            Debug.Log("GameOver");
            //SceneManager.LoadScene(sceneName);
        }
    }
}
