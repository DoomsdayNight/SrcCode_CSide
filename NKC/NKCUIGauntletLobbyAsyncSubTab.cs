using System;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000956 RID: 2390
	public class NKCUIGauntletLobbyAsyncSubTab : MonoBehaviour
	{
		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x06005F4C RID: 24396 RVA: 0x001D9D3F File Offset: 0x001D7F3F
		public int Tier
		{
			get
			{
				return this.m_iTier;
			}
		}

		// Token: 0x06005F4D RID: 24397 RVA: 0x001D9D48 File Offset: 0x001D7F48
		public static NKCUIGauntletLobbyAsyncSubTab GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_gauntlet", "NKM_UI_GAUNTLET_ASYNC_SUBTAB_BUTTON", false, null);
			NKCUIGauntletLobbyAsyncSubTab nkcuigauntletLobbyAsyncSubTab = (nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant.GetComponent<NKCUIGauntletLobbyAsyncSubTab>() : null;
			if (nkcuigauntletLobbyAsyncSubTab == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError(string.Format("{0} Prefab null!", "NKM_UI_GAUNTLET_ASYNC_SUBTAB_BUTTON"));
				return null;
			}
			nkcuigauntletLobbyAsyncSubTab.m_InstanceData = nkcassetInstanceData;
			nkcuigauntletLobbyAsyncSubTab.Init();
			if (parent != null)
			{
				nkcuigauntletLobbyAsyncSubTab.transform.SetParent(parent);
			}
			nkcuigauntletLobbyAsyncSubTab.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			nkcuigauntletLobbyAsyncSubTab.gameObject.SetActive(false);
			return nkcuigauntletLobbyAsyncSubTab;
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x001D9DEC File Offset: 0x001D7FEC
		public void Init()
		{
			NKCUtil.SetBindFunction(this.m_csbtnClick, new UnityAction(this.OnClick));
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x001D9E08 File Offset: 0x001D8008
		public void SetData(int _iTier, NKCUIGauntletLobbyAsyncSubTab.OnClickSubTab _callBack, bool bSelected)
		{
			NKCComText[] strName = this.m_strName;
			for (int i = 0; i < strName.Length; i++)
			{
				NKCUtil.SetLabelText(strName[i], string.Format("Tier {0}", _iTier));
			}
			this.m_iTier = _iTier;
			this.m_dCallBack = _callBack;
			this.m_Ani.enabled = false;
			NKCUtil.SetGameobjectActive(this.m_objLock, NKCScenManager.CurrentUserData().m_NpcData.MaxOpenedTier < this.m_iTier);
			this.OnSelect(bSelected);
		}

		// Token: 0x06005F50 RID: 24400 RVA: 0x001D9E85 File Offset: 0x001D8085
		public void OnClick()
		{
			if (this.m_objLock.activeSelf)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_GAUNTLET_ASYNC_NPC_BLOCK_DESC, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCUIGauntletLobbyAsyncSubTab.OnClickSubTab dCallBack = this.m_dCallBack;
			if (dCallBack == null)
			{
				return;
			}
			dCallBack(this.m_iTier);
		}

		// Token: 0x06005F51 RID: 24401 RVA: 0x001D9EBE File Offset: 0x001D80BE
		public void OnSelect(bool bSel)
		{
			if (this.m_objLock.activeSelf)
			{
				return;
			}
			this.m_csbtnClick.Select(bSel, true, false);
		}

		// Token: 0x06005F52 RID: 24402 RVA: 0x001D9EDD File Offset: 0x001D80DD
		public void OnActiveEffect()
		{
			this.m_Ani.enabled = true;
			this.m_Ani.SetTrigger("PLAY");
		}

		// Token: 0x04004B5E RID: 19294
		private const string ASSET_BUNDLE_PATH = "ab_ui_nkm_ui_gauntlet";

		// Token: 0x04004B5F RID: 19295
		private const string ASSET_BUNDLE_FILE_NAME = "NKM_UI_GAUNTLET_ASYNC_SUBTAB_BUTTON";

		// Token: 0x04004B60 RID: 19296
		public NKCUIComStateButton m_csbtnClick;

		// Token: 0x04004B61 RID: 19297
		public NKCComText[] m_strName;

		// Token: 0x04004B62 RID: 19298
		public GameObject m_objLock;

		// Token: 0x04004B63 RID: 19299
		private NKCUIGauntletLobbyAsyncSubTab.OnClickSubTab m_dCallBack;

		// Token: 0x04004B64 RID: 19300
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04004B65 RID: 19301
		private int m_iTier;

		// Token: 0x04004B66 RID: 19302
		public Animator m_Ani;

		// Token: 0x020015D8 RID: 5592
		// (Invoke) Token: 0x0600AE6A RID: 44650
		public delegate void OnClickSubTab(int iKey);
	}
}
