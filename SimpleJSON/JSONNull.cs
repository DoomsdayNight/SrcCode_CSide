using System;
using System.IO;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x0200003C RID: 60
	public class JSONNull : JSONNode
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00009403 File Offset: 0x00007603
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.NullValue;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00009406 File Offset: 0x00007606
		public override bool IsNull
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00009409 File Offset: 0x00007609
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00009410 File Offset: 0x00007610
		public override string Value
		{
			get
			{
				return "null";
			}
			set
			{
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00009412 File Offset: 0x00007612
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00009415 File Offset: 0x00007615
		public override bool AsBool
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00009417 File Offset: 0x00007617
		public override bool Equals(object obj)
		{
			return this == obj || obj is JSONNull;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00009428 File Offset: 0x00007628
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000942B File Offset: 0x0000762B
		public override void Serialize(BinaryWriter aWriter)
		{
			aWriter.Write(5);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00009434 File Offset: 0x00007634
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append("null");
		}
	}
}
