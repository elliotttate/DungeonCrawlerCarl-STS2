using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace DungeonCrawlerCarl;

/// <summary>
/// Whenever you gain Rating, draw 1 card. Upgraded version: also gain 2 Block.
/// Amount tracks how many cards to draw (always 1 base).
/// </summary>
public sealed class FanMailPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (amount <= 0m)
        {
            return;
        }
        if (applier != base.Owner && power.Owner != base.Owner)
        {
            return;
        }
        if (power is RatingsPower && power.Owner == base.Owner)
        {
            Flash();
            await CardPileCmd.Draw(new BlockingPlayerChoiceContext(), 1m, base.Owner.Player);
            if (base.Amount >= 2)
            {
                await CreatureCmd.GainBlock(base.Owner, 2m, ValueProp.Unpowered, null);
            }
        }
    }
}
