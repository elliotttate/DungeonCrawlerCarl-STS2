using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace DungeonCrawlerCarl;

/// <summary>
/// At end of combat, gain Gold = multiplier * Ratings.
/// Amount = gold multiplier.
/// </summary>
public sealed class CarlsHighlightReelPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCombatEnd(CombatRoom combatRoom)
    {
        int ratings = base.Owner.GetPowerAmount<RatingsPower>();
        if (ratings > 0)
        {
            int gold = (int)(base.Amount * ratings);
            if (gold > 0)
            {
                Flash();
                await PlayerCmd.GainGold(gold, base.Owner.Player);
            }
        }
    }
}
