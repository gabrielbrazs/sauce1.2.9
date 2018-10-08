using System;
using UnityEngine;

public class ResourceLoad : DisableNotifyMonoBehaviour
{
	public BetterList<ResourceObject> list;

	public bool destroyNotify
	{
		get;
		set;
	}

	public void SetReference(ResourceObject resobj)
	{
		if (resobj != null && !(resobj.obj == null))
		{
			resobj.refCount++;
			if (list != null)
			{
				list.Add(resobj);
			}
		}
	}

	public void SetReference(ResourceObject[] resobjs)
	{
		if (resobjs != null)
		{
			int i = 0;
			for (int num = resobjs.Length; i < num; i++)
			{
				if (resobjs[i] != null)
				{
					resobjs[i].refCount++;
				}
			}
			if (list != null)
			{
				int j = 0;
				for (int num2 = resobjs.Length; j < num2; j++)
				{
					if (resobjs[j] != null)
					{
						list.Add(resobjs[j]);
					}
				}
			}
		}
	}

	public bool IsStock(ResourceObject obj)
	{
		if (obj.category == RESOURCE_CATEGORY.EFFECT_UI)
		{
			return true;
		}
		if (obj.category == RESOURCE_CATEGORY.EFFECT_ACTION && obj.name != null && !obj.name.Contains("_bg_"))
		{
			return true;
		}
		return false;
	}

	protected override void OnDisable()
	{
		if (!destroyNotify)
		{
			base.OnDisable();
		}
	}

	private void OnDestroy()
	{
		if (destroyNotify)
		{
			base.OnDisable();
		}
		if (!AppMain.isApplicationQuit && list != null && list.buffer != null)
		{
			MonoBehaviourSingleton<ResourceManager>.I.cache.ReleaseResourceObjects(list.buffer);
		}
	}

