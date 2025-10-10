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

            _collectedResources[resource.Type] += resource.Amount;
        }

        public void Add(GameResource[] resources)
        {
            foreach (var resource in resources)
            {
                Add(resource);
            }
        }

        public bool HasEnoughResources(GameResource resource)
        {
            if (_collectedResources.TryGetValue(resource.Type, out var current) && current >= resource.Amount)
            {
                return true;
            }

            return false;
        }

        public void Consume(GameResource resource)
        {
            _collectedResources[resource.Type] -= resource.Amount;
        }

        public bool HasEnoughResources(GameResource[] resources)
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

            return true;
        }

        public void Consume(GameResource[] resources)
        {
            foreach (var resource in resources)
            {
                _collectedResources[resource.Type] -= resource.Amount;
            }
        }

        public int GetAmount(ResourceType type)
        {
            return _collectedResources[type];
        }

        public ResourceType GetLowestResourceType()
        {
            ResourceType type = default; 
            int lowestValue = int.MaxValue;

            foreach (var resource in _collectedResources)
            {
                if (resource.Value < lowestValue)
                {
                    lowestValue = resource.Value;
                    type = resource.Key;
                }
            }

            return type;
        }
    }
}