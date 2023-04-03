using System;
using System.Text;

namespace NKC.Converter
{
	// Token: 0x0200090B RID: 2315
	public class EasyStrConverter : IStrConverter
	{
		// Token: 0x06005C7E RID: 23678 RVA: 0x001C9AC4 File Offset: 0x001C7CC4
		public string Encryption(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return null;
			}
			this._strBuilder.Clear();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char ch = str[i];
				char value = this.ShiftChar(ch, length);
				this._strBuilder.Append(Convert.ToString(value));
			}
			return this._strBuilder.ToString();
		}

		// Token: 0x06005C7F RID: 23679 RVA: 0x001C9B28 File Offset: 0x001C7D28
		public string Decryption(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return null;
			}
			this._strBuilder.Clear();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char ch = str[i];
				char value = this.ShiftChar(ch, -length);
				this._strBuilder.Append(Convert.ToString(value));
			}
			return this._strBuilder.ToString();
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x001C9B90 File Offset: 0x001C7D90
		public char ShiftChar(char ch, int range)
		{
			if (char.IsUpper(ch))
			{
				return this.ShiftChar(ch, range, 65, 90);
			}
			if (char.IsLower(ch))
			{
				return this.ShiftChar(ch, range, 97, 122);
			}
			if (char.IsNumber(ch))
			{
				return this.ShiftChar(ch, range, 48, 57);
			}
			return ch;
		}

		// Token: 0x06005C81 RID: 23681 RVA: 0x001C9BE0 File Offset: 0x001C7DE0
		private char ShiftChar(char ch, int range, int min, int max)
		{
			int num = max - min;
			int num2 = range % num;
			if (num2 == 0)
			{
				if (range > 0)
				{
					num2 += 2;
				}
				if (range < 0)
				{
					num2 -= 2;
				}
			}
			int num3 = (int)ch + num2;
			if (num3 > max)
			{
				int num4 = num3 % max;
				return Convert.ToChar(min + num4 - 1);
			}
			if (num3 < min)
			{
				int num5 = min % num3;
				return Convert.ToChar(max - num5 + 1);
			}
			return Convert.ToChar(num3);
		}

		// Token: 0x040048E5 RID: 18661
		private readonly StringBuilder _strBuilder = new StringBuilder();
	}
}
