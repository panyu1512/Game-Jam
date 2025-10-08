using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    
    private CharacterController controller;
    
    // Input variables
    private float horizontal;
    private float vertical;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        // Lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        HandleInput();
        HandleMovement();
    }
    
    void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal"); // A/D o flechas izquierda/derecha
        vertical = Input.GetAxis("Vertical");     // W/S o flechas arriba/abajo
        
        // Toggle cursor lock with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    void HandleMovement()
    {
        // Calcular dirección del movimiento basado en la rotación del jugador
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        
        // Aplicar movimiento
        controller.Move(move * walkSpeed * Time.deltaTime);
    }
}