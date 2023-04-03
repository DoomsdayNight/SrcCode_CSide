using System;
using UnityEngine;

namespace NKC.UI.Shop
{
	// Token: 0x02000ACD RID: 2765
	public class NKCPopupShopCustomPackageSubstitudeSlot : MonoBehaviour
	{
		// Token: 0x06007BEB RID: 31723 RVA: 0x00295B45 File Offset: 0x00293D45
		public void Init()
		{
			NKCUISlot slotBefore = this.m_slotBefore;
			if (slotBefore != null)
			{
				slotBefore.Init();
			}
			NKCUISlot slotAfter = this.m_slotAfter;
			if (slotAfter == null)
			{
				return;
			}
			slotAfter.Init();
		}

		// Token: 0x06007BEC RID: 31724 RVA: 0x00295B68 File Offset: 0x00293D68
		public void SetData(NKCUISlot.SlotData before, NKCUISlot.SlotData after)
		{
			if (this.m_slotBefore != null)
			{
				this.m_slotBefore.SetData(before, true, null);
				this.m_slotBefore.SetOnClickAction(new NKCUISlot.SlotClickType[1]);
			}
			if (this.m_slotAfter != null)
			{
				this.m_slotAfter.SetData(after, true, null);
				this.m_slotAfter.SetOnClickAction(new NKCUISlot.SlotClickType[1]);
			}
		}

		// Token: 0x040068A4 RID: 26788
		public NKCUISlot m_slotBefore;

		// Token: 0x040068A5 RID: 26789
		public NKCUISlot m_slotAfter;
	}
}
