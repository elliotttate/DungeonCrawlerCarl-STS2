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
/// Shop. +1 potion slot.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class CarlsFannyPack : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Shop;

    // +1 potion slot is description-only; the game does not expose a ModifyPotionSlots hook.
}
