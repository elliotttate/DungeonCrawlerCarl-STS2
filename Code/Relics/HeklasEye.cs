using BaseLib.Utils;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DungeonCrawlerCarl;

/// <summary>
/// Rare. Start of combat, play top 2 cards free.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class HeklasEye : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new CardsVar(2)
    };

    public override async Task BeforeCombatStartLate()
    {
        Flash();
        int count = (int)base.DynamicVars.Cards.BaseValue;
        CardPile drawPile = PileType.Draw.GetPile(base.Owner);
        for (int i = 0; i < count && drawPile.Cards.Count > 0; i++)
        {
            CardModel card = drawPile.Cards.First();
            await CardCmd.AutoPlay(new BlockingPlayerChoiceContext(), card, null);
        }
    }
}
