using UnityEngine;

public class ForestRelic : RelicBase
{    
    public override ResourceProductionContext ApplyEffects(ref ResourceProductionContext productionContext)
    {
        if (!IsActive || productionContext.ProducedResource.Type != ResourceType.Wood)
        {
            return productionContext;
        }

        bool result = Random.value < 0.5f;

        if (result)
        {
            var bonusRes = new GameResource(ResourceType.Wheat, productionContext.ProducedResource.Amount);
            productionContext.BonusResources.Add(bonusRes);
            Debug.Log($"{this.GetType().Name} produce Wheat = {bonusRes.Amount}");
        }

        return productionContext;
    }
}
