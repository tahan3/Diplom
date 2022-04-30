using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform mainCanvasTransform;

        [Header("Panels")] 
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        private bool gameOverStatus;

        public virtual void SetGameOver(bool winStatus)
        {
            if (gameOverStatus)
            {
                return;
            }
            
            gameOverStatus = !gameOverStatus;
                
            Instantiate(winStatus ? winPanel : losePanel, mainCanvasTransform);
        }
    }
}
