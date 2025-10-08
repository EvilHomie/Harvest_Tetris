using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GetRelicArea : MonoBehaviour
{
    [field:SerializeField] public CostArea CostArea { get; private set; }
    public RelicBase NextRelic { get; private set; }

    [SerializeField] public Button _getButton;
    [SerializeField] RectTransform _nextRelicArea;
    [SerializeField] TextMeshProUGUI _discriptionText;
    [SerializeField] TextMeshProUGUI _getButtonText;
    private readonly string _getRelicText = "Get New Relic";
    private readonly string _getNewItemText = "Get New Item";  

    public void ShowNewRandomRelic(List<RelicBase> relics)
    {
        int randomIndex = Random.Range(0, relics.Count);
        var relic = relics[randomIndex];
        _discriptionText.text = relic.Discription;
        relic.transform.SetParent(_nextRelicArea, false);
        relic.transform.localPosition = Vector3.zero;
        relic.gameObject.SetActive(true);
        NextRelic = relic;
        relics.RemoveAt(randomIndex);
    }

    public void ConfigureForRelic(UnityAction onGetAction)
    {
        _getButtonText.text = _getRelicText;
        _discriptionText.gameObject.SetActive(true);
        _nextRelicArea.gameObject.SetActive(true);
        _getButton.onClick.RemoveAllListeners();
        _getButton.onClick.AddListener(onGetAction);
    }

    public void ConfigureForItem(UnityAction onGetAction)
    {
        _getButtonText.text = _getNewItemText;
        _discriptionText.transform.parent.gameObject.SetActive(false);
        _nextRelicArea.gameObject.SetActive(false);
        _getButton.onClick.RemoveAllListeners();
        _getButton.onClick.AddListener(onGetAction);
    }
}