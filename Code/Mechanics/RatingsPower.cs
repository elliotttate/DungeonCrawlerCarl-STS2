using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DungeonCrawlerCarl;

/// <summary>
/// Tracks Carl's Ratings resource. Ratings are a buff counter that accumulates
/// during combat and can be consumed or referenced by other cards and effects.
/// </summary>
public sealed class RatingsPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        // Hook point for cards that care about card play counts (e.g., ViewerFavorite).
        // The power itself just tracks Ratings; specific triggers are on individual cards/powers.
        await Task.CompletedTask;
    }

    public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        // Hook point for end-of-turn effects that reference Ratings.
        // Ratings persist across turns within a combat (natural power behavior).
        return Task.CompletedTask;
    }
}
