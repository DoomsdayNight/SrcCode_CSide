using System;
using LZ4ps;

namespace LZ4.Services
{
	// Token: 0x02000614 RID: 1556
	internal class Safe32LZ4Service : ILZ4Service
	{
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x000EB092 File Offset: 0x000E9292
		public string CodecName
		{
			get
			{
				return "Safe 32";
			}
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000EB099 File Offset: 0x000E9299
		public int Encode(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return LZ4Codec.Encode32(input, inputOffset, inputLength, output, outputOffset, outputLength);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000EB0A9 File Offset: 0x000E92A9
		public int Decode(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength, bool knownOutputLength)
		{
			return LZ4Codec.Decode32(input, inputOffset, inputLength, output, outputOffset, outputLength, knownOutputLength);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000EB0BB File Offset: 0x000E92BB
		public int EncodeHC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return LZ4Codec.Encode32HC(input, inputOffset, inputLength, output, outputOffset, outputLength);
		}
	}
}
