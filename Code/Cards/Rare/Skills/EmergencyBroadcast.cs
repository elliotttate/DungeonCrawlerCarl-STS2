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
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace DungeonCrawlerCarl;

[Pool(typeof(CarlCardPool))]
public sealed class EmergencyBroadcast : CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DynamicVar("Amount", 10m)
    };

    public EmergencyBroadcast()
        : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int ratings = base.Owner.Creature.GetPowerAmount<RatingsPower>();
        if (ratings <= 0)
        {
            return;
        }

        // Remove all Ratings
        PowerModel? ratingsPower = base.Owner.Creature.GetPower<RatingsPower>();
        if (ratingsPower != null)
        {
            await PowerCmd.Remove(ratingsPower);
        }

        decimal perRating = base.DynamicVars["Amount"].BaseValue;
        decimal totalBlock = perRating * ratings;
        decimal totalDamage = perRating * ratings;

        await CreatureCmd.GainBlock(base.Owner.Creature, totalBlock, ValueProp.Move, null);
        await CreatureCmd.Damage(choiceContext, base.CombatState.HittableEnemies, totalDamage, ValueProp.Unpowered | ValueProp.Move, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["Amount"].UpgradeValueBy(5m);
    }
}
