using BaseLib.Utils;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Rooms;

namespace DungeonCrawlerCarl;

/// <summary>
/// Uncommon. Double combat gold. Lose 5 Max HP on pickup.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class BorantsContract : CustomRelicModel
{
    private decimal _pendingBonusGold;
    private bool _isApplyingBonus;

    public override RelicRarity Rarity => RelicRarity.Uncommon;

    public override bool ShouldGainGold(decimal amount, Player player)
    {
        if (_isApplyingBonus || player != base.Owner)
        {
            return true;
        }
        _pendingBonusGold = amount; // capture the gold amount to double it
        return true;
    }

    public override async Task AfterGoldGained(Player player)
    {
        if (player == base.Owner && !_isApplyingBonus && _pendingBonusGold > 0m)
        {
            decimal bonus = _pendingBonusGold;
            _pendingBonusGold = 0m;
            _isApplyingBonus = true;
            Flash();
            await PlayerCmd.GainGold(bonus, base.Owner);
            _isApplyingBonus = false;
        }
    }

    public override async Task AfterObtained()
    {
        await CreatureCmd.LoseMaxHp(new ThrowingPlayerChoiceContext(), base.Owner.Creature, 5m, isFromCard: false);
    }
}
