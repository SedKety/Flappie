using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    [SerializeField] int width = 1;
    [SerializeField] int height = 1;

    private int meshScale = 25;

    [SerializeField] float xOffset, zOffset;
    [SerializeField] float noiseScale = 0.03f;
    [SerializeField] float heightMultiplier = 7;

    [SerializeField] int octaves = 1;
    [SerializeField] float lacunarity = 2;
    [SerializeField] float persistance = 0.5f;
    float lastNoiseHeight;

    float minTerrainHeight;
    float maxTerrainHeight;

    [SerializeField] Gradient terrainGradient;
    [SerializeField] Material material;

    private int[] triangles;
    private Mesh mesh;
    private Texture2D gradientTexture;

    [SerializeField] GameObject treeHolder, objectHolder, grassHolder;

    [SerializeField] GameObject[] trees, objects;
    [SerializeField] GameObject grass;
    Color[] colours;
    Vector3[] vertices;

    public void CallInAwake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        TerrainGenerate();
        Colours();

        UpdateMesh();
        SpawnObjects();

        xOffset = Random.Range(0, 99999);
        zOffset = Random.Range(0, 99999);
        //CreateMap();
    }

   /* private void CreateMap()
    {
        TerrainGenerate();
        Colours();
    }*/

    void Update()
    {
        //TerrainGenerate();
        Colours();
       
        minTerrainHeight = mesh.bounds.min.y + transform.position.y - 0.1f;
        maxTerrainHeight = mesh.bounds.max.y + transform.position.y + 0.1f;

        UpdateMesh();

        material.SetTexture("TexturedTerrainMaterial", gradientTexture);

        material.SetFloat("minTerrainHeight", minTerrainHeight);
        material.SetFloat("maxTerrainHeight", maxTerrainHeight);

        //zOffset += 0.001f;
    }

    private void Colours()
    {
        gradientTexture = new Texture2D(1, 10);
        colours = new Color[vertices.Length];

        for (int i = 0, z = 0; z < vertices.Length; z++)
        {
            float colourHeight = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
            colours[i] = terrainGradient.Evaluate(colourHeight);
            i++;
        }

        gradientTexture.SetPixels(colours);
        gradientTexture.Apply();
    }

    private void SpawnObjects()
    {
        //looping through all of the vertecis and checking if an object can instatiate there


        for(int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPoint = transform.TransformPoint(vertices[i]);
            var noiseHeight = worldPoint.y;

            if(System.Math.Abs(lastNoiseHeight - worldPoint.y) < 25)
            {
                if(noiseHeight > 0)
                {
                    if (Random.Range(0, 4) == 1)
                    {
                        GameObject treesToSpawn = trees[Random.Range(0, trees.Length)];
                        var spawnAboveTerrain = noiseHeight * 1.5f;
                        
                        Instantiate(treesToSpawn, new Vector3(mesh.vertices[i].x * meshScale, spawnAboveTerrain, mesh.vertices[i].z * meshScale), Quaternion.Euler(new Vector3(0, Random.Range(0,360), 0)), treeHolder.transform);
                        //treesToSpawn.transform.SetParent(treeHolder.transform);
                    }
                    else if(Random.Range(0, 3) == 1)
                    {
                        GameObject objectsToSpawn = objects[Random.Range(0, objects.Length)];
                        var spawnAboveTerrain = noiseHeight * 1.5f;
                        Instantiate(objectsToSpawn, new Vector3(mesh.vertices[i].x * meshScale, spawnAboveTerrain, mesh.vertices[i].z * meshScale), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), objectHolder.transform);
                    }
                   
                    var spawnGrassAboveTerrain = noiseHeight * 1.5f;

                    Instantiate(grass, new Vector3(mesh.vertices[i].x * meshScale, spawnGrassAboveTerrain, mesh.vertices[i].z * meshScale), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), grassHolder.transform);

                    
                }
            }

            lastNoiseHeight = noiseHeight;
        }
    }

    private void TerrainGenerate()
    {
        //VERTICES
        vertices = new Vector3[(width + 1) * (height + 1)];

        int i = 0;
        for (int z = 0; z <= height; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                float yPos = 0;

                for(int o = 0; o < octaves; o++)
                {
                    float frequency = Mathf.Pow(lacunarity, o);
                    float amplitude = Mathf.Pow(persistance, o);

                    yPos += Mathf.PerlinNoise((x + xOffset) * noiseScale * frequency, (z + zOffset) * noiseScale * frequency) * amplitude;
                }
                yPos *= heightMultiplier;

                vertices[i] = new Vector3(x, yPos, z);
                i++;
            }
        }

        //TRIANGLES
        //generating triangles
        //we are making a square which has 2 triangles and every tri has 3 points thats why where multiplying by 6
        triangles = new int[width * height * 6];

        int vertex = 0;
        int triangleIndex = 0;

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                triangles[triangleIndex + 0] = vertex + 0;
                triangles[triangleIndex + 1] = vertex + width + 1;
                triangles[triangleIndex + 2] = vertex + 1;

                triangles[triangleIndex + 3] = vertex + 1;
                triangles[triangleIndex + 4] = vertex + width + 1;
                triangles[triangleIndex + 5] = vertex + width + 2;

                vertex++;
                triangleIndex += 6;
            }
            vertex++;
        }
        //UpdateMesh();
    }


    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colours;
        mesh.RecalculateNormals();
        //mesh.RecalculateTangents();

        GetComponent<MeshCollider>().sharedMesh = mesh;
        gameObject.transform.localScale = new Vector3(meshScale, meshScale, meshScale);


        //SpawnObjects();
    }
}
