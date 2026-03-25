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
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace DungeonCrawlerCarl;

[Pool(typeof(MegaCrit.Sts2.Core.Models.CardPools.CurseCardPool))]
public sealed class BoatmansToll : CustomCardModel
{
    public override int MaxUpgradeLevel => 0;

    public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Unplayable };

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new EnergyVar(1)
    };

    public BoatmansToll()
        : base(-1, CardType.Curse, CardRarity.Curse, TargetType.None)
    {
    }

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card == this)
        {
            // Cards don't have Flash(); just lose energy
            await PlayerCmd.LoseEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
        }
    }
}
