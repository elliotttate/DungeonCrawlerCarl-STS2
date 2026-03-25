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
/// 1 cost Power. Start of turn gain 4 Block. Upgrade: 6 Block.
/// The brindle beast is a loyal companion. Mostly because Carl feeds it.
/// </summary>
[Pool(typeof(CarlCardPool))]
public sealed class BrindleBeastCompanion : CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new PowerVar<BrindleBeastPower>(4m)
    };

    protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
    {
        HoverTipFactory.FromPower<BrindleBeastPower>(),
        HoverTipFactory.Static(StaticHoverTip.Block)
    };

    public BrindleBeastCompanion()
        : base(1, CardType.Power, CardRarity.Common, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<BrindleBeastPower>(base.Owner.Creature, base.DynamicVars["BrindleBeastPower"].IntValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["BrindleBeastPower"].UpgradeValueBy(2m);
    }
}
