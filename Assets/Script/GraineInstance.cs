using UnityEngine;

public class GraineInstance : MonoBehaviour
{ 
    private GameObject carriedSeed;

    public bool isPlantable = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("La graine touche le sol");
        if (other.CompareTag("Sol"))
        {
            Debug.Log("comparetag détecte");
            transform.Translate(Vector3.down * Time.deltaTime * 0.2f);
            
        }
    }

}
