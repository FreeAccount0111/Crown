using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PopupWin : BasePopup
    {
        [SerializeField] private Button btnReplay, btnHome, btnNext;
        [SerializeField] private Text txtCurrentScore, txtBestScore;

        private void Awake()
        {
            btnReplay.onClick.AddListener(() =>
            {
                CircleOutline.Instance.ScaleIn(() =>
                {
                    HideImmediately(true);
                    PopupCtrl.Instance.GetPopupByType<PopupGameplay>().ShowImmediately(false);
                    SceneManager.LoadScene("Gameplay");
                    CircleOutline.Instance.ScaleOut();
                });
            });

            btnHome.onClick.AddListener(() =>
            {
                CircleOutline.Instance.ScaleIn(() =>
                {
                    HideImmediately(true);
                    PopupCtrl.Instance.GetPopupByType<PopupHome>().ShowImmediately(false);
                    CircleOutline.Instance.ScaleOut();
                });
            });
            
            btnNext.onClick.AddListener(() =>
            {
                CircleOutline.Instance.ScaleIn(() =>
                {
                    HideImmediately(true);
                    PopupCtrl.Instance.GetPopupByType<PopupGameplay>().ShowImmediately(false);
                    SceneManager.LoadScene("Gameplay");
                    CircleOutline.Instance.ScaleOut();
                });
            });
        }

        public void UpdateSore(int amount)
        {
            txtCurrentScore.text = amount.ToString();   
        }
    }
}
