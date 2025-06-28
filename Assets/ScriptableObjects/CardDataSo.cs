using UnityEngine;

namespace DataSO
{
    public enum ColorType
    {
        Blue = 0,
        Green = 1,
        Orange = 2,
        Purple = 3,
        Red = 4,
        Yellow = 5
    }
    
    [CreateAssetMenu(menuName = "DataSO/CardData")]
    public class CardDataSo : ScriptableObject
    {
        public int indexCard;
        //public ColorType colorType;
        public Sprite icon;
    }
}
