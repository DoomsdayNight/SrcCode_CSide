using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A74 RID: 2676
	public class NKCPopupPointExchange : NKCUIBase
	{
		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x0600763E RID: 30270 RVA: 0x00275650 File Offset: 0x00273850
		public static NKCPopupPointExchange Instance
		{
			get
			{
				if (NKCPopupPointExchange.m_Instance == null)
				{
					NKCPopupPointExchange.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupPointExchange>("ab_ui_pointexchange", "AB_UI_POINTEXCHANGE", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupPointExchange.CleanUpInstance)).GetInstance<NKCPopupPointExchange>();
					NKCPopupPointExchange.m_Instance.InitUI();
				}
				return NKCPopupPointExchange.m_Instance;
			}
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x0600763F RID: 30271 RVA: 0x0027569F File Offset: 0x0027389F
		public static bool HasInstance
		{
			get
			{
				return NKCPopupPointExchange.m_Instance != null;
			}
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06007640 RID: 30272 RVA: 0x002756AC File Offset: 0x002738AC
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupPointExchange.m_Instance != null && NKCPopupPointExchange.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007641 RID: 30273 RVA: 0x002756C7 File Offset: 0x002738C7
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupPointExchange.m_Instance != null && NKCPopupPointExchange.m_Instance.IsOpen)
			{
				NKCPopupPointExchange.m_Instance.Close();
			}
		}

		// Token: 0x06007642 RID: 30274 RVA: 0x002756EC File Offset: 0x002738EC
		private static void CleanUpInstance()
		{
			NKCPopupPointExchange instance = NKCPopupPointExchange.m_Instance;
			if (instance != null)
			{
				instance.Release();
			}
			NKCPopupPointExchange.m_Instance = null;
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06007643 RID: 30275 RVA: 0x00275704 File Offset: 0x00273904
		public override string MenuName
		{
			get
			{
				return "Point Exchange";
			}
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06007644 RID: 30276 RVA: 0x0027570B File Offset: 0x0027390B
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06007645 RID: 30277 RVA: 0x0027570E File Offset: 0x0027390E
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x06007646 RID: 30278 RVA: 0x00275714 File Offset: 0x00273914
		private void InitUI()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(this.OnClickClose));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnInfomation, new UnityAction(this.OnClickInfomation));
			NKMPointExchangeTemplet byTime = NKMPointExchangeTemplet.GetByTime(NKCSynchronizedTime.ServiceTime);
			string text = "";
			if (byTime != null)
			{
				text = byTime.PrefabId;
				this.m_missionTabId = byTime.MissionTabId;
			}
			if (this.m_pointExchangeUI == null)
			{
				NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(text, text);
				this.m_pointExchangeUI = this.OpenInstanceByAssetName<NKCUIPointExchange>(nkmassetName.m_BundleName, nkmassetName.m_AssetName, this.m_UIParent);
				if (this.m_pointExchangeUI != null)
				{
					this.m_pointExchangeUI.Init(new NKCUIPointExchange.OnClose(this.OnClickClose), new NKCUIPointExchange.OnInformation(this.OnClickInfomation));
					this.m_pointExchangeUI.ResetUI();
				}
			}
			NKCUIEventBarMissionGroupList comMissionGroupList = this.m_comMissionGroupList;
			if (comMissionGroupList != null)
			{
				comMissionGroupList.Init("ab_ui_pointexchange", "POPUP_POINTEXCHANGE_MISSION_LIST_SLOT");
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007647 RID: 30279 RVA: 0x00275810 File Offset: 0x00273A10
		public override void CloseInternal()
		{
			foreach (NKCUIPointExchangeTransition nkcuipointExchangeTransition in NKCUIManager.GetOpenedUIsByType<NKCUIPointExchangeTransition>())
			{
				if (nkcuipointExchangeTransition.IsOpen)
				{
					nkcuipointExchangeTransition.Close();
				}
			}
			NKCUIPointExchange pointExchangeUI = this.m_pointExchangeUI;
			if (pointExchangeUI != null)
			{
				pointExchangeUI.RevertMusic();
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007648 RID: 30280 RVA: 0x00275888 File Offset: 0x00273A88
		public override void OnBackButton()
		{
			this.OnClickClose();
		}

		// Token: 0x06007649 RID: 30281 RVA: 0x00275890 File Offset: 0x00273A90
		public void Open()
		{
			base.gameObject.SetActive(true);
			NKCUIPointExchange pointExchangeUI = this.m_pointExchangeUI;
			if (pointExchangeUI != null)
			{
				pointExchangeUI.ResetUI();
			}
			NKCUIEventBarMissionGroupList comMissionGroupList = this.m_comMissionGroupList;
			if (comMissionGroupList != null)
			{
				comMissionGroupList.CloseImmediately();
			}
			base.UIOpened(true);
		}

		// Token: 0x0600764A RID: 30282 RVA: 0x002758C7 File Offset: 0x00273AC7
		public void RefreshPoint()
		{
			NKCUIPointExchange pointExchangeUI = this.m_pointExchangeUI;
			if (pointExchangeUI == null)
			{
				return;
			}
			pointExchangeUI.RefreshPoint();
		}

		// Token: 0x0600764B RID: 30283 RVA: 0x002758D9 File Offset: 0x00273AD9
		public void RefreshProduct()
		{
			if (this.m_pointExchangeUI != null)
			{
				this.m_pointExchangeUI.RefreshPoint();
				this.m_pointExchangeUI.RefreshScrollRect();
			}
		}

		// Token: 0x0600764C RID: 30284 RVA: 0x002758FF File Offset: 0x00273AFF
		public void RefreshMission()
		{
			if (this.m_comMissionGroupList == null || !this.m_comMissionGroupList.IsOpened())
			{
				return;
			}
			this.m_comMissionGroupList.Refresh();
		}

		// Token: 0x0600764D RID: 30285 RVA: 0x00275928 File Offset: 0x00273B28
		public void PlayMusic()
		{
			NKCUIPointExchange pointExchangeUI = this.m_pointExchangeUI;
			if (pointExchangeUI == null)
			{
				return;
			}
			pointExchangeUI.PlayMusic();
		}

		// Token: 0x0600764E RID: 30286 RVA: 0x0027593C File Offset: 0x00273B3C
		private T OpenInstanceByAssetName<T>(string BundleName, string AssetName, Transform parent) where T : MonoBehaviour
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(BundleName, AssetName, false, parent);
			if (nkcassetInstanceData == null || !(nkcassetInstanceData.m_Instant != null))
			{
				Debug.LogWarning("prefab is null - " + BundleName + "/" + AssetName);
				return default(T);
			}
			GameObject instant = nkcassetInstanceData.m_Instant;
			T component = instant.GetComponent<T>();
			if (component == null)
			{
				UnityEngine.Object.Destroy(instant);
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				return default(T);
			}
			this.m_assetInstanceData = nkcassetInstanceData;
			return component;
		}

		// Token: 0x0600764F RID: 30287 RVA: 0x002759BE File Offset: 0x00273BBE
		private bool CanClose()
		{
			if (this.m_comMissionGroupList != null && !this.m_comMissionGroupList.IsClosed())
			{
				this.m_comMissionGroupList.Close();
				return false;
			}
			return true;
		}

		// Token: 0x06007650 RID: 30288 RVA: 0x002759E9 File Offset: 0x00273BE9
		private void OnClickInfomation()
		{
			if (this.m_comMissionGroupList == null)
			{
				return;
			}
			if (this.m_comMissionGroupList.IsOpened())
			{
				this.m_comMissionGroupList.Close();
				return;
			}
			this.m_comMissionGroupList.Open(NKCUIEventBarMissionGroupList.MissionType.MissionTabId, this.m_missionTabId);
		}

		// Token: 0x06007651 RID: 30289 RVA: 0x00275A25 File Offset: 0x00273C25
		private void OnClickClose()
		{
			if (!this.CanClose())
			{
				return;
			}
			base.Close();
		}

		// Token: 0x06007652 RID: 30290 RVA: 0x00275A36 File Offset: 0x00273C36
		private void Release()
		{
			this.m_pointExchangeUI = null;
			NKCAssetResourceManager.CloseInstance(this.m_assetInstanceData);
			this.m_assetInstanceData = null;
		}

		// Token: 0x040062B4 RID: 25268
		public const string UI_ASSET_BUNDLE_NAME = "ab_ui_pointexchange";

		// Token: 0x040062B5 RID: 25269
		public const string UI_ASSET_NAME = "AB_UI_POINTEXCHANGE";

		// Token: 0x040062B6 RID: 25270
		private static NKCPopupPointExchange m_Instance;

		// Token: 0x040062B7 RID: 25271
		public GameObject m_objRoot;

		// Token: 0x040062B8 RID: 25272
		public Transform m_UIParent;

		// Token: 0x040062B9 RID: 25273
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x040062BA RID: 25274
		public NKCUIComStateButton m_csbtnInfomation;

		// Token: 0x040062BB RID: 25275
		public NKCUIEventBarMissionGroupList m_comMissionGroupList;

		// Token: 0x040062BC RID: 25276
		private NKCUIPointExchange m_pointExchangeUI;

		// Token: 0x040062BD RID: 25277
		private NKCAssetInstanceData m_assetInstanceData;

		// Token: 0x040062BE RID: 25278
		private int m_missionTabId;
	}
}
