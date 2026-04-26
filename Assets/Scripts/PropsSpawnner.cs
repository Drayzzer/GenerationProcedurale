using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PropsSpawnner : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _offsetX = 100f;
    [SerializeField] private float _offsetY = 100f;
    [SerializeField] private float _minSpawn;
    [SerializeField] private float _maxSpawn;
    [SerializeField][Range(0.0005f, 0.001f)] private float _noiseScale;
    
    public int xSize = 20;
    public int zSize = 20;
    public int Amplitude = 1;
    
    List<GameObject> _Props = new();

    void Start()
    {
        CreateShape();
    }

    private void Update()
    {
        //parcours la liste de props 
        foreach (var props in _Props)
        {
            //permet de détruire les props 
            Destroy(props);
        }
        CreateShape();
    }

    void CreateShape()
    {
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y  = Mathf.PerlinNoise(x / _noiseScale + _offsetX, z / _noiseScale + _offsetY) * Amplitude;
                
                //si c'est supérieur ou égale a x alors des props peuvent spawnner
                if (y >= _minSpawn && y <= _maxSpawn)
                {
                    // instancie le gameobject
                    GameObject go = Instantiate(_gameObject, new Vector3(x, y, z), Quaternion.identity);
                    _Props.Add(go);
                }
            }
        }
    } 
}
