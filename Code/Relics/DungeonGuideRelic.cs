using BaseLib.Utils;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DungeonCrawlerCarl;

/// <summary>
/// Common. At start of combat, Scry 3.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class DungeonGuideRelic : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Common;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DynamicVar("Amount", 3m)
    };

    public override async Task BeforeCombatStart()
    {
        Flash();
        await CardPileCmd.Draw(new BlockingPlayerChoiceContext(), (int)base.DynamicVars["Amount"].BaseValue, base.Owner);
    }
}
