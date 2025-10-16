using UnityEngine;
using UnityEngine.InputSystem;

public class MoverDrone : MonoBehaviour
{
    public InputActionReference ActionReference;
    private float playerSpeed = 1f;

    private CharacterController controller;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        ActionReference.action.Enable();
    }

    private void OnDisable()
    {
        ActionReference.action.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        // Read input
        Vector2 stickdirection = ActionReference.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(stickdirection.x, 0, stickdirection.y);
        move = Vector3.ClampMagnitude(move, 1f);

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        controller.Move(move * Time.deltaTime);
    }
}
