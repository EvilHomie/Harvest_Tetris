using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Scriptable Objects/Config")]
public class Config : ScriptableObject
{
    //[field: SerializeField] public TileModifierData[] TileModifierDatas { get; private set; }
}

//[Serializable]
//public struct TileModifierData
//{
//    [field: SerializeField] public TileModifierType TileType {  get; private set; }
//    [field: SerializeField] public float Efficiency { get; private set; }
//}