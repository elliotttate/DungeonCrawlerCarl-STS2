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
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace DungeonCrawlerCarl;

/// <summary>
/// Event. Play 10 cards -> 10 Gold after combat.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class DonutsMemoirDraft : CustomRelicModel
{
    private int _cardsPlayedThisCombat;

    public override RelicRarity Rarity => RelicRarity.Event;

    public override bool ShowCounter => CombatManager.Instance.IsInProgress;

    public override int DisplayAmount => _cardsPlayedThisCombat;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new CardsVar(10),
        new DynamicVar("Gold", 10m)
    };

    public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature == base.Owner.Creature)
        {
            _cardsPlayedThisCombat++;
            InvokeDisplayAmountChanged();
        }
        return Task.CompletedTask;
    }

    public override async Task AfterCombatEnd(CombatRoom _)
    {
        if (_cardsPlayedThisCombat >= base.DynamicVars.Cards.IntValue)
        {
            Flash();
            int gold = (int)base.DynamicVars["Gold"].BaseValue;
            await PlayerCmd.GainGold(gold, base.Owner);
        }
        _cardsPlayedThisCombat = 0;
    }

    public override Task BeforeCombatStart()
    {
        _cardsPlayedThisCombat = 0;
        return Task.CompletedTask;
    }
}
