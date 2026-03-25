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
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace DungeonCrawlerCarl;

/// <summary>
/// 2 cost. Gain 16 Block. Upgrade: 20 Block.
/// Carl huddles inside whatever shell he can find.
/// </summary>
[Pool(typeof(CarlCardPool))]
public sealed class ProtectiveShell : CustomCardModel
{
    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new BlockVar(16m, ValueProp.Move)
    };

    public ProtectiveShell()
        : base(2, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Block.UpgradeValueBy(4m);
    }
}
