using BaseLib.Abstracts;
using Godot;

namespace DungeonCrawlerCarl;

public sealed class CarlCardPool : CustomCardPoolModel
{
    public override string Title => "carl";
    public override float H => 30f / 360f;
    public override float S => 1f;
    public override float V => 1f;
    public override Color DeckEntryCardColor => new Color("FF8C00");
    public override bool IsColorless => false;
    public override bool IsShared => false;
}
