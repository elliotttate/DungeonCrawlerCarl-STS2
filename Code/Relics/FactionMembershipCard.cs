using BaseLib.Utils;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace DungeonCrawlerCarl;

/// <summary>
/// Uncommon. Start combat with random Power card (costs 0).
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class FactionMembershipCard : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    public override async Task BeforeCombatStart()
    {
        Flash();
        // TODO: await CardPileCmd.AddRandomCardToCombat(base.Owner, PileType.Hand, CardRarity.Common, addedByPlayer: true, CardType.Power);
    }
}
