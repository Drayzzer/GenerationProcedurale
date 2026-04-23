using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PropsSpawnner : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _offsetX = 100f;
    [SerializeField] private float _offsetY = 100f;
    [SerializeField] private float _scale = 20f;
    [SerializeField] private float _minSpawn;
    [SerializeField] private float _maxSpawn;
    
    public int xSize = 20;
    public int zSize = 20;
    
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
                float xCoord = (float)x;
                float zCoord = (float)z;

                float y  = Mathf.PerlinNoise(xCoord / xSize * _scale + _offsetX, zCoord / zSize * _scale + _offsetY);
                
                //si c'est supérieur ou égale a x alors des props peuvent spawnner
                if (y >= _minSpawn && y <= _maxSpawn)
                {
                    // instancie le gameobject
                    GameObject go = Instantiate(_gameObject, new Vector3(xCoord, y, zCoord), Quaternion.identity);
                    _Props.Add(go);
                }
            }
        }
    } 
}
