using System.Text.RegularExpressions;

using CustomKnight;

namespace EnemyHPBarUpdated;
internal static class ResourceLoader {
	private static byte[] GetImage(string name) => File.ReadAllBytes(Path.Combine(EnemyHPBarUpdated.DATA_DIR, EnemyHPBarUpdated.CurrentSkin.GetId(), name));

	public static byte[] GetCKImage(string name) => Directory.Exists(Path.Combine(SkinManager.GetCurrentSkin().getSwapperPath(), "HPBar"))
			? File.ReadAllBytes(Path.Combine(SkinManager.GetCurrentSkin().getSwapperPath(), "HPBar", name))
			: GetImage(name);

	public static Sprite[] GetAllImages(string name) {
		string[] imagenames = Directory.GetFiles(EnemyHPBarUpdated.SkinPath, $"{AnimJson.FilterExtension(name)}_*.png");
		var sprites = new Sprite[imagenames.Length];
		foreach (string imagename in imagenames) {
			_ = int.TryParse(Regex.Match(imagename, ".*_([0-9]*).png").Groups[1].Value, out int num);
			Logger.LogDebug($"num:{num}");
			sprites[num] = EnemyHPBarUpdated.HPBarCreateSprite(File.ReadAllBytes(imagename));
		}

		return sprites;

	}
	public static byte[] GetBackgroundImage() => GetImage(EnemyHPBarUpdated.HPBAR_BG);

	public static byte[] GetForegroundImage() => GetImage(EnemyHPBarUpdated.HPBAR_FG);

	public static byte[] GetMiddlegroundImage() => GetImage(EnemyHPBarUpdated.HPBAR_MG);

	public static byte[] GetOutlineImage() => GetImage(EnemyHPBarUpdated.HPBAR_OL);

	public static byte[] GetBossBackgroundImage() => GetImage(EnemyHPBarUpdated.HPBAR_BOSSBG);

	public static byte[] GetBossForegroundImage() => GetImage(EnemyHPBarUpdated.HPBAR_BOSSFG);

	public static byte[] GetBossOutlineImage() => GetImage(EnemyHPBarUpdated.HPBAR_BOSSOL);
14	
15		public static byte[] GetBossMiddlegroundImage() => GetImage(EnemyHPBarUpdated.HPBAR_BOSSMG);
}
