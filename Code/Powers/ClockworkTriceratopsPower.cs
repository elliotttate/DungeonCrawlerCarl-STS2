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
/// At end of turn, deal damage to lowest HP enemy.
/// Amount = power stack amount.
/// </summary>
public sealed class ClockworkTriceratopsPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != base.Owner.Side)
        {
            return;
        }

        IReadOnlyList<Creature> enemies = base.Owner.CombatState.HittableEnemies;
        if (enemies.Count == 0)
        {
            return;
        }

        Creature lowestHp = enemies[0];
        foreach (var enemy in enemies)
        {
            if (enemy.CurrentHp < lowestHp.CurrentHp)
            {
                lowestHp = enemy;
            }
        }

        Flash();
        await CreatureCmd.Damage(choiceContext, lowestHp, base.Amount, ValueProp.Unpowered, base.Owner, null);
    }
}
