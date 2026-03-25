using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DungeonCrawlerCarl;

/// <summary>
/// At start of turn, if you have enough Ratings, gain 1 Energy.
/// Amount = threshold (lowered by upgrade).
/// </summary>
public sealed class StudioAudiencePower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
    {
        if (side != base.Owner.Side)
        {
            return;
        }

        int ratings = base.Owner.GetPowerAmount<RatingsPower>();
        if (ratings >= base.Amount)
        {
            Flash();
            await PlayerCmd.GainEnergy(1m, base.Owner.Player);
        }
    }
}
