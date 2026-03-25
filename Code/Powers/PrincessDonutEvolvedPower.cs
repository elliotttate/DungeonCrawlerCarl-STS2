using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DungeonCrawlerCarl;

/// <summary>
/// Whenever a Donut card is played, play it again.
/// Upgraded: also gain 1 Rating per trigger.
/// Amount >= 2 means upgraded.
/// </summary>
public sealed class PrincessDonutEvolvedPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != base.Owner)
        {
            return;
        }

        // Check if it's a "Donut" card by name
        string cardName = cardPlay.Card.GetType().Name;
        if (!cardName.Contains("Donut") && !cardName.Contains("PrincessDonut"))
        {
            return;
        }

        Flash();
        // Create a dupe and auto-play it
        CardModel copy = cardPlay.Card.CreateDupe();
        await CardCmd.AutoPlay(context, copy, cardPlay.Target);

        if (base.Amount >= 2)
        {
            await PowerCmd.Apply<RatingsPower>(base.Owner, 1m, base.Owner, null);
        }
    }
}
