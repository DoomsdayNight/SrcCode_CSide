using System;
using LZ4ps;

namespace LZ4.Services
{
	// Token: 0x02000615 RID: 1557
	internal class Safe64LZ4Service : ILZ4Service
	{
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06003013 RID: 12307 RVA: 0x000EB0D3 File Offset: 0x000E92D3
		public string CodecName
		{
			get
			{
				return "Safe 64";
			}
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000EB0DA File Offset: 0x000E92DA
		public int Encode(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return LZ4Codec.Encode64(input, inputOffset, inputLength, output, outputOffset, outputLength);
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000EB0EA File Offset: 0x000E92EA
		public int Decode(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength, bool knownOutputLength)
		{
			return LZ4Codec.Decode64(input, inputOffset, inputLength, output, outputOffset, outputLength, knownOutputLength);
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000EB0FC File Offset: 0x000E92FC
		public int EncodeHC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return LZ4Codec.Encode64HC(input, inputOffset, inputLength, output, outputOffset, outputLength);
		}
	}
}
