using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField] private Material m_customMaterial;
    [SerializeField] private bool weaponEnabled;
    [SerializeField] private float spawnTime;
    [SerializeField] private float monoSpawnDisplay;
    [SerializeField] private bool firstTime;
    [SerializeField] private bool isEven;
    [SerializeField] private float width;
    private int x;

    [SerializeField] private GameObject player;
    private GameObject line;
    [SerializeField] private Material tempMaterial;
    private List<Vector3> verticesDef = new List<Vector3>();
    private List<int> trianglesDef = new List<int>();

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    [Button ("Destroy first vertices")]
    // Start is called before the first frame update
    void Start()
    {
        firstTime = true;
        CreateCube();
    }

    private void CreateCube()
    {
        GameObject cube = new GameObject();
        cube.name = "ExempleCube";

        var meshFilter = cube.AddComponent<MeshFilter>();
        var meshRenderer = cube.AddComponent<MeshRenderer>();

        meshRenderer.material = m_customMaterial;

        Vector3[] vertices =
        {
            new Vector3 (0,0,0),
            new Vector3 (1,0,0),
            new Vector3 (1,1,0),
            new Vector3 (0,1,0),
            new Vector3 (0,1,1),
            new Vector3 (1,1,1),
            new Vector3 (1,0,1),
            new Vector3 (0,0,1),
        };

        meshFilter.mesh.vertices = vertices;

        int[] triangles =
        {
            0,2,1, //front face
            0,3,2,
            2,3,4, //top face
            2,4,5,
            1,2,5, // right face
            1,5,6,
            0,7,4, // left face
            0,4,3, 
            5,4,7, // back face
            5,7,6,
            0,6,7, // bottom face
            0,1,6

        };

        meshFilter.mesh.triangles = triangles;
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponEnabled)
        {
            if( Time.time > spawnTime )
            {
                spawnTime = Time.time + monoSpawnDisplay;
                MonoLine();
            }
        }
    }

    private void MonoLine()
    {
        Vector3[] vertices = null;
        int[] triangles = null;

        var backward = player.transform.position;

        if (firstTime)
        {
            vertices = new Vector3[]
            {
                backward + (player.transform.right * -width),
                backward - (player.transform.right * -width),
                backward - (player.transform.right * -width) + player.transform.up * width,
                backward + (player.transform.right * -width) + player.transform.up * width

            };

            triangles = new int[]
            {
                0,2,1, // face front;
                0,3,2,
            };

            line = new GameObject();
            line.tag = "Laser";
            line.name = "Laser";

            meshFilter = line.AddComponent<MeshFilter>();
            line.AddComponent<MeshRenderer>().material = tempMaterial;
            line.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            meshCollider = line.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = meshFilter.mesh;

            meshFilter.mesh.vertices = vertices;
            meshFilter.mesh.triangles = triangles;

            verticesDef = new List<Vector3>();
            trianglesDef = new List<int>();
            foreach (var ver in vertices) verticesDef.Add(ver);
            foreach (var tri in triangles) trianglesDef.Add(tri);

            isEven = false;
            firstTime = false;

            x = 4;

            return;
        }

        if (isEven)
        {
            meshCollider.sharedMesh = meshFilter.mesh;
            verticesDef.Add(backward + (player.transform.right * -width));
            verticesDef.Add(backward - (player.transform.right * -width));
            verticesDef.Add(backward - (player.transform.right * -width) + player.transform.up * width);
            verticesDef.Add(backward + (player.transform.right * -width) + player.transform.up * width);

            //left face 
            trianglesDef.Add(x - 4);
            trianglesDef.Add(x - 1);
            trianglesDef.Add(x);

            trianglesDef.Add(x - 4);
            trianglesDef.Add(x);
            trianglesDef.Add(x + 3);

            //top face
            trianglesDef.Add(x - 4); 
            trianglesDef.Add(x + 3);
            trianglesDef.Add(x + 2);

            trianglesDef.Add(x - 4);
            trianglesDef.Add(x + 2);
            trianglesDef.Add(x - 3);

            //right face
            trianglesDef.Add(x - 3);
            trianglesDef.Add(x + 2);
            trianglesDef.Add(x + 1);

            trianglesDef.Add(x - 3);
            trianglesDef.Add(x + 1);
            trianglesDef.Add(x - 2);

            // bottom face
/*            trianglesDef.Add(x - 2);
            trianglesDef.Add(x + 1);
            trianglesDef.Add(x);

            trianglesDef.Add(x - 2);
            trianglesDef.Add(x);
            trianglesDef.Add(x - 1);*/

            //back face
/*            trianglesDef.Add(x);
            trianglesDef.Add(x + 1);
            trianglesDef.Add(x + 2);

            trianglesDef.Add(x);
            trianglesDef.Add(x + 2);
            trianglesDef.Add(x + 3);*/

            isEven = false;
        }
        else
        {
            meshCollider.sharedMesh = meshFilter.mesh;
            verticesDef.Add(backward + (player.transform.right * -width) + player.transform.up * width);
            verticesDef.Add(backward - (player.transform.right * -width) + player.transform.up * width);
            verticesDef.Add(backward - (player.transform.right * -width));
            verticesDef.Add(backward + (player.transform.right * -width));

            //left face 
            trianglesDef.Add(x - 4);
            trianglesDef.Add(x + 3);
            trianglesDef.Add(x);

            trianglesDef.Add(x - 4);
            trianglesDef.Add(x);
            trianglesDef.Add(x - 1);

            //top face
            trianglesDef.Add(x - 2);
            trianglesDef.Add(x - 1);
            trianglesDef.Add(x);

            trianglesDef.Add(x - 2);
            trianglesDef.Add(x);
            trianglesDef.Add(x + 1);

            //right face
            trianglesDef.Add(x - 3);
            trianglesDef.Add(x - 2);
            trianglesDef.Add(x + 1);

            trianglesDef.Add(x - 3);
            trianglesDef.Add(x + 1);
            trianglesDef.Add(x + 2);

            // bottom face
/*            trianglesDef.Add(x - 3);
            trianglesDef.Add(x + 2);
            trianglesDef.Add(x + 3);

            trianglesDef.Add(x - 3);
            trianglesDef.Add(x + 3);
            trianglesDef.Add(x - 4);*/

            //back face
    /*            trianglesDef.Add(x);
            trianglesDef.Add(x + 3);
            trianglesDef.Add(x + 2);

            trianglesDef.Add(x);
            trianglesDef.Add(x + 2);
            trianglesDef.Add(x + 1);*/

            isEven = true;
        }

        x += 4;
        meshFilter.mesh.vertices = verticesDef.ToArray();
        meshFilter.mesh.triangles = trianglesDef.ToArray();
       
        
        
    }
}
