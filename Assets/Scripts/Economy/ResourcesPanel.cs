using System.Collections.Generic;
using UnityEngine;

namespace Economy
{
    public class ResourcesPanel : MonoBehaviour
    {
        [SerializeField] ResourcesElement _wheatElement;
        [SerializeField] ResourcesElement _woodElement;
        [SerializeField] ResourcesElement _ironElement;

        private Dictionary<ResourceType, ResourcesElement> _elements = new();

        private void Awake()
        {
            _elements.Add(ResourceType.Wheat, _wheatElement);
            _elements.Add(ResourceType.Wood, _woodElement);
            _elements.Add(ResourceType.Iron, _ironElement);
        }

        private void Start()
        {
            foreach (var element in _elements)
            {
                element.Value.ResetElements();
            }
        }

        public void UpdatePanel(ResourceType resourceType, int amount)
        {
            _elements[resourceType].UpdatePresentation(amount);
        }
    }
}