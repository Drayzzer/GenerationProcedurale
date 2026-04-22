using System;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float offsetX = 100f;
    [SerializeField] private float offsetY = 100f;
    [SerializeField] private float scale = 20f;
    
    public int xSize = 20;
    public int zSize = 20;
   
    
    List<GameObject> _trees = new();

    void Start()
    {
        CreateShape();
    }

    private void Update()
    {
        foreach (var tree in _trees)
        {
            Destroy(tree);
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

                float y  = Mathf.PerlinNoise(xCoord / xSize * scale + offsetX, zCoord / zSize * scale + offsetY);
                
                if (y >= 0.5f)
                {
                    GameObject go = Instantiate(_gameObject, new Vector3(xCoord, y, zCoord), Quaternion.identity);
                    _trees.Add(go);
                }
            }
        }
    } 
}