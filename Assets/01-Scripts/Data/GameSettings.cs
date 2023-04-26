using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Custom/GameSettings")]
public class GameSettings : ScriptableObject
{   
    // Prefabs
    public List<GameObject> blockPrefabs;
    public GameObject stackPrefab;
    public GameObject rowPrefab;
    
    // Animation parameters
    public float EndPhysicsMovementDuration;
    
    //Network Parameters
    public string sampleURI;
    
    // Block dimensions ( based on real Jenga specifications )
    public float blockHeight = 1.5f;
    public float blockWidth = 2.5f;
    public float blockLength = 7.5f;
    
    // Stack parameters
    public float stackOffset = 20;
}
