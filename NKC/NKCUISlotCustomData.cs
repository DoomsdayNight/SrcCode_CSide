using System;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x020009EA RID: 2538
	[RequireComponent(typeof(NKCUISlot))]
	public class NKCUISlotCustomData : MonoBehaviour
	{
		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06006DA5 RID: 28069 RVA: 0x0023F8DB File Offset: 0x0023DADB
		// (set) Token: 0x06006DA6 RID: 28070 RVA: 0x0023F8E3 File Offset: 0x0023DAE3
		public NKCUISlot Slot { get; private set; }

		// Token: 0x06006DA7 RID: 28071 RVA: 0x0023F8EC File Offset: 0x0023DAEC
		private void Awake()
		{
			this.Slot = base.GetComponent<NKCUISlot>();
		}

		// Token: 0x06006DA8 RID: 28072 RVA: 0x0023F8FA File Offset: 0x0023DAFA
		public void Init()
		{
			this.Slot.Init();
		}

		// Token: 0x06006DA9 RID: 28073 RVA: 0x0023F907 File Offset: 0x0023DB07
		public void SetData(int data, NKCUISlot.SlotData slotData, bool bEnableLayoutElement = true, NKCUISlotCustomData.OnClick onClick = null)
		{
			this.Slot.SetData(slotData, bEnableLayoutElement, new NKCUISlot.OnClick(this.OnSlotClick));
			this.m_Data = data;
			this.dOnClick = onClick;
		}

		// Token: 0x06006DAA RID: 28074 RVA: 0x0023F931 File Offset: 0x0023DB31
		public void SetData(int data, NKCUISlot.SlotData slotData, bool bShowName, bool bShowNumber, bool bEnableLayoutElement, NKCUISlotCustomData.OnClick onClick)
		{
			this.Slot.SetData(slotData, bShowName, bShowNumber, bEnableLayoutElement, new NKCUISlot.OnClick(this.OnSlotClick));
			this.m_Data = data;
			this.dOnClick = onClick;
		}

		// Token: 0x06006DAB RID: 28075 RVA: 0x0023F95F File Offset: 0x0023DB5F
		public void SetOnClick(NKCUISlotCustomData.OnClick onClick)
		{
			this.Slot.SetOnClick(new NKCUISlot.OnClick(this.OnSlotClick));
			this.dOnClick = onClick;
		}

		// Token: 0x06006DAC RID: 28076 RVA: 0x0023F97F File Offset: 0x0023DB7F
		private void OnSlotClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKCUISlotCustomData.OnClick onClick = this.dOnClick;
			if (onClick == null)
			{
				return;
			}
			onClick(slotData, bLocked, this.m_Data);
		}

		// Token: 0x04005944 RID: 22852
		public int m_Data;

		// Token: 0x04005945 RID: 22853
		private NKCUISlotCustomData.OnClick dOnClick;

		// Token: 0x020016FB RID: 5883
		// (Invoke) Token: 0x0600B1F0 RID: 45552
		public delegate void OnClick(NKCUISlot.SlotData slotData, bool bLocked, int data);
	}
}
