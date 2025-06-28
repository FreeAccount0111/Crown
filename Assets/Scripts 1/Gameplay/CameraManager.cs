using UnityEngine;

namespace Scripts_1.Gameplay
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance;
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
    }
}
