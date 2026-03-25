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
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DungeonCrawlerCarl;

/// <summary>
/// 1 cost Power. Whenever you play 3 cards in a turn, gain 3 Block. Upgrade: 4 Block.
/// The viewers love a good combo. Give them what they want.
/// </summary>
[Pool(typeof(CarlCardPool))]
public sealed class ViewerFavorite : CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new PowerVar<ViewerFavoritePower>(3m)
    };

    protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
    {
        HoverTipFactory.FromPower<ViewerFavoritePower>(),
        HoverTipFactory.Static(StaticHoverTip.Block)
    };

    public ViewerFavorite()
        : base(1, CardType.Power, CardRarity.Common, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<ViewerFavoritePower>(base.Owner.Creature, base.DynamicVars["ViewerFavoritePower"].IntValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["ViewerFavoritePower"].UpgradeValueBy(1m);
    }
}
