using BaseLib;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace DungeonCrawlerCarl;

[Pool(typeof(CarlCardPool))]
public sealed class CatClassDivaStrike : CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DamageVar(16m, ValueProp.Move)
    };

    public CatClassDivaStrike()
        : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");

        int enemiesBefore = base.CombatState.HittableEnemies.Count;
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
        int enemiesAfter = base.CombatState.HittableEnemies.Count;

        int killed = enemiesBefore - enemiesAfter;
        if (killed > 0)
        {
            await PowerCmd.Apply<RatingsPower>(base.Owner.Creature, killed, base.Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(6m);
    }
}
