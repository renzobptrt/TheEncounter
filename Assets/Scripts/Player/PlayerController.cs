using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Components")]
    public static PlayerController instance;
    private Animator animator;
    [SerializeField] private LayerMask touch;
    public LayerMask Touch { get => touch; set => touch = value; }
    Vector2 MousePosition;
    Vector2 tempMousePos;
    [SerializeField] private GameObject Inventario;
    [SerializeField] private Sprite _playerFace;
    [SerializeField] private Sprite _morriganFace;

    [Header("Features")]
    [SerializeField] private float rightVelocity;
    [SerializeField] private bool _isAvailableToMove = false;
    private float velocity;
    public float rango;
    private bool move = false;
    

    public float Velocity { get => velocity; set => velocity = value; }

    public bool Move { get => move; set => move = value; }
    public float RightVelocity { get => rightVelocity; set => rightVelocity = value; }
    public bool IsAvailableToMove { get => _isAvailableToMove; set => _isAvailableToMove = value; }
    public Sprite PlayerFace { get => _playerFace; set => _playerFace = value; }
    public Sprite MorriganFace { get => _morriganFace; set => _morriganFace = value; }
    public GameObject Inventario1 { get => Inventario; set => Inventario = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        animator = GetComponent<Animator>();
    }
    private void Start()
    {

        Inventario = GameObject.FindGameObjectWithTag("Inventario");
    }
    void Update()
    {

        if (IsAvailableToMove)
        {
            MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            bool TouchTerrain = Physics2D.Raycast(MousePosition, Vector3.forward, 5f, Touch);
            float dis = Vector3.Distance(MousePosition, Inventario.transform.position);
            if (dis < rango)
            {
                TouchTerrain = false;
            }


            if (Input.GetMouseButtonDown(0) && TouchTerrain)
            {
                if (MousePosition.x < transform.position.x)
                {
                    SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                    sprite.flipX = true;
                }
                else
                {
                    SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                    sprite.flipX = false;
                }
                tempMousePos = MousePosition;
                Move = true;
                velocity = RightVelocity;
            }
            if (Move)
            {
                animator.SetInteger("Action", 1);
                Vector3 Direction = ((Vector3)tempMousePos - transform.position);
                transform.position += Direction.normalized * Velocity * Time.deltaTime;
                float Distancia = Direction.magnitude;
                if (Mathf.Sqrt(Distancia) < 0.5f)
                {
                    ResetMovement();
                }
            }
        }
    }

    public void ResetMovement()
    {
        Move = false;
        animator.SetInteger("Action", 0);
        Velocity = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Item"))
        {
           
            if (transform.position.y > collision.gameObject.transform.position.y)
            {
              
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                sprite.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
            }
            else
            {

                SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                sprite.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Inventario.transform.position, rango);
    }

}
