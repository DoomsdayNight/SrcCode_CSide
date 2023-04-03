using System;
using System.IO;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000039 RID: 57
	public class JSONString : JSONNode
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00009137 File Offset: 0x00007337
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.String;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000913A File Offset: 0x0000733A
		public override bool IsString
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000913D File Offset: 0x0000733D
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00009145 File Offset: 0x00007345
		public override string Value
		{
			get
			{
				return this.m_Data;
			}
			set
			{
				this.m_Data = value;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000914E File Offset: 0x0000734E
		public JSONString(string aData)
		{
			this.m_Data = aData;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000915D File Offset: 0x0000735D
		public override void Serialize(BinaryWriter aWriter)
		{
			aWriter.Write(3);
			aWriter.Write(this.m_Data);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00009172 File Offset: 0x00007372
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append('"').Append(JSONNode.Escape(this.m_Data)).Append('"');
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00009194 File Offset: 0x00007394
		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				return true;
			}
			string text = obj as string;
			if (text != null)
			{
				return this.m_Data == text;
			}
			JSONString jsonstring = obj as JSONString;
			return jsonstring != null && this.m_Data == jsonstring.m_Data;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000091E6 File Offset: 0x000073E6
		public override int GetHashCode()
		{
			return this.m_Data.GetHashCode();
		}

		// Token: 0x04000140 RID: 320
		private string m_Data;
	}
}
