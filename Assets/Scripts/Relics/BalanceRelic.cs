using UnityEngine;

public class BalanceRelic : RelicBase
{
    public override ResourceProductionContext ApplyEffects(ref ResourceProductionContext productionContext)
    {
        if (!IsActive)
        {
            return productionContext;
        }

        ResourceType smallestResType = productionContext.ResourceStorage.GetLowestResourceType();

        if (productionContext.ProducedResource.Type == smallestResType)
        {
            productionContext.ProducedResource.Add(1);
        }
        else
        {
            productionContext.AddBonusResource(smallestResType, 1);
        }

        Debug.Log($"{GetType().Name} produce {smallestResType} = {1}");
        return productionContext;
    }
}
