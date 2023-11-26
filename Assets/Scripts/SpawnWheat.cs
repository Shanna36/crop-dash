using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWheat : MonoBehaviour
{
public GameObject wheatPrefab; 
public Terrain terrain;


void Start()
{
    int numberOfWheat = 600; // Adjust as needed

   
    float fieldStartX = -290;
    float fieldEndX = -189;
    float fieldAreaWidth = fieldEndX - fieldStartX;

    float fieldStartZ = 100;
    float fieldEndZ = 200;
    float fieldAreaDepth = fieldEndZ - fieldStartZ;

    // Calculate potential rows and columns with given distances
    int maxRows = Mathf.FloorToInt(fieldAreaDepth);
    int maxColumns = Mathf.FloorToInt(fieldAreaWidth);

    // Calculate total number of positions
    int totalPositions = maxRows * maxColumns;

    // Adjust the spacing if the required number of wheat is less than total positions
    if (numberOfWheat < totalPositions)
    {
        float spacingFactor = Mathf.Sqrt(totalPositions / (float)numberOfWheat);
        maxRows = Mathf.FloorToInt(maxRows / spacingFactor);
        maxColumns = Mathf.FloorToInt(maxColumns / spacingFactor);
    }

    float rowDistance = fieldAreaDepth / maxRows;
    float wheatSpacing = fieldAreaWidth / maxColumns;

    for (int rowIndex = 0; rowIndex < maxRows; rowIndex++)
    {
        for (int columnIndex = 0; columnIndex < maxColumns; columnIndex++)
        {
            float x = fieldStartX + columnIndex * wheatSpacing;
            float z = fieldStartZ + rowIndex * rowDistance;

            // Get the height of the terrain at this x, z coordinate
            float y = terrain.SampleHeight(new Vector3(x, 0, z));

            y += terrain.transform.position.y;

            Vector3 normal = terrain.terrainData.GetInterpolatedNormal((x - fieldStartX) / (fieldEndX - fieldStartX), (z - fieldStartZ) / (fieldEndZ - fieldStartZ));
            Quaternion rotation = Quaternion.Euler(10f, 0f, 0f);
            //rotation *= Quaternion.Euler(70f, 0f, 0f); fixing my model's wonky rotation


            Instantiate(wheatPrefab, new Vector3(x, y, z), rotation);
        }
    }
}



}
