using MenuButton = Satchel.BetterMenus.MenuButton;

namespace EnemyHPBarUpdated;

// Thanks to CustomKnight menu: https://github.com/PrashantMohta/HollowKnight.CustomKnight/blob/moreskin/CustomKnight/Menu/BetterMenu.cs
internal static class BetterMenu {
	internal static int selectedSkin = 0;
	internal static Menu MenuRef = null;

	internal static MenuScreen GetMenu(MenuScreen lastMenu, ModToggleDelegates? toggleDelegates) {
		MenuRef ??= PrepareMenu((ModToggleDelegates) toggleDelegates);

		MenuRef.OnBuilt += (_, Element) => {
			if (EnemyHPBarUpdated.CurrentSkin != null) {
				BetterMenu.SelectedSkin(EnemyHPBarUpdated.CurrentSkin.GetId());
			}
		};

		return MenuRef.GetMenuScreen(lastMenu);
	}

	internal static void ApplySkin() {
		ISelectableSkin skinToApply = EnemyHPBarUpdated.SkinList[selectedSkin];
		BetterMenu.SetSkinById(skinToApply.GetId());
		EnemyHPBarUpdated.CompleteImage(Path.Combine(EnemyHPBarUpdated.DATA_DIR, EnemyHPBarUpdated.CurrentSkin.GetId()));
		EnemyHPBarUpdated.bossol = EnemyHPBarUpdated.HPBarCreateSprite(ResourceLoader.GetBossOutlineImage());
		EnemyHPBarUpdated.bossbg = EnemyHPBarUpdated.HPBarCreateSprite(ResourceLoader.GetBossBackgroundImage());
		EnemyHPBarUpdated.bossfg = EnemyHPBarUpdated.HPBarCreateSprite(ResourceLoader.GetBossForegroundImage());
29			EnemyHPBarUpdated.bossmg = EnemyHPBarUpdated.HPBarCreateSprite(ResourceLoader.GetBossMiddlegroundImage());
		EnemyHPBarUpdated.ol = EnemyHPBarUpdated.HPBarCreateSprite(ResourceLoader.GetOutlineImage());
		EnemyHPBarUpdated.fg = EnemyHPBarUpdated.HPBarCreateSprite(ResourceLoader.GetForegroundImage());
		EnemyHPBarUpdated.mg = EnemyHPBarUpdated.HPBarCreateSprite(ResourceLoader.GetMiddlegroundImage());
		EnemyHPBarUpdated.bg = EnemyHPBarUpdated.HPBarCreateSprite(ResourceLoader.GetBackgroundImage());
		AnimJson.animDict.Clear();
		AnimJson.Initdic();
		AnimJson.LoadAllConfig();
		AnimJson.SaveAllConfig();
	}

	internal static string[] GetSkinNameArray() => EnemyHPBarUpdated.SkinList.Select(s => HPBarList.MaxLength(s.GetName(), EnemyHPBarUpdated.globalSettings.NameLength)).ToArray();

	internal static Menu PrepareMenu(ModToggleDelegates toggleDelegates) => new("EnemyHPBar", new Element[] {
		Blueprints.CreateToggle(toggleDelegates,"HPBar Toggle","","Enabled","Disabled"),
			new HorizontalOption(
				"Select Skin",
				"The skin will be used for current",
				GetSkinNameArray(),
				(setting) => selectedSkin = setting,
				() => selectedSkin,
				Id: "SelectSkinOption"
			),
			Blueprints.HorizontalBoolOption(
				"Intergration",
				"Intergration with CK? (Make sure you install CK if you want to use it, and reset skin whenyou turn it to true), Create a folder named \"HPBar\" in your skin folder",
				(choose) => EnemyHPBarUpdated.globalSettings.Intergration = choose,
				() => EnemyHPBarUpdated.globalSettings.Intergration,
				Id:"CKIntergration"
			),
			new MenuRow(
				new List<Element>{
					Blueprints.NavigateToMenu(
						"Skin List",
						"Opens a list of Skins",
						() => HPBarList.GetMenu(MenuRef.menuScreen)),
						new MenuButton("Apply Skin","Apply The currently selected skin.",
						_ => ApplySkin()
					),
				},
				Id: "ApplyButtonGroup"
			){ XDelta = 400f },
		});

	internal static void SelectedSkin(string skinId) => selectedSkin = EnemyHPBarUpdated.SkinList.FindIndex(skin => skin.GetId() == skinId);

	public static ISelectableSkin GetSkinById(string id) => EnemyHPBarUpdated.SkinList.Find(skin => skin.GetId() == id) ?? GetDefaultSkin();

	public static ISelectableSkin GetDefaultSkin() {
		EnemyHPBarUpdated.DefaultSkin ??= GetSkinById("Default");
		return EnemyHPBarUpdated.DefaultSkin;
	}

	public static void SetSkinById(string id) {
		ISelectableSkin Skin = GetSkinById(id);
		if (EnemyHPBarUpdated.CurrentSkin.GetId() == Skin.GetId()) { return; }

		EnemyHPBarUpdated.CurrentSkin = Skin;
	}
}
