using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 100, height = 100, depth = 20;

    public float scale = 20;
    public float offsetX, offsetY;


    //terrainColour
    public Gradient terrainGradient;
    private Color[,] colours;

    private void Start()
    {
        offsetX = Random.Range(0, 99999f);
        offsetY = Random.Range(0, 99999f);
    }
    //should be start but is in the update for testing
    private void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

       // terrain.materialTemplate.color = colours[width,height]; // idex out of bounds error
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3 (width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights ()
    {
        float[,] heights = new float[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++) 
            {
                heights[x, y] = CalculateHeights(x,y); // perlin noise value
            }
        }
        ColourMap();

        return heights;
    }

    private void ColourMap()
    {
        colours = new Color[width, height];

        for(int x = 0; x < width ; x++)
        {
            for (int y = 0;y < height ; y++)
            {
                float colourHeight = Mathf.InverseLerp(depth, width, height);
                colours[x, y] = terrainGradient.Evaluate(colourHeight);

            }
        }
    }
    //taking coords and converting them into noise map coords, then were returning the value of the perlinNoise function coords and feeding them into the heights array
    float CalculateHeights(int x, int y)
    {
        float xCoord = (float)x/ width * scale + offsetX;
        float yCoord = (float)y/ height * scale + offsetX;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
