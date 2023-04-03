using System;
using System.IO;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x0200003B RID: 59
	public class JSONBool : JSONNode
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00009345 File Offset: 0x00007545
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.Boolean;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00009348 File Offset: 0x00007548
		public override bool IsBoolean
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000934B File Offset: 0x0000754B
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00009358 File Offset: 0x00007558
		public override string Value
		{
			get
			{
				return this.m_Data.ToString();
			}
			set
			{
				bool data;
				if (bool.TryParse(value, out data))
				{
					this.m_Data = data;
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00009376 File Offset: 0x00007576
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000937E File Offset: 0x0000757E
		public override bool AsBool
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

		// Token: 0x060001FF RID: 511 RVA: 0x00009387 File Offset: 0x00007587
		public JSONBool(bool aData)
		{
			this.m_Data = aData;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009396 File Offset: 0x00007596
		public JSONBool(string aData)
		{
			this.Value = aData;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000093A5 File Offset: 0x000075A5
		public override void Serialize(BinaryWriter aWriter)
		{
			aWriter.Write(6);
			aWriter.Write(this.m_Data);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000093BA File Offset: 0x000075BA
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append(this.m_Data ? "true" : "false");
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000093D7 File Offset: 0x000075D7
		public override bool Equals(object obj)
		{
			return obj != null && obj is bool && this.m_Data == (bool)obj;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000093F6 File Offset: 0x000075F6
		public override int GetHashCode()
		{
			return this.m_Data.GetHashCode();
		}

		// Token: 0x04000142 RID: 322
		private bool m_Data;
	}
}
