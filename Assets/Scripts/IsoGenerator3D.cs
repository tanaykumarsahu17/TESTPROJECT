using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoGridGenerator3D : MonoBehaviour
{
    public GameObject Cube; //assign a plain Cube prefab in Inspector
    public int gridWidth = 10; //number of cubes along x axis 
    public int gridHeight = 10; //number of cubes along z axis 
    public float gap = 1.0f; //spaching between cubes
    
    [HideInInspector]
    public GameObject[,] gCubes; //2D array to keep references to all generated cubes

    void Start()
    {
        
        GenGrid(); //called upon once the scene starts
    }

    void GenGrid() //generates a grid of cubes based on width,height and gap
    {
        gCubes = new GameObject[gridWidth,gridHeight];
        for (int x =0; x<gridWidth; x++)
        {
            for (int y =0; y<gridHeight; y++)
            {
                Vector3 pos = new Vector3(x *gap, 0, y *gap); //calculate world position of a cube
                GameObject cube = Instantiate(Cube, pos, Quaternion.identity, transform); //spawn cube
                cube.name = $"Cube_{x}_{y}"; //gives cube a unqiue name in the hierarchy

                gCubes[x,y] = cube; //store reference to cube in the array
                TileInfo info = cube.AddComponent<TileInfo>(); //add TileInfo script automatically
                info.SetCoordinates(x, y);
            }
        }
    }
}

