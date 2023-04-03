using System;
using System.IO;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x0200003A RID: 58
	public class JSONNumber : JSONNode
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000091F3 File Offset: 0x000073F3
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.Number;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000091F6 File Offset: 0x000073F6
		public override bool IsNumber
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000091F9 File Offset: 0x000073F9
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00009208 File Offset: 0x00007408
		public override string Value
		{
			get
			{
				return this.m_Data.ToString();
			}
			set
			{
				double data;
				if (double.TryParse(value, out data))
				{
					this.m_Data = data;
				}
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00009226 File Offset: 0x00007426
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000922E File Offset: 0x0000742E
		public override double AsDouble
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

		// Token: 0x060001F2 RID: 498 RVA: 0x00009237 File Offset: 0x00007437
		public JSONNumber(double aData)
		{
			this.m_Data = aData;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009246 File Offset: 0x00007446
		public JSONNumber(string aData)
		{
			this.Value = aData;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00009255 File Offset: 0x00007455
		public override void Serialize(BinaryWriter aWriter)
		{
			aWriter.Write(4);
			aWriter.Write(this.m_Data);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000926A File Offset: 0x0000746A
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append(this.m_Data);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000927C File Offset: 0x0000747C
		private static bool IsNumeric(object value)
		{
			return value is int || value is uint || value is float || value is double || value is decimal || value is long || value is ulong || value is short || value is ushort || value is sbyte || value is byte;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000092E4 File Offset: 0x000074E4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (base.Equals(obj))
			{
				return true;
			}
			JSONNumber jsonnumber = obj as JSONNumber;
			if (jsonnumber != null)
			{
				return this.m_Data == jsonnumber.m_Data;
			}
			return JSONNumber.IsNumeric(obj) && Convert.ToDouble(obj) == this.m_Data;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009338 File Offset: 0x00007538
		public override int GetHashCode()
		{
			return this.m_Data.GetHashCode();
		}

		// Token: 0x04000141 RID: 321
		private double m_Data;
	}
}
