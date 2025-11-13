using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InteractDrone : MonoBehaviour
{

    public InputActionReference ActionReference;
    public InputActionReference WaterAction;

    public float interactRange = 3f; // distance max pour interagir
    public LayerMask interactLayer; // couche des objets interactifs (ex: "Seed")

    public GameObject carriedSeed; // la graine que le drone tient actuellement
    public Transform holdPoint; // un point enfant du drone où la graine est tenue

    public ParticleSystem waterParticles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
    }

    private void OnEnable()
    {

        ActionReference.action.performed += OnActionPerformed;

        WaterAction.action.performed += OnWaterStart;
        WaterAction.action.canceled += OnWaterStop;


        ActionReference.action.Enable();
        WaterAction.action.Enable();
    }

    private void OnDisable()
    {
        ActionReference.action.performed -= OnActionPerformed;

        WaterAction.action.performed -= OnWaterStart;
        WaterAction.action.canceled -= OnWaterStop;

        ActionReference.action.Disable();
        WaterAction.action.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        // Si le drone tient une graine, on met à jour sa position chaque frame
        if (carriedSeed != null)
        {
            carriedSeed.transform.position = holdPoint.position;
            carriedSeed.transform.rotation = holdPoint.rotation;
        }
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


    private void OnWaterStart(InputAction.CallbackContext context)
    {
        waterParticles.Play();
        Debug.Log("lancement de l'arrosage");
    }

    private void OnWaterStop(InputAction.CallbackContext context)
    {
        waterParticles.Stop();
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
        {
            GraineInstance seed = hit.collider.GetComponent<GraineInstance>();
            if (seed != null && !seed.isWatered)
            {
                seed.Water();
            }
        }
    }

    private void TryPickupSeed()
    {
        // Raycast devant le drone
        Ray ray = new Ray(transform.position, Vector3.down);
        Debug.Log("On entre dans TryPickupSeed");
        Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
        {
            GameObject target = hit.collider.gameObject;
            Debug.Log(target, target);

            if (target.GetComponent<GraineInstance>() != null) 
            {
                GraineInstance graine = target.GetComponent<GraineInstance>();
                bool recolter = graine.Recolte();
                if (recolter)
                {
                    ScorePlayer.Instance.AddScore(graine.recoltedValue);
                    Debug.Log("Graine récoltée, score ajouté !");
                    return;
                }
            }

            if (target.GetComponent<GraineInstance>().isPlantable)
            {
                // "Ramasser" la graine
                carriedSeed = target;
                carriedSeed.transform.SetParent(holdPoint);
                carriedSeed.transform.localPosition = Vector3.zero;
                carriedSeed.transform.localRotation = Quaternion.identity;

                target.GetComponent<GraineInstance>().isPlantable = false;
            }

        }
        else
        {
            Debug.Log("No seed detected under drone.");
        }
    }

    private void DropSeed()
    {
        if (carriedSeed == null) return;

        // La lâcher
        carriedSeed.transform.SetParent(null);

        // Réactiver la physique
        if (carriedSeed.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = false;
            rb.AddForce(Vector3.down * 2f, ForceMode.Impulse); // petit lancer
        }

        carriedSeed = null;
        Debug.Log("Planté");     
    }

   
}

