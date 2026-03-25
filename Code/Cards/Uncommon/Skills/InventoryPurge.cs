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
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace DungeonCrawlerCarl;

[Pool(typeof(CarlCardPool))]
public sealed class InventoryPurge : CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new BlockVar(4m, ValueProp.Move),
        new DynamicVar("Amount", 3m)
    };

    public InventoryPurge()
        : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int maxExhaust = (int)base.DynamicVars["Amount"].BaseValue;
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 0, maxExhaust)
        {
            Cancelable = true
        };
        var selected = (await CardSelectCmd.FromHand(choiceContext, base.Owner, prefs, null, this)).ToList();
        foreach (CardModel card in selected)
        {
            await CardCmd.Exhaust(choiceContext, card);
        }
        if (selected.Count > 0)
        {
            await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block.BaseValue * selected.Count, ValueProp.Move, null);
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Block.UpgradeValueBy(2m);
    }
}
