using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 12f;
    private CharacterController controller;
    private float verticalVelocity;
    private Vector2 move;

    [Header("Gravity")]
    [SerializeField] private float gravity = -20f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    void Update()
    {
        movePlayer();
    }

    public void movePlayer()
    {
        float dt = Time.deltaTime;

        float turn = move.x;
        transform.Rotate(0f, turn * rotationSpeed * dt, 0f);

        float forward = move.y;
        Vector3 moveDirection = transform.forward * (forward * speed);

        if(controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Small negative value to keep grounded
        }

        verticalVelocity += gravity * dt;
        moveDirection.y = verticalVelocity;

        controller.Move(moveDirection * dt);
        
    }
}
