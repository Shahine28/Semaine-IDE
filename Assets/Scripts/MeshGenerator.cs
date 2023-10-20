using NaughtyAttributes;
using System.Collections.Generic;
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
    [SerializeField] private float height; // Hauteur de la mesh
    private int x;

    [SerializeField] private GameObject player;
    private GameObject line;
    [SerializeField] private Material tempMaterial;
    [SerializeField] private LayerMask m_playerLayer;
    [SerializeField] private LayerMask m_nothingLayer;


    private List<Vector3> verticesDef = new List<Vector3>();
    private List<int> trianglesDef = new List<int>();

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    // Déclarez une variable pour suivre la longueur totale du mesh.
    private float totalMeshLength = 0.0f;
    private int cubeCount = 0;
    [SerializeField] private float offset;
    [SerializeField] private float offsetY;
    [SerializeField] private float offsetX;

    // Longueur maximale souhaitée pour le mesh.
    public float maxMeshLength = 10.0f; // Réglez cette valeur en fonction de vos besoins.

    private void Awake()
    {
        /*player = gameObject.transform.parent.gameObject;*/
    }
    // Start is called before the first frame update
    void Start()
    {
        // Obtenez le composant Renderer du GameObject
        Renderer renderer = player.GetComponent<Renderer>();
        height = renderer.bounds.size.y;
        Debug.Log("Hauteur du GameObject : " + height);
        firstTime = true;
/*        CreateCube();*/
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
            new Vector3(0,0,0),
            new Vector3(1,0,0),
            new Vector3(1,1,0),
            new Vector3(0,1,0),
            new Vector3(0,1,1),
            new Vector3(1,1,1),
            new Vector3(1,0,1),
            new Vector3(0,0,1),
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
            if (Time.time > spawnTime)
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

        var backward = player.transform.position - (player.transform.forward) * offset;

        if (firstTime)
        {
            vertices = new Vector3[]
            {
                backward + (player.transform.right * -width) + Vector3.up * offsetY,
                backward - (player.transform.right * -width) + Vector3.up * offsetY,
                backward - (player.transform.right * -width) + player.transform.up * height,
                backward + (player.transform.right * -width) + player.transform.up * height,
            };

            triangles = new int[]
            {
                0, 2, 1, // face front;
                0, 3, 2,
            };

            line = new GameObject();
            line.tag = "Laser";
            line.name = "Laser";

            meshFilter = line.AddComponent<MeshFilter>();
            line.AddComponent<MeshRenderer>().material = tempMaterial;
/*            line.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;*/

            meshCollider = line.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = meshFilter.mesh;

            meshFilter.mesh.vertices = vertices;
            meshFilter.mesh.triangles = triangles;

            verticesDef = new List<Vector3>();
            trianglesDef = new List<int>();
            verticesDef.AddRange(vertices);
            trianglesDef.AddRange(triangles);

            isEven = false;
            firstTime = false;

            x = 4;

            return;
        }

        if (isEven)
        {
            meshCollider.sharedMesh = meshFilter.mesh;
            verticesDef.Add(backward + (player.transform.right * -width) + Vector3.up * offsetY);
            verticesDef.Add(backward - (player.transform.right * -width) + Vector3.up * offsetY);
            verticesDef.Add(backward - (player.transform.right * -width) + player.transform.up * height);
            verticesDef.Add(backward + (player.transform.right * -width) + player.transform.up * height);

            if (totalMeshLength > maxMeshLength) x = 400;
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
/*          trianglesDef.Add(x - 2);
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
            verticesDef.Add(backward + (player.transform.right * -width) + player.transform.up * height);
            verticesDef.Add(backward - (player.transform.right * -width) + player.transform.up * height);
            verticesDef.Add(backward - (player.transform.right * -width) + Vector3.up * offsetY);
            verticesDef.Add(backward + (player.transform.right * -width) + Vector3.up * offsetY);

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
    /*      trianglesDef.Add(x - 3);
            trianglesDef.Add(x + 2);
            trianglesDef.Add(x + 3);

            trianglesDef.Add(x - 3);
            trianglesDef.Add(x + 3);
            trianglesDef.Add(x - 4);*/

            //back face
    /*      trianglesDef.Add(x);
            trianglesDef.Add(x + 3);
            trianglesDef.Add(x + 2);

            trianglesDef.Add(x);
            trianglesDef.Add(x + 2);
            trianglesDef.Add(x + 1);*/

            isEven = true;
        }

        x += 4;


        totalMeshLength += width;

        

        while (totalMeshLength > maxMeshLength)
        {
            // Supprime les sommets et les triangles excédentaires du début.
            verticesDef.RemoveRange(0, 4);
            trianglesDef.RemoveRange(0, 18);
           

            // Mettez à jour les indices des triangles restants en soustrayant 4.
            for (int i = 0; i < trianglesDef.Count; i++)
            {
                trianglesDef[i] -= 18;
            }

            totalMeshLength -= width;
        }

        meshFilter.mesh.vertices = verticesDef.ToArray();
        meshFilter.mesh.triangles = trianglesDef.ToArray();



        //Debug.Log($"Current count1 {meshFilter.mesh.triangles.Length}");
        //Debug.Log($"Current count2 {meshFilter.mesh.vertices.Length}");
        //Debug.Log($"Current count3 {meshFilter.mesh.vertexCount}");
    }
}
