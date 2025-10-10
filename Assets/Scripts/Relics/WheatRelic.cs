using UnityEngine;

public class WheatRelic : RelicBase
{   
    public override ResourceProductionContext ApplyEffects(ref ResourceProductionContext productionContext)
    {
        if (!IsActive || productionContext.ProducedResource.Type != ResourceType.Wheat)
        {
            return productionContext;
        }

        int bonusAmount = productionContext.ProductionItem.Cells.Length;
        productionContext.ProducedResource.Add(bonusAmount);
        Debug.Log($"{this.GetType().Name} produce Wheat = {bonusAmount}");
        return productionContext;
    }
}
