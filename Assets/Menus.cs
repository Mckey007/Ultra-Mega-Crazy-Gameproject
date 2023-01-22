using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _howTo;
    [SerializeField] private GameObject _options;
    [SerializeField] private GameObject _credits;
    private List<GameObject> _menus;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private string _nameOfMainScene;
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private AudioClip bgm;

    public void UpdateVolumeSFX()
    {
        if (sfxSlider == null) return;
        GameManager.Instance.sfxVolume = sfxSlider.value;
        SoundManager.UpdateVolumeSFX(sfxSlider.value);
    }

    public void UpdateVolumeBGM()
    {
        if (bgmSlider == null) return;
        GameManager.Instance.bgmVolume = bgmSlider.value;
        SoundManager.UpdateVolumeBGM(bgmSlider.value);
    }

    private void Start()
    {
        _menus = new List<GameObject>
        {
            _mainMenu,
            _howTo,
            _options,
            _credits
        };

        EnableMenu(_mainMenu);

        bgmSlider.value = GameManager.Instance.bgmVolume;
        sfxSlider.value = GameManager.Instance.bgmVolume;

        SoundManager.PlayBGM(bgm, bgmSlider.value);
    }

    public void EnableMenu(GameObject targetMenu)
    {
        PlayButtonSound();
        foreach (var menu in _menus)
        {
            // disable every menu
            menu.SetActive(false);
            // enable only the target menu
            if (targetMenu.Equals(menu)) menu.SetActive(true);
        }
    }

    public void PlayButtonSound()
    {
        SoundManager.PlaySoundOnce(_buttonSound);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(_nameOfMainScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
