using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 2f;
    public float verticalLookLimit = 90f;
    public bool invertY = false;
    
    [Header("References")]
    public Transform playerBody;
    
    private float xRotation = 0f;
    
    void Start()
    {
        // Auto-find player body if not assigned
        if (playerBody == null)
        {
            playerBody = transform.root; // Assumes camera is child of player
        }
        
        // Lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        HandleMouseLook();
    }
    
    void HandleMouseLook()
    {
        // Obtener movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        // Aplicar inversión en Y si está habilitada
        if (invertY)
            mouseY = -mouseY;
        
        // Rotar el cuerpo del jugador horizontalmente (izquierda/derecha)
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
        
        // Rotar la cámara verticalmente (arriba/abajo)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    
    // Métodos públicos para acceso externo
    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSensitivity = sensitivity;
    }
    
    public void SetInvertY(bool invert)
    {
        invertY = invert;
    }
    
    // Resetear rotación de cámara (útil para respawn o teletransporte)
    public void ResetRotation()
    {
        xRotation = 0f;
        transform.localRotation = Quaternion.identity;
        if (playerBody != null)
        {
            playerBody.rotation = Quaternion.identity;
        }
    }
}