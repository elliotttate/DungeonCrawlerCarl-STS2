using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace DungeonCrawlerCarl;

/// <summary>
/// When you play a card that costs 2+, deal damage to ALL enemies.
/// Amount = power stack amount.
/// </summary>
public sealed class CatClassPrimaDonnaPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != base.Owner)
        {
            return;
        }
        if (cardPlay.Resources.EnergyValue >= 2)
        {
            Flash();
            await CreatureCmd.Damage(context, base.Owner.CombatState.HittableEnemies, base.Amount, ValueProp.Unpowered, base.Owner, null);
        }
    }
}
