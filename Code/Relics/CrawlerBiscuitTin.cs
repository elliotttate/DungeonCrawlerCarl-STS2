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
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DungeonCrawlerCarl;

/// <summary>
/// Common. At start of combat, heal 6 HP.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class CrawlerBiscuitTin : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Common;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new HealVar(6m)
    };

    public override async Task BeforeCombatStart()
    {
        Flash();
        await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.BaseValue);
    }
}
