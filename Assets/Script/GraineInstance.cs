using System.Collections;
using UnityEngine;

public class GraineInstance : MonoBehaviour
{
    private Rigidbody rb;
    public bool isPlantable = true;
    private bool isGrained = true;
    public float timeBeforeScale = 3f;
    private float currentBeforeTime = 0;
    private bool isRecolted = false;

    private float maxscale = 3f;
    public int recoltedValue = 1;
    public bool isWatered = false;
    public GameObject plant; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entre dans le trigger");

        // On vérifie si la graine se plante dans le sol.
        if (other.gameObject.tag == "Sol" && isGrained && !isPlantable)
        {
            Debug.Log("La graine touche le sol");

            // On va arrêter la physique de la graine quand elle va être dans le sol pour quel puisse ne plus bouger
            rb.isKinematic = true;
            rb.useGravity = false;

            // Ajuster un peu la position pour pas quel soit encréer dans le sol
            transform.position = new Vector3(transform.position.x,0f, transform.position.z);

            // On marque que la graine est planté
            isPlantable = false;
            isGrained = true;

            // On va enlevé à la graine tout parent pour quel soit indépendante du drone
            transform.SetParent(null);

            Planté();
        }
    }

    public void Water()
    {
        if (isGrained && !isWatered)
        {
            isWatered = true;
            StartCoroutine(Coroutine_Scale());
        }
    }

    public void Planté()
    {
        if (isGrained)
        {
            this.GetComponent<MeshFilter>().sharedMesh = plant.GetComponent<MeshFilter>().sharedMesh;
        }
    }

    private IEnumerator Coroutine_Scale()
    {
        Debug.Log("je rentre dans ma coroutine");
        while (maxscale > transform.localScale.x)
        {
            yield return new WaitForEndOfFrame();
            transform.localScale += Vector3.one * Time.deltaTime * 0.3f;
            if(transform.localScale.x > maxscale/2)
            {
                isRecolted = true;
            }
            Debug.Log("je fait");
        }
        Explosion();
        yield return null;
    }

    private void Explosion()
    {
        Destroy(gameObject);
    }

    public bool Recolte()
    {
        if (isRecolted)
        {
            Explosion();
            return true;
        }
        return false;
    }

}
