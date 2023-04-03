using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B28 RID: 2856
	public class NKCPopupGuildCoopArtifactList : NKCUIBase
	{
		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x0600821C RID: 33308 RVA: 0x002BE6E4 File Offset: 0x002BC8E4
		public static NKCPopupGuildCoopArtifactList Instance
		{
			get
			{
				if (NKCPopupGuildCoopArtifactList.m_Instance == null)
				{
					NKCPopupGuildCoopArtifactList.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildCoopArtifactList>("AB_UI_NKM_UI_CONSORTIUM_COOP", "NKM_UI_POPUP_CONSORTIUM_COOP_ARITFACT_LIST", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null).GetInstance<NKCPopupGuildCoopArtifactList>();
					if (NKCPopupGuildCoopArtifactList.m_Instance != null)
					{
						NKCPopupGuildCoopArtifactList.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildCoopArtifactList.m_Instance;
			}
		}

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x0600821D RID: 33309 RVA: 0x002BE73A File Offset: 0x002BC93A
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildCoopArtifactList.m_Instance != null && NKCPopupGuildCoopArtifactList.m_Instance.IsOpen;
			}
		}

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x0600821E RID: 33310 RVA: 0x002BE755 File Offset: 0x002BC955
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x0600821F RID: 33311 RVA: 0x002BE758 File Offset: 0x002BC958
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06008220 RID: 33312 RVA: 0x002BE760 File Offset: 0x002BC960
		public void InitUI()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loop.dOnGetObject += this.GetObject;
			this.m_loop.dOnReturnObject += this.ReturnObject;
			this.m_loop.dOnProvideData += this.ProvideData;
			this.m_loop.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loop, null);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008221 RID: 33313 RVA: 0x002BE80E File Offset: 0x002BCA0E
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008222 RID: 33314 RVA: 0x002BE81C File Offset: 0x002BCA1C
		private void OnDestroy()
		{
			NKCPopupGuildCoopArtifactList.m_Instance = null;
		}

		// Token: 0x06008223 RID: 33315 RVA: 0x002BE824 File Offset: 0x002BCA24
		private RectTransform GetObject(int idx)
		{
			NKCPopupGuildCoopArtifactListSlot nkcpopupGuildCoopArtifactListSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcpopupGuildCoopArtifactListSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcpopupGuildCoopArtifactListSlot = UnityEngine.Object.Instantiate<NKCPopupGuildCoopArtifactListSlot>(this.m_pfbSlot);
				nkcpopupGuildCoopArtifactListSlot.Init();
			}
			nkcpopupGuildCoopArtifactListSlot.transform.SetParent(this.m_trParent);
			return nkcpopupGuildCoopArtifactListSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008224 RID: 33316 RVA: 0x002BE878 File Offset: 0x002BCA78
		private void ReturnObject(Transform tr)
		{
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
			tr.GetComponent<NKCPopupGuildCoopArtifactListSlot>();
		}

		// Token: 0x06008225 RID: 33317 RVA: 0x002BE894 File Offset: 0x002BCA94
		private void ProvideData(Transform tr, int idx)
		{
			NKCPopupGuildCoopArtifactListSlot component = tr.GetComponent<NKCPopupGuildCoopArtifactListSlot>();
			if (component == null)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			tr.SetParent(this.m_trParent);
			NKCUtil.SetGameobjectActive(component, true);
			component.SetData(this.m_lstTemplet[idx]);
			component.SetClear(idx < this.m_CurrentIndex);
			component.SetCurrent(idx == this.m_CurrentIndex);
			component.m_slot.GetComponent<NKCUIComRaycastTarget>().enabled = false;
		}

		// Token: 0x06008226 RID: 33318 RVA: 0x002BE910 File Offset: 0x002BCB10
		public void Open(GuildDungeonInfoTemplet guildDungeonInfoTemplet, int clearCount)
		{
			this.m_CurrentIndex = clearCount;
			this.m_lstTemplet = GuildDungeonTempletManager.GetDungeonArtifactList(guildDungeonInfoTemplet.GetStageRewardArtifactGroup());
			if (this.m_lstTemplet == null || this.m_lstTemplet.Count == 0)
			{
				this.m_lstTemplet = new List<GuildDungeonArtifactTemplet>();
				Log.Error(string.Format("ArtifactCount is 0 - id : {0}", guildDungeonInfoTemplet.GetStageRewardArtifactGroup()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/GuildCoop/NKCPopupGuildCoopArtifactList.cs", 142);
			}
			NKCUtil.SetLabelText(this.m_lbTitle, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_ARTIFACT_DUNGEON_POPUP_TITLE, guildDungeonInfoTemplet.GetArenaIndex()));
			NKCUtil.SetLabelText(this.m_lbArtifactCount, string.Format("<color=#FFCF3B>{0}</color>/{1}", this.m_CurrentIndex, this.m_lstTemplet.Count));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loop.TotalCount = this.m_lstTemplet.Count;
			this.m_loop.SetIndexPosition(Mathf.Min(this.m_loop.TotalCount, this.m_CurrentIndex));
			base.UIOpened(true);
		}

		// Token: 0x04006E45 RID: 28229
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM_COOP";

		// Token: 0x04006E46 RID: 28230
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_CONSORTIUM_COOP_ARITFACT_LIST";

		// Token: 0x04006E47 RID: 28231
		private static NKCPopupGuildCoopArtifactList m_Instance;

		// Token: 0x04006E48 RID: 28232
		public NKCPopupGuildCoopArtifactListSlot m_pfbSlot;

		// Token: 0x04006E49 RID: 28233
		public Text m_lbTitle;

		// Token: 0x04006E4A RID: 28234
		public Text m_lbArtifactCount;

		// Token: 0x04006E4B RID: 28235
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04006E4C RID: 28236
		public LoopScrollRect m_loop;

		// Token: 0x04006E4D RID: 28237
		public Transform m_trParent;

		// Token: 0x04006E4E RID: 28238
		private Stack<NKCPopupGuildCoopArtifactListSlot> m_stkSlot = new Stack<NKCPopupGuildCoopArtifactListSlot>();

		// Token: 0x04006E4F RID: 28239
		private List<GuildDungeonArtifactTemplet> m_lstTemplet = new List<GuildDungeonArtifactTemplet>();

		// Token: 0x04006E50 RID: 28240
		private int m_CurrentIndex;
	}
}
