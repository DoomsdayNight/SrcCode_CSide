using System;
using System.Text;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000622 RID: 1570
	public class NKCComUITalkBox : MonoBehaviour
	{
		// Token: 0x06003071 RID: 12401 RVA: 0x000EF074 File Offset: 0x000ED274
		public void Awake()
		{
			NKCUIComStringChanger component = this.m_AB_UI_TALK_BOX_TEXT_Text.GetComponent<NKCUIComStringChanger>();
			if (component != null)
			{
				component.Translate();
			}
			this.m_bHorizontalOverflow = this.m_AB_UI_TALK_BOX_TEXT_Text.horizontalOverflow;
			if (this.m_AB_UI_TALK_BOX_TEXT_RectTransform != null)
			{
				this.m_OffsetMin = this.m_AB_UI_TALK_BOX_TEXT_RectTransform.offsetMin;
				this.m_OffsetMax = this.m_AB_UI_TALK_BOX_TEXT_RectTransform.offsetMax;
			}
			this.m_TextOrg = this.m_AB_UI_TALK_BOX_TEXT_Text.text;
			this.SetText(this.m_TextOrg, this.m_fadeTime, 0f);
			this.SetDir(this.m_NKC_UI_TALK_BOX_DIR);
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000EF111 File Offset: 0x000ED311
		public void OnEnable()
		{
			this.SetText(this.m_TextOrg, this.m_fadeTime, 0f);
			this.SetDir(this.m_NKC_UI_TALK_BOX_DIR);
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000EF138 File Offset: 0x000ED338
		public void SetText(string text, float fadeTime = 0f, float delayTime = 0f)
		{
			this.m_TextOrg = text;
			text = NKCUtil.TextSplitLine(text, this.m_AB_UI_TALK_BOX_TEXT_Text, 500f);
			if (this.m_Text.CompareTo(text) != 0)
			{
				if (this.m_NextLineCount > 0)
				{
					StringBuilder builder = NKMString.GetBuilder();
					int num = 0;
					for (int i = 0; i < text.Length; i++)
					{
						builder.Append(text[i]);
						if (text[i] == '\n')
						{
							num = 0;
						}
						else
						{
							num++;
							if (num >= this.m_NextLineCount)
							{
								builder.Append('\n');
								num = 0;
							}
						}
					}
					this.m_Text = builder.ToString();
				}
				else
				{
					this.m_Text = text;
				}
				this.m_AB_UI_TALK_BOX_TEXT_Text.text = this.m_Text;
				this.ReSize();
			}
			if (delayTime > 0f)
			{
				this.m_delayTime = delayTime;
				this.m_delayTimeNow = 0f;
			}
			if (this.m_fSpreadTime > 0f)
			{
				this.m_fSpreadTimeNow = 0f;
				this.m_SpreadIndex = 0;
				this.m_AB_UI_TALK_BOX_TEXT_Text.text = "";
				this.m_fadeTime = fadeTime;
				this.m_fadeTimeNow = 0f;
			}
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000EF250 File Offset: 0x000ED450
		public void Update()
		{
			if (this.m_Text.Length == 0)
			{
				return;
			}
			if (this.m_delayTime > 0f && this.m_delayTimeNow < this.m_delayTime)
			{
				this.m_delayTimeNow += Time.deltaTime;
				return;
			}
			if (this.m_fSpreadTime > 0f && this.m_SpreadIndex < this.m_Text.Length)
			{
				this.m_fSpreadTimeNow += Time.deltaTime;
				if (this.m_fSpreadTimeNow >= this.m_fSpreadTime)
				{
					this.m_SpreadIndex++;
					StringBuilder builder = NKMString.GetBuilder();
					builder.AppendFormat("{0}<color=00000000>{1}</color>", this.m_Text.Substring(0, this.m_SpreadIndex), this.m_Text.Substring(this.m_SpreadIndex));
					this.m_AB_UI_TALK_BOX_TEXT_Text.text = builder.ToString();
					this.m_fSpreadTimeNow = 0f;
				}
				return;
			}
			if (this.m_fadeTime > 0f && this.m_fadeTime > this.m_fadeTimeNow)
			{
				this.m_fadeTimeNow += Time.deltaTime;
				if (this.m_fadeTime <= this.m_fadeTimeNow)
				{
					NKCUtil.SetGameobjectActive(base.gameObject, false);
				}
			}
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000EF386 File Offset: 0x000ED586
		private void OnDisable()
		{
			if (this.m_fadeTime > 0f)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
			}
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000EF3A4 File Offset: 0x000ED5A4
		private void ReSize()
		{
			this.m_PreferredWidth = this.m_AB_UI_TALK_BOX_TEXT_Text.preferredWidth;
			this.m_PreferredHeight = this.m_AB_UI_TALK_BOX_TEXT_Text.preferredHeight;
			if (this.m_PreferredWidth > this.MAX_PREFERRED_WIDTH)
			{
				if (this.m_AB_UI_TALK_BOX_TEXT_RectTransform != null)
				{
					this.m_AB_UI_TALK_BOX_TEXT_RectTransform.offsetMin = new Vector2(this.TEXT_PADDING, this.TEXT_PADDING);
					this.m_AB_UI_TALK_BOX_TEXT_RectTransform.offsetMax = new Vector2(-this.TEXT_PADDING, 0f);
				}
				this.m_PreferredHeight += this.m_PreferredWidth / (this.MAX_PREFERRED_WIDTH - this.TEXT_PADDING * 2f) * ((float)this.m_AB_UI_TALK_BOX_TEXT_Text.fontSize + this.m_AB_UI_TALK_BOX_TEXT_Text.lineSpacing);
				this.m_PreferredWidth = this.MAX_PREFERRED_WIDTH;
				this.m_AB_UI_TALK_BOX_TEXT_Text.horizontalOverflow = HorizontalWrapMode.Wrap;
			}
			else
			{
				if (this.m_AB_UI_TALK_BOX_TEXT_RectTransform != null)
				{
					this.m_AB_UI_TALK_BOX_TEXT_RectTransform.offsetMin = this.m_OffsetMin;
					this.m_AB_UI_TALK_BOX_TEXT_RectTransform.offsetMax = this.m_OffsetMax;
				}
				this.m_AB_UI_TALK_BOX_TEXT_Text.horizontalOverflow = this.m_bHorizontalOverflow;
			}
			this.m_AB_UI_TALK_BOX_BG_RectTransform.SetWidth(this.m_PreferredWidth + this.m_BASE_WIDTH);
			this.m_AB_UI_TALK_BOX_BG_RectTransform.SetHeight(this.m_PreferredHeight + this.m_BASE_HEIGHT);
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000EF4F8 File Offset: 0x000ED6F8
		public void SetDir(NKC_UI_TALK_BOX_DIR eNKC_UI_TALK_BOX_DIR)
		{
			this.m_NKC_UI_TALK_BOX_DIR = eNKC_UI_TALK_BOX_DIR;
			switch (this.m_NKC_UI_TALK_BOX_DIR)
			{
			case NKC_UI_TALK_BOX_DIR.NTBD_RIGHT:
				NKCUtil.SetGameObjectLocalScale(this.m_AB_UI_TALK_BOX_BG_RectTransform, 1f, 1f, 1f);
				NKCUtil.SetGameObjectLocalScale(this.m_AB_UI_TALK_BOX_TEXT_RectTransform, 1f, 1f, 1f);
				return;
			case NKC_UI_TALK_BOX_DIR.NTBD_LEFT:
				NKCUtil.SetGameObjectLocalScale(this.m_AB_UI_TALK_BOX_BG_RectTransform, -1f, 1f, 1f);
				NKCUtil.SetGameObjectLocalScale(this.m_AB_UI_TALK_BOX_TEXT_RectTransform, -1f, 1f, 1f);
				return;
			case NKC_UI_TALK_BOX_DIR.NTBD_RIGHT_DOWN:
				NKCUtil.SetGameObjectLocalScale(this.m_AB_UI_TALK_BOX_BG_RectTransform, 1f, -1f, 1f);
				NKCUtil.SetGameObjectLocalScale(this.m_AB_UI_TALK_BOX_TEXT_RectTransform, 1f, -1f, 1f);
				return;
			case NKC_UI_TALK_BOX_DIR.NTBD_LEFT_DOWN:
				NKCUtil.SetGameObjectLocalScale(this.m_AB_UI_TALK_BOX_BG_RectTransform, -1f, -1f, 1f);
				NKCUtil.SetGameObjectLocalScale(this.m_AB_UI_TALK_BOX_TEXT_RectTransform, -1f, -1f, 1f);
				return;
			default:
				return;
			}
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000EF5FD File Offset: 0x000ED7FD
		public void ReserveText(string text)
		{
			this.m_ReservedText = text;
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000EF606 File Offset: 0x000ED806
		public void ShowReservedText(float fadeTime = 0f)
		{
			if (string.IsNullOrEmpty(this.m_ReservedText))
			{
				return;
			}
			this.SetText(this.m_ReservedText, fadeTime, 0f);
			this.m_ReservedText = "";
		}

		// Token: 0x04002FD8 RID: 12248
		private float m_BASE_WIDTH = 100f;

		// Token: 0x04002FD9 RID: 12249
		private float m_BASE_HEIGHT = 100f;

		// Token: 0x04002FDA RID: 12250
		public float MAX_PREFERRED_WIDTH = 500f;

		// Token: 0x04002FDB RID: 12251
		public float TEXT_PADDING = 30f;

		// Token: 0x04002FDC RID: 12252
		public RectTransform m_AB_UI_TALK_BOX_BG_RectTransform;

		// Token: 0x04002FDD RID: 12253
		public RectTransform m_AB_UI_TALK_BOX_TEXT_RectTransform;

		// Token: 0x04002FDE RID: 12254
		public Text m_AB_UI_TALK_BOX_TEXT_Text;

		// Token: 0x04002FDF RID: 12255
		public NKC_UI_TALK_BOX_DIR m_NKC_UI_TALK_BOX_DIR;

		// Token: 0x04002FE0 RID: 12256
		public int m_NextLineCount;

		// Token: 0x04002FE1 RID: 12257
		public float m_fSpreadTime;

		// Token: 0x04002FE2 RID: 12258
		public float m_fadeTime;

		// Token: 0x04002FE3 RID: 12259
		private float m_fSpreadTimeNow;

		// Token: 0x04002FE4 RID: 12260
		private int m_SpreadIndex;

		// Token: 0x04002FE5 RID: 12261
		private string m_TextOrg = "";

		// Token: 0x04002FE6 RID: 12262
		private string m_Text = "";

		// Token: 0x04002FE7 RID: 12263
		private float m_PreferredWidth;

		// Token: 0x04002FE8 RID: 12264
		private float m_PreferredHeight;

		// Token: 0x04002FE9 RID: 12265
		private float m_fadeTimeNow;

		// Token: 0x04002FEA RID: 12266
		private HorizontalWrapMode m_bHorizontalOverflow = HorizontalWrapMode.Overflow;

		// Token: 0x04002FEB RID: 12267
		private Vector2 m_OffsetMin;

		// Token: 0x04002FEC RID: 12268
		private Vector2 m_OffsetMax;

		// Token: 0x04002FED RID: 12269
		private float m_delayTime;

		// Token: 0x04002FEE RID: 12270
		private float m_delayTimeNow;

		// Token: 0x04002FEF RID: 12271
		private string m_ReservedText = "";
	}
}
