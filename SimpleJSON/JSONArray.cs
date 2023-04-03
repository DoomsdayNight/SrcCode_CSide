using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000037 RID: 55
	public class JSONArray : JSONNode, IEnumerable
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00008B1E File Offset: 0x00006D1E
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.Array;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00008B21 File Offset: 0x00006D21
		public override bool IsArray
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000053 RID: 83
		public override JSONNode this[int aIndex]
		{
			get
			{
				if (aIndex < 0 || aIndex >= this.m_List.Count)
				{
					return new JSONLazyCreator(this);
				}
				return this.m_List[aIndex];
			}
			set
			{
				if (value == null)
				{
					value = new JSONNull();
				}
				if (aIndex < 0 || aIndex >= this.m_List.Count)
				{
					this.m_List.Add(value);
					return;
				}
				this.m_List[aIndex] = value;
			}
		}

		// Token: 0x17000054 RID: 84
		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				if (value == null)
				{
					value = new JSONNull();
				}
				this.m_List.Add(value);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00008BAF File Offset: 0x00006DAF
		public override int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008BBC File Offset: 0x00006DBC
		public override void Add(string aKey, JSONNode aItem)
		{
			if (aItem == null)
			{
				aItem = new JSONNull();
			}
			this.m_List.Add(aItem);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008BDA File Offset: 0x00006DDA
		public override JSONNode Remove(int aIndex)
		{
			if (aIndex < 0 || aIndex >= this.m_List.Count)
			{
				return null;
			}
			JSONNode result = this.m_List[aIndex];
			this.m_List.RemoveAt(aIndex);
			return result;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008C08 File Offset: 0x00006E08
		public override JSONNode Remove(JSONNode aNode)
		{
			this.m_List.Remove(aNode);
			return aNode;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00008C18 File Offset: 0x00006E18
		public override IEnumerable<JSONNode> Children
		{
			get
			{
				foreach (JSONNode jsonnode in this.m_List)
				{
					yield return jsonnode;
				}
				List<JSONNode>.Enumerator enumerator = default(List<JSONNode>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008C28 File Offset: 0x00006E28
		public IEnumerator GetEnumerator()
		{
			foreach (JSONNode jsonnode in this.m_List)
			{
				yield return jsonnode;
			}
			List<JSONNode>.Enumerator enumerator = default(List<JSONNode>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008C38 File Offset: 0x00006E38
		public override void Serialize(BinaryWriter aWriter)
		{
			aWriter.Write(1);
			aWriter.Write(this.m_List.Count);
			for (int i = 0; i < this.m_List.Count; i++)
			{
				this.m_List[i].Serialize(aWriter);
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008C88 File Offset: 0x00006E88
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append('[');
			int count = this.m_List.Count;
			if (this.inline)
			{
				aMode = JSONTextMode.Compact;
			}
			for (int i = 0; i < count; i++)
			{
				if (i > 0)
				{
					aSB.Append(',');
				}
				if (aMode == JSONTextMode.Indent)
				{
					aSB.AppendLine();
				}
				if (aMode == JSONTextMode.Indent)
				{
					aSB.Append(' ', aIndent + aIndentInc);
				}
				this.m_List[i].WriteToStringBuilder(aSB, aIndent + aIndentInc, aIndentInc, aMode);
			}
			if (aMode == JSONTextMode.Indent)
			{
				aSB.AppendLine().Append(' ', aIndent);
			}
			aSB.Append(']');
		}

		// Token: 0x0400013C RID: 316
		private List<JSONNode> m_List = new List<JSONNode>();

		// Token: 0x0400013D RID: 317
		public bool inline;
	}
}
