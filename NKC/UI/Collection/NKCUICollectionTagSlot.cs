using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C2C RID: 3116
	public class NKCUICollectionTagSlot : MonoBehaviour
	{
		// Token: 0x06009073 RID: 36979 RVA: 0x0031317B File Offset: 0x0031137B
		public void TagClicked()
		{
			if (this.m_Callback != null)
			{
				this.m_Callback(this.m_tagType, this.m_idx);
			}
		}

		// Token: 0x06009074 RID: 36980 RVA: 0x0031319C File Offset: 0x0031139C
		private void Awake()
		{
			this.m_btn_Toggle = base.GetComponent<NKCUIComStateButton>();
			if (null != this.m_btn_Toggle)
			{
				this.m_btn_Toggle.PointerClick.AddListener(delegate()
				{
					this.TagClicked();
				});
			}
		}

		// Token: 0x06009075 RID: 36981 RVA: 0x003131D4 File Offset: 0x003113D4
		public int GetSlotIndex()
		{
			return this.m_idx;
		}

		// Token: 0x06009076 RID: 36982 RVA: 0x003131DC File Offset: 0x003113DC
		public short GetTagType()
		{
			return this.m_tagType;
		}

		// Token: 0x06009077 RID: 36983 RVA: 0x003131E4 File Offset: 0x003113E4
		public void SetData(NKCUICollectionTagSlot.OnSelected callback, short type, int slotIdx, bool bActive, string title, int count, bool bTop)
		{
			this.m_Callback = callback;
			this.m_tagType = type;
			this.m_idx = slotIdx;
			NKCUtil.SetGameobjectActive(this.m_ON, bActive);
			NKCUtil.SetGameobjectActive(this.m_OFF, !bActive);
			NKCUtil.SetLabelText(this.m_ON_TEXT, title);
			NKCUtil.SetLabelText(this.m_OFF_TEXT, title);
			NKCUtil.SetLabelText(this.m_ON_COUNT_COUNT_TEXT, count.ToString());
			NKCUtil.SetLabelText(this.m_OFF_COUNT_COUNT_TEXT, count.ToString());
			NKCUtil.SetGameobjectActive(this.m_offCrownIcon, bTop);
			NKCUtil.SetGameobjectActive(this.m_onCrownIcon, bTop);
		}

		// Token: 0x06009078 RID: 36984 RVA: 0x0031327C File Offset: 0x0031147C
		public void UpdateVoteData(int count, bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_ON, bActive);
			NKCUtil.SetGameobjectActive(this.m_OFF, !bActive);
			NKCUtil.SetLabelText(this.m_ON_COUNT_COUNT_TEXT, count.ToString());
			NKCUtil.SetLabelText(this.m_OFF_COUNT_COUNT_TEXT, count.ToString());
		}

		// Token: 0x04007D96 RID: 32150
		public GameObject m_OFF;

		// Token: 0x04007D97 RID: 32151
		public GameObject m_ON;

		// Token: 0x04007D98 RID: 32152
		public Text m_OFF_COUNT_COUNT_TEXT;

		// Token: 0x04007D99 RID: 32153
		public Text m_ON_COUNT_COUNT_TEXT;

		// Token: 0x04007D9A RID: 32154
		public Text m_OFF_TEXT;

		// Token: 0x04007D9B RID: 32155
		public Text m_ON_TEXT;

		// Token: 0x04007D9C RID: 32156
		public GameObject m_offCrownIcon;

		// Token: 0x04007D9D RID: 32157
		public GameObject m_onCrownIcon;

		// Token: 0x04007D9E RID: 32158
		private NKCUIComStateButton m_btn_Toggle;

		// Token: 0x04007D9F RID: 32159
		private NKCUICollectionTagSlot.OnSelected m_Callback;

		// Token: 0x04007DA0 RID: 32160
		private int m_idx;

		// Token: 0x04007DA1 RID: 32161
		private short m_tagType;

		// Token: 0x020019EF RID: 6639
		// (Invoke) Token: 0x0600BA9B RID: 47771
		public delegate void OnSelected(short tagType, int Idx);
	}
}
