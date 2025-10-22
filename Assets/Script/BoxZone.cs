using UnityEngine;

public class BoxZone : MonoBehaviour
{
    private GameObject Sol; 
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
        Debug.Log("Le drone vient de rentrer");
        if (other.CompareTag("Sol"))
        {
            Debug.Log("Drone est bien rentrer dans la zone");
        }
    }
}
