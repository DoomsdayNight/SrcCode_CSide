using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NKC
{
	// Token: 0x0200068F RID: 1679
	public class Korean
	{
		// Token: 0x060036D5 RID: 14037 RVA: 0x0011A57F File Offset: 0x0011877F
		public static string ReplaceJosa(string src)
		{
			return Korean.josa.Replace(src);
		}

		// Token: 0x040033FF RID: 13311
		private static Korean.Josa josa = new Korean.Josa();

		// Token: 0x02001359 RID: 4953
		public class Josa
		{
			// Token: 0x0600A5A1 RID: 42401 RVA: 0x00345988 File Offset: 0x00343B88
			public string Replace(string src)
			{
				StringBuilder stringBuilder = new StringBuilder(src.Length);
				MatchCollection matchCollection = this._josaRegex.Matches(src);
				int num = 0;
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					Korean.Josa.JosaPair josaPair = this._josaPatternPaird[match.Value];
					stringBuilder.Append(src, num, match.Index - num);
					if (match.Index > 0)
					{
						char inChar = src[match.Index - 1];
						for (int i = match.Index - 1; i >= 0; i--)
						{
							char c = src[i];
							if (Korean.Josa.IsKorean(c))
							{
								inChar = c;
								break;
							}
						}
						if ((Korean.Josa.HasJong(inChar) && !this._josaRegex_Rieul.IsMatch(match.Value)) || (Korean.Josa.HasJongExceptRieul(inChar) && this._josaRegex_Rieul.IsMatch(match.Value)))
						{
							stringBuilder.Append(josaPair.josa1);
						}
						else
						{
							stringBuilder.Append(josaPair.josa2);
						}
					}
					else
					{
						stringBuilder.Append(josaPair.josa1);
					}
					num = match.Index + match.Length;
				}
				stringBuilder.Append(src, num, src.Length - num);
				return stringBuilder.ToString();
			}

			// Token: 0x0600A5A2 RID: 42402 RVA: 0x00345B00 File Offset: 0x00343D00
			private static bool IsKorean(char inChar)
			{
				return inChar >= '가' && inChar <= '힣';
			}

			// Token: 0x0600A5A3 RID: 42403 RVA: 0x00345B17 File Offset: 0x00343D17
			private static bool HasJong(char inChar)
			{
				return inChar >= '가' && inChar <= '힣' && (inChar - '가') % '\u001c' > '\0';
			}

			// Token: 0x0600A5A4 RID: 42404 RVA: 0x00345B3C File Offset: 0x00343D3C
			private static bool HasJongExceptRieul(char inChar)
			{
				if (inChar >= '가' && inChar <= '힣')
				{
					int num = (int)((inChar - '가') % '\u001c');
					return num != 8 && num != 0;
				}
				return false;
			}

			// Token: 0x04009951 RID: 39249
			private Regex _josaRegex = new Regex("\\(이\\)가|\\(와\\)과|\\(을\\)를|\\(은\\)는|\\(아\\)야|\\(이\\)여|\\(으\\)로|\\(이\\)라");

			// Token: 0x04009952 RID: 39250
			private Regex _josaRegex_Rieul = new Regex("\\(으\\)로");

			// Token: 0x04009953 RID: 39251
			private Dictionary<string, Korean.Josa.JosaPair> _josaPatternPaird = new Dictionary<string, Korean.Josa.JosaPair>
			{
				{
					"(이)가",
					new Korean.Josa.JosaPair("이", "가")
				},
				{
					"(와)과",
					new Korean.Josa.JosaPair("과", "와")
				},
				{
					"(을)를",
					new Korean.Josa.JosaPair("을", "를")
				},
				{
					"(은)는",
					new Korean.Josa.JosaPair("은", "는")
				},
				{
					"(아)야",
					new Korean.Josa.JosaPair("아", "야")
				},
				{
					"(이)여",
					new Korean.Josa.JosaPair("이여", "여")
				},
				{
					"(으)로",
					new Korean.Josa.JosaPair("으로", "로")
				},
				{
					"(이)라",
					new Korean.Josa.JosaPair("이라", "라")
				}
			};

			// Token: 0x02001A54 RID: 6740
			private class JosaPair
			{
				// Token: 0x0600BB99 RID: 48025 RVA: 0x0036F881 File Offset: 0x0036DA81
				public JosaPair(string josa1, string josa2)
				{
					this.josa1 = josa1;
					this.josa2 = josa2;
				}

				// Token: 0x17001A10 RID: 6672
				// (get) Token: 0x0600BB9A RID: 48026 RVA: 0x0036F897 File Offset: 0x0036DA97
				// (set) Token: 0x0600BB9B RID: 48027 RVA: 0x0036F89F File Offset: 0x0036DA9F
				public string josa1 { get; private set; }

				// Token: 0x17001A11 RID: 6673
				// (get) Token: 0x0600BB9C RID: 48028 RVA: 0x0036F8A8 File Offset: 0x0036DAA8
				// (set) Token: 0x0600BB9D RID: 48029 RVA: 0x0036F8B0 File Offset: 0x0036DAB0
				public string josa2 { get; private set; }
			}
		}
	}
}
