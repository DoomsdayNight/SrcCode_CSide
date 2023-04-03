using System;
using System.Collections;
using System.Collections.Generic;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI.Result
{
	// Token: 0x02000BB0 RID: 2992
	public class NKCUIResultSubUIRearmament : NKCUIResultSubUIBase
	{
		// Token: 0x06008A53 RID: 35411 RVA: 0x002F081C File Offset: 0x002EEA1C
		public void SetData(NKMRewardData rewardData, NKMRewardData synergyRewardData, bool bIgnoreAutoClose = false)
		{
			if (rewardData == null)
			{
				base.ProcessRequired = false;
				return;
			}
			foreach (NKCUISlot.SlotData data in NKCUISlot.MakeSlotDataListFromReward(rewardData, false, false))
			{
				NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_rtExtractRewardSlotParent);
				if (newInstance != null)
				{
					newInstance.SetData(data, true, null);
					NKCUtil.SetGameobjectActive(newInstance.gameObject, true);
					this.m_lstRewardSlot.Add(newInstance);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_ObjSynergy, synergyRewardData != null);
			if (synergyRewardData != null)
			{
				NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeMiscItemData(synergyRewardData.MiscItemDataList[0], 0);
				if (slotData != null)
				{
					this.m_SynergyRewardSlot.Init();
					this.m_SynergyRewardSlot.SetData(slotData, true, null);
				}
			}
			base.ProcessRequired = true;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_bIgnoreAutoClose = bIgnoreAutoClose;
		}

		// Token: 0x06008A54 RID: 35412 RVA: 0x002F0908 File Offset: 0x002EEB08
		public override void FinishProcess()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.m_bFinished = true;
			for (int i = 0; i < this.m_lstRewardSlot.Count; i++)
			{
				UnityEngine.Object.Destroy(this.m_lstRewardSlot[i]);
			}
			this.m_lstRewardSlot.Clear();
			base.StopAllCoroutines();
		}

		// Token: 0x06008A55 RID: 35413 RVA: 0x002F0962 File Offset: 0x002EEB62
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x06008A56 RID: 35414 RVA: 0x002F096A File Offset: 0x002EEB6A
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			this.m_bFinished = false;
			this.m_bHadUserInput = false;
			yield return null;
			yield break;
		}

		// Token: 0x06008A57 RID: 35415 RVA: 0x002F0979 File Offset: 0x002EEB79
		public override void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x04007703 RID: 30467
		[Header("재무장")]
		public GameObject m_ObjSynergy;

		// Token: 0x04007704 RID: 30468
		public NKCUISlot m_SynergyRewardSlot;

		// Token: 0x04007705 RID: 30469
		public RectTransform m_rtExtractRewardSlotParent;

		// Token: 0x04007706 RID: 30470
		private bool m_bFinished;

		// Token: 0x04007707 RID: 30471
		private List<NKCUISlot> m_lstRewardSlot = new List<NKCUISlot>();
	}
}
