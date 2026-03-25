using BaseLib.Utils;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace DungeonCrawlerCarl;

/// <summary>
/// Uncommon. Choose from 1 additional chest option.
/// This is a passive effect handled by the treasure/reward system.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class LootBoxKey : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;
}
