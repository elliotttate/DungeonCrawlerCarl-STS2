using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;

namespace DungeonCrawlerCarl;

[ModInitializer("Init")]
public static class ModEntry
{
    private static Harmony? _harmony;

    public static void Init()
    {
        Log.Warn("[DungeonCrawlerCarl] Initializing Dungeon Crawler Carl mod...");

        _harmony = new Harmony("com.crawlermods.dungeoncrawlercarl");
        _harmony.PatchAll();

        Log.Warn("[DungeonCrawlerCarl] Harmony patches applied.");
        Log.Warn("[DungeonCrawlerCarl] Welcome, Crawler. The world is watching.");
        Log.Warn("[DungeonCrawlerCarl] Loaded successfully!");
    }
}
