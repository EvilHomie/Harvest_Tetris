using Inventory;
using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Scriptable Objects/ItemConfig")]
public class ItemConfig : ScriptableObject
{
    [field: SerializeField] public ResourceVisualSet ResourceVisualSet { get; private set; }
    [field: SerializeField] public ItemProductionView ItemProdViewPF { get; private set; }
    [field: SerializeField] public CollectPopup CollectPopupPF { get; private set; }
    [field: SerializeField] public float PopupFloatSpeed { get; private set; }
    [field: SerializeField] public float PopupShowTime { get; private set; }
}

[Serializable]
public struct ResourceVisualSet
{
    [field: SerializeField] public ResourceVisual[] ResourceVisuals { get; private set; }

    public readonly Sprite GetSprite(ResourceType resourceType)
    {
        return ResourceVisuals.First(visual => visual.ResourceType == resourceType).Sprite;
    }
}

[Serializable]
public struct ResourceVisual
{
    [field: SerializeField] public ResourceType ResourceType { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}