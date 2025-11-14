using UnityEngine;
using UnityEngine.Rendering;

public class BoxZone : MonoBehaviour
{

    public int rows; // nombre de lignes (Z)
    public int columns; // nombre de colonne (X) 
    public float cellSize; // taille d'une case
 


    public GameObject boxPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Taille réelle du plane (Unity plane mesh fait 10x10 par défaut)
        float planeWidth = transform.localScale.x;
        float planeHeight = transform.localScale.z;

        // On calcule automatiquement cellSize en fonction du nombre de cases désiré
        float cellWidth = planeWidth / columns;
        float cellHeight = planeHeight / rows;

        Vector3 origin = transform.position - new Vector3(planeWidth / 2f, 0f, planeHeight / 2f);// Calcule la position automatiquement des colonnes et lignes. 

        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3 cellPos = origin + new Vector3(x * cellWidth + cellWidth / 2f, 0f, z * cellHeight + cellHeight / 2f);// Calcule de la position celon l'origine

                GameObject cell = Instantiate(boxPrefab, cellPos, Quaternion.identity);
                cell.transform.position = cellPos;

                // Redimensionne le cube pour qu'il remplisse exactement la case
                cell.transform.localScale = new Vector3(cellWidth, cell.transform.localScale.y, cellHeight);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
