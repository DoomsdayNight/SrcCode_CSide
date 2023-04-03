using System;
using System.Collections.Generic;
using ClientPacket.LeaderBoard;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BB9 RID: 3001
	public class NKCUIPopUpFierceBattleRankSide : MonoBehaviour
	{
		// Token: 0x06008A95 RID: 35477 RVA: 0x002F20C0 File Offset: 0x002F02C0
		public void Init()
		{
			if (this.m_FIERCE_BATTLE_BOSS_PERSONAL_RANK_ScrollRect != null)
			{
				this.m_FIERCE_BATTLE_BOSS_PERSONAL_RANK_ScrollRect.dOnGetObject += this.GetObject;
				this.m_FIERCE_BATTLE_BOSS_PERSONAL_RANK_ScrollRect.dOnProvideData += this.ProvideData;
				this.m_FIERCE_BATTLE_BOSS_PERSONAL_RANK_ScrollRect.dOnReturnObject += this.ReturnObject;
				this.m_FIERCE_BATTLE_BOSS_PERSONAL_RANK_ScrollRect.PrepareCells(0);
			}
			NKCUtil.SetBindFunction(this.m_BUTTON_X, delegate()
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
			});
		}

		// Token: 0x06008A96 RID: 35478 RVA: 0x002F2144 File Offset: 0x002F0344
		public void Open()
		{
			bool flag = false;
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null)
			{
				string targetBossName = nkcfierceBattleSupportDataMgr.GetTargetBossName(nkcfierceBattleSupportDataMgr.CurBossGroupID, nkcfierceBattleSupportDataMgr.GetCurSelectedBossLv());
				NKCUtil.SetLabelText(this.m_BossName, targetBossName);
				if (nkcfierceBattleSupportDataMgr.IsHasFierceRankingData(true))
				{
					this.m_FIERCE_BATTLE_BOSS_PERSONAL_RANK_ScrollRect.TotalCount = Mathf.Min(nkcfierceBattleSupportDataMgr.GetBossGroupRankingDataCnt(0), 50);
					this.m_FIERCE_BATTLE_BOSS_PERSONAL_RANK_ScrollRect.RefreshCells(false);
					flag = true;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BOSS_PERSONAL_RANK_ScrollRect.gameObject, flag);
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BOSS_PERSONAL_RANK_NODATA, !flag);
		}

		// Token: 0x06008A97 RID: 35479 RVA: 0x002F21D0 File Offset: 0x002F03D0
		public void Clear()
		{
			for (int i = 0; i < this.m_lstVisible.Count; i++)
			{
				this.m_stk.Push(this.m_lstVisible[i]);
			}
			while (this.m_stk.Count > 0)
			{
				NKCUIFierceBattleBossPersonalRankSlot nkcuifierceBattleBossPersonalRankSlot = this.m_stk.Pop();
				if (nkcuifierceBattleBossPersonalRankSlot != null)
				{
					nkcuifierceBattleBossPersonalRankSlot.DestoryInstance();
				}
			}
		}

		// Token: 0x06008A98 RID: 35480 RVA: 0x002F2230 File Offset: 0x002F0430
		private RectTransform GetObject(int index)
		{
			NKCUIFierceBattleBossPersonalRankSlot nkcuifierceBattleBossPersonalRankSlot;
			if (this.m_stk.Count > 0)
			{
				nkcuifierceBattleBossPersonalRankSlot = this.m_stk.Pop();
			}
			else
			{
				nkcuifierceBattleBossPersonalRankSlot = NKCUIFierceBattleBossPersonalRankSlot.GetNewInstance(this.m_FIERCE_BATTLE_BOSS_PERSONAL_RANK_ScrollRect.content.transform);
			}
			this.m_lstVisible.Add(nkcuifierceBattleBossPersonalRankSlot);
			if (nkcuifierceBattleBossPersonalRankSlot == null)
			{
				return null;
			}
			return nkcuifierceBattleBossPersonalRankSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008A99 RID: 35481 RVA: 0x002F2288 File Offset: 0x002F0488
		private void ReturnObject(Transform tr)
		{
			NKCUIFierceBattleBossPersonalRankSlot component = tr.GetComponent<NKCUIFierceBattleBossPersonalRankSlot>();
			this.m_lstVisible.Remove(component);
			this.m_stk.Push(component);
			tr.SetParent(base.transform);
			NKCUtil.SetGameobjectActive(component, false);
		}

		// Token: 0x06008A9A RID: 35482 RVA: 0x002F22C8 File Offset: 0x002F04C8
		private void ProvideData(Transform tr, int idx)
		{
			NKCUIFierceBattleBossPersonalRankSlot component = tr.GetComponent<NKCUIFierceBattleBossPersonalRankSlot>();
			int rank = idx + 1;
			NKMFierceData fierceRankingData = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr().GetFierceRankingData(idx);
			if (fierceRankingData == null)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			component.SetData(fierceRankingData, rank);
		}

		// Token: 0x04007760 RID: 30560
		public Text m_BossName;

		// Token: 0x04007761 RID: 30561
		public NKCUIComStateButton m_BUTTON_X;

		// Token: 0x04007762 RID: 30562
		public LoopScrollRect m_FIERCE_BATTLE_BOSS_PERSONAL_RANK_ScrollRect;

		// Token: 0x04007763 RID: 30563
		public GameObject m_FIERCE_BATTLE_BOSS_PERSONAL_RANK_NODATA;

		// Token: 0x04007764 RID: 30564
		private Stack<NKCUIFierceBattleBossPersonalRankSlot> m_stk = new Stack<NKCUIFierceBattleBossPersonalRankSlot>();

		// Token: 0x04007765 RID: 30565
		private List<NKCUIFierceBattleBossPersonalRankSlot> m_lstVisible = new List<NKCUIFierceBattleBossPersonalRankSlot>();
	}
}
