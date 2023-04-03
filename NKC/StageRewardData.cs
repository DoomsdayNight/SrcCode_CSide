using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x0200092A RID: 2346
	public abstract class StageRewardData
	{
		// Token: 0x06005DEE RID: 24046 RVA: 0x001D065D File Offset: 0x001CE85D
		public StageRewardData(Transform slotParent)
		{
			this.m_cSlot = NKCUISlot.GetNewInstance(slotParent);
		}

		// Token: 0x06005DEF RID: 24047
		public abstract void CreateSlotData(NKMStageTempletV2 stageTemplet, NKMUserData cNKMUserData, List<int> listNRGI, List<NKCUISlot> listSlotToShow);

		// Token: 0x06005DF0 RID: 24048 RVA: 0x001D0671 File Offset: 0x001CE871
		public virtual void Release()
		{
			this.m_cSlot = null;
		}

		// Token: 0x06005DF1 RID: 24049 RVA: 0x001D067A File Offset: 0x001CE87A
		public void SetSlotActive(bool isActive)
		{
			if (this.m_cSlot == null)
			{
				return;
			}
			this.m_cSlot.SetActive(isActive);
		}

		// Token: 0x06005DF2 RID: 24050 RVA: 0x001D0698 File Offset: 0x001CE898
		protected void InitSlot(NKCUISlot cItemSlot)
		{
			cItemSlot.Init();
			cItemSlot.gameObject.GetComponent<RectTransform>().localScale = Vector2.one;
			Vector3 localPosition = cItemSlot.gameObject.GetComponent<RectTransform>().localPosition;
			cItemSlot.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(localPosition.x, localPosition.y, 0f);
		}

		// Token: 0x04004A31 RID: 18993
		protected NKCUISlot m_cSlot;
	}
}
