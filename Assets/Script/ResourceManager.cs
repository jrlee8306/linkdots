using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public GameObject blockPrefab;

    public static ResourceManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject LoadBlockObject( Transform transform )
    {
        return GameObject.Instantiate( blockPrefab , transform ) ; 
    }
}
