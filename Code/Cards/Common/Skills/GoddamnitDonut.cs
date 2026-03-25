using BaseLib;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DungeonCrawlerCarl;

/// <summary>
/// 0 cost. Draw 3, Exhaust 1 random card from hand. Upgrade: Draw 4.
/// Goddamnit, Donut! Not again!
/// </summary>
[Pool(typeof(CarlCardPool))]
public sealed class GoddamnitDonut : CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new CardsVar(3)
    };

    protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
    {
        HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
    };

    public GoddamnitDonut()
        : base(0, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
        // Exhaust 1 random card from hand
        CardPile hand = PileType.Hand.GetPile(base.Owner);
        List<CardModel> handCards = hand.Cards.ToList();
        if (handCards.Count > 0)
        {
            CardModel randomCard = base.Owner.RunState.Rng.CombatTargets.NextItem(handCards);
            if (randomCard != null)
            {
                await CardCmd.Exhaust(choiceContext, randomCard);
            }
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(1m);
    }
}
