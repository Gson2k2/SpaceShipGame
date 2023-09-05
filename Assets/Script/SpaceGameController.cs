using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace HyperSol.Games.SpaceGame
{
    public class SpaceGameController : MonoBehaviour
    {
        public static SpaceGameController Instance;
        [SerializeField] private EnemyShip[] _enemyShips;
        [SerializeField] private SpriteRenderer bg;

        [HideInInspector] public bool isCellCoroutineMoving;
        
        public EnemyShipPosAsset dataAsset;
        
        private int shipIndex;
        private Material bgMat;
        private float bgOffset;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            bgMat = bg.material;
            StartCoroutine(IShipStart(0.5f));
        }

        IEnumerator IShipStart(float time)
        {
            foreach (var item in _enemyShips)
            {
                StartCoroutine(item.IShipBeginMove(time));
                yield return new WaitForSeconds(time);
            }
            
            StopCoroutine(IShipStart(0));
        }

        public IEnumerator IShipGridCellSetUp(float time)
        {
            isCellCoroutineMoving = true;
            for (int i = dataAsset.cellPositionSequences[0].VerticalCell - 1; i >= 0; i--)
            {
                for (int j = dataAsset.cellPositionSequences[0].HorizontalCell - 1; j >= 0; j--)
                {
                    if (shipIndex <= _enemyShips.Length)
                    {
                        _enemyShips[shipIndex].transform.DOMove(new Vector3(j-1.5f, i, 0),time);
                        shipIndex++;
                    }
                    yield return new WaitForSeconds(time);
                }
            }
            foreach (var item in _enemyShips)
            {
                item.OnShipIdleMoving();
            }
            StopCoroutine(IShipGridCellSetUp(0f));
        }

        private void Update()
        {
            bgOffset += (Time.deltaTime * 0.5f) / 10f;
            bgMat.SetTextureOffset("_MainTex",new Vector2(bgOffset,0));
        }
    }

    public interface ShipMoverment
    {
        public IEnumerator IShipBeginMove(float time);
        public void OnShipIdleMoving();
    }

}
