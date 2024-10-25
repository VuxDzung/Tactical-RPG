using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevOpsGuy.GUI
{
    public class UIBehaviour : MonoBehaviour
    {
        [SerializeField]
        private CursorLockMode cursorMode;
        [SerializeField]
        private KeyCode shortcutInput;

        public KeyCode ShortcutInput => shortcutInput;
        public CursorLockMode CursorMode => cursorMode;

        protected UIManager manager;

        public virtual void Setup(UIManager manager)
        {
            this.manager = manager;
        }


        public virtual void OnShortcutPressed()
        {
            if (!gameObject.activeSelf)
                Show();
            else
                Hide();
        }

        public virtual void Show() {
            gameObject.SetActive(true);
        }

        public virtual void Hide() {
            gameObject.SetActive(false);
        }
    }
}