using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    private int width = 256; 
    private int height = 256;

    [SerializeField] private float scale = 20f;

    [SerializeField] private float offsetX = 100f;
    [SerializeField] private float offsetY = 100f;

    private Renderer _renderer;

    void Start()
    {
        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);
        _renderer = GetComponent<Renderer>();
      
    }
    void Update()
    {
        _renderer.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        // génère un perlin noise pour la texture
        Texture2D texture = new Texture2D(width,  height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        
        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;
        
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }
}
