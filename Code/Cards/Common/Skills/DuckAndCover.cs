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
/// 1 cost. Gain 8 Block (12 if you have 0 Block). Upgrade: 10/16.
/// Duck! And also cover!
/// </summary>
[Pool(typeof(CarlCardPool))]
public sealed class DuckAndCover : CustomCardModel
{
    public override bool GainsBlock => true;

    private const string _bonusBlockKey = "BonusBlock";

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new BlockVar(8m, ValueProp.Move),
        new DynamicVar("BonusBlock", 12m)
    };

    public DuckAndCover()
        : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.Owner.Creature.Block <= 0)
        {
            // Use bonus block amount when at 0 block
            await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars[_bonusBlockKey].BaseValue, ValueProp.Move, null);
        }
        else
        {
            await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Block.UpgradeValueBy(2m);
        base.DynamicVars[_bonusBlockKey].UpgradeValueBy(4m);
    }
}
