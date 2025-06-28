using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PopupCtrl : MonoBehaviour
{
    public static PopupCtrl Instance;

    [FormerlySerializedAs("_popups")] [SerializeField] private List<BasePopup> popups=new List<BasePopup>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public T GetPopupByType<T>() where T : BasePopup
    {
        foreach (var popup in popups)
        {
            if(popup is T basePopup)
                return basePopup;
        }

        return null;
    }
}
