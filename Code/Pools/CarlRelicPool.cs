using BaseLib.Abstracts;
using Godot;

namespace DungeonCrawlerCarl;

public sealed class CarlRelicPool : CustomRelicPoolModel
{
    public override string EnergyColorName => "carl";
    public override Color LabOutlineColor => new Color("FF8C00");
    public override bool IsShared => false;
}
