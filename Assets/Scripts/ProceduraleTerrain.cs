using UnityEngine;
using UnityEngine.Serialization;

public class ProceduraleTerrain : MonoBehaviour
{
    [SerializeField] private GameObject _ocean;
    [SerializeField] private float _offsetX = 100f; 
    [SerializeField] private float _offsetY = 100f;
    [SerializeField] [Range(0.0005f, 0.001f)] private float _scale;
    
    public int xSize = 20;
    public int zSize = 20;
    public int Amplitude = 1;
    
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;

    void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        Instantiate(_ocean, new Vector3(transform.position.x, 0.3f, transform.position.z), Quaternion.identity);
        CreateShape();
    }

    void Update()
    {
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        _vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                
                float y = Mathf.PerlinNoise(x * _scale + _offsetX, z * _scale + _offsetY) * Amplitude;
                
                _vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }
        
        _triangles = new int[xSize * zSize * 6];
        
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < xSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                _triangles[tris + 0] = vert + 0;
                _triangles[tris + 1] = vert + xSize + 1;
                _triangles[tris + 2] = vert + 1;
                _triangles[tris + 3] = vert + 1;
                _triangles[tris + 4] = vert + xSize + 1;
                _triangles[tris + 5] = vert + xSize + 2;
            
                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        _mesh.Clear();
        
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        
        _mesh.RecalculateNormals();
    }
}
