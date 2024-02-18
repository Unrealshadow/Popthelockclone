using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dotsRemainingTm;
    [SerializeField] private GameObject lockHat;
    [SerializeField] private GameObject lockParent;
    public DataHolder dataHolder;

    private void Start()
    {
        EventManager.instance.AddListener<DotMissEvent>(DotMissAnimation);
        EventManager.instance.AddListener<LevelWinEvent>(LevelWinAnimation);
    }

    private void OnDestroy()
    {
        EventManager.instance.RemoveListener<DotMissEvent>(DotMissAnimation);
        EventManager.instance.RemoveListener<LevelWinEvent>(LevelWinAnimation);
    }

    private void DotMissAnimation(object obj)
    {
        Sequence dotMissSeq = DOTween.Sequence();

        dotMissSeq.Append(dotsRemainingTm.transform.DOScale(0, 0.25f));
        dotMissSeq.Append(lockParent.transform.DOLocalRotate(new Vector3(0, 0, -15f), 0.25f));
        dotMissSeq.Append(lockParent.transform.DOLocalRotate(new Vector3(0, 0, 25f), 0.25f));
        dotMissSeq.Append(lockParent.transform.DOLocalRotate(new Vector3(0, 0, -20f), 0.25f));
        dotMissSeq.Append(lockParent.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.25f));
        dotMissSeq.Append(dotsRemainingTm.transform.DOScale(1, 0.25f));

        dotMissSeq.OnComplete(() =>
        {
            dataHolder.ResetLevel();
            EventManager.instance.TriggerEvent(new PaddleResetEvent());
        });
        dotMissSeq.Play();
    }

    private void LevelWinAnimation(object obj)
    {
        Sequence levelWinSeq = DOTween.Sequence();

        levelWinSeq.Append(dotsRemainingTm.transform.DOScale(0, 0.25f));
        levelWinSeq.Append(lockHat.transform.DOMoveY(3.25f, 0.5f));
        levelWinSeq.Append(lockParent.transform.DOMoveX(-6f, 0.25f).OnComplete(() =>
        {
            lockParent.transform.position = new Vector3(6f, 0, 0);
        }));
        levelWinSeq.Append(lockHat.transform.DOMoveY(2.5f, 0.25f).OnComplete(() =>
        {
            EventManager.instance.TriggerEvent(new PaddleResetEvent());
        }));
        levelWinSeq.Append(lockParent.transform.DOMoveX(0, 0.25f));
        levelWinSeq.Append(dotsRemainingTm.transform.DOScale(1, 0.25f));
        levelWinSeq.OnComplete(() =>
        {
            EventManager.instance.TriggerEvent(new DotHitEvent());
        });
        levelWinSeq.Play();
    }
}