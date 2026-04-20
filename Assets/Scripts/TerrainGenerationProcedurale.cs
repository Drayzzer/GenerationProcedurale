using UnityEngine;

public class TerrainGenerationProcedurale : MonoBehaviour
{
    [SerializeField] private int Xsize = 10;
    [SerializeField] private int Zsize = 10;
    [SerializeField] private float noiseScale = 0.03f;
    [SerializeField] private float heightMultiplier = 7;
    [SerializeField] private int Xoffset;
    [SerializeField] private int Zoffset;
    
    private Mesh _mesh;
    Vector3[] _vertices;

    void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        GenerateTerrain();
    }

    void Update()
    {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        //Vertices
         _vertices = new Vector3[(Xsize + 1) * (Zsize + 1)];

        int i = 0;
        for (int z = 0; z < Xsize; z++)
        {
            for (int x = 0; x < Zsize; x++)
            {
                float Ypos = Mathf.PerlinNoise(x + Xoffset * noiseScale, z + Zoffset *  noiseScale) * heightMultiplier;
                _vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }
        
        // triangles
        int[] triangles = new int [Xsize * Zsize * 6];
        
        int vertex = 0;
        int triangleIndex = 0;
        
        for (int x = 0; x < Xsize; x++)
        {
            triangles[triangleIndex + 0] = vertex + 0;
            triangles[triangleIndex + 1] = vertex + Xsize + 1;
            triangles[triangleIndex + 2] = vertex + 1;
            
            triangles[triangleIndex + 3] = vertex + 1;
            triangles[triangleIndex + 4] = vertex + Xsize + 1;
            triangles[triangleIndex + 5] = vertex + Xsize + 2;

            vertex++;
            triangleIndex += 6;
        }
        
        _mesh.Clear();

        _mesh.vertices = _vertices;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
    }
}
