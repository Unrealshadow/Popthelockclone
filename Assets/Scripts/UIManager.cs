using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class UIManager : MonoBehaviour
{
    public DataHolder dataHolder;
    [SerializeField] private Button startBtn;
    [SerializeField] private TextMeshProUGUI levelTm;
    [SerializeField] private TextMeshProUGUI starTm;
    [SerializeField] private TextMeshProUGUI dotsRemainingTm;

    private void Start()
    {
        UpdateAll();
        startBtn.onClick.AddListener(OnClickStartBtn);
        EventManager.instance.AddListener<DotMissEvent>(EnableStartBtn);
        EventManager.instance.AddListener<DotHitEvent>(UpdateDotsRemainingTm);
        EventManager.instance.AddListener<LevelWinEvent>(EnableStartBtn);
        EventManager.instance.AddListener<LevelWinEvent>(UpdateAll);
    }

    private void OnDestroy()
    {
        EventManager.instance.RemoveListener<DotMissEvent>(EnableStartBtn);
        EventManager.instance.RemoveListener<DotHitEvent>(UpdateDotsRemainingTm);
        EventManager.instance.RemoveListener<LevelWinEvent>(EnableStartBtn);
        EventManager.instance.RemoveListener<LevelWinEvent>(UpdateAll);
    }

    private void UpdateLevelTm()
    {
        levelTm.text = $"level : {dataHolder.currentLevel}";
    }

    private void UpdateStarTm()
    {
        starTm.text = $"stars : {dataHolder.stars}";
    }

    private void UpdateDotsRemainingTm(object obj = null)
    {
        dotsRemainingTm.text = dataHolder.dotsRemaining.ToString();
    }

    private void UpdateAll(object obj = null)
    {
        dataHolder.ResetLevel();
        UpdateLevelTm();
        UpdateStarTm();
        UpdateDotsRemainingTm();
    }


    private void EnableStartBtn(object obj)
    {
        startBtn.transform.DOScale(Vector3.one, 1f).OnComplete(() => { startBtn.gameObject.SetActive(true); });
    }

    private void OnClickStartBtn()
    {
        dataHolder.StartGame();
        startBtn.gameObject.SetActive(false);
    }
}