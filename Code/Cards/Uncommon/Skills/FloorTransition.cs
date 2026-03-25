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
using MegaCrit.Sts2.Core.Models.Cards;

namespace DungeonCrawlerCarl;

[Pool(typeof(CarlCardPool))]
public sealed class FloorTransition : CustomCardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust };

    public FloorTransition()
        : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        CardPile discardPile = PileType.Discard.GetPile(base.Owner);
        var cardsToShuffle = discardPile.Cards.ToList();
        foreach (var card in cardsToShuffle)
        {
            await CardPileCmd.Add(card, PileType.Draw, CardPilePosition.Random);
        }
        if (base.IsUpgraded)
        {
            await CardPileCmd.Draw(choiceContext, 2m, base.Owner);
        }
    }
}
