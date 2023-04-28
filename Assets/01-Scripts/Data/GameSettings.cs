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
    public float EndPhysicsMovementDuration = 1f;
    public float StackChangeDuration = 1.5f;
    public float buttonPressAnimationDuration = 0.25f;
    public float buttonPressAnimationScale = 0.8f;
    public float popupAnimationDuration = 0.25f;
    
    //Network Parameters
    public string sampleURI;
    
    // Block dimensions ( based on real Jenga specifications )
    public float blockHeight = 1.5f;
    public float blockWidth = 2.5f;
    public float blockLength = 7.5f;
    
    // Stack parameters
    public float stackOffset = 20;
    
    // Camera Control Parameters
    public float orbitalCameraXSpeed = 1;
    public float orbitalCameraYSpeed = 1;
    public float orbitalCameraZUpperLimit = 40;
    public float orbitalCameraZLowerLimit = -20;
    
    // Extra
    public char descriptionSeparator;
}
