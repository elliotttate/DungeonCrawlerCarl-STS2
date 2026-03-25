using BaseLib.Utils;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DungeonCrawlerCarl;

/// <summary>
/// Event. Start combat: 1 random card in hand costs 0.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class KatiasLockpick : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Event;

    public override async Task BeforeCombatStartLate()
    {
        CardPile hand = PileType.Hand.GetPile(base.Owner);
        if (hand.Cards.Count > 0)
        {
            Flash();
            var playableCards = hand.Cards.Where(c => c.EnergyCost.GetWithModifiers(CostModifiers.None) > 0).ToList();
            if (playableCards.Count > 0)
            {
                CardModel card = base.Owner.RunState.Rng.CombatTargets.NextItem(playableCards);
                if (card != null)
                {
                    card.EnergyCost.SetThisTurnOrUntilPlayed(0);
                }
            }
        }
    }
}
