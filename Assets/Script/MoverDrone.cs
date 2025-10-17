using UnityEngine;
using UnityEngine.InputSystem;

public class MoverDrone : MonoBehaviour
{
    public InputActionReference ActionReference;
    public float playerSpeed = 10f;
    private float rotationSpeed = 90.0f;

    private CharacterController charactercontroller;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charactercontroller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        // Read input
        Vector2 stickdirection = ActionReference.action.ReadValue<Vector2>();
        // Rotate character
        transform.Rotate(Vector3.up, stickdirection.x * rotationSpeed * Time.deltaTime);

        // Move character
        Vector3 moveDirection = transform.forward * stickdirection.y * playerSpeed * Time.deltaTime;

        charactercontroller.Move(moveDirection);
        Debug.Log(stickdirection);
    }
}
