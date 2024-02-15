using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawnPickUp : MonoBehaviour
{
    public GameObject objectPrefab;

    public abstract void Interact(Transform playerPickup);
    
}
