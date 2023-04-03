using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007D9 RID: 2009
	public class NKCUITypeWriter
	{
		// Token: 0x06004F40 RID: 20288 RVA: 0x0017EE33 File Offset: 0x0017D033
		public void SetTypingSound(bool bUse)
		{
			this.m_bUseTypingSound = bUse;
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x0017EE3C File Offset: 0x0017D03C
		public void SetSpaceSound(bool bUse)
		{
			this.m_bUseSpaceSound = bUse;
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x0017EE48 File Offset: 0x0017D048
		public void Start(Text lbTarget, string nameStr, string goalStr, float fCoolTime, bool _bTalkAppend)
		{
			this.m_cStringBuilder.Remove(0, this.m_cStringBuilder.Length);
			if (nameStr != null && nameStr.Length > 0)
			{
				this.m_cStringBuilder.AppendFormat("<color=#FFFFFFFF>{0}</color>: {1}", nameStr, goalStr);
			}
			else
			{
				this.m_cStringBuilder.AppendFormat(goalStr, Array.Empty<object>());
			}
			this.m_FinalText = this.m_cStringBuilder.ToString();
			this.m_FinalText = NKCUtil.TextSplitLine(this.m_FinalText, lbTarget, 0f);
			this.m_NKCTextChunk.TextAnalyze(this.m_FinalText);
			this.m_NKCTextChunk.ReplaceNameString();
			this.m_FinalText = NKCUITypeWriter.ReplaceNameString(this.m_FinalText, false);
			this.m_PureWordCount = this.m_NKCTextChunk.GetPureTextCount();
			this.m_lbTarget = lbTarget;
			this.m_fCoolTime = fCoolTime;
			if (!_bTalkAppend)
			{
				this.m_lbTarget.text = "";
			}
			this.m_bTyping = true;
			if (!_bTalkAppend)
			{
				this.m_strCurrentIndex = 1;
				if (nameStr != null)
				{
					this.m_NKCTextChunkForName.TextAnalyze(nameStr);
					this.m_NKCTextChunkForName.ReplaceNameString();
					this.m_strCurrentIndex += this.m_NKCTextChunkForName.GetPureTextCount();
				}
				this.m_SpaceCount = 0;
			}
			this.m_fElapsedTime = 0f;
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x0017EF80 File Offset: 0x0017D180
		public void Start(Text lbTarget, string goalStr, float fCoolTime, bool _bTalkAppend)
		{
			this.m_cStringBuilder.Remove(0, this.m_cStringBuilder.Length);
			this.m_cStringBuilder.AppendFormat(goalStr, Array.Empty<object>());
			this.m_FinalText = this.m_cStringBuilder.ToString();
			this.m_FinalText = NKCUtil.TextSplitLine(this.m_FinalText, lbTarget, 0f);
			this.m_NKCTextChunk.TextAnalyze(this.m_FinalText);
			this.m_NKCTextChunk.ReplaceNameString();
			this.m_FinalText = NKCUITypeWriter.ReplaceNameString(this.m_FinalText, false);
			this.m_PureWordCount = this.m_NKCTextChunk.GetPureTextCount();
			this.m_lbTarget = lbTarget;
			this.m_fCoolTime = fCoolTime;
			if (!_bTalkAppend)
			{
				this.m_lbTarget.text = "";
			}
			this.m_bTyping = true;
			if (!_bTalkAppend)
			{
				this.m_strCurrentIndex = 1;
				this.m_SpaceCount = 0;
			}
			this.m_fElapsedTime = 0f;
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x0017F064 File Offset: 0x0017D264
		public void Start(TextMeshProUGUI Target, string nameStr, string goalStr, float fCoolTime, bool _bTalkAppend)
		{
			if (!_bTalkAppend)
			{
				this.m_strCurrentIndex = 1;
				if (nameStr != null)
				{
					string text = NKCUITypeWriter.ReplaceNameString(nameStr, false);
					TMP_TextInfo textInfo = Target.GetTextInfo(text);
					this.m_strCurrentIndex += textInfo.characterCount;
				}
				this.m_SpaceCount = 0;
			}
			this.m_cStringBuilder.Remove(0, this.m_cStringBuilder.Length);
			if (nameStr != null && nameStr.Length > 0)
			{
				this.m_cStringBuilder.AppendFormat("<color=#FFFFFFFF>{0}</color>: {1}", nameStr, goalStr);
			}
			else
			{
				this.m_cStringBuilder.AppendFormat(goalStr, Array.Empty<object>());
			}
			this.m_FinalText = this.m_cStringBuilder.ToString();
			this.m_FinalText = NKCUtil.TextSplitLine(this.m_FinalText, Target, 0f);
			this.m_FinalText = NKCUITypeWriter.ReplaceNameString(this.m_FinalText, false);
			TMP_TextInfo textInfo2 = Target.GetTextInfo(this.m_FinalText);
			this.m_PureWordCount = textInfo2.characterCount;
			this.m_tmpTarget = Target;
			this.m_fCoolTime = fCoolTime;
			if (!_bTalkAppend)
			{
				this.m_tmpTarget.maxVisibleCharacters = 0;
			}
			this.m_bTyping = true;
			this.m_fElapsedTime = 0f;
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x0017F178 File Offset: 0x0017D378
		public void Start(TextMeshProUGUI Target, string goalStr, float fCoolTime, bool _bTalkAppend)
		{
			this.m_cStringBuilder.Remove(0, this.m_cStringBuilder.Length);
			this.m_cStringBuilder.AppendFormat(goalStr, Array.Empty<object>());
			this.m_FinalText = this.m_cStringBuilder.ToString();
			this.m_FinalText = NKCUtil.TextSplitLine(this.m_FinalText, Target, 0f);
			this.m_FinalText = NKCUITypeWriter.ReplaceNameString(this.m_FinalText, false);
			TMP_TextInfo textInfo = Target.GetTextInfo(this.m_FinalText);
			this.m_PureWordCount = textInfo.characterCount;
			this.m_tmpTarget = Target;
			this.m_fCoolTime = fCoolTime;
			if (!_bTalkAppend)
			{
				this.m_tmpTarget.maxVisibleCharacters = 0;
			}
			this.m_bTyping = true;
			if (!_bTalkAppend)
			{
				this.m_strCurrentIndex = 1;
				this.m_SpaceCount = 0;
			}
			this.m_fElapsedTime = 0f;
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x0017F244 File Offset: 0x0017D444
		public static string ReplaceNameString(string targetString, bool bBlock)
		{
			if (bBlock)
			{
				targetString = targetString.Replace("<", "@111@");
				targetString = targetString.Replace(">", "@222@");
			}
			else
			{
				targetString = targetString.Replace("@111@", "<");
				targetString = targetString.Replace("@222@", ">");
			}
			return targetString;
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x0017F2A0 File Offset: 0x0017D4A0
		public void Update()
		{
			if (this.m_bTyping)
			{
				if (this.m_strCurrentIndex <= this.m_PureWordCount && this.m_fElapsedTime >= this.m_fCoolTime)
				{
					if (this.m_strCurrentIndex <= this.m_PureWordCount)
					{
						if (this.m_lbTarget != null)
						{
							this.m_NKCTextChunk.MakeText(this.m_strCurrentIndex, ref this.m_cStringBuilder);
							string text = this.m_cStringBuilder.ToString();
							this.m_lbTarget.text = text;
						}
						if (this.m_tmpTarget != null)
						{
							this.m_tmpTarget.maxVisibleCharacters = this.m_strCurrentIndex;
						}
						bool flag = false;
						if (this.m_bUseTypingSound && !this.m_bUseSpaceSound)
						{
							int num = 0;
							if (this.m_lbTarget != null)
							{
								this.m_NKCTextChunk.GetSpaceCount(this.m_strCurrentIndex, ref num);
							}
							if (this.m_tmpTarget != null)
							{
								num = this.GetTextMeshProSpaceCount(this.m_strCurrentIndex);
							}
							if (num != this.m_SpaceCount)
							{
								flag = true;
							}
							this.m_SpaceCount = num;
						}
						this.m_strCurrentIndex++;
						if (this.m_bUseTypingSound && !flag)
						{
							NKCSoundManager.PlaySound("TYPING", 1f, 0f, 0f, false, 0f, false, 0f);
						}
					}
					this.m_fElapsedTime = 0f;
					if (this.m_strCurrentIndex > this.m_PureWordCount)
					{
						this.m_bTyping = false;
						return;
					}
				}
				this.m_fElapsedTime += Time.deltaTime;
			}
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x0017F41C File Offset: 0x0017D61C
		public void Finish()
		{
			if (this.m_lbTarget != null)
			{
				this.m_lbTarget.text = this.m_FinalText;
			}
			if (this.m_tmpTarget != null)
			{
				this.m_tmpTarget.maxVisibleCharacters = this.m_PureWordCount;
			}
			this.m_strCurrentIndex = this.m_PureWordCount;
			this.m_bTyping = false;
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x0017F47A File Offset: 0x0017D67A
		public bool IsTyping()
		{
			return this.m_bTyping;
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x0017F484 File Offset: 0x0017D684
		private int GetTextMeshProSpaceCount(int strCurrentIndex)
		{
			int num = 0;
			if (this.m_tmpTarget != null)
			{
				TMP_CharacterInfo[] characterInfo = this.m_tmpTarget.textInfo.characterInfo;
				int i = 0;
				int characterCount = this.m_tmpTarget.textInfo.characterCount;
				while (i < characterCount)
				{
					if (characterInfo[i].character == ' ')
					{
						num++;
					}
					i++;
					if (i == strCurrentIndex)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x04003F39 RID: 16185
		private Text m_lbTarget;

		// Token: 0x04003F3A RID: 16186
		private TextMeshProUGUI m_tmpTarget;

		// Token: 0x04003F3B RID: 16187
		private float m_fCoolTime;

		// Token: 0x04003F3C RID: 16188
		private float m_fElapsedTime;

		// Token: 0x04003F3D RID: 16189
		private int m_strCurrentIndex = 1;

		// Token: 0x04003F3E RID: 16190
		private bool m_bTyping;

		// Token: 0x04003F3F RID: 16191
		private NKCTextChunk m_NKCTextChunk = new NKCTextChunk();

		// Token: 0x04003F40 RID: 16192
		private NKCTextChunk m_NKCTextChunkForName = new NKCTextChunk();

		// Token: 0x04003F41 RID: 16193
		private StringBuilder m_cStringBuilder = new StringBuilder();

		// Token: 0x04003F42 RID: 16194
		private string m_FinalText = "";

		// Token: 0x04003F43 RID: 16195
		private int m_PureWordCount;

		// Token: 0x04003F44 RID: 16196
		private bool m_bUseTypingSound;

		// Token: 0x04003F45 RID: 16197
		private bool m_bUseSpaceSound = true;

		// Token: 0x04003F46 RID: 16198
		private int m_SpaceCount;
	}
}
