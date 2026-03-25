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
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DungeonCrawlerCarl;

/// <summary>
/// Starter relic. Begin combat with 2 Ratings.
/// A tiny tiara that belongs to a very brave cat.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class DonutsTiara : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new PowerVar<RatingsPower>(2m)
    };

    protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
    {
        HoverTipFactory.FromPower<RatingsPower>()
    };

    public override async Task BeforeCombatStart()
    {
        Flash();
        await PowerCmd.Apply<RatingsPower>(base.Owner.Creature, base.DynamicVars["RatingsPower"].IntValue, base.Owner.Creature, null);
    }
}
