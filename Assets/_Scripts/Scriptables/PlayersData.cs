using System.Collections.Generic;
using _Scripts.Scriptables;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayersData", order = 51)]
// ReSharper disable once CheckNamespace
public class PlayersData : ScriptableObject
{
    public List<HeroType> playerSpawnCardsList = new List<HeroType>();
    
    private void OnValidate()
    {
        if (playerSpawnCardsList.Count > 5)
        {
            playerSpawnCardsList = new List<HeroType>(new HeroType[5]);
        }
    }
}
