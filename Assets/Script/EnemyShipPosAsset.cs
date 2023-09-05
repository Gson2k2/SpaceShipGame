using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HyperSol/Game/SpaceShipGame",fileName = "SpaceShipDataAsset",order = 1)]
public class EnemyShipPosAsset : ScriptableObject
{
    [Serializable]
    public class PositionSequence
    {
        public Vector3 defaultSpawn;
        public Vector3[] position;
    }

    [Serializable]
    public class IdlePositionSequence
    {
        [Range(1,6)]
        public int HorizontalCell;
        [Range(1,6)]
        public int VerticalCell;
    }

    public IdlePositionSequence[] cellPositionSequences;
    public PositionSequence[] positionSequence;
}
