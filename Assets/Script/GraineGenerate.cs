using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PanneauGraine
{
    public Transform panneau;       // Le panneau dans la scène
    public GameObject grainePrefab; // Prefab de la graine correspondante
    public GameObject graineInstance; // Instance actuelle
    public bool grainePresente = false; // État
}


public class GraineGenerate : MonoBehaviour
{

    public PanneauGraine[] panneaux; // Tableau des panneaux et graines
    public float distanceDevant = 0.5f; // Distance devant le panneau
    public float delaiRegeneration = 1f; // délai avant régénération


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        // Générer toutes les graines au début
        foreach (var panneau in panneaux)
        {
            GenererGraine(panneau);
        }
    }


    // Update is called once per frame
    void Update()
    {
    }


    public void Regénèration(Transform graine)
    {
        foreach (var panneau in panneaux)
        {
            if (panneau.graineInstance != null && panneau.graineInstance.transform == graine)
            {
                panneau.grainePresente = false;
                StartCoroutine(RegenererGraine(panneau));
                break;
            }
        }
    }


    void GenererGraine(PanneauGraine panneau)
    {
        Vector3 spawnPos = panneau.panneau.position + panneau.panneau.forward * distanceDevant;
        panneau.graineInstance = Instantiate(panneau.grainePrefab, spawnPos, Quaternion.identity);
        panneau.grainePresente = true;
    }


    IEnumerator RegenererGraine(PanneauGraine panneau)
    {
        yield return new WaitForSeconds(delaiRegeneration);
        GenererGraine(panneau);
    }


}
