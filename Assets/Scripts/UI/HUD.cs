using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private Slider _playerSlider;

    [SerializeField]
    private Slider _enemySlider;

    [SerializeField]
    private Image _arrow;

    public void EnableArrow(bool isEnable)
    {
        _arrow.gameObject.SetActive(isEnable);
    }

    public void EnableSlider(bool isPlayer)
    {
        _playerSlider.gameObject.SetActive(false);
        _enemySlider.gameObject.SetActive(false);

        if (isPlayer)
        {
            _playerSlider.gameObject.SetActive(true);

            return;
        }

        _enemySlider.gameObject.SetActive(true);
    }

    public void UpdateSlider(int value)
    {
        _playerSlider.value = value;
        _enemySlider.value = value;
    }
}
