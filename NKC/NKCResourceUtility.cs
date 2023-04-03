using System;
using System.Collections.Generic;
using System.IO;
using NKM;
using NKM.Guild;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006C4 RID: 1732
	public static class NKCResourceUtility
	{
		// Token: 0x06003B3D RID: 15165 RVA: 0x00130863 File Offset: 0x0012EA63
		public static T GetOrLoadAssetResource<T>(NKMAssetName cNKMAssetName) where T : UnityEngine.Object
		{
			return NKCResourceUtility.GetOrLoadAssetResource<T>(cNKMAssetName.m_BundleName, cNKMAssetName.m_AssetName, false);
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x00130878 File Offset: 0x0012EA78
		public static T GetOrLoadAssetResource<T>(string bundleName, string assetName, bool tryParseAssetName = false) where T : UnityEngine.Object
		{
			if (string.IsNullOrEmpty(bundleName))
			{
				return default(T);
			}
			if (string.IsNullOrEmpty(assetName))
			{
				return default(T);
			}
			if (tryParseAssetName)
			{
				NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(bundleName, assetName);
				bundleName = nkmassetName.m_BundleName;
				assetName = nkmassetName.m_AssetName;
			}
			if (!NKCResourceUtility.HasAssetResourceBundle(bundleName, assetName))
			{
				NKCResourceUtility.LoadAssetResourceTemp<T>(bundleName, assetName, false);
			}
			NKCAssetResourceData assetResource = NKCResourceUtility.GetAssetResource(bundleName, assetName);
			if (assetResource == null)
			{
				return default(T);
			}
			return assetResource.GetAsset<T>();
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x001308EE File Offset: 0x0012EAEE
		private static bool HasAssetResourceBundle(string bundleName, string assetName)
		{
			if (string.IsNullOrEmpty(assetName))
			{
				return false;
			}
			bundleName = NKCAssetResourceManager.RemapLocBundle(bundleName, assetName);
			return NKCResourceUtility.m_dicAssetResourceBundle.ContainsKey(bundleName) && NKCResourceUtility.m_dicAssetResourceBundle[bundleName].m_dicNKCResourceData.ContainsKey(assetName);
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x0013092B File Offset: 0x0012EB2B
		public static void LoadAssetResourceTemp<T>(NKMAssetName cNKMAssetName, bool bAsync = true) where T : UnityEngine.Object
		{
			NKCResourceUtility.LoadAssetResourceTemp<T>(cNKMAssetName.m_BundleName, cNKMAssetName.m_AssetName, bAsync);
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x0013093F File Offset: 0x0012EB3F
		public static void LoadAssetResourceTemp<T>(string assetName, bool bAsync = true) where T : UnityEngine.Object
		{
			NKCResourceUtility.LoadAssetResourceTemp<T>(NKCAssetResourceManager.GetBundleName(assetName), assetName, bAsync);
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x00130950 File Offset: 0x0012EB50
		public static void LoadAssetResourceTemp<T>(string bundleName, string assetName, bool bAsync = true) where T : UnityEngine.Object
		{
			bundleName = NKCAssetResourceManager.RemapLocBundle(bundleName, assetName);
			NKCAssetResourceBundle assetResourceBundle;
			if (bAsync)
			{
				assetResourceBundle = NKCResourceUtility.GetAssetResourceBundle(bundleName, NKCResourceUtility.m_dicAssetResourceBundleTemp);
			}
			else
			{
				assetResourceBundle = NKCResourceUtility.GetAssetResourceBundle(bundleName, NKCResourceUtility.m_dicAssetResourceBundle);
			}
			if (!assetResourceBundle.m_dicNKCResourceData.ContainsKey(assetName))
			{
				NKCAssetResourceData value = NKCAssetResourceManager.OpenResource<T>(bundleName, assetName, bAsync, null);
				assetResourceBundle.m_dicNKCResourceData.Add(assetName, value);
			}
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x001309AA File Offset: 0x0012EBAA
		public static Sprite GetRewardInvenIcon(NKMRewardInfo rewardInfo, bool bSmall = false)
		{
			return NKCResourceUtility.GetRewardInvenIcon(rewardInfo.rewardType, rewardInfo.ID, bSmall);
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x001309C0 File Offset: 0x0012EBC0
		public static Sprite GetRewardInvenIcon(NKM_REWARD_TYPE rewardType, int id, bool bSmall = false)
		{
			switch (rewardType)
			{
			default:
				return null;
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_SHIP:
			case NKM_REWARD_TYPE.RT_OPERATOR:
			{
				NKMUnitTempletBase cNKMUnitTempletBase = NKMUnitTempletBase.Find(id);
				return NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, cNKMUnitTempletBase);
			}
			case NKM_REWARD_TYPE.RT_MISC:
			case NKM_REWARD_TYPE.RT_MISSION_POINT:
			case NKM_REWARD_TYPE.RT_PASS_EXP:
				NKMItemMiscTemplet.Find(id);
				if (bSmall)
				{
					return NKCResourceUtility.GetOrLoadMiscItemSmallIcon(id);
				}
				return NKCResourceUtility.GetOrLoadMiscItemIcon(id);
			case NKM_REWARD_TYPE.RT_USER_EXP:
				if (bSmall)
				{
					return NKCResourceUtility.GetOrLoadMiscItemSmallIcon(501);
				}
				return NKCResourceUtility.GetOrLoadMiscItemIcon(501);
			case NKM_REWARD_TYPE.RT_EQUIP:
				return NKCResourceUtility.GetOrLoadEquipIcon(NKMItemManager.GetEquipTemplet(id));
			case NKM_REWARD_TYPE.RT_MOLD:
				return NKCResourceUtility.GetOrLoadMoldIcon(NKMItemMoldTemplet.Find(id));
			case NKM_REWARD_TYPE.RT_SKIN:
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(id);
				return NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, skinTemplet);
			}
			case NKM_REWARD_TYPE.RT_BUFF:
				return NKCResourceUtility.GetOrLoadBuffIconForItemPopup(NKMCompanyBuffTemplet.Find(id));
			case NKM_REWARD_TYPE.RT_EMOTICON:
				return NKCResourceUtility.GetOrLoadEmoticonIcon(NKMEmoticonTemplet.Find(id));
			}
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x00130A8C File Offset: 0x0012EC8C
		public static void PreloadUnitResource(NKCResourceUtility.eUnitResourceType type, NKMUnitData cNKMUnitData, bool bAsync = true)
		{
			if (cNKMUnitData == null)
			{
				return;
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(cNKMUnitData);
			if (skinTemplet != null)
			{
				NKCResourceUtility.PreloadUnitResource(type, skinTemplet, bAsync);
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMUnitData.m_UnitID);
			NKCResourceUtility.PreloadUnitResource(type, unitTempletBase, bAsync);
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x00130AC4 File Offset: 0x0012ECC4
		public static void PreloadUnitResource(NKCResourceUtility.eUnitResourceType type, NKMSkinTemplet cNKMSkinTemplet, bool bAsync = true)
		{
			if (cNKMSkinTemplet == null)
			{
				return;
			}
			switch (type)
			{
			case NKCResourceUtility.eUnitResourceType.FACE_CARD:
				NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_UNIT_FACE_CARD", cNKMSkinTemplet.m_FaceCardName, bAsync);
				return;
			case NKCResourceUtility.eUnitResourceType.INVEN_ICON:
				NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_INVEN_ICON_UNIT", cNKMSkinTemplet.m_InvenIconName, bAsync);
				return;
			case NKCResourceUtility.eUnitResourceType.INVEN_ICON_GRAY:
				NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_INVEN_ICON_UNIT", cNKMSkinTemplet.m_InvenIconName + "_GRAY", bAsync);
				return;
			case NKCResourceUtility.eUnitResourceType.SPINE_ILLUST:
				NKCResourceUtility.LoadAssetResourceTemp<GameObject>(cNKMSkinTemplet.m_SpineIllustName, cNKMSkinTemplet.m_SpineIllustName, true);
				return;
			case NKCResourceUtility.eUnitResourceType.SPINE_SD:
				NKCResourceUtility.LoadAssetResourceTemp<GameObject>(cNKMSkinTemplet.m_SpineSDName, cNKMSkinTemplet.m_SpineSDName, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x00130B58 File Offset: 0x0012ED58
		public static void PreloadUnitResource(NKCResourceUtility.eUnitResourceType type, NKMUnitTempletBase cNKMUnitTempletBase, bool bAsync = true)
		{
			if (cNKMUnitTempletBase == null)
			{
				return;
			}
			switch (type)
			{
			case NKCResourceUtility.eUnitResourceType.FACE_CARD:
				NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_UNIT_FACE_CARD", cNKMUnitTempletBase.m_FaceCardName, bAsync);
				return;
			case NKCResourceUtility.eUnitResourceType.INVEN_ICON:
				NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_INVEN_ICON_UNIT", cNKMUnitTempletBase.m_InvenIconName, bAsync);
				return;
			case NKCResourceUtility.eUnitResourceType.INVEN_ICON_GRAY:
				NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_INVEN_ICON_UNIT", cNKMUnitTempletBase.m_InvenIconName + "_GRAY", bAsync);
				return;
			case NKCResourceUtility.eUnitResourceType.SPINE_ILLUST:
				NKCResourceUtility.LoadAssetResourceTemp<GameObject>(cNKMUnitTempletBase.m_SpineIllustName, cNKMUnitTempletBase.m_SpineIllustName, true);
				return;
			case NKCResourceUtility.eUnitResourceType.SPINE_SD:
				NKCResourceUtility.LoadAssetResourceTemp<GameObject>(cNKMUnitTempletBase.m_SpineSDName, cNKMUnitTempletBase.m_SpineSDName, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x00130BEC File Offset: 0x0012EDEC
		public static NKCAssetResourceData GetUnitResource(NKCResourceUtility.eUnitResourceType type, NKMUnitData cNKMUnitData)
		{
			if (cNKMUnitData == null)
			{
				return null;
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(cNKMUnitData);
			if (skinTemplet != null)
			{
				NKCAssetResourceData unitResource = NKCResourceUtility.GetUnitResource(type, skinTemplet);
				if (unitResource != null)
				{
					return unitResource;
				}
				Debug.LogError("Skin Unitresource get failed. fallback to base resource");
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMUnitData.m_UnitID);
			return NKCResourceUtility.GetUnitResource(type, unitTempletBase);
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x00130C34 File Offset: 0x0012EE34
		public static NKCAssetResourceData GetUnitResource(NKCResourceUtility.eUnitResourceType type, NKMSkinTemplet cNKMSkinTemplet)
		{
			if (cNKMSkinTemplet == null)
			{
				return null;
			}
			switch (type)
			{
			case NKCResourceUtility.eUnitResourceType.FACE_CARD:
				return NKCResourceUtility.GetAssetResource("AB_UNIT_FACE_CARD", cNKMSkinTemplet.m_FaceCardName);
			case NKCResourceUtility.eUnitResourceType.INVEN_ICON:
				return NKCResourceUtility.GetAssetResource("AB_INVEN_ICON_UNIT", cNKMSkinTemplet.m_InvenIconName);
			case NKCResourceUtility.eUnitResourceType.INVEN_ICON_GRAY:
				return NKCResourceUtility.GetAssetResource("AB_INVEN_ICON_UNIT", cNKMSkinTemplet.m_InvenIconName + "_GRAY");
			case NKCResourceUtility.eUnitResourceType.SPINE_ILLUST:
				return NKCResourceUtility.GetAssetResource(cNKMSkinTemplet.m_SpineIllustName, cNKMSkinTemplet.m_SpineIllustName);
			case NKCResourceUtility.eUnitResourceType.SPINE_SD:
				return NKCResourceUtility.GetAssetResource(cNKMSkinTemplet.m_SpineSDName, cNKMSkinTemplet.m_SpineSDName);
			default:
				return null;
			}
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x00130CC4 File Offset: 0x0012EEC4
		public static NKCAssetResourceData GetUnitResource(NKCResourceUtility.eUnitResourceType type, NKMUnitTempletBase cNKMUnitTempletBase)
		{
			if (cNKMUnitTempletBase == null)
			{
				return null;
			}
			switch (type)
			{
			case NKCResourceUtility.eUnitResourceType.FACE_CARD:
				return NKCResourceUtility.GetAssetResource("AB_UNIT_FACE_CARD", cNKMUnitTempletBase.m_FaceCardName);
			case NKCResourceUtility.eUnitResourceType.INVEN_ICON:
				return NKCResourceUtility.GetAssetResource("AB_INVEN_ICON_UNIT", cNKMUnitTempletBase.m_InvenIconName);
			case NKCResourceUtility.eUnitResourceType.INVEN_ICON_GRAY:
				return NKCResourceUtility.GetAssetResource("AB_INVEN_ICON_UNIT", cNKMUnitTempletBase.m_InvenIconName + "_GRAY");
			case NKCResourceUtility.eUnitResourceType.SPINE_ILLUST:
				return NKCResourceUtility.GetAssetResource(cNKMUnitTempletBase.m_SpineIllustName, cNKMUnitTempletBase.m_SpineIllustName);
			case NKCResourceUtility.eUnitResourceType.SPINE_SD:
				return NKCResourceUtility.GetAssetResource(cNKMUnitTempletBase.m_SpineSDName, cNKMUnitTempletBase.m_SpineSDName);
			default:
				return null;
			}
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x00130D54 File Offset: 0x0012EF54
		public static Sprite GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType type, NKMOperator cNKMOperator)
		{
			if (cNKMOperator == null)
			{
				return null;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMOperator.id);
			return NKCResourceUtility.GetorLoadUnitSprite(type, unitTempletBase);
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x00130D7C File Offset: 0x0012EF7C
		public static Sprite GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType type, NKMUnitData cNKMUnitData)
		{
			if (cNKMUnitData == null)
			{
				return null;
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(cNKMUnitData);
			if (skinTemplet != null)
			{
				Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(type, skinTemplet);
				if (sprite != null)
				{
					return sprite;
				}
				Debug.LogError("Skin Sprite load failed. fallback to base sprite");
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMUnitData.m_UnitID);
			return NKCResourceUtility.GetorLoadUnitSprite(type, unitTempletBase);
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x00130DC8 File Offset: 0x0012EFC8
		public static Sprite GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType type, int unitID, int skinID)
		{
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
			if (skinTemplet != null && skinTemplet.m_SkinEquipUnitID == unitID)
			{
				Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(type, skinTemplet);
				if (sprite != null)
				{
					return sprite;
				}
				Debug.LogError("Skin Sprite load failed. fallback to base sprite");
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			return NKCResourceUtility.GetorLoadUnitSprite(type, unitTempletBase);
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x00130E14 File Offset: 0x0012F014
		public static Sprite GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType type, NKMUnitTempletBase cNKMUnitTempletBase, int skinID)
		{
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
			if (skinTemplet != null && skinTemplet.m_SkinEquipUnitID == cNKMUnitTempletBase.m_UnitID)
			{
				Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(type, skinTemplet);
				if (sprite != null)
				{
					return sprite;
				}
				Debug.LogError("Skin Sprite load failed. fallback to base sprite");
			}
			return NKCResourceUtility.GetorLoadUnitSprite(type, cNKMUnitTempletBase);
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x00130E60 File Offset: 0x0012F060
		public static Sprite GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType type, NKMSkinTemplet cNKMSkinTemplet)
		{
			if (cNKMSkinTemplet == null)
			{
				return null;
			}
			switch (type)
			{
			case NKCResourceUtility.eUnitResourceType.FACE_CARD:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UNIT_FACE_CARD", cNKMSkinTemplet.m_FaceCardName, false);
			case NKCResourceUtility.eUnitResourceType.INVEN_ICON:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_UNIT", cNKMSkinTemplet.m_InvenIconName, false);
			case NKCResourceUtility.eUnitResourceType.INVEN_ICON_GRAY:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_UNIT", cNKMSkinTemplet.m_InvenIconName + "_GRAY", false);
			case NKCResourceUtility.eUnitResourceType.SPINE_ILLUST:
			case NKCResourceUtility.eUnitResourceType.SPINE_SD:
				Debug.LogWarning("Wrong type");
				return null;
			default:
				return null;
			}
		}

		// Token: 0x06003B50 RID: 15184 RVA: 0x00130EDC File Offset: 0x0012F0DC
		public static Sprite GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType type, NKMUnitTempletBase cNKMUnitTempletBase)
		{
			if (cNKMUnitTempletBase == null)
			{
				return null;
			}
			switch (type)
			{
			case NKCResourceUtility.eUnitResourceType.FACE_CARD:
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UNIT_FACE_CARD", cNKMUnitTempletBase.m_FaceCardName, false);
			case NKCResourceUtility.eUnitResourceType.INVEN_ICON:
			{
				Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_UNIT", cNKMUnitTempletBase.m_InvenIconName, false);
				if (orLoadAssetResource == null)
				{
					Debug.LogError(string.Format("UnitIconSprite {0}(From UnitStrID {1}) not found", cNKMUnitTempletBase.m_InvenIconName, cNKMUnitTempletBase.m_UnitStrID));
				}
				return orLoadAssetResource;
			}
			case NKCResourceUtility.eUnitResourceType.INVEN_ICON_GRAY:
			{
				Sprite orLoadAssetResource2 = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_UNIT", cNKMUnitTempletBase.m_InvenIconName + "_GRAY", false);
				if (orLoadAssetResource2 == null)
				{
					Debug.LogError(string.Format("UnitIconSprite {0}(From UnitStrID {1}) not found", cNKMUnitTempletBase.m_InvenIconName, cNKMUnitTempletBase.m_UnitStrID));
				}
				return orLoadAssetResource2;
			}
			case NKCResourceUtility.eUnitResourceType.SPINE_ILLUST:
			case NKCResourceUtility.eUnitResourceType.SPINE_SD:
				Debug.LogWarning("Wrong type");
				return null;
			default:
				return null;
			}
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x00130FA2 File Offset: 0x0012F1A2
		public static void PreloadUnitInvenIconEmpty()
		{
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_INVEN_ICON_UNIT", "AB_INVEN_ICON_NKM_UNIT_EMPTY", true);
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x00130FB4 File Offset: 0x0012F1B4
		public static NKCAssetResourceData GetAssetResourceUnitInvenIconEmpty()
		{
			return NKCResourceUtility.GetAssetResource("AB_INVEN_ICON_UNIT", "AB_INVEN_ICON_NKM_UNIT_EMPTY");
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x00130FC8 File Offset: 0x0012F1C8
		public static NKCASUIUnitIllust OpenSpineIllust(NKMUnitData cNKMUnitData, bool bAsync = false)
		{
			if (cNKMUnitData == null)
			{
				return null;
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(cNKMUnitData);
			if (skinTemplet != null)
			{
				NKCASUIUnitIllust nkcasuiunitIllust = NKCResourceUtility.OpenSpineIllust(skinTemplet, bAsync);
				if (nkcasuiunitIllust != null)
				{
					return nkcasuiunitIllust;
				}
				Debug.LogError("Skin Spineillust load failed. fallback to base sprite");
			}
			return NKCResourceUtility.OpenSpineIllust(NKMUnitManager.GetUnitTempletBase(cNKMUnitData.m_UnitID), bAsync);
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x0013100C File Offset: 0x0012F20C
		public static NKCASUIUnitIllust OpenSpineIllust(NKMSkinTemplet skinTemplet, bool bAsync = false)
		{
			if (skinTemplet == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(skinTemplet.m_SpineIllustName))
			{
				return null;
			}
			return NKCResourceUtility.OpenSpineIllust(skinTemplet.m_SpineIllustName, skinTemplet.m_SpineIllustName, bAsync);
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x00131034 File Offset: 0x0012F234
		public static NKCASUIUnitIllust OpenSpineIllust(NKMUnitTempletBase unitTempletBase, int skinID, bool bAsync = false)
		{
			if (unitTempletBase == null)
			{
				return null;
			}
			if (skinID == 0)
			{
				return NKCResourceUtility.OpenSpineIllust(unitTempletBase, bAsync);
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
			if (NKMSkinManager.IsSkinForCharacter(unitTempletBase.m_UnitID, skinTemplet))
			{
				return NKCResourceUtility.OpenSpineIllust(skinTemplet, false);
			}
			Debug.LogError("Skin Spineillust load failed, or not a skin for target unit. fallback to base sprite");
			return NKCResourceUtility.OpenSpineIllust(unitTempletBase, bAsync);
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x0013107F File Offset: 0x0012F27F
		public static NKCASUIUnitIllust OpenSpineIllust(NKMUnitTempletBase unitTempletBase, bool bAsync = false)
		{
			if (unitTempletBase == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(unitTempletBase.m_SpineIllustName))
			{
				return null;
			}
			return NKCResourceUtility.OpenSpineIllust(unitTempletBase.m_SpineIllustName, unitTempletBase.m_SpineIllustName, bAsync);
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x001310A7 File Offset: 0x0012F2A7
		public static void CloseSpineIllust(NKCASUIUnitIllust spineIllust)
		{
			NKCScenManager.GetScenManager().GetObjectPool().CloseObj(spineIllust);
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x001310B9 File Offset: 0x0012F2B9
		public static NKCASUIUnitIllust OpenSpineIllustWithManualNaming(string unitStrID, bool bAsync = false)
		{
			return NKCResourceUtility.OpenSpineIllust("AB_UNIT_ILLUST_" + unitStrID, "AB_UNIT_ILLUST_" + unitStrID, bAsync);
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x001310D7 File Offset: 0x0012F2D7
		public static NKCASUIUnitIllust OpenSpineIllust(string bundleName, string assetName, bool bAsync = false)
		{
			return (NKCASUISpineIllust)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUISpineIllust, bundleName, assetName, bAsync);
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x001310F4 File Offset: 0x0012F2F4
		public static NKCASUIUnitIllust OpenSpineSD(NKMUnitData cNKMUnitData, bool bAsync = false)
		{
			if (cNKMUnitData == null)
			{
				return null;
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(cNKMUnitData);
			if (skinTemplet != null)
			{
				NKCASUIUnitIllust nkcasuiunitIllust = NKCResourceUtility.OpenSpineSD(skinTemplet, bAsync);
				if (nkcasuiunitIllust != null)
				{
					return nkcasuiunitIllust;
				}
				Debug.LogError("Skin SD load failed. fallback to base sprite");
			}
			return NKCResourceUtility.OpenSpineSD(NKMUnitManager.GetUnitTempletBase(cNKMUnitData.m_UnitID), bAsync);
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x00131138 File Offset: 0x0012F338
		public static NKCASUIUnitIllust OpenSpineSD(int unitID, int skinID, bool bAsync = false)
		{
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID, unitID);
			if (skinTemplet != null)
			{
				NKCASUIUnitIllust nkcasuiunitIllust = NKCResourceUtility.OpenSpineSD(skinTemplet, bAsync);
				if (nkcasuiunitIllust != null)
				{
					return nkcasuiunitIllust;
				}
				Debug.LogError("Skin SD load failed. fallback to base sprite");
			}
			return NKCResourceUtility.OpenSpineSD(NKMUnitManager.GetUnitTempletBase(unitID), bAsync);
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x00131173 File Offset: 0x0012F373
		public static NKCASUIUnitIllust OpenSpineSD(NKMSkinTemplet skinTemplet, bool bAsync = false)
		{
			if (skinTemplet == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(skinTemplet.m_SpineSDName))
			{
				return null;
			}
			return (NKCASUISpineIllust)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUISpineIllust, skinTemplet.m_SpineSDName, skinTemplet.m_SpineSDName, bAsync);
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x001311AC File Offset: 0x0012F3AC
		public static NKCASUIUnitIllust OpenSpineSD(NKMUnitTempletBase unitTempletBase, bool bAsync = false)
		{
			if (unitTempletBase == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(unitTempletBase.m_SpineSDName))
			{
				return null;
			}
			return (NKCASUISpineIllust)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUISpineIllust, unitTempletBase.m_SpineSDName, unitTempletBase.m_SpineSDName, bAsync);
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x001311E5 File Offset: 0x0012F3E5
		public static NKCASUISpineIllust OpenSpineSD(NKMWorldMapEventTemplet cNKMWorldMapEventTemplet, bool bAsync = false)
		{
			if (cNKMWorldMapEventTemplet == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(cNKMWorldMapEventTemplet.spineSDName))
			{
				return null;
			}
			return (NKCASUISpineIllust)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUISpineIllust, cNKMWorldMapEventTemplet.spineSDName, cNKMWorldMapEventTemplet.spineSDName, bAsync);
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x0013121E File Offset: 0x0012F41E
		public static NKCASUISpineIllust OpenSpineSD(string spineSDName, bool bAsync = false)
		{
			if (string.IsNullOrEmpty(spineSDName))
			{
				return null;
			}
			return (NKCASUISpineIllust)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUISpineIllust, spineSDName, spineSDName, bAsync);
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x00131243 File Offset: 0x0012F443
		public static NKCASUISpineIllust OpenSpineSD(string bundleName, string spineSDName, bool bAsync = false)
		{
			if (string.IsNullOrEmpty(spineSDName))
			{
				return null;
			}
			return (NKCASUISpineIllust)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUISpineIllust, bundleName, spineSDName, bAsync);
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x00131268 File Offset: 0x0012F468
		public static Sprite GetOrLoadMinimapFaceIcon(NKMUnitTempletBase cNKMUnitTempletBase, bool bSub = false)
		{
			if (cNKMUnitTempletBase == null)
			{
				return null;
			}
			if (bSub && !string.IsNullOrEmpty(cNKMUnitTempletBase.m_MiniMapFaceNameSub))
			{
				return NKCResourceUtility.GetOrLoadMinimapFaceIcon(cNKMUnitTempletBase.m_MiniMapFaceNameSub);
			}
			return NKCResourceUtility.GetOrLoadMinimapFaceIcon(cNKMUnitTempletBase.m_MiniMapFaceName);
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x00131296 File Offset: 0x0012F496
		public static Sprite GetOrLoadMinimapFaceIcon(string minimapFaceName)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UNIT_MINI_MAP_FACE", minimapFaceName, false);
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x001312A4 File Offset: 0x0012F4A4
		public static string GetMiscItemIconBundleName(NKMItemMiscTemplet itemMiscTemplet)
		{
			if (itemMiscTemplet == null)
			{
				return "AB_INVEN_ICON_ITEM_MISC";
			}
			NKM_ITEM_MISC_TYPE itemMiscType = itemMiscTemplet.m_ItemMiscType;
			if (itemMiscType - NKM_ITEM_MISC_TYPE.IMT_EMBLEM > 1)
			{
				switch (itemMiscType)
				{
				case NKM_ITEM_MISC_TYPE.IMT_PIECE:
					return "AB_INVEN_ICON_UNIT_PIECE";
				case NKM_ITEM_MISC_TYPE.IMT_BACKGROUND:
					return "AB_INVEN_ICON_BG";
				case NKM_ITEM_MISC_TYPE.IMT_SELFIE_FRAME:
					return "AB_INVEN_ICON_BORDER";
				case NKM_ITEM_MISC_TYPE.IMT_INTERIOR:
					return "AB_INVEN_ICON_FNC";
				}
				return "AB_INVEN_ICON_ITEM_MISC";
			}
			return "AB_INVEN_ICON_EMBLEM";
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x0013130D File Offset: 0x0012F50D
		public static void PreloadMiscItemIcon(NKMItemMiscTemplet itemMiscTemplet, bool bAsync = true)
		{
			if (itemMiscTemplet == null)
			{
				return;
			}
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>(NKMAssetName.ParseBundleName(NKCResourceUtility.GetMiscItemIconBundleName(itemMiscTemplet), itemMiscTemplet.m_ItemMiscIconName, "AB_INVEN_"), true);
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x0013132F File Offset: 0x0012F52F
		public static NKCAssetResourceData GetAssetResourceMiscItemIcon(NKMItemMiscTemplet itemMiscTemplet)
		{
			if (itemMiscTemplet == null)
			{
				return null;
			}
			return NKCResourceUtility.GetAssetResource(NKMAssetName.ParseBundleName(NKCResourceUtility.GetMiscItemIconBundleName(itemMiscTemplet), itemMiscTemplet.m_ItemMiscIconName, "AB_INVEN_"));
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x00131351 File Offset: 0x0012F551
		public static Sprite GetOrLoadMiscItemIcon(NKMItemMiscTemplet itemMiscTemplet)
		{
			if (itemMiscTemplet == null)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName(NKCResourceUtility.GetMiscItemIconBundleName(itemMiscTemplet), itemMiscTemplet.m_ItemMiscIconName, "AB_INVEN_"));
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x00131373 File Offset: 0x0012F573
		public static Sprite GetOrLoadMiscItemIcon(int miscItemID)
		{
			return NKCResourceUtility.GetOrLoadMiscItemIcon(NKMItemManager.GetItemMiscTempletByID(miscItemID));
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x00131380 File Offset: 0x0012F580
		public static void PreloadMiscItemSmallIcon(NKMItemMiscTemplet itemMiscTemplet, bool bAsync = true)
		{
			if (itemMiscTemplet == null)
			{
				return;
			}
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>(NKMAssetName.ParseBundleName("AB_INVEN_ICON_ITEM_MISC_SMALL", itemMiscTemplet.m_ItemMiscIconName, "AB_INVEN_"), true);
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x001313A1 File Offset: 0x0012F5A1
		public static NKCAssetResourceData GetAssetResourceMiscItemSmallIcon(NKMItemMiscTemplet itemMiscTemplet)
		{
			if (itemMiscTemplet == null)
			{
				return null;
			}
			return NKCResourceUtility.GetAssetResource(NKMAssetName.ParseBundleName("AB_INVEN_ICON_ITEM_MISC_SMALL", itemMiscTemplet.m_ItemMiscIconName, "AB_INVEN_"));
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x001313C4 File Offset: 0x0012F5C4
		public static Sprite GetOrLoadMiscItemSmallIcon(NKMItemMiscTemplet itemMiscTemplet)
		{
			if (itemMiscTemplet == null)
			{
				return null;
			}
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName("AB_INVEN_ICON_ITEM_MISC_SMALL", itemMiscTemplet.m_ItemMiscIconName, "AB_INVEN_");
			if (!NKCAssetResourceManager.IsAssetExists(nkmassetName.m_BundleName, nkmassetName.m_AssetName, true))
			{
				Debug.LogWarning(string.Format("ItemID {0} : Small icon does not exist. defaulting to normal icon", itemMiscTemplet.m_ItemMiscID));
				return NKCResourceUtility.GetOrLoadMiscItemIcon(itemMiscTemplet);
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(nkmassetName);
			if (orLoadAssetResource == null)
			{
				Debug.LogWarning(string.Format("ItemID {0} : Small icon does not exist. defaulting to normal icon", itemMiscTemplet.m_ItemMiscID));
				return NKCResourceUtility.GetOrLoadMiscItemIcon(itemMiscTemplet);
			}
			return orLoadAssetResource;
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x00131453 File Offset: 0x0012F653
		public static Sprite GetOrLoadMiscItemSmallIcon(int miscItemID)
		{
			return NKCResourceUtility.GetOrLoadMiscItemSmallIcon(NKMItemManager.GetItemMiscTempletByID(miscItemID));
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x00131460 File Offset: 0x0012F660
		public static void PreloadEquipIcon(NKMEquipTemplet equipTemplet, bool bAsync = true)
		{
			if (equipTemplet == null)
			{
				return;
			}
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_INVEN_ICON_ITEM_EQUIP", equipTemplet.m_ItemEquipIconName, true);
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x00131477 File Offset: 0x0012F677
		public static NKCAssetResourceData GetAssetResourceEquipIcon(NKMEquipTemplet equipTemplet)
		{
			if (equipTemplet == null)
			{
				return null;
			}
			return NKCResourceUtility.GetAssetResource("AB_INVEN_ICON_ITEM_EQUIP", equipTemplet.m_ItemEquipIconName);
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x0013148E File Offset: 0x0012F68E
		public static Sprite GetOrLoadEquipIcon(NKMEquipTemplet equipTemplet)
		{
			if (equipTemplet == null)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadEquipIcon(equipTemplet.m_ItemEquipIconName);
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x001314A0 File Offset: 0x0012F6A0
		public static Sprite GetOrLoadEquipIcon(string equipIconName)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_ITEM_EQUIP", "AB_INVEN_ICON_IQI_EQUIP_" + equipIconName, false);
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x001314B8 File Offset: 0x0012F6B8
		public static void PreloadUnitRoleIcon(NKM_UNIT_ROLE_TYPE roleType, bool bAwaken, bool bSmall = false, bool bAsync = true)
		{
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>("ab_ui_nkm_ui_common_icon", NKCResourceUtility.GetUnitRoleIconAssetName(roleType, bAwaken, bSmall), bAsync);
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x001314CD File Offset: 0x0012F6CD
		public static NKCAssetResourceData GetAssetResourceUnitRoleIcon(NKM_UNIT_ROLE_TYPE roleType, bool bAwaken, bool bSmall = false)
		{
			return NKCResourceUtility.GetAssetResource("ab_ui_nkm_ui_common_icon", NKCResourceUtility.GetUnitRoleIconAssetName(roleType, bAwaken, bSmall));
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x001314E1 File Offset: 0x0012F6E1
		public static Sprite GetOrLoadUnitTypeIcon(NKMUnitTempletBase templetBase, bool bSmall = false)
		{
			if (templetBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_INVALID && templetBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_INVALID)
			{
				return NKCResourceUtility.GetOrLoadUnitStyleIcon(templetBase.m_NKM_UNIT_STYLE_TYPE, bSmall);
			}
			return NKCResourceUtility.GetOrLoadUnitRoleIcon(templetBase.m_NKM_UNIT_ROLE_TYPE, templetBase.m_bAwaken, bSmall);
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x00131512 File Offset: 0x0012F712
		public static Sprite GetOrLoadUnitRoleIcon(NKMUnitTempletBase templetBase, bool bSmall = false)
		{
			if (templetBase == null)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadUnitRoleIcon(templetBase.m_NKM_UNIT_ROLE_TYPE, templetBase.m_bAwaken, bSmall);
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x0013152B File Offset: 0x0012F72B
		public static Sprite GetOrLoadUnitRoleIcon(NKM_UNIT_ROLE_TYPE roleType, bool bAwaken, bool bSmall = false)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_common_icon", NKCResourceUtility.GetUnitRoleIconAssetName(roleType, bAwaken, bSmall), false);
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x00131540 File Offset: 0x0012F740
		private static string GetUnitRoleIconAssetName(NKM_UNIT_ROLE_TYPE roleType, bool bAwaken, bool bSmall)
		{
			if (bAwaken)
			{
				if (bSmall)
				{
					switch (roleType)
					{
					case NKM_UNIT_ROLE_TYPE.NURT_INVALID:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_NONE_SMALL";
					case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_STRIKER_AWAKEN_SMALL";
					case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_RANGER_AWAKEN_SMALL";
					case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_DEFENDER_AWAKEN_SMALL";
					case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_SNIPER_AWAKEN_SMALL";
					case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_SUPPORTER_AWAKEN_SMALL";
					case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_SIEGE_AWAKEN_SMALL";
					case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_TOWER_AWAKEN_SMALL";
					default:
						return "";
					}
				}
				else
				{
					switch (roleType)
					{
					case NKM_UNIT_ROLE_TYPE.NURT_INVALID:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_NONE_SMALL";
					case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_STRIKER_AWAKEN";
					case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_RANGER_AWAKEN";
					case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_DEFENDER_AWAKEN";
					case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_SNIPER_AWAKEN";
					case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_SUPPORTER_AWAKEN";
					case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_SIEGE_AWAKEN";
					case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
						return "NKM_UI_COMMON_UNIT_CLASS_ICON_TOWER_AWAKEN";
					default:
						return "";
					}
				}
			}
			else if (bSmall)
			{
				switch (roleType)
				{
				case NKM_UNIT_ROLE_TYPE.NURT_INVALID:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_NONE_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_STRIKER_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_RANGER_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_DEFENDER_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_SNIPER_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_SUPPORTER_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_SIEGE_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_TOWER_SMALL";
				default:
					return "";
				}
			}
			else
			{
				switch (roleType)
				{
				case NKM_UNIT_ROLE_TYPE.NURT_INVALID:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_NONE_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_STRIKER";
				case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_RANGER";
				case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_DEFENDER";
				case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_SNIPER";
				case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_SUPPORTER";
				case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_SIEGE";
				case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
					return "NKM_UI_COMMON_UNIT_CLASS_ICON_TOWER";
				default:
					return "";
				}
			}
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x001316D0 File Offset: 0x0012F8D0
		public static Sprite GetOrLoadUnitRoleIconInGame(NKMUnitTempletBase unitTempletBase)
		{
			if (unitTempletBase == null)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadUnitRoleIconInGame(unitTempletBase.m_NKM_UNIT_ROLE_TYPE, unitTempletBase.m_bAwaken);
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x001316E8 File Offset: 0x0012F8E8
		public static Sprite GetOrLoadUnitRoleIconInGame(NKM_UNIT_ROLE_TYPE roleType, bool bAwaken)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UNIT_GAME_NKM_UNIT_SPRITE", NKCResourceUtility.GetUnitRoleIconForInGameAssetName(roleType, bAwaken), false);
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x001316FC File Offset: 0x0012F8FC
		private static string GetUnitRoleIconForInGameAssetName(NKM_UNIT_ROLE_TYPE roleType, bool bAwaken)
		{
			if (bAwaken)
			{
				switch (roleType)
				{
				case NKM_UNIT_ROLE_TYPE.NURT_INVALID:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_NONE_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_STRIKER_AWAKEN_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_RANGER_AWAKEN_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_DEFENDER_AWAKEN_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_SNIPER_AWAKEN_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_SUPPORTER_AWAKEN_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_SIEGE_AWAKEN_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_TOWER_AWAKEN_SMALL";
				default:
					return "";
				}
			}
			else
			{
				switch (roleType)
				{
				case NKM_UNIT_ROLE_TYPE.NURT_INVALID:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_NONE_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_STRIKER_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_RANGER_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_DEFENDER_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_SNIPER_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_SUPPORTER_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_SIEGE_SMALL";
				case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
					return "AB_UNIT_GAME_COMMON_UNIT_CLASS_ICON_TOWER_SMALL";
				default:
					return "";
				}
			}
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x001317C7 File Offset: 0x0012F9C7
		public static void PreloadUnitAttackTypeIcon(NKM_FIND_TARGET_TYPE attackType, bool bSmall = false, bool bAsync = true)
		{
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>("ab_ui_nkm_ui_common_icon", NKCResourceUtility.GetUnitAttackTypeIconAssetName(attackType, bSmall), bAsync);
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x001317DB File Offset: 0x0012F9DB
		public static NKCAssetResourceData GetAssetResourceUnitAttackTypeIcon(NKM_FIND_TARGET_TYPE attackType, bool bSmall = false)
		{
			return NKCResourceUtility.GetAssetResource("ab_ui_nkm_ui_common_icon", NKCResourceUtility.GetUnitAttackTypeIconAssetName(attackType, bSmall));
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x001317EE File Offset: 0x0012F9EE
		public static Sprite GetOrLoadUnitAttackTypeIcon(NKMUnitTempletBase unitTempletBase, bool bSmall = false)
		{
			if (unitTempletBase == null)
			{
				return null;
			}
			if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc == NKM_FIND_TARGET_TYPE.NFTT_INVALID)
			{
				return NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase.m_NKM_FIND_TARGET_TYPE, bSmall);
			}
			return NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc, bSmall);
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x00131816 File Offset: 0x0012FA16
		public static Sprite GetOrLoadUnitAttackTypeIcon(NKMUnitData unitData, bool bSmall = false)
		{
			if (unitData == null)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID), bSmall);
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x0013182E File Offset: 0x0012FA2E
		public static Sprite GetOrLoadUnitAttackTypeIcon(NKM_FIND_TARGET_TYPE attackType, bool bSmall = false)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_common_icon", NKCResourceUtility.GetUnitAttackTypeIconAssetName(attackType, bSmall), false);
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x00131844 File Offset: 0x0012FA44
		private static string GetUnitAttackTypeIconAssetName(NKM_FIND_TARGET_TYPE attackType, bool bSmall)
		{
			if (bSmall)
			{
				switch (attackType)
				{
				case NKM_FIND_TARGET_TYPE.NFTT_NO:
					return "NKM_UI_COMMON_UNIT_BATTLE_TYPE_ICON_NONE_SMALL";
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR_FIRST:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_BOSS_LAST:
				case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_ONLY:
				case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM:
					return "NKM_UI_COMMON_UNIT_BATTLE_TYPE_ICON_ALL_ATK_SMALL";
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND_RANGER_SUPPORTER_SNIPER_FIRST:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND_BOSS_LAST:
				case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_LAND:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LAND:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_LAND:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM_LAND:
					return "NKM_UI_COMMON_UNIT_BATTLE_TYPE_ICON_GROUND_ATK_SMALL";
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR_BOSS_LAST:
				case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_AIR:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_AIR:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_AIR:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM_AIR:
					return "NKM_UI_COMMON_UNIT_BATTLE_TYPE_ICON_AIR_ATK_SMALL";
				default:
					return "";
				}
			}
			else
			{
				switch (attackType)
				{
				case NKM_FIND_TARGET_TYPE.NFTT_NO:
					return "NKM_UI_COMMON_UNIT_BATTLE_TYPE_ICON_NONE_SMALL";
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR_FIRST:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_BOSS_LAST:
				case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_ONLY:
				case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM:
					return "NKM_UI_COMMON_UNIT_BATTLE_TYPE_ICON_ALL_ATK";
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND_RANGER_SUPPORTER_SNIPER_FIRST:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND_BOSS_LAST:
				case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_LAND:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LAND:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_LAND:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM_LAND:
					return "NKM_UI_COMMON_UNIT_BATTLE_TYPE_ICON_GROUND_ATK";
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR_BOSS_LAST:
				case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_AIR:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_AIR:
				case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_AIR:
				case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM_AIR:
					return "NKM_UI_COMMON_UNIT_BATTLE_TYPE_ICON_AIR_ATK";
				default:
					return "";
				}
			}
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x0013196E File Offset: 0x0012FB6E
		public static Sprite GetOrLoadDiveArtifactIcon(NKMDiveArtifactTemplet cNKMDiveArtifactTemplet)
		{
			if (cNKMDiveArtifactTemplet == null)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadDiveArtifactIcon(cNKMDiveArtifactTemplet.ArtifactMiscIconName);
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x00131980 File Offset: 0x0012FB80
		public static Sprite GetOrLoadDiveArtifactIcon(string artifactMiscIconName)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_DIVE_ARTIFACT", artifactMiscIconName, false);
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x0013198E File Offset: 0x0012FB8E
		public static Sprite GetOrLoadDiveArtifactIconBig(NKMDiveArtifactTemplet cNKMDiveArtifactTemplet)
		{
			if (cNKMDiveArtifactTemplet == null)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadDiveArtifactIconBig(cNKMDiveArtifactTemplet.ArtifactMiscIconName);
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x001319A0 File Offset: 0x0012FBA0
		public static Sprite GetOrLoadDiveArtifactIconBig(string artifactMiscIconName)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_DIVE_ARTIFACT_BIG", artifactMiscIconName, false);
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x001319AE File Offset: 0x0012FBAE
		public static Sprite GetOrLoadGuildArtifactIcon(GuildDungeonArtifactTemplet cGuildDungeonArtifactTemplet)
		{
			if (cGuildDungeonArtifactTemplet == null)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadGuildArtifactIcon(cGuildDungeonArtifactTemplet.GetIconName());
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x001319C0 File Offset: 0x0012FBC0
		public static Sprite GetOrLoadGuildArtifactIcon(string iconName)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_DIVE_ARTIFACT", iconName, false);
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x001319CE File Offset: 0x0012FBCE
		public static Sprite GetOrLoadGuildArtifactIconBig(GuildDungeonArtifactTemplet cGuildDungeonArtifactTemplet)
		{
			if (cGuildDungeonArtifactTemplet == null)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadGuildArtifactIconBig(cGuildDungeonArtifactTemplet.GetIconName());
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x001319E0 File Offset: 0x0012FBE0
		public static Sprite GetOrLoadGuildArtifactIconBig(string iconName)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_DIVE_ARTIFACT_BIG", iconName, false);
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x001319EE File Offset: 0x0012FBEE
		public static Sprite GetOrLoadEmoticonIcon(NKMEmoticonTemplet cNKMEmoticonTemplet)
		{
			if (cNKMEmoticonTemplet == null)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadEmoticonIcon(cNKMEmoticonTemplet.m_EmoticonaIconName);
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x00131A00 File Offset: 0x0012FC00
		public static Sprite GetOrLoadEmoticonIcon(string iconName)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_EMOTICON_ICON", "AB_UI_NKM_UI_EMOTICON_ICON_" + iconName, false);
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x00131A18 File Offset: 0x0012FC18
		public static Sprite GetOrLoadMoldIcon(NKMItemMoldTemplet moldTemplet)
		{
			if (moldTemplet == null)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadMoldIcon(moldTemplet.m_MoldIconName);
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x00131A2A File Offset: 0x0012FC2A
		public static Sprite GetOrLoadMoldIcon(string moldIconName)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_ITEM_MISC", "AB_INVEN_" + moldIconName, false);
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x00131A42 File Offset: 0x0012FC42
		public static Sprite GetOrLoadBuffIconForItemPopup(NKMCompanyBuffTemplet buffTemplet)
		{
			if (buffTemplet == null)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadBuffIconForItemPopup(buffTemplet.m_CompanyBuffItemIcon);
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x00131A54 File Offset: 0x0012FC54
		public static Sprite GetOrLoadBuffIconForItemPopup(string buffIconName)
		{
			if (!string.IsNullOrEmpty(buffIconName))
			{
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_ITEM_MISC", "AB_INVEN_" + buffIconName, false);
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_ITEM_MISC", "AB_INVEN_ICON_IMI_MISC_BUFF_BASIC_CREDIT", false);
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x00131A85 File Offset: 0x0012FC85
		public static void PreloadUnitStyleIcon(NKM_UNIT_STYLE_TYPE StyleType, bool bSmall = false, bool bAsync = true)
		{
			if (StyleType == NKM_UNIT_STYLE_TYPE.NUST_INVALID)
			{
				return;
			}
			NKCResourceUtility.LoadAssetResourceTemp<Sprite>("ab_ui_nkm_ui_common_icon", NKCResourceUtility.GetUnitStyleIconAssetName(StyleType, bSmall), bAsync);
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x00131A9D File Offset: 0x0012FC9D
		public static NKCAssetResourceData GetAssetResourceUnitStyleIcon(NKM_UNIT_STYLE_TYPE StyleType, bool bSmall = false)
		{
			if (StyleType == NKM_UNIT_STYLE_TYPE.NUST_INVALID)
			{
				return null;
			}
			return NKCResourceUtility.GetAssetResource("ab_ui_nkm_ui_common_icon", NKCResourceUtility.GetUnitStyleIconAssetName(StyleType, bSmall));
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x00131AB5 File Offset: 0x0012FCB5
		public static Sprite GetOrLoadUnitStyleIcon(NKM_UNIT_STYLE_TYPE StyleType, bool bSmall = false)
		{
			if (StyleType == NKM_UNIT_STYLE_TYPE.NUST_INVALID)
			{
				return null;
			}
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_common_icon", NKCResourceUtility.GetUnitStyleIconAssetName(StyleType, bSmall), false);
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x00131AD0 File Offset: 0x0012FCD0
		private static string GetUnitStyleIconAssetName(NKM_UNIT_STYLE_TYPE styleType, bool bSmall)
		{
			if (bSmall)
			{
				switch (styleType)
				{
				case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
					return "NKM_UI_COMMON_UNIT_TYPE_COUNTER_SMALL";
				case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
					return "NKM_UI_COMMON_UNIT_TYPE_SOLDIER_SMALL";
				case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
					return "NKM_UI_COMMON_UNIT_TYPE_MECHANIC_SMALL";
				case NKM_UNIT_STYLE_TYPE.NUST_CORRUPTED:
				case NKM_UNIT_STYLE_TYPE.NUST_REPLACER:
				case NKM_UNIT_STYLE_TYPE.NUST_TRAINER:
				case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ETC:
				case NKM_UNIT_STYLE_TYPE.NUST_ENV:
				case NKM_UNIT_STYLE_TYPE.NUST_ETC:
					return "NKM_UI_COMMON_UNIT_TYPE_ETC_SMALL";
				case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
					return "NKM_UI_COMMON_SHIP_TYPE_ASSAULT_SMALL";
				case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
					return "NKM_UI_COMMON_SHIP_TYPE_HEAVY_SMALL";
				case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
					return "NKM_UI_COMMON_SHIP_TYPE_CRUISER_SMALL";
				case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
					return "NKM_UI_COMMON_SHIP_TYPE_SPECIAL_SMALL";
				case NKM_UNIT_STYLE_TYPE.NUST_SHIP_PATROL:
					return "NKM_UI_COMMON_SHIP_TYPE_PATROL_VEHCLE_SMALL";
				case NKM_UNIT_STYLE_TYPE.NUST_OPERATOR:
					return "NKM_UI_COMMON_UNIT_TYPE_OPERATOR";
				}
				return "";
			}
			switch (styleType)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
				return "NKM_UI_COMMON_UNIT_TYPE_COUNTER";
			case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
				return "NKM_UI_COMMON_UNIT_TYPE_SOLDIER";
			case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
				return "NKM_UI_COMMON_UNIT_TYPE_MECHANIC";
			case NKM_UNIT_STYLE_TYPE.NUST_CORRUPTED:
			case NKM_UNIT_STYLE_TYPE.NUST_REPLACER:
			case NKM_UNIT_STYLE_TYPE.NUST_TRAINER:
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ETC:
			case NKM_UNIT_STYLE_TYPE.NUST_ENV:
			case NKM_UNIT_STYLE_TYPE.NUST_ETC:
				return "NKM_UI_COMMON_UNIT_TYPE_ETC";
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
				return "NKM_UI_COMMON_SHIP_TYPE_ASSAULT";
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
				return "NKM_UI_COMMON_SHIP_TYPE_HEAVY";
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
				return "NKM_UI_COMMON_SHIP_TYPE_CRUISER";
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
				return "NKM_UI_COMMON_SHIP_TYPE_SPECIAL";
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_PATROL:
				return "NKM_UI_COMMON_SHIP_TYPE_PATROL_VEHCLE";
			case NKM_UNIT_STYLE_TYPE.NUST_OPERATOR:
				return "NKM_UI_COMMON_UNIT_TYPE_OPERATOR";
			}
			return "";
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x00131C00 File Offset: 0x0012FE00
		private static NKCAssetResourceBundle GetAssetResourceBundle(string bundleName, Dictionary<string, NKCAssetResourceBundle> dicBundle)
		{
			bundleName = bundleName.ToLower();
			NKCAssetResourceBundle nkcassetResourceBundle;
			if (!dicBundle.ContainsKey(bundleName))
			{
				nkcassetResourceBundle = new NKCAssetResourceBundle();
				dicBundle.Add(bundleName, nkcassetResourceBundle);
			}
			else
			{
				nkcassetResourceBundle = dicBundle[bundleName];
			}
			return nkcassetResourceBundle;
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x00131C3C File Offset: 0x0012FE3C
		public static void ClearResource()
		{
			foreach (KeyValuePair<string, NKCAssetResourceBundle> keyValuePair in NKCResourceUtility.m_dicAssetResourceBundle)
			{
				NKCAssetResourceBundle value = keyValuePair.Value;
				foreach (KeyValuePair<string, NKCAssetResourceData> keyValuePair2 in value.m_dicNKCResourceData)
				{
					NKCAssetResourceManager.CloseResource(keyValuePair2.Value);
				}
				value.m_dicNKCResourceData.Clear();
			}
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x00131CA8 File Offset: 0x0012FEA8
		public static void SwapResource()
		{
			NKCResourceUtility.ClearResource();
			foreach (KeyValuePair<string, NKCAssetResourceBundle> keyValuePair in NKCResourceUtility.m_dicAssetResourceBundleTemp)
			{
				NKCAssetResourceBundle value = keyValuePair.Value;
				foreach (KeyValuePair<string, NKCAssetResourceData> keyValuePair2 in value.m_dicNKCResourceData)
				{
					NKCAssetResourceData value2 = keyValuePair2.Value;
					NKCAssetResourceBundle assetResourceBundle = NKCResourceUtility.GetAssetResourceBundle(value2.m_NKMAssetName.m_BundleName, NKCResourceUtility.m_dicAssetResourceBundle);
					if (!assetResourceBundle.m_dicNKCResourceData.ContainsKey(value2.m_NKMAssetName.m_AssetName))
					{
						assetResourceBundle.m_dicNKCResourceData.Add(value2.m_NKMAssetName.m_AssetName, value2);
					}
					else
					{
						NKCAssetResourceManager.CloseResource(value2);
					}
				}
				value.m_dicNKCResourceData.Clear();
			}
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x00131D70 File Offset: 0x0012FF70
		public static NKCAssetResourceData GetAssetResource(NKMAssetName cNKMAssetName)
		{
			return NKCResourceUtility.GetAssetResource(cNKMAssetName.m_BundleName, cNKMAssetName.m_AssetName);
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x00131D84 File Offset: 0x0012FF84
		public static NKCAssetResourceData GetAssetResource(string bundleName, string assetName)
		{
			bundleName = NKCAssetResourceManager.RemapLocBundle(bundleName, assetName);
			NKCAssetResourceBundle assetResourceBundle = NKCResourceUtility.GetAssetResourceBundle(bundleName, NKCResourceUtility.m_dicAssetResourceBundle);
			if (assetResourceBundle.m_dicNKCResourceData.ContainsKey(assetName))
			{
				return assetResourceBundle.m_dicNKCResourceData[assetName];
			}
			return null;
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x00131DC4 File Offset: 0x0012FFC4
		public static Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100f)
		{
			Texture2D texture2D = NKCResourceUtility.LoadTexture(FilePath);
			if (texture2D != null)
			{
				return Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0f, 0f), PixelsPerUnit);
			}
			return null;
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x00131E18 File Offset: 0x00130018
		public static Texture2D LoadTexture(string FilePath)
		{
			if (File.Exists(FilePath))
			{
				byte[] data = File.ReadAllBytes(FilePath);
				Texture2D texture2D = new Texture2D(2, 2);
				if (texture2D.LoadImage(data))
				{
					return texture2D;
				}
			}
			return null;
		}

		// Token: 0x04003551 RID: 13649
		public const string UNIT_FACE_CARD_BUNDLE_NAME = "AB_UNIT_FACE_CARD";

		// Token: 0x04003552 RID: 13650
		public const string UNIT_INVEN_ICON_BUNDLE_NAME = "AB_INVEN_ICON_UNIT";

		// Token: 0x04003553 RID: 13651
		public const string UNIT_INVEN_ICON_NKM_UNIT_EMPTY = "AB_INVEN_ICON_NKM_UNIT_EMPTY";

		// Token: 0x04003554 RID: 13652
		public const string UNIT_MINI_MAP_FACE_BUNDLE_NAME = "AB_UNIT_MINI_MAP_FACE";

		// Token: 0x04003555 RID: 13653
		public const string MISC_ITEM_ICON_BUNDLE_NAME = "AB_INVEN_ICON_ITEM_MISC";

		// Token: 0x04003556 RID: 13654
		public const string MISC_ITEM_EMBLEM_ICON_BUNDLE_NAME = "AB_INVEN_ICON_EMBLEM";

		// Token: 0x04003557 RID: 13655
		public const string MISC_ITEM_PIECE_ICON_BUNDLE_NAME = "AB_INVEN_ICON_UNIT_PIECE";

		// Token: 0x04003558 RID: 13656
		public const string MISC_ITEM_BG_ICON_BUNDLE_NAME = "AB_INVEN_ICON_BG";

		// Token: 0x04003559 RID: 13657
		public const string MISC_ITEM_SELFIE_FRAME_BUNDLE_NAME = "AB_INVEN_ICON_BORDER";

		// Token: 0x0400355A RID: 13658
		public const string MISC_ITEM_INTERIOR_BUNDLE_NAME = "AB_INVEN_ICON_FNC";

		// Token: 0x0400355B RID: 13659
		public const string COMMON_ICON_BUNDLE_NAME = "ab_ui_nkm_ui_common_icon";

		// Token: 0x0400355C RID: 13660
		public const string INGAME_ICON_BUNDLE_NAME = "AB_UNIT_GAME_NKM_UNIT_SPRITE";

		// Token: 0x0400355D RID: 13661
		public const string MISC_SMALL_ITEM_ICON_BUNDLE_NAME = "AB_INVEN_ICON_ITEM_MISC_SMALL";

		// Token: 0x0400355E RID: 13662
		public const string EQUIP_ICON_BUNDLE_NAME = "AB_INVEN_ICON_ITEM_EQUIP";

		// Token: 0x0400355F RID: 13663
		public const string MOLD_ICON_BUNDLE_NAME = "AB_INVEN_ICON_ITEM_MISC";

		// Token: 0x04003560 RID: 13664
		public const string BUFF_ICON_BUNDLE_NAME = "AB_INVEN_ICON_ITEM_MISC";

		// Token: 0x04003561 RID: 13665
		public const string DIVE_ARTIFACT_ICON_BUNDLE_NAME = "AB_UI_NKM_UI_WORLD_MAP_DIVE_ARTIFACT";

		// Token: 0x04003562 RID: 13666
		public const string DIVE_ARTIFACT_BIG_ICON_BUNDLE_NAME = "AB_UI_NKM_UI_WORLD_MAP_DIVE_ARTIFACT_BIG";

		// Token: 0x04003563 RID: 13667
		public const string GUILD_ARTIFACT_ICON_BUNDLE_NAME = "AB_UI_NKM_UI_WORLD_MAP_DIVE_ARTIFACT";

		// Token: 0x04003564 RID: 13668
		public const string GUILD_ARTIFACT_BIG_ICON_BUNDLE_NAME = "AB_UI_NKM_UI_WORLD_MAP_DIVE_ARTIFACT_BIG";

		// Token: 0x04003565 RID: 13669
		public const string EMOTICON_ICON_BUNDLE_NAME = "AB_UI_NKM_UI_EMOTICON_ICON";

		// Token: 0x04003566 RID: 13670
		private static Dictionary<string, NKCAssetResourceBundle> m_dicAssetResourceBundle = new Dictionary<string, NKCAssetResourceBundle>();

		// Token: 0x04003567 RID: 13671
		private static Dictionary<string, NKCAssetResourceBundle> m_dicAssetResourceBundleTemp = new Dictionary<string, NKCAssetResourceBundle>();

		// Token: 0x0200138F RID: 5007
		public enum eUnitResourceType
		{
			// Token: 0x04009A89 RID: 39561
			FACE_CARD,
			// Token: 0x04009A8A RID: 39562
			INVEN_ICON,
			// Token: 0x04009A8B RID: 39563
			INVEN_ICON_GRAY,
			// Token: 0x04009A8C RID: 39564
			SPINE_ILLUST,
			// Token: 0x04009A8D RID: 39565
			SPINE_SD
		}
	}
}
