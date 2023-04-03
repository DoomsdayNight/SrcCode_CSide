using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000753 RID: 1875
	public class NKCUIComCaption : MonoBehaviour
	{
		// Token: 0x06004B09 RID: 19209 RVA: 0x00167D49 File Offset: 0x00165F49
		public NKCUIComCaption.CaptionData GetCaptionData()
		{
			return this.m_CaptionData;
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06004B0A RID: 19210 RVA: 0x00167D51 File Offset: 0x00165F51
		public bool IsActive
		{
			get
			{
				return this.m_CaptionData is NKCUIComCaption.CaptionDataTime || this.m_CaptionData.key != int.MinValue || base.gameObject.activeSelf;
			}
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x00167D81 File Offset: 0x00165F81
		private void Awake()
		{
			this.m_Background = base.gameObject.GetComponentInChildren<Image>();
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x00167D94 File Offset: 0x00165F94
		public void SetEnableBackgound(bool bEnable)
		{
			if (null != this.m_Background)
			{
				this.m_Background.enabled = bEnable;
			}
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x00167DB0 File Offset: 0x00165FB0
		private void SaveOrgTextPos()
		{
			if (this.m_lbCaption != null)
			{
				this.m_OrgTextPos = this.m_lbCaption.rectTransform.anchoredPosition;
			}
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x00167DD6 File Offset: 0x00165FD6
		public void RestoreTextPos()
		{
			if (this.m_lbCaption != null)
			{
				this.m_lbCaption.rectTransform.anchoredPosition = this.m_OrgTextPos;
			}
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x00167DFC File Offset: 0x00165FFC
		public void SetTextPosOffset(float offsetX, float offsetY)
		{
			this.m_ReservedTextOffsetPos = new Vector2(offsetX, offsetY);
			this.m_bReservedTextOffset = true;
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x00167E12 File Offset: 0x00166012
		public void ResetCaption()
		{
			this.RestoreTextPos();
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x00167E1A File Offset: 0x0016601A
		public bool SetData(string caption, int soundUID)
		{
			return this.SetData(new NKCUIComCaption.CaptionData(caption, soundUID));
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x00167E2C File Offset: 0x0016602C
		public bool SetData(NKCUIComCaption.CaptionData captionData)
		{
			if (string.IsNullOrEmpty(captionData.caption) || captionData.key == -2147483648)
			{
				this.CloseCaption();
				return false;
			}
			if (this.m_bReservedTextOffset)
			{
				this.m_bReservedTextOffset = false;
				if (this.m_lbCaption != null)
				{
					this.m_lbCaption.rectTransform.anchoredPosition = new Vector2(this.m_OrgTextPos.x + this.m_ReservedTextOffsetPos.x, this.m_OrgTextPos.y + this.m_ReservedTextOffsetPos.y);
				}
			}
			this.SetEnableBackgound(true);
			this.m_CaptionData = captionData;
			string msg = NKCUtil.TextSplitLine(captionData.caption, this.m_lbCaption, 0f);
			NKCUtil.SetLabelText(this.m_lbCaption, msg);
			NKCUtil.SetGameobjectActive(this, true);
			return true;
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x00167EF4 File Offset: 0x001660F4
		public bool SetData(NKCUIComCaption.CaptionDataTime captionData)
		{
			if (string.IsNullOrEmpty(captionData.caption))
			{
				this.CloseCaption();
				return false;
			}
			this.m_CaptionData = captionData;
			this.SetEnableBackgound(!captionData.hideBackground);
			string msg = NKCUtil.TextSplitLine(captionData.caption, this.m_lbCaption, 0f);
			NKCUtil.SetLabelText(this.m_lbCaption, msg);
			NKCUtil.SetGameobjectActive(this, true);
			return true;
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x00167F57 File Offset: 0x00166157
		public void CloseCaption()
		{
			if (this.m_CaptionData != null)
			{
				this.m_CaptionData.key = int.MinValue;
			}
			NKCUtil.SetGameobjectActive(this, false);
		}

		// Token: 0x040039BB RID: 14779
		public Text m_lbCaption;

		// Token: 0x040039BC RID: 14780
		private const int INVALID_SOUND_UID = -2147483648;

		// Token: 0x040039BD RID: 14781
		private NKCUIComCaption.CaptionData m_CaptionData;

		// Token: 0x040039BE RID: 14782
		public Image m_Background;

		// Token: 0x040039BF RID: 14783
		private Vector2 m_OrgTextPos;

		// Token: 0x040039C0 RID: 14784
		private bool m_bReservedTextOffset;

		// Token: 0x040039C1 RID: 14785
		private Vector2 m_ReservedTextOffsetPos;

		// Token: 0x02001429 RID: 5161
		public class CaptionData
		{
			// Token: 0x0600A804 RID: 43012 RVA: 0x0034BA87 File Offset: 0x00349C87
			public CaptionData(string _caption, int _key)
			{
				this.caption = _caption;
				this.key = _key;
			}

			// Token: 0x04009D9B RID: 40347
			public string caption;

			// Token: 0x04009D9C RID: 40348
			public int key;
		}

		// Token: 0x0200142A RID: 5162
		public class CaptionDataTime : NKCUIComCaption.CaptionData
		{
			// Token: 0x0600A805 RID: 43013 RVA: 0x0034BA9D File Offset: 0x00349C9D
			public CaptionDataTime(string _caption, int _key, long _startTime, long _endTime, bool _hideBackground) : base(_caption, _key)
			{
				this.startTime = _startTime;
				this.endTime = _endTime;
				this.hideBackground = _hideBackground;
			}

			// Token: 0x0600A806 RID: 43014 RVA: 0x0034BAC0 File Offset: 0x00349CC0
			public void ConvertTimeToTick()
			{
				this.endTime = DateTime.Now.AddSeconds((double)(this.startTime + this.endTime)).Ticks;
				this.startTime = DateTime.Now.AddSeconds((double)this.startTime).Ticks;
			}

			// Token: 0x04009D9D RID: 40349
			public long startTime;

			// Token: 0x04009D9E RID: 40350
			public long endTime;

			// Token: 0x04009D9F RID: 40351
			public bool hideBackground;
		}
	}
}
