using System;
using NKM;
using NKM.Event;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Module
{
	// Token: 0x02000B20 RID: 2848
	public class NKCUIModuleLobby : MonoBehaviour
	{
		// Token: 0x060081C0 RID: 33216 RVA: 0x002BBDC4 File Offset: 0x002B9FC4
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEventModule, new UnityAction(this.OnClickEventModuleHome));
			NKMEventCollectionIndexTemplet eventCollectionIndexTemplet = NKCUIModuleLobby.GetEventCollectionIndexTemplet();
			base.gameObject.SetActive(eventCollectionIndexTemplet != null);
			if (eventCollectionIndexTemplet == null)
			{
				return;
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("ab_ui_nkm_ui_lobby_texture", eventCollectionIndexTemplet.EventBannerStrId));
			NKCUtil.SetImageSprite(this.m_buttonIconImage, orLoadAssetResource, false);
		}

		// Token: 0x060081C1 RID: 33217 RVA: 0x002BBE24 File Offset: 0x002BA024
		public void SetData()
		{
			NKMEventCollectionIndexTemplet eventCollectionIndexTemplet = NKCUIModuleLobby.GetEventCollectionIndexTemplet();
			if (eventCollectionIndexTemplet == null)
			{
				base.gameObject.SetActive(false);
				return;
			}
			this.SetEventRemainTime(NKMIntervalTemplet.Find(eventCollectionIndexTemplet.DateStrId));
			bool bValue = this.CheckMissionCompleteEnable();
			NKCUtil.SetGameobjectActive(this.m_objRedDot, bValue);
		}

		// Token: 0x060081C2 RID: 33218 RVA: 0x002BBE6C File Offset: 0x002BA06C
		public bool IsEventModuleOpen()
		{
			NKMEventCollectionIndexTemplet eventCollectionIndexTemplet = NKCUIModuleLobby.GetEventCollectionIndexTemplet();
			if (eventCollectionIndexTemplet == null)
			{
				return false;
			}
			if (!eventCollectionIndexTemplet.IsOpen)
			{
				return false;
			}
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(eventCollectionIndexTemplet.EventPrefabId, eventCollectionIndexTemplet.EventPrefabId);
			return NKCUIManager.GetInstance(nkmassetName.m_AssetName, nkmassetName.m_AssetName, true) != null;
		}

		// Token: 0x060081C3 RID: 33219 RVA: 0x002BBEB8 File Offset: 0x002BA0B8
		private void SetEventRemainTime(NKMIntervalTemplet intervalTemplet)
		{
			if (intervalTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_lbRemainTime, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbRemainTime, true);
			TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(NKCSynchronizedTime.ToUtcTime(intervalTemplet.EndDate));
			string msg;
			if (timeLeft.Days > 0)
			{
				msg = string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_DAYS", false), timeLeft.Days);
			}
			else if (timeLeft.Hours > 0)
			{
				msg = string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_HOURS", false), timeLeft.Hours);
			}
			else
			{
				msg = string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_MINUTES", false), timeLeft.Minutes);
			}
			NKCUtil.SetLabelText(this.m_lbRemainTime, msg);
		}

		// Token: 0x060081C4 RID: 33220 RVA: 0x002BBF74 File Offset: 0x002BA174
		public static NKMEventCollectionIndexTemplet GetEventCollectionIndexTemplet()
		{
			foreach (NKMEventCollectionIndexTemplet nkmeventCollectionIndexTemplet in NKMEventCollectionIndexTemplet.Values)
			{
				if (nkmeventCollectionIndexTemplet != null && nkmeventCollectionIndexTemplet.IsOpen)
				{
					NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(nkmeventCollectionIndexTemplet.DateStrId);
					if (nkmintervalTemplet != null && nkmintervalTemplet.IsValidTime(NKCSynchronizedTime.ServiceTime))
					{
						return nkmeventCollectionIndexTemplet;
					}
				}
			}
			return null;
		}

		// Token: 0x060081C5 RID: 33221 RVA: 0x002BBFE8 File Offset: 0x002BA1E8
		private bool CheckMissionCompleteEnable()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return false;
			}
			NKMUserMissionData missionData = myUserData.m_MissionData;
			if (missionData == null)
			{
				return false;
			}
			NKMEventCollectionIndexTemplet eventCollectionIndexTemplet = NKCUIModuleLobby.GetEventCollectionIndexTemplet();
			if (eventCollectionIndexTemplet == null)
			{
				return false;
			}
			foreach (int nkm_MISSION_TAB_ID in eventCollectionIndexTemplet.MissionTabIds)
			{
				if (missionData.CheckCompletableMission(myUserData, nkm_MISSION_TAB_ID, false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060081C6 RID: 33222 RVA: 0x002BC074 File Offset: 0x002BA274
		public void OpenEventModuleHome(int iKey)
		{
			NKMEventCollectionIndexTemplet eventCollectionIndexTemplet = NKMTempletContainer<NKMEventCollectionIndexTemplet>.Find(iKey);
			this.OpenEventModuleHome(eventCollectionIndexTemplet);
		}

		// Token: 0x060081C7 RID: 33223 RVA: 0x002BC090 File Offset: 0x002BA290
		private void OnClickEventModuleHome()
		{
			NKMEventCollectionIndexTemplet eventCollectionIndexTemplet = NKCUIModuleLobby.GetEventCollectionIndexTemplet();
			this.OpenEventModuleHome(eventCollectionIndexTemplet);
		}

		// Token: 0x060081C8 RID: 33224 RVA: 0x002BC0AC File Offset: 0x002BA2AC
		private void OpenEventModuleHome(NKMEventCollectionIndexTemplet eventCollectionIndexTemplet)
		{
			if (eventCollectionIndexTemplet == null || !eventCollectionIndexTemplet.IsOpen)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_MENU_EXCEPTION_EVENT_EXPIRED_POPUP", false), delegate()
				{
					base.gameObject.SetActive(false);
				}, "");
				return;
			}
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(eventCollectionIndexTemplet.EventPrefabId, eventCollectionIndexTemplet.EventPrefabId);
			NKCUIManager.LoadedUIData instance = NKCUIManager.GetInstance(nkmassetName.m_AssetName.ToLower(), nkmassetName.m_AssetName, false);
			NKCUIModuleHome nkcuimoduleHome;
			if (instance == null)
			{
				nkcuimoduleHome = NKCUIModuleHome.MakeInstance(nkmassetName.m_AssetName, nkmassetName.m_AssetName);
			}
			else
			{
				nkcuimoduleHome = (instance.GetInstance() as NKCUIModuleHome);
			}
			if (nkcuimoduleHome != null)
			{
				nkcuimoduleHome.Open(eventCollectionIndexTemplet, 0);
			}
		}

		// Token: 0x04006DE9 RID: 28137
		public NKCUIComStateButton m_csbtnEventModule;

		// Token: 0x04006DEA RID: 28138
		public Image m_buttonIconImage;

		// Token: 0x04006DEB RID: 28139
		public Image m_buttonTitleImage;

		// Token: 0x04006DEC RID: 28140
		public Text m_lbRemainTime;

		// Token: 0x04006DED RID: 28141
		public GameObject m_objRedDot;
	}
}
