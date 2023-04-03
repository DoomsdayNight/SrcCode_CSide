using System;
using System.Collections.Generic;
using System.Text;
using AssetBundles;
using Cs.Logging;
using NKC.Localization;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006E0 RID: 1760
	public static class NKCUIVoiceManager
	{
		// Token: 0x06003D55 RID: 15701 RVA: 0x0013B710 File Offset: 0x00139910
		public static bool NeedSelectVoice()
		{
			NKC_VOICE_CODE item = (NKC_VOICE_CODE)PlayerPrefs.GetInt("LOCAL_VOICE_CODE", 0);
			List<NKC_VOICE_CODE> availableVoiceCode = NKCUIVoiceManager.GetAvailableVoiceCode();
			if (PlayerPrefs.HasKey("LOCAL_VOICE_CODE") && availableVoiceCode.Contains(item))
			{
				Debug.Log("[VoiceManager] Local voice code is exist.");
				return false;
			}
			if (availableVoiceCode.Count <= 1)
			{
				Debug.Log(string.Format("[VoiceManager] Available voice count : {0}.", availableVoiceCode.Count));
				return false;
			}
			return true;
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x0013B777 File Offset: 0x00139977
		public static void DeleteLocalVoiceCode()
		{
			PlayerPrefs.DeleteKey("LOCAL_VOICE_CODE");
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x0013B784 File Offset: 0x00139984
		public static NKC_VOICE_CODE LoadLocalVoiceCode()
		{
			NKC_VOICE_CODE nkc_VOICE_CODE = (NKC_VOICE_CODE)PlayerPrefs.GetInt("LOCAL_VOICE_CODE", 0);
			List<NKC_VOICE_CODE> availableVoiceCode = NKCUIVoiceManager.GetAvailableVoiceCode();
			if (availableVoiceCode.Contains(nkc_VOICE_CODE))
			{
				return nkc_VOICE_CODE;
			}
			foreach (NKC_VOICE_CODE nkc_VOICE_CODE2 in availableVoiceCode)
			{
				if (NKMContentsVersionManager.HasTag(string.Format("DEFAULT_VOICE_{0}", NKCLocalization.GetVariant(nkc_VOICE_CODE2).ToUpper())))
				{
					return nkc_VOICE_CODE2;
				}
			}
			if (availableVoiceCode.Count > 0)
			{
				return availableVoiceCode[0];
			}
			return NKC_VOICE_CODE.NVC_KOR;
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06003D58 RID: 15704 RVA: 0x0013B820 File Offset: 0x00139A20
		// (set) Token: 0x06003D59 RID: 15705 RVA: 0x0013B827 File Offset: 0x00139A27
		public static NKC_VOICE_CODE CurrentVoiceCode { get; private set; }

		// Token: 0x06003D5A RID: 15706 RVA: 0x0013B830 File Offset: 0x00139A30
		public static List<NKC_VOICE_CODE> GetAvailableVoiceCode()
		{
			List<NKC_VOICE_CODE> list = new List<NKC_VOICE_CODE>();
			foreach (KeyValuePair<string, NKC_VOICE_CODE> keyValuePair in NKCLocalization.s_dicVoiceTag)
			{
				if (NKMContentsVersionManager.HasTag(keyValuePair.Key))
				{
					list.Add(keyValuePair.Value);
				}
			}
			return list;
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x0013B8A0 File Offset: 0x00139AA0
		public static void SetVoiceCode(NKC_VOICE_CODE code)
		{
			NKCUIVoiceManager.CurrentVoiceCode = code;
			PlayerPrefs.SetInt("LOCAL_VOICE_CODE", (int)code);
			PlayerPrefs.Save();
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x0013B8B8 File Offset: 0x00139AB8
		public static string GetVoiceLanguageName(NKC_VOICE_CODE code)
		{
			return NKCStringTable.GetString("SI_PF_OPTION_VOICE_" + code.ToString(), false);
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x0013B8D7 File Offset: 0x00139AD7
		public static int GetCurrentSoundUID()
		{
			return NKCUIVoiceManager.m_CurrentSoundUID;
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x0013B8DE File Offset: 0x00139ADE
		public static void Init()
		{
			NKCUIVoiceManager.LoadLua("LUA_VOICE_TEMPLET");
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x0013B8EA File Offset: 0x00139AEA
		private static string GetAssetName(string unitStrID, string voicePostID)
		{
			NKCUIVoiceManager.m_AssetName.Remove(0, NKCUIVoiceManager.m_AssetName.Length);
			NKCUIVoiceManager.m_AssetName.AppendFormat("AB_UI_UNIT_VOICE_{0}_{1}", unitStrID, voicePostID);
			return NKCUIVoiceManager.m_AssetName.ToString();
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x0013B920 File Offset: 0x00139B20
		public static bool CheckAsset(string unitStrID, int skinID, string postID, VOICE_BUNDLE bundleType)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitStrID);
			if (unitTempletBase == null || !unitTempletBase.m_bExistVoiceBundle)
			{
				return false;
			}
			string text;
			if (unitTempletBase.BaseUnit != null)
			{
				text = unitTempletBase.BaseUnit.m_UnitStrID;
			}
			else
			{
				text = "";
			}
			if (bundleType.HasFlag(VOICE_BUNDLE.SKIN))
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
				if (skinTemplet != null && !string.IsNullOrEmpty(skinTemplet.m_VoiceBundleName))
				{
					if (NKCUIVoiceManager.CheckAsset(skinTemplet.m_VoiceBundleName, NKCUIVoiceManager.GetAssetName(unitStrID, postID)))
					{
						return true;
					}
					if (!string.IsNullOrEmpty(text) && NKCUIVoiceManager.CheckAsset(skinTemplet.m_VoiceBundleName, NKCUIVoiceManager.GetAssetName(text, postID)))
					{
						return true;
					}
				}
			}
			if (bundleType.HasFlag(VOICE_BUNDLE.UNIT))
			{
				NKCUIVoiceManager.m_BundleName.Clear();
				NKCUIVoiceManager.m_BundleName.AppendFormat("AB_UI_UNIT_VOICE_{0}", unitStrID);
				if (NKCUIVoiceManager.CheckAsset(NKCUIVoiceManager.m_BundleName.ToString(), NKCUIVoiceManager.GetAssetName(unitStrID, postID)))
				{
					return true;
				}
				if (!string.IsNullOrEmpty(text))
				{
					NKCUIVoiceManager.m_BundleName.Clear();
					NKCUIVoiceManager.m_BundleName.AppendFormat("AB_UI_UNIT_VOICE_{0}", text);
					if (NKCUIVoiceManager.CheckAsset(NKCUIVoiceManager.m_BundleName.ToString(), NKCUIVoiceManager.GetAssetName(text, postID)))
					{
						return true;
					}
				}
			}
			if (bundleType.HasFlag(VOICE_BUNDLE.COMMON))
			{
				NKCUIVoiceManager.m_BundleName.Clear();
				if (!string.IsNullOrEmpty(unitTempletBase.m_CommonVoiceBundle))
				{
					NKCUIVoiceManager.m_BundleName.AppendFormat("AB_UI_UNIT_VOICE_{0}", unitTempletBase.m_CommonVoiceBundle);
					if (NKCUIVoiceManager.CheckAsset(NKCUIVoiceManager.m_BundleName.ToString(), NKCUIVoiceManager.GetAssetName(unitTempletBase.m_CommonVoiceBundle, postID)))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x0013BAA4 File Offset: 0x00139CA4
		public static List<NKMAssetName> GetAssetList(string unitStrID, int skinID, string postID, VOICE_BUNDLE bundleType)
		{
			List<NKMAssetName> list = new List<NKMAssetName>();
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitStrID);
			if (unitTempletBase == null || !unitTempletBase.m_bExistVoiceBundle)
			{
				return list;
			}
			string text = "";
			if (unitTempletBase.BaseUnit != null)
			{
				text = unitTempletBase.BaseUnit.m_UnitStrID;
			}
			if (bundleType.HasFlag(VOICE_BUNDLE.SKIN))
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
				if (skinTemplet != null)
				{
					if (NKCUIVoiceManager.CheckAsset(skinTemplet.m_VoiceBundleName, NKCUIVoiceManager.GetAssetName(unitStrID, postID)))
					{
						list.Add(new NKMAssetName(skinTemplet.m_VoiceBundleName, NKCUIVoiceManager.GetAssetName(unitStrID, postID)));
					}
					else if (!string.IsNullOrEmpty(text) && NKCUIVoiceManager.CheckAsset(skinTemplet.m_VoiceBundleName, NKCUIVoiceManager.GetAssetName(text, postID)))
					{
						list.Add(new NKMAssetName(skinTemplet.m_VoiceBundleName, NKCUIVoiceManager.GetAssetName(text, postID)));
					}
				}
			}
			if (bundleType.HasFlag(VOICE_BUNDLE.UNIT))
			{
				NKCUIVoiceManager.m_BundleName.Remove(0, NKCUIVoiceManager.m_BundleName.Length);
				NKCUIVoiceManager.m_BundleName.AppendFormat("AB_UI_UNIT_VOICE_{0}", unitStrID);
				if (NKCUIVoiceManager.CheckAsset(NKCUIVoiceManager.m_BundleName.ToString(), NKCUIVoiceManager.GetAssetName(unitStrID, postID)))
				{
					list.Add(new NKMAssetName(NKCUIVoiceManager.m_BundleName.ToString(), NKCUIVoiceManager.GetAssetName(unitStrID, postID)));
				}
				else if (!string.IsNullOrEmpty(text))
				{
					NKCUIVoiceManager.m_BundleName.Clear();
					NKCUIVoiceManager.m_BundleName.AppendFormat("AB_UI_UNIT_VOICE_{0}", text);
					if (NKCUIVoiceManager.CheckAsset(NKCUIVoiceManager.m_BundleName.ToString(), NKCUIVoiceManager.GetAssetName(text, postID)))
					{
						list.Add(new NKMAssetName(NKCUIVoiceManager.m_BundleName.ToString(), NKCUIVoiceManager.GetAssetName(text, postID)));
					}
				}
			}
			if (bundleType.HasFlag(VOICE_BUNDLE.COMMON))
			{
				NKCUIVoiceManager.m_BundleName.Remove(0, NKCUIVoiceManager.m_BundleName.Length);
				if (!string.IsNullOrEmpty(unitTempletBase.m_CommonVoiceBundle))
				{
					NKCUIVoiceManager.m_BundleName.AppendFormat("AB_UI_UNIT_VOICE_{0}", unitTempletBase.m_CommonVoiceBundle);
					if (NKCUIVoiceManager.CheckAsset(NKCUIVoiceManager.m_BundleName.ToString(), NKCUIVoiceManager.GetAssetName(unitTempletBase.m_CommonVoiceBundle, postID)))
					{
						list.Add(new NKMAssetName(NKCUIVoiceManager.m_BundleName.ToString(), NKCUIVoiceManager.GetAssetName(unitTempletBase.m_CommonVoiceBundle, postID)));
					}
				}
			}
			return list;
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x0013BCBC File Offset: 0x00139EBC
		public static int PlayVoice(VOICE_TYPE type, NKMUnitData unitData, bool bIgnoreShowNormalAfterLifeTimeOption = false, bool bShowCaption = false)
		{
			if (unitData == null)
			{
				return 0;
			}
			if (!NKCUIVoiceManager.m_dicTempletByType.ContainsKey(type))
			{
				return 0;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			if (unitTempletBase == null)
			{
				return 0;
			}
			if (NKMRandom.Range(0, 101) > NKCUIVoiceManager.m_dicTempletByType[type][0].Rate)
			{
				return 0;
			}
			List<NKCVoiceTemplet> list = NKCUIVoiceManager.m_dicTempletByType[type].FindAll((NKCVoiceTemplet v) => NKCUIVoiceManager.CheckCondition(unitData, v.Condition, v.ConditionValue));
			if (list.Count == 0)
			{
				return 0;
			}
			return NKCUIVoiceManager.PlayVoice(type, unitTempletBase, unitData.m_SkinID, list, bIgnoreShowNormalAfterLifeTimeOption, bShowCaption, true);
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x0013BD60 File Offset: 0x00139F60
		public static int PlayVoice(VOICE_TYPE type, int unitID, int skinID = 0, bool bIgnoreShowNormalAfterLifeTimeOption = false, bool bShowCaption = false)
		{
			string unitStrID = NKMUnitManager.GetUnitStrID(unitID);
			if (string.IsNullOrEmpty(unitStrID))
			{
				Log.Error(string.Format("VoiceManager : unitID -> unitStrID Error ({0})", unitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCUIVoiceManager.cs", 402);
				return 0;
			}
			return NKCUIVoiceManager.PlayVoice(type, unitStrID, skinID, bIgnoreShowNormalAfterLifeTimeOption, bShowCaption);
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x0013BDA8 File Offset: 0x00139FA8
		public static int PlayVoice(VOICE_TYPE type, string unitStrID, int skinID = 0, bool bIgnoreShowNormalAfterLifeTimeOption = false, bool bShowCaption = false)
		{
			if (!NKCUIVoiceManager.m_dicTempletByType.ContainsKey(type))
			{
				return 0;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitStrID);
			if (unitTempletBase == null)
			{
				return 0;
			}
			if (NKMRandom.Range(0, 101) > NKCUIVoiceManager.m_dicTempletByType[type][0].Rate)
			{
				return 0;
			}
			List<NKCVoiceTemplet> list = NKCUIVoiceManager.m_dicTempletByType[type].FindAll((NKCVoiceTemplet v) => v.Condition == VOICE_CONDITION.VC_NONE);
			if (list.Count == 0)
			{
				return 0;
			}
			return NKCUIVoiceManager.PlayVoice(type, unitTempletBase, skinID, list, bIgnoreShowNormalAfterLifeTimeOption, bShowCaption, true);
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x0013BE39 File Offset: 0x0013A039
		public static int PlayOperatorVoice(VOICE_TYPE type, NKMOperator operatorData, bool bShowCaption = false)
		{
			return NKCUIVoiceManager.PlayVoice(type, operatorData, bShowCaption, true);
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x0013BE44 File Offset: 0x0013A044
		public static int PlayVoice(VOICE_TYPE type, NKMOperator operatorData, bool bShowCaption = false, bool bStopCurrentVoice = true)
		{
			if (operatorData == null)
			{
				return 0;
			}
			if (!NKCUIVoiceManager.m_dicTempletByType.ContainsKey(type))
			{
				return 0;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorData);
			if (unitTempletBase == null)
			{
				return 0;
			}
			if (NKMRandom.Range(0, 101) > NKCUIVoiceManager.m_dicTempletByType[type][0].Rate)
			{
				return 0;
			}
			List<NKCVoiceTemplet> list = NKCUIVoiceManager.m_dicTempletByType[type].FindAll((NKCVoiceTemplet v) => NKCUIVoiceManager.CheckCondition(operatorData, v.Condition, v.ConditionValue));
			if (list.Count == 0)
			{
				return 0;
			}
			return NKCUIVoiceManager.PlayVoice(type, unitTempletBase, 0, list, false, bShowCaption, bStopCurrentVoice);
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x0013BEE0 File Offset: 0x0013A0E0
		public static int PlayRandomVoiceInBundle(string bundleName)
		{
			if (!NKCUIVoiceManager.IsAssetBundleLoaded(bundleName))
			{
				NKCUIVoiceManager.LoadAssetBundle(bundleName);
			}
			string[] allAssetNameInBundle = AssetBundleManager.GetAllAssetNameInBundle(bundleName);
			if (allAssetNameInBundle != null && allAssetNameInBundle.Length != 0)
			{
				int num = NKMRandom.Range(0, allAssetNameInBundle.Length);
				return NKCSoundManager.PlayVoice(bundleName, allAssetNameInBundle[num], 0, false, false, 1f, 0f, 0f, false, 0f, false, 0f, 0f);
			}
			Debug.LogWarning("Failed to load asset. bundleName : " + bundleName);
			return 0;
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x0013BF51 File Offset: 0x0013A151
		public static void StopVoice()
		{
			if (NKCUIVoiceManager.m_CurrentSoundUID != 0)
			{
				NKCSoundManager.StopSound(NKCUIVoiceManager.m_CurrentSoundUID);
				NKCUIManager.NKCUIOverlayCaption.CloseCaption(NKCUIVoiceManager.m_CurrentSoundUID);
			}
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x0013BF74 File Offset: 0x0013A174
		private static int PlayVoice(VOICE_TYPE type, NKMUnitTempletBase unitTempletBase, int skinID, List<NKCVoiceTemplet> templetList, bool bIgnoreShowNormalAfterLifeTimeOption = false, bool bShowCaption = false, bool bStopCurrentVoice = true)
		{
			if (unitTempletBase == null)
			{
				return 0;
			}
			string unitStrID = unitTempletBase.m_UnitStrID;
			string baseUnitStrID = "";
			if (unitTempletBase.BaseUnit != null)
			{
				baseUnitStrID = unitTempletBase.BaseUnit.m_UnitStrID;
			}
			List<NKCVoiceTemplet> list = new List<NKCVoiceTemplet>();
			VOICE_BUNDLE flag = VOICE_BUNDLE.NONE;
			if (skinID > 0)
			{
				flag = VOICE_BUNDLE.SKIN;
				list = templetList.FindAll((NKCVoiceTemplet v) => NKCUIVoiceManager.CheckAsset(unitStrID, skinID, v.FileName, flag));
				if (list.Count == 0 && !string.IsNullOrEmpty(baseUnitStrID))
				{
					list = templetList.FindAll((NKCVoiceTemplet v) => NKCUIVoiceManager.CheckAsset(baseUnitStrID, skinID, v.FileName, flag));
					if (list.Count > 0)
					{
						unitStrID = baseUnitStrID;
					}
				}
			}
			if (list.Count == 0)
			{
				flag = (VOICE_BUNDLE.UNIT | VOICE_BUNDLE.COMMON);
				list = templetList.FindAll((NKCVoiceTemplet v) => NKCUIVoiceManager.CheckAsset(unitStrID, skinID, v.FileName, flag));
				if (list.Count == 0 && !string.IsNullOrEmpty(baseUnitStrID))
				{
					list = templetList.FindAll((NKCVoiceTemplet v) => NKCUIVoiceManager.CheckAsset(baseUnitStrID, skinID, v.FileName, flag));
					if (list.Count > 0)
					{
						unitStrID = baseUnitStrID;
					}
				}
			}
			if (list.Count == 0)
			{
				templetList.Exists((NKCVoiceTemplet v) => !string.IsNullOrEmpty(v.Npc));
				return 0;
			}
			bool flag2 = false;
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null)
			{
				flag2 = gameOptionData.UseShowNormalSubtitleAfterLifeTime;
			}
			if (!bIgnoreShowNormalAfterLifeTimeOption && flag2)
			{
				if (list.Exists((NKCVoiceTemplet v) => v.Condition == VOICE_CONDITION.VC_LIFETIME))
				{
					goto IL_1D2;
				}
			}
			if (list.Exists((NKCVoiceTemplet v) => v.Condition > VOICE_CONDITION.VC_NONE))
			{
				list = list.FindAll((NKCVoiceTemplet v) => v.Condition > VOICE_CONDITION.VC_NONE);
			}
			IL_1D2:
			List<NKMAssetName> list2 = new List<NKMAssetName>();
			Dictionary<int, List<NKMAssetName>> dictionary = new Dictionary<int, List<NKMAssetName>>();
			for (int i = 0; i < list.Count; i++)
			{
				List<NKMAssetName> assetList = NKCUIVoiceManager.GetAssetList(unitStrID, skinID, list[i].FileName, flag);
				list2.AddRange(assetList);
				dictionary.Add(i, assetList);
			}
			int index = NKMRandom.Range(0, list2.Count);
			NKMAssetName nkmassetName = list2[index];
			int index2 = 0;
			foreach (KeyValuePair<int, List<NKMAssetName>> keyValuePair in dictionary)
			{
				if (keyValuePair.Value.Contains(nkmassetName))
				{
					index2 = keyValuePair.Key;
					break;
				}
			}
			NKCVoiceTemplet nkcvoiceTemplet = list[index2];
			if (NKCSoundManager.IsPlayingVoice(NKCUIVoiceManager.m_CurrentSoundUID) && NKCUIVoiceManager.m_CurrentVoiceTemplet != null && NKCUIVoiceManager.m_CurrentVoiceTemplet.Priority < nkcvoiceTemplet.Priority)
			{
				return NKCUIVoiceManager.m_CurrentSoundUID;
			}
			if (NKCUIVoiceManager.m_CurrentSoundUID != 0 && bStopCurrentVoice)
			{
				NKCSoundManager.StopSound(NKCUIVoiceManager.m_CurrentSoundUID);
			}
			Log.Debug(string.Format("[Voice] play {0} - {1}(voiceTemplet:{2}", nkmassetName.m_BundleName, nkmassetName.m_AssetName, nkcvoiceTemplet.Index), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCUIVoiceManager.cs", 609);
			float delayTime = NKCVoiceTimingManager.GetDelayTime(unitTempletBase.m_UnitID, skinID, nkcvoiceTemplet);
			NKCUIVoiceManager.m_CurrentSoundUID = NKCSoundManager.PlayVoice(nkmassetName.m_BundleName, nkmassetName.m_AssetName, 0, bStopCurrentVoice, false, (float)nkcvoiceTemplet.Volume * 0.01f, 0f, 0f, false, 0f, false, 0f, delayTime);
			if (NKCUIVoiceManager.m_CurrentSoundUID > 0)
			{
				NKCUIVoiceManager.m_CurrentVoiceTemplet = nkcvoiceTemplet;
			}
			if (NKCUIVoiceManager.m_CurrentSoundUID != 0 && bShowCaption)
			{
				NKCUIManager.NKCUIOverlayCaption.OpenCaption(NKCUtilString.GetVoiceCaption(nkmassetName), NKCUIVoiceManager.m_CurrentSoundUID, delayTime);
			}
			return NKCUIVoiceManager.m_CurrentSoundUID;
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x0013C334 File Offset: 0x0013A534
		public static int ForcePlayVoice(NKMAssetName assetName, float delayTime = 0f, float volume = 1f, bool bShowCaption = false, bool bStopCurrentVoice = true)
		{
			if (NKCUIVoiceManager.m_CurrentSoundUID != 0 && bStopCurrentVoice)
			{
				NKCSoundManager.StopSound(NKCUIVoiceManager.m_CurrentSoundUID);
			}
			Log.Debug("[Voice] ForcePlay " + assetName.m_BundleName + " - " + assetName.m_AssetName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCUIVoiceManager.cs", 630);
			NKCUIVoiceManager.m_CurrentSoundUID = NKCSoundManager.PlayVoice(assetName.m_BundleName, assetName.m_AssetName, 0, bStopCurrentVoice, false, volume, 0f, 0f, false, 0f, false, 0f, delayTime);
			if (NKCUIVoiceManager.m_CurrentSoundUID > 0)
			{
				NKCUIVoiceManager.m_CurrentVoiceTemplet = null;
			}
			if (NKCUIVoiceManager.m_CurrentSoundUID != 0 && bShowCaption)
			{
				NKCUIManager.NKCUIOverlayCaption.OpenCaption(NKCUtilString.GetVoiceCaption(assetName), NKCUIVoiceManager.m_CurrentSoundUID, delayTime);
			}
			return NKCUIVoiceManager.m_CurrentSoundUID;
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x0013C3E8 File Offset: 0x0013A5E8
		public static bool GetVoiceBundleInfo(NKMUnitData unitData, string audioClipName, out NKMAssetName assetName)
		{
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(unitData);
			if (skinTemplet != null && NKCUIVoiceManager.CheckAsset(skinTemplet.m_VoiceBundleName, audioClipName))
			{
				assetName = new NKMAssetName(skinTemplet.m_VoiceBundleName, audioClipName);
				return true;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			if (unitTempletBase != null)
			{
				string bundleName = string.Format("AB_UI_UNIT_VOICE_{0}", unitTempletBase.m_UnitStrID);
				if (NKCUIVoiceManager.CheckAsset(bundleName, audioClipName))
				{
					assetName = new NKMAssetName(bundleName, audioClipName);
					return true;
				}
				if (unitTempletBase.BaseUnit != null)
				{
					bundleName = string.Format("AB_UI_UNIT_VOICE_{0}", unitTempletBase.BaseUnit.m_UnitStrID);
					if (NKCUIVoiceManager.CheckAsset(bundleName, audioClipName))
					{
						assetName = new NKMAssetName(bundleName, audioClipName);
						return true;
					}
				}
			}
			assetName = new NKMAssetName();
			return false;
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x0013C486 File Offset: 0x0013A686
		private static bool CheckCondition(NKMUnitData unitData, VOICE_CONDITION condition, int value)
		{
			switch (condition)
			{
			case VOICE_CONDITION.VC_NONE:
				return true;
			case VOICE_CONDITION.VC_LIFETIME:
				if (unitData.IsPermanentContract)
				{
					return true;
				}
				break;
			case VOICE_CONDITION.VC_LEVEL:
				if (unitData.m_UnitLevel >= value)
				{
					return true;
				}
				break;
			case VOICE_CONDITION.VC_DEVOTION:
				if (unitData.loyalty >= value)
				{
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x0013C4C3 File Offset: 0x0013A6C3
		private static bool CheckCondition(NKMOperator operatorData, VOICE_CONDITION condition, int value)
		{
			if (condition != VOICE_CONDITION.VC_NONE)
			{
				if (condition == VOICE_CONDITION.VC_LEVEL)
				{
					if (operatorData.level >= value)
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x0013C4DC File Offset: 0x0013A6DC
		private static void LoadLua(string fileName)
		{
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT", fileName, true) && nkmlua.OpenTable("m_voiceTemplet"))
			{
				int num = 1;
				while (nkmlua.OpenTable(num))
				{
					NKCVoiceTemplet nkcvoiceTemplet = new NKCVoiceTemplet();
					nkcvoiceTemplet.LoadLUA(nkmlua);
					if (!NKCUIVoiceManager.m_dicTempletByType.ContainsKey(nkcvoiceTemplet.Type))
					{
						NKCUIVoiceManager.m_dicTempletByType.Add(nkcvoiceTemplet.Type, new List<NKCVoiceTemplet>());
					}
					NKCUIVoiceManager.m_dicTempletByType[nkcvoiceTemplet.Type].Add(nkcvoiceTemplet);
					num++;
					nkmlua.CloseTable();
				}
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x0013C57C File Offset: 0x0013A77C
		public static void CleanUp()
		{
			foreach (string assetBundleName in NKCUIVoiceManager.s_setLoadedAssetBundleNames)
			{
				AssetBundleManager.UnloadAssetBundle(assetBundleName);
			}
			NKCUIVoiceManager.s_setLoadedAssetBundleNames.Clear();
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x0013C5D8 File Offset: 0x0013A7D8
		private static void LoadAssetBundle(string bundleName)
		{
			bundleName = bundleName.ToLower();
			NKCUIVoiceManager.s_setLoadedAssetBundleNames.Add(bundleName);
			AssetBundleManager.LoadAssetBundle(bundleName, false);
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x0013C5F5 File Offset: 0x0013A7F5
		private static bool IsAssetBundleLoaded(string bundleName)
		{
			bundleName = bundleName.ToLower();
			return NKCUIVoiceManager.s_setLoadedAssetBundleNames.Contains(bundleName) || AssetBundleManager.IsAssetBundleLoaded(bundleName);
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x0013C614 File Offset: 0x0013A814
		private static bool CheckAsset(string bundleName, string assetName)
		{
			bundleName = bundleName.ToLower();
			if (!AssetBundleManager.IsBundleExists(bundleName))
			{
				return false;
			}
			if (!NKCUIVoiceManager.IsAssetBundleLoaded(bundleName))
			{
				NKCUIVoiceManager.LoadAssetBundle(bundleName);
			}
			return AssetBundleManager.IsAssetExists(bundleName, assetName);
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x0013C640 File Offset: 0x0013A840
		public static List<NKCVoiceTemplet> GetTemplets()
		{
			List<NKCVoiceTemplet> list = new List<NKCVoiceTemplet>();
			foreach (List<NKCVoiceTemplet> collection in NKCUIVoiceManager.m_dicTempletByType.Values)
			{
				list.AddRange(collection);
			}
			return list;
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x0013C6A0 File Offset: 0x0013A8A0
		public static List<NKCVoiceTemplet> GetVoiceTempletByType(VOICE_TYPE type)
		{
			if (!NKCUIVoiceManager.m_dicTempletByType.ContainsKey(type))
			{
				return new List<NKCVoiceTemplet>();
			}
			return NKCUIVoiceManager.m_dicTempletByType[type];
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x0013C6C0 File Offset: 0x0013A8C0
		public static NKMAssetName PlayOnUI(string unitStrID, int skinID, string fileName, float vol, VOICE_BUNDLE bundleType, bool bShowCaption = false)
		{
			if (!NKCUIVoiceManager.CheckAsset(unitStrID, skinID, fileName, bundleType))
			{
				return null;
			}
			List<NKMAssetName> assetList = NKCUIVoiceManager.GetAssetList(unitStrID, skinID, fileName, bundleType);
			if (assetList.Count == 0)
			{
				return null;
			}
			if (NKCUIVoiceManager.m_CurrentSoundUID != 0)
			{
				NKCSoundManager.StopSound(NKCUIVoiceManager.m_CurrentSoundUID);
			}
			NKCUIVoiceManager.m_CurrentSoundUID = NKCSoundManager.PlayVoice(assetList[0].m_BundleName, assetList[0].m_AssetName, 0, true, false, vol * 0.01f, 0f, 0f, false, 0f, bShowCaption, 0f, 0f);
			return assetList[0];
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x0013C750 File Offset: 0x0013A950
		public static bool UnitHasVoice(NKMUnitTempletBase unitTemplet)
		{
			if (unitTemplet == null)
			{
				return false;
			}
			if (!unitTemplet.m_bExistVoiceBundle)
			{
				return false;
			}
			string text;
			if (unitTemplet.BaseUnit != null)
			{
				text = string.Format("AB_UI_UNIT_VOICE_{0}", unitTemplet.BaseUnit.m_UnitStrID);
			}
			else
			{
				text = string.Format("AB_UI_UNIT_VOICE_{0}", unitTemplet.m_UnitStrID);
			}
			text = text.ToLower();
			return AssetBundleManager.IsBundleExists(text);
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x0013C7B0 File Offset: 0x0013A9B0
		public static bool IsPlayingVoice(int soundUID = -1)
		{
			return NKCSoundManager.IsPlayingVoice(soundUID);
		}

		// Token: 0x040036DE RID: 14046
		private static StringBuilder m_BundleName = new StringBuilder();

		// Token: 0x040036DF RID: 14047
		private static StringBuilder m_AssetName = new StringBuilder();

		// Token: 0x040036E0 RID: 14048
		private static Dictionary<VOICE_TYPE, List<NKCVoiceTemplet>> m_dicTempletByType = new Dictionary<VOICE_TYPE, List<NKCVoiceTemplet>>();

		// Token: 0x040036E1 RID: 14049
		private static int m_CurrentSoundUID = 0;

		// Token: 0x040036E2 RID: 14050
		private static NKCVoiceTemplet m_CurrentVoiceTemplet = null;

		// Token: 0x040036E3 RID: 14051
		private const string VOICE_BUNDLE_FORMAT = "AB_UI_UNIT_VOICE_{0}";

		// Token: 0x040036E4 RID: 14052
		private const string VOICE_PREF_KEY = "LOCAL_VOICE_CODE";

		// Token: 0x040036E5 RID: 14053
		private const string DEFAULT_LANGUAGE_TAG = "DEFAULT_VOICE_{0}";

		// Token: 0x040036E7 RID: 14055
		private static HashSet<string> s_setLoadedAssetBundleNames = new HashSet<string>();
	}
}
