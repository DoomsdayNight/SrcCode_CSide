using System;

namespace NKC.Converter
{
	// Token: 0x0200090C RID: 2316
	public interface IStrConverter
	{
		// Token: 0x06005C83 RID: 23683
		string Encryption(string str);

		// Token: 0x06005C84 RID: 23684
		string Decryption(string str);

		// Token: 0x06005C85 RID: 23685
		char ShiftChar(char ch, int range);
	}
}
