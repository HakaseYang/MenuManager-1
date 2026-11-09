using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    // Section 3 : generic base class that turns every menu subclass into a singleton
    // Section 4 : adds the static Open() helper so callers do not repeat the MenuManager check
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        private static T _instance;
        public static T Instance { get { return _instance; } }

        // protected means outside the class cannot see this method but derive class can
        // as subclass might require to edit the Awake() method, virtual is added to the prefix
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = (T)this;
            }
            else if (_instance != (T)this)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == (T)this)
            {
                _instance = null;
            }
        }

        public static void Open()
        {
            if (MenuManager.Instance != null && Instance != null)
            {
                MenuManager.Instance.OpenMenu(Instance);
            }
        }
    }

    [RequireComponent(typeof(Canvas))]
    public abstract class Menu : MonoBehaviour
    {
        public virtual void OnBackPressed()
        {
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.CloseMenu();
            }
        }
    }
}
