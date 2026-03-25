using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace DungeonCrawlerCarl;

/// <summary>
/// Whenever you play a card, deal damage to a random enemy.
/// Amount = damage per trigger.
/// </summary>
public sealed class CrawlOrDiePower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != base.Owner)
        {
            return;
        }

        IReadOnlyList<Creature> enemies = base.Owner.CombatState.HittableEnemies;
        if (enemies.Count == 0)
        {
            return;
        }

        Creature target = base.Owner.Player.RunState.Rng.CombatTargets.NextItem(enemies);
        if (target != null)
        {
            Flash();
            await CreatureCmd.Damage(context, target, base.Amount, ValueProp.Unpowered, base.Owner, null);
        }
    }
}
