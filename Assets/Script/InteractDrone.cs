using UnityEngine;
using UnityEngine.InputSystem;

public class InteractDrone : MonoBehaviour
{

    public InputActionReference ActionReference;

    public float interactRange = 3f; // distance max pour interagir
    public LayerMask interactLayer; // couche des objets interactifs (ex: "Seed")

    private GameObject carriedSeed; // la graine que le drone tient actuellement
    public Transform holdPoint; // un point enfant du drone où la graine est tenue
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {

        ActionReference.action.performed += OnActionPerformed;
        ActionReference.action.Enable();
    }

    private void OnDisable()
    {

        ActionReference.action.performed -= OnActionPerformed;
        ActionReference.action.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        // Si le drone ne porte rien, il essaie de ramasser une graine
        if (carriedSeed == null)
        {
            TryPickupSeed();
        }
        else
        {
            DropSeed();
        }
    }

    private void TryPickupSeed()
    {
        // Raycast devant le drone
        Ray ray = new Ray(transform.position, Vector3.down);
        Debug.Log("oui");
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
        {
            GameObject target = hit.collider.gameObject;
            Debug.Log(target, target);

            // "Ramasser" la graine
            carriedSeed = target;
            carriedSeed.transform.SetParent(holdPoint);
            carriedSeed.transform.localPosition = Vector3.down*2;
            carriedSeed.transform.localRotation = Quaternion.identity;

            // Désactiver sa physique si elle en a
            if (carriedSeed.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = true;
            }
        }
    }

    private void DropSeed()
    {
        // La lâcher
        carriedSeed.transform.SetParent(null);

        // Réactiver la physique
        if (carriedSeed.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = false;
            rb.AddForce(transform.forward * 2f, ForceMode.Impulse); // petit lancer
        }

        carriedSeed = null;
    }
}

