using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector3 movement;
    private Vector2 mouseDirection;
    private int _speed = 4;
    private int _runSpeed = 7;
    private int _backSpeed = 2;
    [Range(0, 10)][SerializeField] private int Sens = 10;
    [SerializeField] private Transform Head;
    private float mouseDirectionY;
    private float mouseDirectionX;

    float yVel;


    public bool IsUIMode;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {

        if (!AnimatorManager.Instance.IsStopAnim)
        {
            Move();
            Rotation();
        }


    }
    private void Rotation()
    {
        if (!IsUIMode)
        {
            mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * Sens;
            mouseDirectionY += mouseDirection.y;
            mouseDirectionY = Mathf.Clamp(mouseDirectionY, -60, 60);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + mouseDirection.x, transform.eulerAngles.z);
            Head.rotation = Quaternion.Euler(-mouseDirectionY, Head.eulerAngles.y, Head.eulerAngles.z);
        }
    }
    private void Move()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        yVel = movement.y;

        movement = ((transform.forward * moveDirection.z) + (transform.right * moveDirection.x)).normalized * Speed();

        movement.y = yVel;

        if (characterController.isGrounded)
        {
            movement.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            AnimatorManager.Instance.Animator.CrossFade("Jump", .1f);
            movement.y = 10f;
        }
        movement.y += Physics.gravity.y * Time.deltaTime * 2.5f;
        characterController.Move(movement * Time.deltaTime);
    }

    public float Speed()
    {


        if (moveDirection.z < 0)
        {
            return _backSpeed;
        }
        else if (moveDirection.z > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            return _runSpeed;
        }
        else
        {
            return _speed;
        }

    }
}

