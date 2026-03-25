using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace DungeonCrawlerCarl;

/// <summary>
/// Whenever you play 3 cards in a turn, gain Block.
/// The viewers love a combo.
/// </summary>
public sealed class ViewerFavoritePower : CustomPowerModel
{
    private const int _cardsRequired = 3;
    private const string _cardsPlayedKey = "CardsPlayed";

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool IsInstanced => true;

    public override int DisplayAmount => base.DynamicVars[_cardsPlayedKey].IntValue;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DynamicVar("CardsPlayed", 0m)
    };

    protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
    {
        HoverTipFactory.Static(StaticHoverTip.Block)
    };

    protected override object InitInternalData()
    {
        return new object();
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != base.Owner.Player)
        {
            return;
        }

        base.DynamicVars[_cardsPlayedKey].BaseValue++;
        InvokeDisplayAmountChanged();

        if (base.DynamicVars[_cardsPlayedKey].IntValue >= _cardsRequired)
        {
            Flash();
            await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null);
            base.DynamicVars[_cardsPlayedKey].BaseValue = 0m;
            InvokeDisplayAmountChanged();
        }
    }

    public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != base.Owner.Side)
        {
            return Task.CompletedTask;
        }
        base.DynamicVars[_cardsPlayedKey].BaseValue = 0m;
        InvokeDisplayAmountChanged();
        return Task.CompletedTask;
    }
}
