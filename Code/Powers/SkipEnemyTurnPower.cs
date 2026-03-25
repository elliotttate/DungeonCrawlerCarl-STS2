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
/// Causes the enemy turn to effectively be skipped by applying
/// massive Weak to all enemies for 1 turn. This is a simplification
/// since truly skipping turns requires deeper engine hooks.
/// </summary>
public sealed class SkipEnemyTurnPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == base.Owner.Side)
        {
            Flash();
            // Apply massive Weak to all enemies to simulate skipping their turn
            var enemies = base.Owner.CombatState.HittableEnemies;
            await PowerCmd.Apply<WeakPower>(enemies, 99m, base.Owner, null);
            await PowerCmd.Remove(this);
        }
    }
}
