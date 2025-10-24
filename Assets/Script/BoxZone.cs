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

        // Centre la grille par rapport au plane
        Vector3 origin = transform.position - new Vector3(columns * cellSize / 2f, 0f, rows * cellSize / 2f);

        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3 cellPos = origin + new Vector3(x * cellSize + cellSize / 2f, 0f, z * cellSize + cellSize / 2f);
                GameObject cell = Instantiate(boxPrefab);
                cell.transform.position = cellPos * 10;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
