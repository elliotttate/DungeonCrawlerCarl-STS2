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
/// 1 cost Power. On enemy death, gain 1 Rating. Upgrade: also draw 1 card.
/// Amount 1 = base (Rating only), Amount 2 = upgraded (Rating + draw).
/// A little friend that sniffs out the good stuff.
/// </summary>
[Pool(typeof(CarlCardPool))]
public sealed class LootGoblin : CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new PowerVar<LootGoblinPower>(1m)
    };

    protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
    {
        HoverTipFactory.FromPower<LootGoblinPower>(),
        HoverTipFactory.FromPower<RatingsPower>()
    };

    public LootGoblin()
        : base(1, CardType.Power, CardRarity.Common, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<LootGoblinPower>(base.Owner.Creature, base.DynamicVars["LootGoblinPower"].IntValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        // Upgrade from 1 to 2: signals the power to also draw cards
        base.DynamicVars["LootGoblinPower"].UpgradeValueBy(1m);
    }
}
