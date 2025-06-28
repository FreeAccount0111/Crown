using Loading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PopupHome : BasePopup
    {
        [SerializeField] private Button btnPlay, btnGuid;

        private void Awake()
        {
            btnPlay.onClick.AddListener(() =>
            {
                CircleOutline.Instance.ScaleIn(() =>
                {
                    HideImmediately(true);
                    PopupCtrl.Instance.GetPopupByType<PopupGameplay>().ShowImmediately(false);
                    SceneManager.LoadScene("Gameplay");
                    CircleOutline.Instance.ScaleOut();
                });
            });

            btnGuid.onClick.AddListener(() =>
            {
                PopupCtrl.Instance.GetPopupByType<PopupGuid>().ShowImmediately(false);
            });
        }
    }
}
