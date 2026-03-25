using BaseLib.Utils;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace DungeonCrawlerCarl;

/// <summary>
/// Uncommon. Gain 1 extra Rating when gaining Rating.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class StudioAudienceBadge : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    public override decimal ModifyPowerAmountGiven(PowerModel power, Creature giver, decimal amount, Creature? target, CardModel? cardSource)
    {
        if (power is RatingsPower && giver == base.Owner.Creature && amount > 0m)
        {
            return amount + 1m;
        }
        return amount;
    }

    public override System.Threading.Tasks.Task AfterModifyingPowerAmountGiven(PowerModel power)
    {
        if (power is RatingsPower)
        {
            Flash();
        }
        return System.Threading.Tasks.Task.CompletedTask;
    }
}
