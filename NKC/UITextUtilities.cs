using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020006F6 RID: 1782
	public class UITextUtilities : MonoBehaviour
	{
		// Token: 0x06003F44 RID: 16196 RVA: 0x00148C94 File Offset: 0x00146E94
		public static bool hasLinkText(string str)
		{
			string input = UITextUtilities.RemoveHtmlLikeTags(str);
			string pattern = "(^|[\\n ])(?<url>(www|ftp)\\.[^ ,\"\\s<]*)";
			string pattern2 = "(^|[\\n ])(?<url>(http://www\\.|http://|https://)[^ ,\"\\s<]*)";
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			Regex regex2 = new Regex(pattern2, RegexOptions.IgnoreCase);
			return regex.IsMatch(input) || regex2.IsMatch(input);
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x00148CD4 File Offset: 0x00146ED4
		public static string RemoveHtmlLikeTags(string str)
		{
			string pattern = "<[^>]+>| ";
			return Regex.Replace(str, pattern, "").Trim();
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x00148CF8 File Offset: 0x00146EF8
		public static string FindIntersectingWord(Text textComp, Vector3 position, Camera camera)
		{
			Vector2 pos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(textComp.GetComponent<RectTransform>(), position, camera, out pos);
			int characterIndexFromPosition = UITextUtilities.GetCharacterIndexFromPosition(textComp, pos);
			if (!string.IsNullOrWhiteSpace(UITextUtilities.GetCharFromIndex(textComp, characterIndexFromPosition)))
			{
				return UITextUtilities.GetWordFromCharIndex(textComp.text, characterIndexFromPosition);
			}
			return "";
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x00148D44 File Offset: 0x00146F44
		private static string GetCharFromIndex(Text textComp, int index)
		{
			char[] array = textComp.text.ToCharArray();
			if (index != -1 && index < array.Length)
			{
				return array[index].ToString() ?? "";
			}
			return "";
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x00148D84 File Offset: 0x00146F84
		private static int GetCharacterIndexFromPosition(Text textComp, Vector2 pos)
		{
			TextGenerator cachedTextGenerator = textComp.cachedTextGenerator;
			if (cachedTextGenerator.lineCount == 0)
			{
				return 0;
			}
			int unclampedCharacterLineFromPosition = UITextUtilities.GetUnclampedCharacterLineFromPosition(textComp, pos, cachedTextGenerator);
			if (unclampedCharacterLineFromPosition < 0)
			{
				return 0;
			}
			if (unclampedCharacterLineFromPosition >= cachedTextGenerator.lineCount)
			{
				return cachedTextGenerator.characterCountVisible;
			}
			int startCharIdx = cachedTextGenerator.lines[unclampedCharacterLineFromPosition].startCharIdx;
			int lineEndPosition = UITextUtilities.GetLineEndPosition(cachedTextGenerator, unclampedCharacterLineFromPosition);
			int num = startCharIdx;
			while (num < lineEndPosition && num < cachedTextGenerator.characterCountVisible)
			{
				UICharInfo uicharInfo = cachedTextGenerator.characters[num];
				Vector2 vector = uicharInfo.cursorPos / textComp.pixelsPerUnit;
				float num2 = pos.x - vector.x;
				float num3 = vector.x + uicharInfo.charWidth / textComp.pixelsPerUnit - pos.x;
				if (num2 < num3)
				{
					return num;
				}
				num++;
			}
			return lineEndPosition;
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x00148E44 File Offset: 0x00147044
		private static int GetUnclampedCharacterLineFromPosition(Text textComp, Vector2 pos, TextGenerator generator)
		{
			float num = pos.y * textComp.pixelsPerUnit;
			float num2 = 0f;
			int i = 0;
			while (i < generator.lineCount)
			{
				float topY = generator.lines[i].topY;
				float num3 = topY - (float)generator.lines[i].height;
				if (num > topY)
				{
					float num4 = topY - num2;
					if (num > topY - 0.5f * num4)
					{
						return i - 1;
					}
					return i;
				}
				else
				{
					if (num > num3)
					{
						return i;
					}
					num2 = num3;
					i++;
				}
			}
			return generator.lineCount;
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x00148ECA File Offset: 0x001470CA
		private static int GetLineStartPosition(TextGenerator gen, int line)
		{
			line = Mathf.Clamp(line, 0, gen.lines.Count - 1);
			return gen.lines[line].startCharIdx;
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x00148EF3 File Offset: 0x001470F3
		private static int GetLineEndPosition(TextGenerator gen, int line)
		{
			line = Mathf.Max(line, 0);
			if (line + 1 < gen.lines.Count)
			{
				return gen.lines[line + 1].startCharIdx - 1;
			}
			return gen.characterCountVisible;
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x00148F2C File Offset: 0x0014712C
		private static string GetWordFromCharIndex(string str, int characterIndex)
		{
			string text = str.Substring(0, characterIndex);
			string text2 = str.Substring(characterIndex);
			string text3 = text;
			int num = text.LastIndexOf(' ');
			if (num != -1)
			{
				text3 = text.Substring(num);
			}
			string text4 = text2;
			int num2 = text2.IndexOf(' ');
			if (num2 != -1)
			{
				text4 = text2.Substring(0, num2);
			}
			if (text3.IndexOf('\n') != -1)
			{
				text3 = text3.Substring(text3.IndexOf('\n'));
			}
			if (text4.IndexOf('\n') != -1)
			{
				text4 = text4.Substring(0, text4.IndexOf('\n'));
			}
			return text3.Replace("\n", "").Replace("\r", "") + text4.Replace("\n", "").Replace("\r", "");
		}
	}
}
