using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace DungeonCrawlerCarl;

/// <summary>
/// At start of turn, deal damage to a random enemy.
/// Amount = power stack amount.
/// </summary>
public sealed class MobBossPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
    {
        if (side != base.Owner.Side)
        {
            return;
        }

        IReadOnlyList<Creature> enemies = combatState.HittableEnemies;
        if (enemies.Count == 0)
        {
            return;
        }

        Creature target = base.Owner.Player.RunState.Rng.CombatTargets.NextItem(enemies);
        if (target != null)
        {
            Flash();
            await CreatureCmd.Damage(choiceContext, target, base.Amount, ValueProp.Unpowered, base.Owner, null);
        }
    }
}
