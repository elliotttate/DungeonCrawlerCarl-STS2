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
/// Whenever you take unblocked damage, gain 1 Strength.
/// Upgraded: also gain 1 Rating.
/// Amount >= 2 means upgraded version.
/// </summary>
public sealed class CarlSpitePower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target != base.Owner)
        {
            return;
        }
        if (result.WasFullyBlocked)
        {
            return;
        }

        Flash();
        await PowerCmd.Apply<StrengthPower>(base.Owner, 1m, base.Owner, null);
        if (base.Amount >= 2)
        {
            await PowerCmd.Apply<RatingsPower>(base.Owner, 1m, base.Owner, null);
        }
    }
}
