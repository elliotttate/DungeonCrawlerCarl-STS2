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
/// When you would die, heal to a small HP amount instead.
/// Amount determines heal amount (1 base, 10 upgraded).
/// Self-removing after trigger.
/// </summary>
public sealed class DonutsNineLivesPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override decimal ModifyHpLostBeforeOstyLate(Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target != base.Owner)
        {
            return amount;
        }
        // If this damage would kill the owner, cap it so they survive at the heal amount
        if (target.CurrentHp - amount <= 0m)
        {
            Flash();
            return target.CurrentHp - 1m; // Leave at 1 HP, then heal in AfterDamageReceived
        }
        return amount;
    }

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target != base.Owner)
        {
            return;
        }
        if (target.CurrentHp <= 1m)
        {
            decimal healTo = base.Amount;
            if (target.CurrentHp < healTo)
            {
                await CreatureCmd.Heal(target, healTo - target.CurrentHp);
            }
            await PowerCmd.Remove(this);
        }
    }
}
