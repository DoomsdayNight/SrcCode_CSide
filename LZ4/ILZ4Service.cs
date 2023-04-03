using System;

namespace LZ4
{
	// Token: 0x0200060F RID: 1551
	internal interface ILZ4Service
	{
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002FD5 RID: 12245
		string CodecName { get; }

		// Token: 0x06002FD6 RID: 12246
		int Encode(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength);

		// Token: 0x06002FD7 RID: 12247
		int EncodeHC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength);

		// Token: 0x06002FD8 RID: 12248
		int Decode(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength, bool knownOutputLength);
	}
}
