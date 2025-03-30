using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public string Name; //이름
    public string Tooltip; //설명
    public Sprite image; //이미지
}
