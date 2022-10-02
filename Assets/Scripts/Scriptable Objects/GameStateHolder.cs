using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game State Holder", fileName = "NewGameState")]
public class GameStateHolder : ScriptableObject
{
    public int score;
    public GameState state;
    public PivotController pivot;
}
