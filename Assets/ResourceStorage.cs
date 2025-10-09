using System.Collections.Generic;

namespace Economy
{
    public class ResourceStorage
    {
        private readonly Dictionary<ResourceType, int> _collectedResources;

        public ResourceStorage()
        {
            _collectedResources = new()
            {
                { ResourceType.Wheat, 0 },
                { ResourceType.Wood, 0 },
                { ResourceType.Iron, 0 }
            };
        }

        public void Add(GameResource resource)
        {
            if (resource.Amount <= 0) return;

            if (_collectedResources.ContainsKey(resource.Type))
            {
                _collectedResources[resource.Type] += resource.Amount;
            }
            else
            {
                _collectedResources[resource.Type] = resource.Amount;
            }
        }

        public bool TryConsume(GameResource resource)
        {
            if (_collectedResources.TryGetValue(resource.Type, out var current) && current >= resource.Amount)
            {
                _collectedResources[resource.Type] -= resource.Amount;
                return true;
            }
            return false;
        }

        public bool TryConsume(GameResource[] resources)
        {
            if (resources == null || resources.Length == 0)
            {
                return true;
            }

            foreach (var resource in resources)
            {
                if (_collectedResources.TryGetValue(resource.Type, out var current) && current < resource.Amount)
                {
                    return false;
                }
            }

            foreach (var resource in resources)
            {
                _collectedResources[resource.Type] -= resource.Amount;
            }

            return true;
        }

        public int GetAmount(ResourceType type)
        {
            return _collectedResources[type];
        }
    }
}