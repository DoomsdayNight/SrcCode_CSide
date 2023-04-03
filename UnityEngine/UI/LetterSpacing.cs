using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UnityEngine.UI
{
	// Token: 0x020002AD RID: 685
	[AddComponentMenu("UI/Effects/Letter Spacing", 15)]
	public class LetterSpacing : BaseMeshEffect
	{
		// Token: 0x06000DD3 RID: 3539 RVA: 0x0002940C File Offset: 0x0002760C
		protected LetterSpacing()
		{
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00029414 File Offset: 0x00027614
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0002941C File Offset: 0x0002761C
		public float spacing
		{
			get
			{
				return this.m_spacing;
			}
			set
			{
				if (this.m_spacing == value)
				{
					return;
				}
				this.m_spacing = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00029448 File Offset: 0x00027648
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> list = new List<UIVertex>();
			vh.GetUIVertexStream(list);
			this.ModifyVertices(list);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00029480 File Offset: 0x00027680
		public void ModifyVertices(List<UIVertex> verts)
		{
			if (!this.IsActive())
			{
				return;
			}
			Text component = base.GetComponent<Text>();
			if (component == null)
			{
				Debug.LogWarning("LetterSpacing: Missing Text component");
				return;
			}
			string text = component.text;
			IList<UILineInfo> lines = component.cachedTextGenerator.lines;
			for (int i = lines.Count - 1; i > 0; i--)
			{
				if (lines[i].startCharIdx > 0)
				{
					text = text.Insert(lines[i].startCharIdx, "\n");
					text = text.Remove(lines[i].startCharIdx - 1, 1);
				}
			}
			string[] array = text.Split(new char[]
			{
				'\n'
			});
			float num = this.spacing * (float)component.fontSize / 100f;
			float num2 = 0f;
			int num3 = 0;
			bool flag = this.useRichText && component.supportRichText;
			IEnumerator enumerator = null;
			Match match = null;
			switch (component.alignment)
			{
			case TextAnchor.UpperLeft:
			case TextAnchor.MiddleLeft:
			case TextAnchor.LowerLeft:
				num2 = 0f;
				break;
			case TextAnchor.UpperCenter:
			case TextAnchor.MiddleCenter:
			case TextAnchor.LowerCenter:
				num2 = 0.5f;
				break;
			case TextAnchor.UpperRight:
			case TextAnchor.MiddleRight:
			case TextAnchor.LowerRight:
				num2 = 1f;
				break;
			}
			foreach (string text2 in array)
			{
				int length = text2.Length;
				if (flag)
				{
					enumerator = this.GetRegexMatchedTagCollection(text2, out length);
					match = null;
					if (enumerator.MoveNext())
					{
						match = (Match)enumerator.Current;
					}
				}
				float num4 = (float)(length - 1) * num * num2;
				int k = 0;
				int num5 = 0;
				while (k < text2.Length)
				{
					if (flag && match != null && match.Index == k)
					{
						k += match.Length - 1;
						num5--;
						num3 += match.Length;
						match = null;
						if (enumerator.MoveNext())
						{
							match = (Match)enumerator.Current;
						}
					}
					else
					{
						int index = num3 * 6;
						int index2 = num3 * 6 + 1;
						int index3 = num3 * 6 + 2;
						int index4 = num3 * 6 + 3;
						int index5 = num3 * 6 + 4;
						int num6 = num3 * 6 + 5;
						if (num6 > verts.Count - 1)
						{
							return;
						}
						UIVertex value = verts[index];
						UIVertex value2 = verts[index2];
						UIVertex value3 = verts[index3];
						UIVertex value4 = verts[index4];
						UIVertex value5 = verts[index5];
						UIVertex value6 = verts[num6];
						Vector3 b = Vector3.right * (num * (float)num5 - num4);
						value.position += b;
						value2.position += b;
						value3.position += b;
						value4.position += b;
						value5.position += b;
						value6.position += b;
						verts[index] = value;
						verts[index2] = value2;
						verts[index3] = value3;
						verts[index4] = value4;
						verts[index5] = value5;
						verts[num6] = value6;
						num3++;
					}
					k++;
					num5++;
				}
				num3++;
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000297F8 File Offset: 0x000279F8
		private IEnumerator GetRegexMatchedTagCollection(string line, out int lineLengthWithoutTags)
		{
			MatchCollection matchCollection = Regex.Matches(line, "<b>|</b>|<i>|</i>|<size=.*?>|</size>|<color=.*?>|</color>|<material=.*?>|</material>");
			lineLengthWithoutTags = 0;
			int num = 0;
			if (matchCollection.Count > 0)
			{
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					num += match.Length;
				}
			}
			lineLengthWithoutTags = line.Length - num;
			return matchCollection.GetEnumerator();
		}

		// Token: 0x0400099F RID: 2463
		private const string SupportedTagRegexPattersn = "<b>|</b>|<i>|</i>|<size=.*?>|</size>|<color=.*?>|</color>|<material=.*?>|</material>";

		// Token: 0x040009A0 RID: 2464
		[SerializeField]
		private bool useRichText;

		// Token: 0x040009A1 RID: 2465
		public float m_spacing;
	}
}
