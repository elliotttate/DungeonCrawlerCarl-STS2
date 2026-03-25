using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models.CardPools;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models.Cards;

namespace DungeonCrawlerCarl;

[Pool(typeof(MegaCrit.Sts2.Core.Models.CardPools.CurseCardPool))]
public sealed class TheCzarsGaze : CustomCardModel
{
    public override int MaxUpgradeLevel => 0;

    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
    {
        CardKeyword.Eternal,
        CardKeyword.Unplayable,
        CardKeyword.Retain
    };

    public TheCzarsGaze()
        : base(-1, CardType.Curse, CardRarity.Curse, TargetType.None)
    {
    }
}
