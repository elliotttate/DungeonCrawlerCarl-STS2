using BaseLib.Utils;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Rooms;

namespace DungeonCrawlerCarl;

/// <summary>
/// Boss. Once per combat, extra turn (simulated by granting massive energy and draws).
/// </summary>
[Pool(typeof(CarlRelicPool))]
public sealed class DungeonMastersRemote : CustomRelicModel
{
    private bool _usedThisCombat;

    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override bool ShowCounter => CombatManager.Instance.IsInProgress && !_usedThisCombat;

    // Activate on first time HP drops below 50%
    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side != base.Owner.Creature.Side)
        {
            return;
        }
        if (_usedThisCombat)
        {
            return;
        }
        if (base.Owner.Creature.CurrentHp <= base.Owner.Creature.MaxHp / 2m)
        {
            _usedThisCombat = true;
            Flash();
            base.Status = RelicStatus.Disabled;
            // Grant extra energy and draws to simulate an extra turn
            await PlayerCmd.GainEnergy(3m, base.Owner);
            await CardPileCmd.Draw(new BlockingPlayerChoiceContext(), 5m, base.Owner);
        }
    }

    public override Task BeforeCombatStart()
    {
        _usedThisCombat = false;
        base.Status = RelicStatus.Normal;
        return Task.CompletedTask;
    }

    public override Task AfterCombatEnd(CombatRoom _)
    {
        _usedThisCombat = false;
        base.Status = RelicStatus.Normal;
        return Task.CompletedTask;
    }
}
