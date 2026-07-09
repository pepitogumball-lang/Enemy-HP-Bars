using Newtonsoft.Json;
namespace EnemyHPBarUpdated;

public class AnimJson {
	public static string FilterExtension(string name) => name.Replace(".png", "");

	public static void SaveAnimConfig(HPBarAnimation anim, string name) {
		var fs = new FileStream(Path.Combine(EnemyHPBarUpdated.SkinPath, name + ".json"), FileMode.Create, FileAccess.Write);
		var fw = new StreamWriter(fs);
		fw.Write(JsonConvert.SerializeObject(anim));
		fw.Close();
		fs.Close();
	}

	public static Dictionary<string, HPBarAnimation> animDict = new();

	public static HPBarAnimation LoadAnimConfig(string path) {
		var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
		var fw = new StreamReader(fs);
		string animstr = fw.ReadToEnd();
		HPBarAnimation anim = JsonConvert.DeserializeObject<HPBarAnimation>(animstr, new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });
		fw.Close();
		fs.Close();
		return anim;

	}
	public static bool ConfigExist(HPBarAnimation anim, string name) => File.Exists(Path.Combine(EnemyHPBarUpdated.SkinPath, name + ".json"));

	public static void Initdic() {
		animDict["ol"] = CustomHPBarAnimation.LoadAnimation(ResourceLoader.GetAllImages(EnemyHPBarUpdated.HPBAR_OL));
		animDict["fg"] = CustomHPBarAnimation.LoadAnimation(ResourceLoader.GetAllImages(EnemyHPBarUpdated.HPBAR_FG));
		animDict["bg"] = CustomHPBarAnimation.LoadAnimation(ResourceLoader.GetAllImages(EnemyHPBarUpdated.HPBAR_BG));
		animDict["mg"] = CustomHPBarAnimation.LoadAnimation(ResourceLoader.GetAllImages(EnemyHPBarUpdated.HPBAR_MG));
		animDict["bossfg"] = CustomHPBarAnimation.LoadAnimation(ResourceLoader.GetAllImages(EnemyHPBarUpdated.HPBAR_BOSSFG));
		animDict["bossbg"] = CustomHPBarAnimation.LoadAnimation(ResourceLoader.GetAllImages(EnemyHPBarUpdated.HPBAR_BOSSBG));
		animDict["bossol"] = CustomHPBarAnimation.LoadAnimation(ResourceLoader.GetAllImages(EnemyHPBarUpdated.HPBAR_BOSSOL));
		animDict["bossmg"] = CustomHPBarAnimation.LoadAnimation(ResourceLoader.GetAllImages(EnemyHPBarUpdated.HPBAR_BOSSMG));
	}
	public static void LoadAllConfig() {
		foreach (string key in animDict.Keys) {
			if (ConfigExist(animDict[key], key)) {
				HPBarAnimation configanim = LoadAnimConfig(Path.Combine(EnemyHPBarUpdated.SkinPath, key + ".json"));
				animDict[key].loop = configanim.loop;
				animDict[key].fps = configanim.fps;
			}
		}
	}

	public static bool AnimExists(string name) => CustomHPBarAnimation.loadedSprites[animDict[name]].Length > 0;

	public static void SaveAllConfig() {
		foreach (string key in animDict.Keys) {
			if (!ConfigExist(animDict[key], key) && CustomHPBarAnimation.loadedSprites[animDict[key]].Length > 0) {
				SaveAnimConfig(animDict[key], key);
			}
		}
	}
}
