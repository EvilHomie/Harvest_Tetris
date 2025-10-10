using System.Collections.Generic;
using UnityEngine;

namespace Economy
{
    public class ResourcesPanel : MonoBehaviour
    {
        [SerializeField] ResourcesElement _wheatElement;
        [SerializeField] ResourcesElement _woodElement;
        [SerializeField] ResourcesElement _ironElement;

        private Dictionary<ResourceType, ResourcesElement> _elements;

        public void Init()
        {
            _elements = new()
            {
                { ResourceType.Wheat, _wheatElement },
                { ResourceType.Wood, _woodElement },
                { ResourceType.Iron, _ironElement }
            };

            foreach (var element in _elements)
            {
                element.Value.ResetElements();
            }
        }

        public void UpdatePanel(ResourceType resourceType, int amount, bool isAdded)
        {
            _elements[resourceType].UpdatePresentation(amount, isAdded);
        }

        public void UpdateTimers(float tickTime)
        {
            foreach (var element in _elements)
            {
                element.Value.UpdateTimers(tickTime);
            }
        }
    }
}