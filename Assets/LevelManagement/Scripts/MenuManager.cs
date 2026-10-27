using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class MenuManager : MonoBehaviour
    {
        public Menu mainMenuPrefab;
        public Menu settingMenuPrefab;
        public Menu creditsScreenPrefab;

        [SerializeField]
        private Transform _menuParent;

        private Stack<Menu> _menuStack = new Stack<Menu>();

        private static MenuManager _instance;
        public static MenuManager Instance
        {
            get{ return _instance;}
        }

        private void Awake()
        {
            if(_instance != null)
            {
                Destroy(gameObject);
            } else
            {
                _instance = this;
                IntializeMenu();
            }
        }

        private void OnDestroy()
        {
            if(_instance == this)
            {
                _instance = null;
            }
        }

        private void IntializeMenu()
        {
            if(_menuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menus");
                _menuParent = menuParentObject.transform;
            }

            Menu[] menuPrefabs = {mainMenuPrefab, settingMenuPrefab, creditsScreenPrefab};

            foreach (Menu prefab in menuPrefabs)
            {
                Menu menuInstance = Instantiate(prefab, _menuParent);
                if(prefab != mainMenuPrefab)
                {
                    menuInstance.gameObject.SetActive(false);
                }else
                {
                    OpenMenu(menuInstance);
                }
            }
        }

        public void OpenMenu(Menu menuInstance)
        {
            if(menuInstance == null)
            {
                Debug.LogWarning("MENU MANAGER OpenMenu ERROR: invalid menu");
                return;
            }

            if(_menuStack.Count > 0)
            {
                foreach (Menu menu in _menuStack)
                {
                    menu.gameObject.SetActive(false);
                }
            }
            menuInstance.gameObject.SetActive(true);
            _menuStack.Push(menuInstance);

        }

        public void CloseMenu()
        {
            if(_menuStack == null)
            {
                Debug.LogWarning("MENU MANAGER CloseMenu ERROR: invalid menu");
                return;
            }
            Menu topMenu = _menuStack.Pop();
            topMenu.gameObject.SetActive(false);

            //Check the top element in stack of the stack is not empty
            if (_menuStack.Count > 0)
            {
                Menu nextMenu = _menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }
        }


    }
}
