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
/// Shop. Heal 10 HP at shops. Shops cost 15% more.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class SkullJuiceSixPack : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Shop;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new HealVar(10m)
    };

    // Shop healing and price increase are description-only;
    // the game does not expose ModifyShopPriceMultiplier or AfterShopEntered hooks.
}
