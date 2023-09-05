using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace HyperSol.Games.SpaceGame
{
    public class EnemyShip : MonoBehaviour,ShipMoverment
    {

        private EnemyShipPosAsset _asset;
        private Sequence beginSeq;
        private Sequence IdleSeq;
        
        private Vector3 defTrans;
        private void Start()
        {
            defTrans = transform.position;
            _asset = SpaceGameController.Instance.dataAsset;
        }

        public IEnumerator IShipBeginMove(float time)
        {
            foreach (var item in _asset.positionSequence[0].position)
            {
                transform.position = _asset.positionSequence[0].defaultSpawn;
                beginSeq = DOTween.Sequence();
                beginSeq.Append(transform.DOMove(item, time/0.9f))
                    .SetEase(Ease.Linear);
                yield return new WaitForSeconds(time);
            }

            if (!SpaceGameController.Instance.isCellCoroutineMoving)
            {
                StartCoroutine(SpaceGameController.Instance.IShipGridCellSetUp(time));
            }
        }

        public void OnShipIdleMoving()
        {
            DOTween.Sequence()
                .Append(transform.DOMoveY(transform.position.y + 1f, 2f))
                .Append(transform.DOMoveY(transform.position.y, 1f)).SetEase(Ease.OutCirc)
                .SetLoops(-1).Loops();
        }
        

        private void OnDisable()
        {
            IdleSeq.Kill();
            beginSeq.Kill();
            
            transform.position = defTrans;
            StopAllCoroutines();
        }

    }
}
