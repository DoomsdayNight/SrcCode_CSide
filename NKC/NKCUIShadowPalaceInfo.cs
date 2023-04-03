using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009DD RID: 2525
	public class NKCUIShadowPalaceInfo : MonoBehaviour
	{
		// Token: 0x06006C7E RID: 27774 RVA: 0x00237170 File Offset: 0x00235370
		public void Init(NKCUIShadowPalaceInfo.OnTouchStart onTouchStart, NKCUIShadowPalaceInfo.OnTouchProgress onTouchProgress, NKCUIShadowPalaceInfo.OnTouchRank onTouchRank)
		{
			NKCUIComStateButton btnRank = this.m_btnRank;
			if (btnRank != null)
			{
				btnRank.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnRank2 = this.m_btnRank;
			if (btnRank2 != null)
			{
				btnRank2.PointerClick.AddListener(new UnityAction(this.TouchRank));
			}
			NKCUIComResourceButton btnStart = this.m_btnStart;
			if (btnStart != null)
			{
				btnStart.PointerClick.RemoveAllListeners();
			}
			NKCUIComResourceButton btnStart2 = this.m_btnStart;
			if (btnStart2 != null)
			{
				btnStart2.PointerClick.AddListener(new UnityAction(this.TouchStart));
			}
			NKCUIComStateButton btnProgress = this.m_btnProgress;
			if (btnProgress != null)
			{
				btnProgress.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnProgress2 = this.m_btnProgress;
			if (btnProgress2 != null)
			{
				btnProgress2.PointerClick.AddListener(new UnityAction(this.TouchProgress));
			}
			this.dOnTouchStart = onTouchStart;
			this.dOnTouchProgress = onTouchProgress;
			this.dOnTouchRank = onTouchRank;
			this.m_ani.Play("NKM_UI_SHADOW_INFO_INTRO_IDLE");
		}

		// Token: 0x06006C7F RID: 27775 RVA: 0x0023724C File Offset: 0x0023544C
		public void SetData(NKMShadowPalaceTemplet palaceTemplet, int currSkipCount, bool bCurrentPalace, bool bUnlock)
		{
			NKCUtil.SetLabelText(this.m_txtNum, string.Format(NKCUtilString.GET_SHADOW_PALACE_NUMBER, palaceTemplet.PALACE_NUM_UI));
			NKCUtil.SetLabelText(this.m_txtName, palaceTemplet.PalaceName);
			List<NKMShadowBattleTemplet> battleTemplets = NKMShadowPalaceManager.GetBattleTemplets(palaceTemplet.PALACE_ID);
			if (battleTemplets == null)
			{
				Debug.LogError(string.Format("ShadowBattleTemplet 찾을 수 없음 - palace#{0}", palaceTemplet.PALACE_NUM_UI));
				return;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(battleTemplets[battleTemplets.Count - 1].DUNGEON_ID);
			string msg = string.Empty;
			if (dungeonTempletBase != null)
			{
				msg = dungeonTempletBase.m_DungeonLevel.ToString();
			}
			NKCUtil.SetLabelText(this.m_txtLv, msg);
			NKCUtil.SetLabelText(this.m_txtBattleCnt, battleTemplets.Count.ToString());
			int count = this.m_lstRewardSlot.Count;
			int count2 = palaceTemplet.COMPLETE_REWARDS.Count;
			for (int i = count; i < count2; i++)
			{
				NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_trReward);
				this.m_lstRewardSlot.Add(newInstance);
			}
			for (int j = 0; j < this.m_lstRewardSlot.Count; j++)
			{
				NKCUISlot nkcuislot = this.m_lstRewardSlot[j];
				if (j < count2)
				{
					NKCUtil.SetGameobjectActive(nkcuislot, true);
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(palaceTemplet.COMPLETE_REWARDS[j], 0);
					nkcuislot.SetData(data, true, null);
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuislot, false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_btnStart, bUnlock && !bCurrentPalace);
			NKCUtil.SetGameobjectActive(this.m_btnProgress, bUnlock && bCurrentPalace);
			NKCUtil.SetGameobjectActive(this.m_objDenied, !bUnlock);
			NKCUtil.SetGameobjectActive(this.m_objLockBtn, !bUnlock);
			if (bUnlock)
			{
				NKCUtil.SetLabelText(this.m_txtDesc, palaceTemplet.PalaceDesc);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_txtDesc, string.Empty);
			}
			int itemCount = palaceTemplet.STAGE_REQ_ITEM_COUNT * currSkipCount;
			this.m_btnStart.SetData(palaceTemplet.STAGE_REQ_ITEM_ID, itemCount);
		}

		// Token: 0x06006C80 RID: 27776 RVA: 0x00237431 File Offset: 0x00235631
		private void TouchRank()
		{
			NKCUIShadowPalaceInfo.OnTouchRank onTouchRank = this.dOnTouchRank;
			if (onTouchRank == null)
			{
				return;
			}
			onTouchRank();
		}

		// Token: 0x06006C81 RID: 27777 RVA: 0x00237443 File Offset: 0x00235643
		private void TouchStart()
		{
			NKCUIShadowPalaceInfo.OnTouchStart onTouchStart = this.dOnTouchStart;
			if (onTouchStart == null)
			{
				return;
			}
			onTouchStart();
		}

		// Token: 0x06006C82 RID: 27778 RVA: 0x00237455 File Offset: 0x00235655
		private void TouchProgress()
		{
			NKCUIShadowPalaceInfo.OnTouchProgress onTouchProgress = this.dOnTouchProgress;
			if (onTouchProgress == null)
			{
				return;
			}
			onTouchProgress();
		}

		// Token: 0x06006C83 RID: 27779 RVA: 0x00237467 File Offset: 0x00235667
		public void PlayIntroAni()
		{
			this.m_ani.Play("NKM_UI_SHADOW_INFO_INTRO");
		}

		// Token: 0x0400582E RID: 22574
		[Header("INFO")]
		public Text m_txtNum;

		// Token: 0x0400582F RID: 22575
		public Text m_txtName;

		// Token: 0x04005830 RID: 22576
		public Text m_txtDesc;

		// Token: 0x04005831 RID: 22577
		public Text m_txtLv;

		// Token: 0x04005832 RID: 22578
		public Text m_txtBattleCnt;

		// Token: 0x04005833 RID: 22579
		[Header("REWARD")]
		public Transform m_trReward;

		// Token: 0x04005834 RID: 22580
		[Header("LOCK")]
		public GameObject m_objDenied;

		// Token: 0x04005835 RID: 22581
		public GameObject m_objLockBtn;

		// Token: 0x04005836 RID: 22582
		[Header("ANIMATION")]
		public Animator m_ani;

		// Token: 0x04005837 RID: 22583
		[Header("BUTTON")]
		public NKCUIComStateButton m_btnRank;

		// Token: 0x04005838 RID: 22584
		public NKCUIComStateButton m_btnProgress;

		// Token: 0x04005839 RID: 22585
		public NKCUIComResourceButton m_btnStart;

		// Token: 0x0400583A RID: 22586
		[Header("COST")]
		public Image m_imgCost;

		// Token: 0x0400583B RID: 22587
		public Text m_txtCost;

		// Token: 0x0400583C RID: 22588
		private NKCUIShadowPalaceInfo.OnTouchStart dOnTouchStart;

		// Token: 0x0400583D RID: 22589
		private NKCUIShadowPalaceInfo.OnTouchProgress dOnTouchProgress;

		// Token: 0x0400583E RID: 22590
		private NKCUIShadowPalaceInfo.OnTouchRank dOnTouchRank;

		// Token: 0x0400583F RID: 22591
		private List<NKCUISlot> m_lstRewardSlot = new List<NKCUISlot>();

		// Token: 0x020016E9 RID: 5865
		// (Invoke) Token: 0x0600B1AD RID: 45485
		public delegate void OnTouchStart();

		// Token: 0x020016EA RID: 5866
		// (Invoke) Token: 0x0600B1B1 RID: 45489
		public delegate void OnTouchProgress();

		// Token: 0x020016EB RID: 5867
		// (Invoke) Token: 0x0600B1B5 RID: 45493
		public delegate void OnTouchRank();
	}
}
