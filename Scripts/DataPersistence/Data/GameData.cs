using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    public int currentScore;

    public Vector2 playerPosition;
    public SerializableDictionary<string, bool> checkpointReached;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData(){
        this.currentScore = 0;
        playerPosition = new Vector2(0,-6);
        checkpointReached = new SerializableDictionary<string, bool>();
    }
}
