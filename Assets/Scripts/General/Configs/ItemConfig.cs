using Inventory;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Scriptable Objects/ItemConfig")]
public class ItemConfig : ScriptableObject
{
    [field: SerializeField] public ItemTypeData[] ItemTypeDatas { get; private set; }
    [field: SerializeField] public ItemProductionView ItemProdViewPF { get; private set; }
    [field: SerializeField] public CollectPopup CollectPopupPF { get; private set; }
    [field: SerializeField] public float PopupFloatSpeed { get; private set; }
    [field: SerializeField] public float PopupShowTime { get; private set; }
}

[Serializable]
public struct ItemTypeData
{
    [field: SerializeField] public ResourceType ResourceType { get; private set; }
    [field: SerializeField] public Sprite ResourceSprite { get; private set; }
}