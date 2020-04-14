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

    public void UpdateSlider(bool isPlayer)
    {
        _playerSlider.gameObject.SetActive(false);
        _enemySlider.gameObject.SetActive(false);

        if (isPlayer)
        {
            _playerSlider.gameObject.SetActive(true);
        }

        _enemySlider.gameObject.SetActive(true);
    }
}
