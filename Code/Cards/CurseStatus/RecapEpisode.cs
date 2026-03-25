using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models.CardPools;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;

namespace DungeonCrawlerCarl;

[Pool(typeof(MegaCrit.Sts2.Core.Models.CardPools.StatusCardPool))]
public sealed class RecapEpisode : CustomCardModel
{
    public override int MaxUpgradeLevel => 0;

    public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust };

    public RecapEpisode()
        : base(1, CardType.Status, CardRarity.Status, TargetType.None)
    {
    }

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // Does nothing when played, just exhausts
        return Task.CompletedTask;
    }
}
