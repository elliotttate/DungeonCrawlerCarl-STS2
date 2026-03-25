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
/// At start of each turn, Scry N.
/// Amount = scry amount.
/// </summary>
public sealed class AiDungeonGuidePower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
    {
        if (side != base.Owner.Side)
        {
            return;
        }

        Flash();
        await CardPileCmd.Draw(choiceContext, base.Amount, base.Owner.Player);
    }
}
