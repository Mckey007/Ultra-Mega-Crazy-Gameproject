using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _howTo;
    [SerializeField] private GameObject _options;
    [SerializeField] private GameObject _credits;
    private List<GameObject> _menus;

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
    }

    public void EnableMenu(GameObject targetMenu)
    {
        foreach(var menu in _menus)
        {
            // disable every menu
            menu.SetActive(false);
            // enable only the target menu
            if(targetMenu.Equals(menu)) menu.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
