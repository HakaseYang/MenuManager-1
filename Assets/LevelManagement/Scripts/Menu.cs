using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleGame;

namespace LevelManagement
{
    [RequireComponent(typeof(Canvas))]
    public class Menu : MonoBehaviour
    {
        // 任務三：使用 GameManager 單例載入下一關
        public void OnPlayPressed()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoadNextLevel();
            }
        }

        // 修改後的設定選單按鈕事件
        public void OnSettingsPressed() 
        {
            if (MenuManager.Instance != null && transform.parent != null)
            {
                // 從同一個父物件下，找出名稱包含 Settings 的選單實例
                Menu settingsMenu = FindMenuInParent("Settings");
                if (settingsMenu != null)
                {
                    MenuManager.Instance.OpenMenu(settingsMenu);
                }
            }
        }

        // 任務二：修改為使用 MenuManager 單例呼叫
        public void OnCreditsPressed()
        {
            if (MenuManager.Instance != null && transform.parent != null)
            {
                // 從同一個父物件下，找出名稱包含 Credits 的選單實例
                Menu creditsScreen = FindMenuInParent("Credits");
                if (creditsScreen != null)
                {
                    MenuManager.Instance.OpenMenu(creditsScreen);
                }
            }
        }

        // 任務二：修改為使用 MenuManager 單例呼叫
        public void OnBackPressed()
        {
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.CloseMenu();
            }
        }

        // ======= 請確保這個方法留在 Menu 類別的大括號之內 =======
        private Menu FindMenuInParent(string menuNameKey)
        {
            Transform parentTransform = transform.parent;
            if (parentTransform == null) return null;

            foreach (Transform child in parentTransform)
            {
                if (child.name.Contains(menuNameKey))
                {
                    Menu menuComponent = child.GetComponent<Menu>();
                    if (menuComponent != null)
                    {
                        return menuComponent;
                    }
                }
            }
            
            Debug.LogWarning($"在父物件下找不到包含 {menuNameKey} 的選單。");
            return null;
        }
    }
}