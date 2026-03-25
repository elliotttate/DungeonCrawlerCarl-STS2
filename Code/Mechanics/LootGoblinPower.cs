using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace DungeonCrawlerCarl;

/// <summary>
/// On enemy death, gain 1 Rating. If Amount >= 2, also draw 1 card.
/// Amount represents the power level: 1 = base, 2+ = upgraded (also draws).
/// The loot goblin scurries over the corpses.
/// </summary>
public sealed class LootGoblinPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
    {
        HoverTipFactory.FromPower<RatingsPower>()
    };

    public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature target, bool wasRemovalPrevented, float deathAnimLength)
    {
        if (target.Side != base.Owner.Side && !wasRemovalPrevented)
        {
            Flash();
            await PowerCmd.Apply<RatingsPower>(base.Owner, 1, base.Owner, null);

            // Upgraded: Amount >= 2 means also draw a card
            if (base.Amount >= 2)
            {
                await CardPileCmd.Draw(choiceContext, 1, base.Owner.Player);
            }
        }
    }
}
