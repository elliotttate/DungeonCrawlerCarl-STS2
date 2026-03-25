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
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DungeonCrawlerCarl;

/// <summary>
/// Boss. +2 cards in starting hand. 2 Recap Episodes shuffled in at combat start.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class Level1Tooltip : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new CardsVar(2)
    };

    public override decimal ModifyHandDraw(Player player, decimal count)
    {
        if (player == base.Owner)
        {
            return count + base.DynamicVars.Cards.BaseValue;
        }
        return count;
    }

    public override async Task BeforeCombatStart()
    {
        Flash();
        await CardPileCmd.AddToCombatAndPreview<RecapEpisode>(base.Owner.Creature, PileType.Draw, 2, addedByPlayer: true, CardPilePosition.Random);
    }
}
