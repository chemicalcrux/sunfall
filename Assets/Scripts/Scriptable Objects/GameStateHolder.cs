using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game State Holder", fileName = "NewGameState")]
public class GameStateHolder : ScriptableObject
{
    public GameState state;
}