	public static ResourceLoad GetResourceLoad(MonoBehaviour mono_behaviour, bool destroy_notify = false)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		ResourceLoad resourceLoad = mono_behaviour.get_gameObject().GetComponent<ResourceLoad>();
		if (resourceLoad == null)
		{
			resourceLoad = mono_behaviour.get_gameObject().AddComponent<ResourceLoad>();
			resourceLoad.SetNotifyMaster(MonoBehaviourSingleton<ResourceManager>.I);
			resourceLoad.destroyNotify = destroy_notify;
		}
		return resourceLoad;
	}

	public static void LoadIconTexture(MonoBehaviour mono_behaviour, RESOURCE_CATEGORY category, string name, Action load_start_callback, Action<Texture> loaded_callback, bool isEventAsset = false)
	{
		ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(category, name);
		if (cachedResourceObject != null)
		{
			loaded_callback(cachedResourceObject.obj as Texture);
		}
		else
		{
			if (load_start_callback != null)
			{
				load_start_callback.Invoke();
			}
			if (string.IsNullOrEmpty(name))
			{
				loaded_callback(null);
			}
			else if (ResourceDefine.types[(int)category] == ResourceManager.CATEGORY_TYPE.HASH256)
			{
				MonoBehaviourSingleton<ResourceManager>.I.Load(isEventAsset, GetResourceLoad(mono_behaviour, true), category, category.ToHash256String(name), new string[1]
				{
					name
				}, OnLoadIconTextureComplate, OnLoadIconTextureError, false, loaded_callback);
			}
			else
			{
				MonoBehaviourSingleton<ResourceManager>.I.Load(isEventAsset, GetResourceLoad(mono_behaviour, true), category, name, OnLoadIconTextureComplate, OnLoadIconTextureError, false, loaded_callback);
			}
		}
	}

	public unsafe static void LoadRushResultIconTexture(UITexture ui_tex, int quest_id)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		_003CLoadRushResultIconTexture_003Ec__AnonStorey72B _003CLoadRushResultIconTexture_003Ec__AnonStorey72B;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.RUSH_RESULT_IMAGE, ResourceName.GetRushResultIconName(quest_id), new Action((object)_003CLoadRushResultIconTexture_003Ec__AnonStorey72B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public unsafe static void LoadRushResultTitleTexture(UITexture ui_tex, int quest_id)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		_003CLoadRushResultTitleTexture_003Ec__AnonStorey72C _003CLoadRushResultTitleTexture_003Ec__AnonStorey72C;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.RUSH_RESULT_IMAGE, ResourceName.GetRushResultTitleName(quest_id), new Action((object)_003CLoadRushResultTitleTexture_003Ec__AnonStorey72C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public unsafe static void LoadItemIconTexture(UITexture ui_tex, int icon_id)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		_003CLoadItemIconTexture_003Ec__AnonStorey72D _003CLoadItemIconTexture_003Ec__AnonStorey72D;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.ICON_ITEM, ResourceName.GetItemIcon(icon_id), new Action((object)_003CLoadItemIconTexture_003Ec__AnonStorey72D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public unsafe static void LoadNPCIconTexture(UITexture ui_tex, int icon_id, bool is_smile)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		_003CLoadNPCIconTexture_003Ec__AnonStorey72E _003CLoadNPCIconTexture_003Ec__AnonStorey72E;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.NPC_ICON, ResourceName.GetNPCIcon(icon_id, is_smile), new Action((object)_003CLoadNPCIconTexture_003Ec__AnonStorey72E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public unsafe static void LoadEnemyIconTexture(UITexture ui_tex, int icon_id)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		_003CLoadEnemyIconTexture_003Ec__AnonStorey72F _003CLoadEnemyIconTexture_003Ec__AnonStorey72F;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.ENEMY_ICON, ResourceName.GetEnemyIcon(icon_id), new Action((object)_003CLoadEnemyIconTexture_003Ec__AnonStorey72F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public unsafe static void LoadFieldIconTexture(UITexture ui_tex, FieldMapTable.FieldMapTableData fieldData)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		string empty = string.Empty;
		_003CLoadFieldIconTexture_003Ec__AnonStorey730 _003CLoadFieldIconTexture_003Ec__AnonStorey;
		LoadIconTexture(name: (!fieldData.IsExistQuestIconId()) ? ResourceName.GetQuestIcon(fieldData.stageName) : ResourceName.GetQuestIcon((int)fieldData.questIconId), mono_behaviour: ui_tex, category: RESOURCE_CATEGORY.QUEST_ICON, load_start_callback: new Action((object)_003CLoadFieldIconTexture_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), loaded_callback: delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, isEventAsset: false);
	}

	public unsafe static void LoadGatherPointIconTexture(UITexture ui_tex, uint icon_id)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		_003CLoadGatherPointIconTexture_003Ec__AnonStorey731 _003CLoadGatherPointIconTexture_003Ec__AnonStorey;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.INGAME_GATHER_POINT, ResourceName.GetGatherPointIcon(icon_id), new Action((object)_003CLoadGatherPointIconTexture_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public unsafe static void LoadCommonImageTexture(UITexture ui_tex, uint image_id)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		_003CLoadCommonImageTexture_003Ec__AnonStorey732 _003CLoadCommonImageTexture_003Ec__AnonStorey;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.COMMON, ResourceName.GetCommmonImageName((int)image_id), new Action((object)_003CLoadCommonImageTexture_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public static void LoadShopImageTexture(UITexture ui_tex, uint image_id, Action<Texture> callback)
	{
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.SHOP_IMG, ResourceName.GetShopImageName((int)image_id), null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback(tex);
			}
		}, false);
	}

	public static void LoadShopImageOfferTexture(UITexture ui_tex, uint image_id, Action<Texture> callback)
	{
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.SHOP_IMG, ResourceName.GetShopImageOfferName((int)image_id), null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback(tex);
			}
		}, false);
	}

	public static void LoadShopImageGemOfferTexture(UITexture ui_tex, uint image_id, Action<Texture> callback)
	{
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.SHOP_IMG, ResourceName.GetShopImageGemOfferName((int)image_id), null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback(tex);
			}
		}, false);
	}

	public static void LoadShopImageMaterialTexture(UITexture ui_tex, string name, Action<Texture> callback)
	{
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.SHOP_IMG, name, null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback(tex);
			}
		}, false);
	}

	public unsafe static void LoadPointIconImageTexture(UITexture ui_tex, uint image_id)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		_003CLoadPointIconImageTexture_003Ec__AnonStorey737 _003CLoadPointIconImageTexture_003Ec__AnonStorey;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName((int)image_id), new Action((object)_003CLoadPointIconImageTexture_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public unsafe static void LoadGrayPointIconImageTexture(UITexture ui_tex, uint image_id)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		_003CLoadGrayPointIconImageTexture_003Ec__AnonStorey738 _003CLoadGrayPointIconImageTexture_003Ec__AnonStorey;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.COMMON, ResourceName.GetGrayPointIconImageName((int)image_id), new Action((object)_003CLoadGrayPointIconImageTexture_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public unsafe static void LoadCommonTexture(UITexture ui_tex, string tex_name)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		_003CLoadCommonTexture_003Ec__AnonStorey739 _003CLoadCommonTexture_003Ec__AnonStorey;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.COMMON, tex_name, new Action((object)_003CLoadCommonTexture_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public unsafe static void LoadPointShopBannerTexture(UITexture ui_tex, uint image_id)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		_003CLoadPointShopBannerTexture_003Ec__AnonStorey73A _003CLoadPointShopBannerTexture_003Ec__AnonStorey73A;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.COMMON, ResourceName.GetPointShopBannerImageName((int)image_id), new Action((object)_003CLoadPointShopBannerTexture_003Ec__AnonStorey73A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, true);
	}

	public unsafe static void LoadPointShopBGTexture(UITexture ui_tex, uint image_id)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		_003CLoadPointShopBGTexture_003Ec__AnonStorey73B _003CLoadPointShopBGTexture_003Ec__AnonStorey73B;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.COMMON, ResourceName.GetPointSHopBGImageName((int)image_id), new Action((object)_003CLoadPointShopBGTexture_003Ec__AnonStorey73B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, true);
	}

	public unsafe static void LoadHomePointSHopBannerTexture(UITexture ui_tex, uint image_id)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		_003CLoadHomePointSHopBannerTexture_003Ec__AnonStorey73C _003CLoadHomePointSHopBannerTexture_003Ec__AnonStorey73C;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.COMMON, ResourceName.GetHomePointSHopBannerImageName((int)image_id), new Action((object)_003CLoadHomePointSHopBannerTexture_003Ec__AnonStorey73C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public unsafe static void LoadEventBannerResultTexture(UITexture ui_tex, uint event_id)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		_003CLoadEventBannerResultTexture_003Ec__AnonStorey73D _003CLoadEventBannerResultTexture_003Ec__AnonStorey73D;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.EVENT_BANNER_RESULT, ResourceName.GetQuestEventBannerResult((int)event_id), new Action((object)_003CLoadEventBannerResultTexture_003Ec__AnonStorey73D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public unsafe static void LoadEventBannerResultBGTexture(UITexture ui_tex, uint event_id)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		_003CLoadEventBannerResultBGTexture_003Ec__AnonStorey73E _003CLoadEventBannerResultBGTexture_003Ec__AnonStorey73E;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.EVENT_BANNER_RESULT, ResourceName.GetQuestEventBannerResultBG((int)event_id), new Action((object)_003CLoadEventBannerResultBGTexture_003Ec__AnonStorey73E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public unsafe static void LoadWithSetUITexture(UITexture ui_tex, RESOURCE_CATEGORY category, string name)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		_003CLoadWithSetUITexture_003Ec__AnonStorey73F _003CLoadWithSetUITexture_003Ec__AnonStorey73F;
		LoadIconTexture(ui_tex, category, name, new Action((object)_003CLoadWithSetUITexture_003Ec__AnonStorey73F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	public static void LoadBlackMarketOfferTexture(UITexture ui_tex, string imageName, Action<Texture> callback)
	{
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.SHOP_IMG, $"DMO_{imageName}", null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback(tex);
			}
		}, false);
	}

	public static void LoadBlackMarketIconTexture(UITexture ui_tex, string imageName, Action<Texture> callback)
	{
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.SHOP_IMG, $"DMI_{imageName}", null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback(tex);
			}
		}, false);
	}

	public static void LoadFortuneWheelIconTexture(UITexture ui_tex, string imageName, Action<Texture> callback)
	{
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.SHOP_IMG, imageName, null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback(tex);
			}
		}, false);
	}

	public static void ItemIconLoadItemIconTexture(ItemIcon item_icon, int icon_id, Action<ItemIcon, Texture, int> callback)
	{
		LoadIconTexture(item_icon, RESOURCE_CATEGORY.ICON_ITEM, ResourceName.GetItemIcon(icon_id), null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback.Invoke(item_icon, tex, icon_id);
			}
		}, false);
	}

	public static void ItemIconLoadAccessoryIconTexture(ItemIcon item_icon, int icon_id, Action<ItemIcon, Texture, int> callback)
	{
		LoadIconTexture(item_icon, RESOURCE_CATEGORY.ICON_ACCESSORY, ResourceName.GetAccessoryIcon(icon_id), null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback.Invoke(item_icon, tex, icon_id);
			}
		}, false);
	}

	public static void ItemIconLoadIconBGTexture(ItemIcon item_icon, int icon_id, Action<ItemIcon, Texture, int> callback)
	{
		LoadIconTexture(item_icon, RESOURCE_CATEGORY.ICON_ITEM, ResourceName.GetItemIcon(icon_id), null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback.Invoke(item_icon, tex, icon_id);
			}
		}, false);
	}

	public static void ItemIconLoadQuestItemIconTexture(ItemIcon item_icon, int icon_id, Action<ItemIcon, Texture, int> callback)
	{
		LoadIconTexture(item_icon, RESOURCE_CATEGORY.ENEMY_ICON, ResourceName.GetEnemyIcon(icon_id), null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback.Invoke(item_icon, tex, icon_id);
			}
		}, false);
	}

	public static void ItemIconLoadEnemyIconItemTexture(ItemIcon item_icon, int icon_id, Action<ItemIcon, Texture, int> callback)
	{
		LoadIconTexture(item_icon, RESOURCE_CATEGORY.ENEMY_ICON_ITEM, ResourceName.GetEnemyIconItem(icon_id), null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback.Invoke(item_icon, tex, icon_id);
			}
		}, false);
	}

	public static void ItemIconLoadCommonTexture(ItemIcon item_icon, int icon_id, Action<ItemIcon, Texture, int> callback)
	{
		LoadIconTexture(item_icon, RESOURCE_CATEGORY.COMMON, ResourceName.GetCommmonImageName(icon_id), null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback.Invoke(item_icon, tex, icon_id);
			}
		}, false);
	}

	public static void ItemIconLoadStampTexture(ItemIcon item_icon, int icon_id, Action<ItemIcon, Texture, int> callback)
	{
		LoadIconTexture(item_icon, RESOURCE_CATEGORY.UI_CHAT_STAMP, ResourceName.GetChatStamp(icon_id), null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback.Invoke(item_icon, tex, icon_id);
			}
		}, false);
	}

	public static void ItemIconLoadDegreeIconTexture(ItemIcon item_icon, DEGREE_TYPE type, Action<ItemIcon, Texture, DEGREE_TYPE> callback)
	{
		LoadIconTexture(item_icon, RESOURCE_CATEGORY.COMMON, ResourceName.GetDegreeIcon(type), null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback.Invoke(item_icon, tex, type);
			}
		}, false);
	}

	public static void ItemIconLoadPointShopPointIconTexture(ItemIcon item_icon, int icon_id, Action<ItemIcon, Texture, int> callback)
	{
		LoadIconTexture(item_icon, RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName(icon_id), null, delegate(Texture tex)
		{
			if (callback != null)
			{
				callback.Invoke(item_icon, tex, icon_id);
			}
		}, false);
	}

	public unsafe static void LoadEvolveIconTexture(UITexture ui_tex, uint evolveId)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		_003CLoadEvolveIconTexture_003Ec__AnonStorey74C _003CLoadEvolveIconTexture_003Ec__AnonStorey74C;
		LoadIconTexture(ui_tex, RESOURCE_CATEGORY.EVOLVE_ICON, ResourceName.GetEvolveIcon(evolveId), new Action((object)_003CLoadEvolveIconTexture_003Ec__AnonStorey74C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), delegate(Texture tex)
		{
			if (ui_tex != null)
			{
				ui_tex.mainTexture = tex;
			}
		}, false);
	}

	private static void OnLoadIconTextureComplate(ResourceManager.LoadRequest request, ResourceObject[] objs)
	{
		objs[0].refCount++;
		(request.userData as Action<Texture>)(objs[0].obj as Texture);
	}

	private static void OnLoadIconTextureError(ResourceManager.LoadRequest request, ResourceManager.ERROR_CODE error_node)
	{
		if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			(request.userData as Action<Texture>)(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.errorIcon);
		}
		else
		{
			(request.userData as Action<Texture>)(null);
		}
	}

	public void ReleaseAllResources()
	{
		if (MonoBehaviourSingleton<ResourceManager>.IsValid() && MonoBehaviourSingleton<ResourceManager>.I.cache != null && list != null && list.buffer != null)
		{
			MonoBehaviourSingleton<ResourceManager>.I.cache.ReleaseResourceObjects(list.buffer);
			list.Release();
		}
	}
}
