using BaseLib;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DungeonCrawlerCarl;

/// <summary>
/// Rare. Play top 5 cards free.
/// </summary>
[Pool(typeof(CarlPotionPool))]
public sealed class EmergencyBroadcastFlask : CustomPotionModel
{
    public override PotionRarity Rarity => PotionRarity.Rare;

    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new CardsVar(5)
    };

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        PotionModel.AssertValidForTargetedPotion(target);
        int count = (int)base.DynamicVars.Cards.BaseValue;
        CardPile drawPile = PileType.Draw.GetPile(target.Player);
        for (int i = 0; i < count && drawPile.Cards.Count > 0; i++)
        {
            CardModel card = drawPile.Cards.First();
            await CardCmd.AutoPlay(choiceContext, card, null);
        }
    }
}
