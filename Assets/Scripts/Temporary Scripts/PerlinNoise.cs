using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public int octaves, persistance, lacunarity;

    

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;


    float noiseHeight = 0;
    float amplitude = 1;
    float frequency = 1;

    private void Start()
    {
        offsetX = Random.Range(0, 999999f);
        offsetY = Random.Range(0, 999999f);
    }
    private void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        float[,] noiseMap = new float[width, height];

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {

                for(int i = 0 ; i < octaves; i++)
                {
                    Color color = CalculateColor(x, y);
                    texture.SetPixel(x, y, color);
                }

                if(noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if(noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noiseMap[x,y] = Mathf.InverseLerp(noiseHeight, maxNoiseHeight, noiseMap[x,y]);
            }
        }

        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {
        /*float noiseHeight = 0;
        float amplitude = 1;
        float frequency = 1;*/

        amplitude *= persistance;
        frequency *= lacunarity;
        float xCoord = (float)x / width * scale * frequency + offsetX;
        float yCoord = (float)y / height * scale * frequency + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord) * 2 - 1;
        noiseHeight += sample * amplitude;
        //do stuff
        return new Color(sample, sample, sample);

    }
}
