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
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace DungeonCrawlerCarl;

/// <summary>
/// Boss. +1 Energy per turn. Start combat -10 HP.
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class Floor1Pants : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new EnergyVar(1),
        new HpLossVar(10m)
    };

    public override decimal ModifyMaxEnergy(Player player, decimal amount)
    {
        if (player == base.Owner)
        {
            return amount + base.DynamicVars.Energy.BaseValue;
        }
        return amount;
    }

    public override async Task BeforeCombatStart()
    {
        Flash();
        await CreatureCmd.Damage(new BlockingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered, (CardModel?)null);
    }
}
