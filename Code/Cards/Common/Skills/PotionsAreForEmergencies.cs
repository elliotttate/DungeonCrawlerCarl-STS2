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
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DungeonCrawlerCarl;

/// <summary>
/// 1 cost. Add a random potion. Upgrade: 2 potions.
/// Potions are for emergencies! This IS an emergency!
/// </summary>
[Pool(typeof(CarlCardPool))]
public sealed class PotionsAreForEmergencies : CustomCardModel
{
    private const string _potionCountKey = "PotionCount";

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DynamicVar("PotionCount", 1m)
    };

    public PotionsAreForEmergencies()
        : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int count = base.DynamicVars[_potionCountKey].IntValue;
        for (int i = 0; i < count; i++)
        {
            if (!base.Owner.HasOpenPotionSlots)
            {
                break;
            }
            PotionModel potion = PotionFactory.CreateRandomPotionOutOfCombat(base.Owner, base.Owner.RunState.Rng.CombatPotionGeneration).ToMutable();
            await PotionCmd.TryToProcure(potion, base.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars[_potionCountKey].UpgradeValueBy(1m);
    }
}
