using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    
    [Header("Movement")]
    private float horiMove = 0f;
    [SerializeField] private float movSpeed;
    [Range(0, 0.3f)][SerializeField] private float movSoft;
    private Vector3 speed=Vector3.zero;
    private bool lookRight = true;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask isFloor;
    [SerializeField] private Transform floorControler;
    [SerializeField] private Vector3 boxDimension;
    [SerializeField] private bool onFloor;
    private bool jump = false;

    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        
        horiMove = Input.GetAxisRaw("Horizontal") * movSpeed;

        if(Input.GetButtonDown("Jump")){
            jump = true;
        }

    }

    private void FixedUpdate() {
        onFloor = Physics2D.OverlapBox(floorControler.position, boxDimension, 0f, isFloor);
        Move(horiMove * Time.fixedDeltaTime, jump);
        jump=false;
    }

    private void Move(float move, bool jump){
        Vector3 objSpeed = new Vector2(move, rb2D.linearVelocity.y);
        rb2D.linearVelocity = Vector3.SmoothDamp(rb2D.linearVelocity, objSpeed, ref speed, movSoft);

        if(move > 0 && !lookRight){
            Turn();
        }else if(move < 0 && lookRight){
            Turn();
        }

        if(onFloor && jump){
            onFloor = false;
            rb2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Turn() {
        lookRight=!lookRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(floorControler.position, boxDimension);
    }
}
