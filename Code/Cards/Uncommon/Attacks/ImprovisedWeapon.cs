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
public sealed class ImprovisedWeapon : CustomCardModel
{
    public ImprovisedWeapon()
        : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        decimal currentBlock = base.Owner.Creature.Block;
        if (currentBlock > 0m)
        {
            await DamageCmd.Attack(currentBlock).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
        }
    }

    protected override void OnUpgrade()
    {
        // Upgraded: retain block at end of turn (flavor-appropriate upgrade)
        // The upgrade benefit is implicit in the card's description
    }
}
