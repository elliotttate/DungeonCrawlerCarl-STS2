using BaseLib.Utils;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DungeonCrawlerCarl;

/// <summary>
/// Boss. On Exhaust, gain 2 Ratings.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class DesperadosRing : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        if (card.Owner.Creature == base.Owner.Creature)
        {
            Flash();
            await PowerCmd.Apply<RatingsPower>(base.Owner.Creature, 2m, base.Owner.Creature, null);
        }
    }
}
