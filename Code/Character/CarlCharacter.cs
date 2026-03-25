using System.Collections.Generic;
using System.Runtime.InteropServices;
using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace DungeonCrawlerCarl;

public sealed class CarlCharacter : CustomCharacterModel
{
    public static readonly Color Color = new("FF8C00");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Masculine;
    public override int StartingHp => 75;
    public override int StartingGold => 99;

    public override CardPoolModel CardPool => ModelDb.CardPool<CarlCardPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<CarlPotionPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<CarlRelicPool>();

    public override IEnumerable<CardModel> StartingDeck => new CardModel[]
    {
        ModelDb.Card<StrikeCarl>(),
        ModelDb.Card<StrikeCarl>(),
        ModelDb.Card<StrikeCarl>(),
        ModelDb.Card<StrikeCarl>(),
        ModelDb.Card<StrikeCarl>(),
        ModelDb.Card<DefendCarl>(),
        ModelDb.Card<DefendCarl>(),
        ModelDb.Card<DefendCarl>(),
        ModelDb.Card<DefendCarl>(),
        ModelDb.Card<DonutsEncouragement>()
    };

    public override IReadOnlyList<RelicModel> StartingRelics => new RelicModel[]
    {
        ModelDb.Relic<DonutsTiara>()
    };

    public override float AttackAnimDelay => 0.15f;
    public override float CastAnimDelay => 0.25f;
    public override Color DialogueColor => new Color("FF6600");
    public override Color MapDrawingColor => Color;

    public override List<string> GetArchitectAttackVfx()
    {
        return new List<string>();
    }
}
