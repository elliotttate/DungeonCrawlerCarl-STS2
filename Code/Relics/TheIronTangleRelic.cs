using BaseLib.Utils;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.Powers;
using System.Threading.Tasks;

namespace DungeonCrawlerCarl;

/// <summary>
/// Rare. When applying debuff, +1 stack.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class TheIronTangleRelic : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    public override decimal ModifyPowerAmountGiven(PowerModel power, Creature giver, decimal amount, Creature? target, CardModel? cardSource)
    {
        if (giver != base.Owner.Creature)
        {
            return amount;
        }
        if (amount <= 0m)
        {
            return amount;
        }
        if (power.Type == PowerType.Debuff)
        {
            return amount + 1m;
        }
        return amount;
    }

    public override Task AfterModifyingPowerAmountGiven(PowerModel power)
    {
        if (power.Type == PowerType.Debuff)
        {
            Flash();
        }
        return Task.CompletedTask;
    }
}
