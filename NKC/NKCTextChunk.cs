using System;
using System.Collections.Generic;
using System.Text;

namespace NKC
{
	// Token: 0x020007D8 RID: 2008
	public class NKCTextChunk
	{
		// Token: 0x06004F2C RID: 20268 RVA: 0x0017E873 File Offset: 0x0017CA73
		private NKCTextChunk OpenChunk()
		{
			if (this.m_qChunkPool.Count > 0)
			{
				return this.m_qChunkPool.Dequeue();
			}
			return new NKCTextChunk();
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x0017E894 File Offset: 0x0017CA94
		private void CloseChunk(NKCTextChunk cNKCTextChunk)
		{
			cNKCTextChunk.Init(this);
			this.m_qChunkPool.Enqueue(cNKCTextChunk);
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x0017E8A9 File Offset: 0x0017CAA9
		private StringBuilder OpenStringBuilder()
		{
			if (this.m_qStringBuilder.Count > 0)
			{
				return this.m_qStringBuilder.Dequeue();
			}
			return new StringBuilder();
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x0017E8CA File Offset: 0x0017CACA
		private void CloseStringBuilder(StringBuilder cStringBuilder)
		{
			cStringBuilder.Remove(0, cStringBuilder.Length);
			this.m_qStringBuilder.Enqueue(cStringBuilder);
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x0017E90F File Offset: 0x0017CB0F
		public void Init(NKCTextChunk topChunk = null)
		{
			this.m_NKC_TEXT_CHUNK_TYPE = NKC_TEXT_CHUNK_TYPE.NTCT_TEXT;
			this.m_Text = "";
			if (topChunk == null)
			{
				topChunk = this;
			}
			if (this.m_ChildTextChunk != null)
			{
				topChunk.CloseChunk(this.m_ChildTextChunk);
				this.m_ChildTextChunk = null;
			}
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x0017E944 File Offset: 0x0017CB44
		public void TextAnalyze(string fullText)
		{
			this.Init(null);
			int num = 0;
			this.TextAnalyze(this, fullText, ref num);
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x0017E964 File Offset: 0x0017CB64
		private void TextAnalyze(NKCTextChunk topChunk, string fullText, ref int textIndex)
		{
			if (this.IsTag(fullText, textIndex))
			{
				this.FindTag(topChunk, fullText, ref textIndex);
				this.MakeNewChunk(topChunk, fullText, ref textIndex);
				return;
			}
			StringBuilder stringBuilder = topChunk.OpenStringBuilder();
			while (textIndex < fullText.Length)
			{
				if (this.IsTag(fullText, textIndex))
				{
					this.m_Text = stringBuilder.ToString();
					this.MakeNewChunk(topChunk, fullText, ref textIndex);
					topChunk.CloseStringBuilder(stringBuilder);
					return;
				}
				char value = fullText[textIndex];
				textIndex++;
				stringBuilder.Append(value);
			}
			this.m_Text = stringBuilder.ToString();
			topChunk.CloseStringBuilder(stringBuilder);
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x0017E9F4 File Offset: 0x0017CBF4
		private void MakeNewChunk(NKCTextChunk topChunk, string fullText, ref int textIndex)
		{
			this.m_ChildTextChunk = topChunk.OpenChunk();
			this.m_ChildTextChunk.TextAnalyze(topChunk, fullText, ref textIndex);
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x0017EA10 File Offset: 0x0017CC10
		private bool IsTag(string fullText, int textIndex)
		{
			if (textIndex >= fullText.Length)
			{
				return false;
			}
			if (fullText[textIndex] == '<')
			{
				if (textIndex + 1 < fullText.Length)
				{
					if (fullText[textIndex + 1] == 'c' || fullText[textIndex + 1] == 'C')
					{
						return true;
					}
					if (fullText[textIndex + 1] == 'b' || fullText[textIndex + 1] == 'B')
					{
						return true;
					}
					if (fullText[textIndex + 1] == '/')
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x0017EA88 File Offset: 0x0017CC88
		private void FindTag(NKCTextChunk topChunk, string fullText, ref int textIndex)
		{
			if (textIndex >= fullText.Length)
			{
				return;
			}
			if (fullText[textIndex] == '<')
			{
				if (fullText[textIndex + 1] == 'c' || fullText[textIndex + 1] == 'C')
				{
					this.GetTag(topChunk, fullText, ref textIndex);
					this.m_NKC_TEXT_CHUNK_TYPE = NKC_TEXT_CHUNK_TYPE.NTCT_COLOR;
					return;
				}
				if (fullText[textIndex + 1] == 'b' || fullText[textIndex + 1] == 'B')
				{
					this.GetTag(topChunk, fullText, ref textIndex);
					this.m_NKC_TEXT_CHUNK_TYPE = NKC_TEXT_CHUNK_TYPE.NTCT_BOLD;
					return;
				}
				if (fullText[textIndex + 1] == '/')
				{
					if (fullText[textIndex + 2] == 'c' || fullText[textIndex + 2] == 'C')
					{
						this.m_NKC_TEXT_CHUNK_TYPE = NKC_TEXT_CHUNK_TYPE.NTCT_COLOR_END;
					}
					else if (fullText[textIndex + 2] == 'b' || fullText[textIndex + 2] == 'B')
					{
						this.m_NKC_TEXT_CHUNK_TYPE = NKC_TEXT_CHUNK_TYPE.NTCT_BOLD_END;
					}
					this.GetTag(topChunk, fullText, ref textIndex);
				}
			}
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x0017EB68 File Offset: 0x0017CD68
		private void GetTag(NKCTextChunk topChunk, string fullText, ref int textIndex)
		{
			StringBuilder stringBuilder = topChunk.OpenStringBuilder();
			while (textIndex < fullText.Length)
			{
				char c = fullText[textIndex];
				textIndex++;
				stringBuilder.Append(c);
				if (c == '>')
				{
					break;
				}
			}
			this.m_Text = stringBuilder.ToString();
			topChunk.CloseStringBuilder(stringBuilder);
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x0017EBB8 File Offset: 0x0017CDB8
		public void GetSpaceCount(int targetIndex, ref int spaceCount)
		{
			int num = targetIndex;
			this.GetSpaceCount(ref num, ref spaceCount);
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x0017EBD0 File Offset: 0x0017CDD0
		private void GetSpaceCount(ref int remainIndex, ref int spaceCount)
		{
			if (this.m_NKC_TEXT_CHUNK_TYPE == NKC_TEXT_CHUNK_TYPE.NTCT_TEXT)
			{
				int i = 0;
				while (i < this.m_Text.Length)
				{
					if (this.m_Text[i] == ' ')
					{
						spaceCount++;
					}
					i++;
					if (remainIndex > 0)
					{
						remainIndex--;
						if (remainIndex == 0)
						{
							return;
						}
					}
				}
			}
			if (this.m_ChildTextChunk == null)
			{
				return;
			}
			this.m_ChildTextChunk.GetSpaceCount(ref remainIndex, ref spaceCount);
		}

		// Token: 0x06004F3A RID: 20282 RVA: 0x0017EC38 File Offset: 0x0017CE38
		public void MakeText(int targetIndex, ref StringBuilder cStringBuilder)
		{
			int num = targetIndex;
			int num2 = 0;
			int num3 = 0;
			cStringBuilder.Remove(0, cStringBuilder.Length);
			if (num == 0)
			{
				cStringBuilder.Append("<color=#00000000>");
				num2++;
			}
			this.MakeText(ref num, ref cStringBuilder, ref num2, ref num3);
			for (int i = 0; i < num2; i++)
			{
				cStringBuilder.Append("</color>");
			}
			for (int j = 0; j < num3; j++)
			{
				cStringBuilder.Append("</b>");
			}
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x0017ECB4 File Offset: 0x0017CEB4
		private void MakeText(ref int remainIndex, ref StringBuilder cStringBuilder, ref int colorOpenCount, ref int boldOpenCount)
		{
			switch (this.m_NKC_TEXT_CHUNK_TYPE)
			{
			case NKC_TEXT_CHUNK_TYPE.NTCT_TEXT:
			{
				int i = 0;
				while (i < this.m_Text.Length)
				{
					cStringBuilder.Append(this.m_Text[i]);
					i++;
					if (remainIndex > 0)
					{
						remainIndex--;
						if (remainIndex == 0)
						{
							cStringBuilder.Append("<color=#00000000>");
							colorOpenCount++;
						}
					}
				}
				break;
			}
			case NKC_TEXT_CHUNK_TYPE.NTCT_COLOR:
				if (remainIndex > 0)
				{
					cStringBuilder.Append(this.m_Text);
					colorOpenCount++;
				}
				break;
			case NKC_TEXT_CHUNK_TYPE.NTCT_COLOR_END:
				if (remainIndex > 0)
				{
					cStringBuilder.Append(this.m_Text);
					colorOpenCount--;
				}
				break;
			case NKC_TEXT_CHUNK_TYPE.NTCT_BOLD:
				if (remainIndex > 0)
				{
					cStringBuilder.Append(this.m_Text);
					boldOpenCount++;
				}
				break;
			case NKC_TEXT_CHUNK_TYPE.NTCT_BOLD_END:
				if (remainIndex > 0)
				{
					cStringBuilder.Append(this.m_Text);
					boldOpenCount--;
				}
				break;
			}
			if (this.m_ChildTextChunk == null)
			{
				return;
			}
			this.m_ChildTextChunk.MakeText(ref remainIndex, ref cStringBuilder, ref colorOpenCount, ref boldOpenCount);
		}

		// Token: 0x06004F3C RID: 20284 RVA: 0x0017EDBC File Offset: 0x0017CFBC
		public int GetPureTextCount()
		{
			int result = 0;
			this.GetPureTextCount(ref result);
			return result;
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x0017EDD4 File Offset: 0x0017CFD4
		public void ReplaceNameString()
		{
			this.ReplaceNameStringFromChunk(this);
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x0017EDDD File Offset: 0x0017CFDD
		private void ReplaceNameStringFromChunk(NKCTextChunk textChunk)
		{
			textChunk.m_Text = NKCUITypeWriter.ReplaceNameString(textChunk.m_Text, false);
			if (textChunk.m_ChildTextChunk != null)
			{
				this.ReplaceNameStringFromChunk(textChunk.m_ChildTextChunk);
			}
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x0017EE05 File Offset: 0x0017D005
		private void GetPureTextCount(ref int textCount)
		{
			if (this.m_NKC_TEXT_CHUNK_TYPE == NKC_TEXT_CHUNK_TYPE.NTCT_TEXT)
			{
				textCount += this.m_Text.Length;
			}
			if (this.m_ChildTextChunk != null)
			{
				this.m_ChildTextChunk.GetPureTextCount(ref textCount);
			}
		}

		// Token: 0x04003F34 RID: 16180
		public NKC_TEXT_CHUNK_TYPE m_NKC_TEXT_CHUNK_TYPE;

		// Token: 0x04003F35 RID: 16181
		public string m_Text = "";

		// Token: 0x04003F36 RID: 16182
		public NKCTextChunk m_ChildTextChunk;

		// Token: 0x04003F37 RID: 16183
		private Queue<NKCTextChunk> m_qChunkPool = new Queue<NKCTextChunk>();

		// Token: 0x04003F38 RID: 16184
		private Queue<StringBuilder> m_qStringBuilder = new Queue<StringBuilder>();
	}
}
