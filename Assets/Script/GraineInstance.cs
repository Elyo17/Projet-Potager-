using UnityEngine;

public class GraineInstance : MonoBehaviour
{
    private Rigidbody rb;
    public bool isPlantable = true;
    public bool isGrained = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        // On vérifie si la graine se plante dans le sol.
        if (other.CompareTag("Sol") && isGrained)
        {
            Debug.Log("La graine touche le sol");

            // On va arrêter la physique de la graine quand elle va être dans le sol pour quel puisse ne plus bouger
            rb.isKinematic = true;
            rb.useGravity = false;

            // Ajuster un peu la position pour pas quel soit encréer dans le sol
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);

            // On marque que la graine est planté
            isPlantable = false;
            isGrained = true;

            // On va enlevé à la graine tout parent pour quel soit indépendante du drone
            transform.SetParent(null);
        }
    }

}
