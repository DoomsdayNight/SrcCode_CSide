using System;
using System.Collections.Generic;
using ClientPacket.Pvp;
using Cs.Logging;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B79 RID: 2937
	public class NKCUIGauntletLobbyReplay : MonoBehaviour
	{
		// Token: 0x0600873E RID: 34622 RVA: 0x002DBE4C File Offset: 0x002DA04C
		public void Init()
		{
			NKCReplayMgr.IsReplayRecordingOpened();
			if (!NKCReplayMgr.IsReplayOpened())
			{
				return;
			}
			this.m_csbtnPlaySelected.PointerClick.RemoveAllListeners();
			this.m_ctglTabDefault.OnValueChanged.RemoveAllListeners();
			this.m_lvsrDefault.dOnGetObject += this.GetObject;
			this.m_lvsrDefault.dOnReturnObject += this.ReturnSlot;
			this.m_lvsrDefault.dOnProvideData += this.ProvideData;
			this.m_lvsrDefault.ContentConstraintCount = 1;
			NKCUtil.SetScrollHotKey(this.m_lvsrDefault, null);
		}

		// Token: 0x0600873F RID: 34623 RVA: 0x002DBEE4 File Offset: 0x002DA0E4
		private void OnClickPlaySelected(int replayDataIndex)
		{
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.FIREBASE_CRASH_TEST))
			{
				this.OnClickPlaySelected(replayDataIndex);
			}
			if (this.m_listReplayData.Count <= replayDataIndex)
			{
				Log.Error(string.Format("[ReplayLobby] Play failed! index[{0}] count[{1}]", replayDataIndex, this.m_listReplayData.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLobbyReplay.cs", 76);
				return;
			}
			NKCScenManager.GetScenManager().GetNKCReplayMgr().StartPlaying(this.m_listReplayData[replayDataIndex]);
		}

		// Token: 0x06008740 RID: 34624 RVA: 0x002DBF56 File Offset: 0x002DA156
		private void OnClickSelectReplayData(int replayDataIndex)
		{
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.FIREBASE_CRASH_TEST))
			{
				((List<int>)null)[0] = 1;
			}
		}

		// Token: 0x06008741 RID: 34625 RVA: 0x002DBF68 File Offset: 0x002DA168
		public RectTransform GetObject(int index)
		{
			return NKCUIGauntletReplaySlot.GetNewInstance(this.m_trDefault, new NKCUIGauntletReplaySlot.OnSelectReplayData(this.OnClickSelectReplayData), new NKCUIGauntletReplaySlot.OnPlayReplay(this.OnClickPlaySelected)).GetComponent<RectTransform>();
		}

		// Token: 0x06008742 RID: 34626 RVA: 0x002DBF94 File Offset: 0x002DA194
		public void ReturnSlot(Transform tr)
		{
			NKCUIGauntletReplaySlot component = tr.GetComponent<NKCUIGauntletReplaySlot>();
			tr.SetParent(base.transform);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06008743 RID: 34627 RVA: 0x002DBFD0 File Offset: 0x002DA1D0
		public void ProvideData(Transform tr, int index)
		{
			NKCUIGauntletReplaySlot component = tr.GetComponent<NKCUIGauntletReplaySlot>();
			if (component != null)
			{
				if (this.m_listReplayData.Count <= index)
				{
					Debug.LogError(string.Format("Async PVP data 이상함. target : {0} <= {1}", this.m_listReplayData.Count, index));
				}
				component.SetUI(index, this.m_listReplayData[index]);
			}
		}

		// Token: 0x06008744 RID: 34628 RVA: 0x002DC033 File Offset: 0x002DA233
		private void RefreshScrollRect()
		{
			this.m_listReplayData.Clear();
			this.m_lvsrDefault.TotalCount = this.m_listReplayData.Count;
			this.m_lvsrDefault.RefreshCells(false);
		}

		// Token: 0x06008745 RID: 34629 RVA: 0x002DC064 File Offset: 0x002DA264
		public void SetUI()
		{
			if (!NKCReplayMgr.IsReplayOpened())
			{
				return;
			}
			if (!this.m_bPrepareLoopScrollCells)
			{
				NKCUtil.SetGameobjectActive(this.m_objScrollDefault, true);
				this.m_lvsrDefault.PrepareCells(0);
				this.m_bPrepareLoopScrollCells = true;
			}
			if (this.m_bFirstOpen)
			{
				this.m_bFirstOpen = false;
			}
			this.m_ctglTabDefault.Select(false, true, false);
			this.m_ctglTabDefault.Select(true, false, false);
			this.RefreshScrollRect();
		}

		// Token: 0x06008746 RID: 34630 RVA: 0x002DC0D3 File Offset: 0x002DA2D3
		public void Close()
		{
			this.m_bFirstOpen = true;
			NKCPopupGauntletBanList.CheckInstanceAndClose();
		}

		// Token: 0x06008747 RID: 34631 RVA: 0x002DC0E1 File Offset: 0x002DA2E1
		public void ClearCacheData()
		{
			if (!NKCReplayMgr.IsReplayOpened())
			{
				return;
			}
			this.m_lvsrDefault.ClearCells();
		}

		// Token: 0x040073A3 RID: 29603
		public Animator m_animator;

		// Token: 0x040073A4 RID: 29604
		[Header("상단 탭")]
		public NKCUIComToggle m_ctglTabDefault;

		// Token: 0x040073A5 RID: 29605
		[Header("스크롤 관련")]
		public GameObject m_objScrollDefault;

		// Token: 0x040073A6 RID: 29606
		public LoopVerticalScrollRect m_lvsrDefault;

		// Token: 0x040073A7 RID: 29607
		public Transform m_trDefault;

		// Token: 0x040073A8 RID: 29608
		[Header("재생하기")]
		public NKCUIComStateButton m_csbtnPlaySelected;

		// Token: 0x040073A9 RID: 29609
		private bool m_bFirstOpen = true;

		// Token: 0x040073AA RID: 29610
		private bool m_bPrepareLoopScrollCells;

		// Token: 0x040073AB RID: 29611
		private List<ReplayData> m_listReplayData = new List<ReplayData>();
	}
}
