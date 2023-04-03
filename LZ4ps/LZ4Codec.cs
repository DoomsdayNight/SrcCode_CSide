using System;
using System.Diagnostics;

namespace LZ4ps
{
	// Token: 0x0200060E RID: 1550
	public static class LZ4Codec
	{
		// Token: 0x06002FA2 RID: 12194 RVA: 0x000E6DAA File Offset: 0x000E4FAA
		[Conditional("DEBUG")]
		private static void Assert(bool condition, string errorMessage)
		{
			if (!condition)
			{
				throw new ArgumentException(errorMessage);
			}
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000E6DB6 File Offset: 0x000E4FB6
		internal static void Poke2(byte[] buffer, int offset, ushort value)
		{
			buffer[offset] = (byte)value;
			buffer[offset + 1] = (byte)(value >> 8);
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000E6DC6 File Offset: 0x000E4FC6
		internal static ushort Peek2(byte[] buffer, int offset)
		{
			return (ushort)((int)buffer[offset] | (int)buffer[offset + 1] << 8);
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000E6DD4 File Offset: 0x000E4FD4
		internal static uint Peek4(byte[] buffer, int offset)
		{
			return (uint)((int)buffer[offset] | (int)buffer[offset + 1] << 8 | (int)buffer[offset + 2] << 16 | (int)buffer[offset + 3] << 24);
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000E6DF4 File Offset: 0x000E4FF4
		private static uint Xor4(byte[] buffer, int offset1, int offset2)
		{
			uint num = (uint)((int)buffer[offset1] | (int)buffer[offset1 + 1] << 8 | (int)buffer[offset1 + 2] << 16 | (int)buffer[offset1 + 3] << 24);
			uint num2 = (uint)((int)buffer[offset2] | (int)buffer[offset2 + 1] << 8 | (int)buffer[offset2 + 2] << 16 | (int)buffer[offset2 + 3] << 24);
			return num ^ num2;
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000E6E40 File Offset: 0x000E5040
		private static ulong Xor8(byte[] buffer, int offset1, int offset2)
		{
			ulong num = (ulong)buffer[offset1] | (ulong)buffer[offset1 + 1] << 8 | (ulong)buffer[offset1 + 2] << 16 | (ulong)buffer[offset1 + 3] << 24 | (ulong)buffer[offset1 + 4] << 32 | (ulong)buffer[offset1 + 5] << 40 | (ulong)buffer[offset1 + 6] << 48 | (ulong)buffer[offset1 + 7] << 56;
			ulong num2 = (ulong)buffer[offset2] | (ulong)buffer[offset2 + 1] << 8 | (ulong)buffer[offset2 + 2] << 16 | (ulong)buffer[offset2 + 3] << 24 | (ulong)buffer[offset2 + 4] << 32 | (ulong)buffer[offset2 + 5] << 40 | (ulong)buffer[offset2 + 6] << 48 | (ulong)buffer[offset2 + 7] << 56;
			return num ^ num2;
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000E6EE2 File Offset: 0x000E50E2
		private static bool Equal2(byte[] buffer, int offset1, int offset2)
		{
			return buffer[offset1] == buffer[offset2] && buffer[offset1 + 1] == buffer[offset2 + 1];
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000E6EFA File Offset: 0x000E50FA
		private static bool Equal4(byte[] buffer, int offset1, int offset2)
		{
			return buffer[offset1] == buffer[offset2] && buffer[offset1 + 1] == buffer[offset2 + 1] && buffer[offset1 + 2] == buffer[offset2 + 2] && buffer[offset1 + 3] == buffer[offset2 + 3];
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000E6F2E File Offset: 0x000E512E
		private static void Copy4(byte[] buf, int src, int dst)
		{
			buf[dst + 3] = buf[src + 3];
			buf[dst + 2] = buf[src + 2];
			buf[dst + 1] = buf[src + 1];
			buf[dst] = buf[src];
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x000E6F54 File Offset: 0x000E5154
		private static void Copy8(byte[] buf, int src, int dst)
		{
			buf[dst + 7] = buf[src + 7];
			buf[dst + 6] = buf[src + 6];
			buf[dst + 5] = buf[src + 5];
			buf[dst + 4] = buf[src + 4];
			buf[dst + 3] = buf[src + 3];
			buf[dst + 2] = buf[src + 2];
			buf[dst + 1] = buf[src + 1];
			buf[dst] = buf[src];
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000E6FB0 File Offset: 0x000E51B0
		private static void BlockCopy(byte[] src, int src_0, byte[] dst, int dst_0, int len)
		{
			if (len >= 16)
			{
				Buffer.BlockCopy(src, src_0, dst, dst_0, len);
				return;
			}
			while (len >= 8)
			{
				dst[dst_0] = src[src_0];
				dst[dst_0 + 1] = src[src_0 + 1];
				dst[dst_0 + 2] = src[src_0 + 2];
				dst[dst_0 + 3] = src[src_0 + 3];
				dst[dst_0 + 4] = src[src_0 + 4];
				dst[dst_0 + 5] = src[src_0 + 5];
				dst[dst_0 + 6] = src[src_0 + 6];
				dst[dst_0 + 7] = src[src_0 + 7];
				len -= 8;
				src_0 += 8;
				dst_0 += 8;
			}
			while (len >= 4)
			{
				dst[dst_0] = src[src_0];
				dst[dst_0 + 1] = src[src_0 + 1];
				dst[dst_0 + 2] = src[src_0 + 2];
				dst[dst_0 + 3] = src[src_0 + 3];
				len -= 4;
				src_0 += 4;
				dst_0 += 4;
			}
			while (len-- > 0)
			{
				dst[dst_0++] = src[src_0++];
			}
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000E7088 File Offset: 0x000E5288
		private static int WildCopy(byte[] src, int src_0, byte[] dst, int dst_0, int dst_end)
		{
			int i = dst_end - dst_0;
			if (i >= 16)
			{
				Buffer.BlockCopy(src, src_0, dst, dst_0, i);
			}
			else
			{
				while (i >= 4)
				{
					dst[dst_0] = src[src_0];
					dst[dst_0 + 1] = src[src_0 + 1];
					dst[dst_0 + 2] = src[src_0 + 2];
					dst[dst_0 + 3] = src[src_0 + 3];
					i -= 4;
					src_0 += 4;
					dst_0 += 4;
				}
				while (i-- > 0)
				{
					dst[dst_0++] = src[src_0++];
				}
			}
			return i;
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000E70FC File Offset: 0x000E52FC
		private static int SecureCopy(byte[] buffer, int src, int dst, int dst_end)
		{
			int num = dst - src;
			int num2 = dst_end - dst;
			int i = num2;
			if (num >= 16)
			{
				if (num >= num2)
				{
					Buffer.BlockCopy(buffer, src, buffer, dst, num2);
					return num2;
				}
				do
				{
					Buffer.BlockCopy(buffer, src, buffer, dst, num);
					src += num;
					dst += num;
					i -= num;
				}
				while (i >= num);
			}
			while (i >= 4)
			{
				buffer[dst] = buffer[src];
				buffer[dst + 1] = buffer[src + 1];
				buffer[dst + 2] = buffer[src + 2];
				buffer[dst + 3] = buffer[src + 3];
				dst += 4;
				src += 4;
				i -= 4;
			}
			while (i-- > 0)
			{
				buffer[dst++] = buffer[src++];
			}
			return num2;
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000E7198 File Offset: 0x000E5398
		public static int Encode32(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			LZ4Codec.CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
			if (outputLength == 0)
			{
				return 0;
			}
			if (inputLength < 65547)
			{
				return LZ4Codec.LZ4_compress64kCtx_safe32(new ushort[8192], input, output, inputOffset, outputOffset, inputLength, outputLength);
			}
			return LZ4Codec.LZ4_compressCtx_safe32(new int[4096], input, output, inputOffset, outputOffset, inputLength, outputLength);
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000E71F0 File Offset: 0x000E53F0
		public static byte[] Encode32(byte[] input, int inputOffset, int inputLength)
		{
			if (inputLength < 0)
			{
				inputLength = input.Length - inputOffset;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (inputOffset < 0 || inputOffset + inputLength > input.Length)
			{
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");
			}
			byte[] array = new byte[LZ4Codec.MaximumOutputLength(inputLength)];
			int num = LZ4Codec.Encode32(input, inputOffset, inputLength, array, 0, array.Length);
			if (num == array.Length)
			{
				return array;
			}
			if (num < 0)
			{
				throw new InvalidOperationException("Compression has been corrupted");
			}
			byte[] array2 = new byte[num];
			Buffer.BlockCopy(array, 0, array2, 0, num);
			return array2;
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000E7270 File Offset: 0x000E5470
		public static int Encode64(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			LZ4Codec.CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
			if (outputLength == 0)
			{
				return 0;
			}
			if (inputLength < 65547)
			{
				return LZ4Codec.LZ4_compress64kCtx_safe64(new ushort[8192], input, output, inputOffset, outputOffset, inputLength, outputLength);
			}
			return LZ4Codec.LZ4_compressCtx_safe64(new int[4096], input, output, inputOffset, outputOffset, inputLength, outputLength);
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000E72C8 File Offset: 0x000E54C8
		public static byte[] Encode64(byte[] input, int inputOffset, int inputLength)
		{
			if (inputLength < 0)
			{
				inputLength = input.Length - inputOffset;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (inputOffset < 0 || inputOffset + inputLength > input.Length)
			{
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");
			}
			byte[] array = new byte[LZ4Codec.MaximumOutputLength(inputLength)];
			int num = LZ4Codec.Encode64(input, inputOffset, inputLength, array, 0, array.Length);
			if (num == array.Length)
			{
				return array;
			}
			if (num < 0)
			{
				throw new InvalidOperationException("Compression has been corrupted");
			}
			byte[] array2 = new byte[num];
			Buffer.BlockCopy(array, 0, array2, 0, num);
			return array2;
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000E7348 File Offset: 0x000E5548
		public static int Decode32(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength, bool knownOutputLength)
		{
			LZ4Codec.CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
			if (outputLength == 0)
			{
				return 0;
			}
			if (knownOutputLength)
			{
				if (LZ4Codec.LZ4_uncompress_safe32(input, output, inputOffset, outputOffset, outputLength) != inputLength)
				{
					throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
				}
				return outputLength;
			}
			else
			{
				int num = LZ4Codec.LZ4_uncompress_unknownOutputSize_safe32(input, output, inputOffset, outputOffset, inputLength, outputLength);
				if (num < 0)
				{
					throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
				}
				return num;
			}
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000E73A8 File Offset: 0x000E55A8
		public static byte[] Decode32(byte[] input, int inputOffset, int inputLength, int outputLength)
		{
			if (inputLength < 0)
			{
				inputLength = input.Length - inputOffset;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (inputOffset < 0 || inputOffset + inputLength > input.Length)
			{
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");
			}
			byte[] array = new byte[outputLength];
			if (LZ4Codec.Decode32(input, inputOffset, inputLength, array, 0, outputLength, true) != outputLength)
			{
				throw new ArgumentException("outputLength is not valid");
			}
			return array;
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000E7408 File Offset: 0x000E5608
		public static int Decode64(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength, bool knownOutputLength)
		{
			LZ4Codec.CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
			if (outputLength == 0)
			{
				return 0;
			}
			if (knownOutputLength)
			{
				if (LZ4Codec.LZ4_uncompress_safe64(input, output, inputOffset, outputOffset, outputLength) != inputLength)
				{
					throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
				}
				return outputLength;
			}
			else
			{
				int num = LZ4Codec.LZ4_uncompress_unknownOutputSize_safe64(input, output, inputOffset, outputOffset, inputLength, outputLength);
				if (num < 0)
				{
					throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
				}
				return num;
			}
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000E7468 File Offset: 0x000E5668
		public static byte[] Decode64(byte[] input, int inputOffset, int inputLength, int outputLength)
		{
			if (inputLength < 0)
			{
				inputLength = input.Length - inputOffset;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (inputOffset < 0 || inputOffset + inputLength > input.Length)
			{
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");
			}
			byte[] array = new byte[outputLength];
			if (LZ4Codec.Decode64(input, inputOffset, inputLength, array, 0, outputLength, true) != outputLength)
			{
				throw new ArgumentException("outputLength is not valid");
			}
			return array;
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000E74C8 File Offset: 0x000E56C8
		private static LZ4Codec.LZ4HC_Data_Structure LZ4HC_Create(byte[] src, int src_0, int src_len, byte[] dst, int dst_0, int dst_len)
		{
			LZ4Codec.LZ4HC_Data_Structure lz4HC_Data_Structure = new LZ4Codec.LZ4HC_Data_Structure
			{
				src = src,
				src_base = src_0,
				src_end = src_0 + src_len,
				src_LASTLITERALS = src_0 + src_len - 5,
				dst = dst,
				dst_base = dst_0,
				dst_len = dst_len,
				dst_end = dst_0 + dst_len,
				hashTable = new int[32768],
				chainTable = new ushort[65536],
				nextToUpdate = src_0 + 1
			};
			ushort[] chainTable = lz4HC_Data_Structure.chainTable;
			for (int i = chainTable.Length - 1; i >= 0; i--)
			{
				chainTable[i] = ushort.MaxValue;
			}
			return lz4HC_Data_Structure;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000E7568 File Offset: 0x000E5768
		private static int LZ4_compressHC_32(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return LZ4Codec.LZ4_compressHCCtx_32(LZ4Codec.LZ4HC_Create(input, inputOffset, inputLength, output, outputOffset, outputLength));
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000E757C File Offset: 0x000E577C
		public static int Encode32HC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			if (inputLength == 0)
			{
				return 0;
			}
			LZ4Codec.CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
			int num = LZ4Codec.LZ4_compressHC_32(input, inputOffset, inputLength, output, outputOffset, outputLength);
			if (num > 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000E75B4 File Offset: 0x000E57B4
		public static byte[] Encode32HC(byte[] input, int inputOffset, int inputLength)
		{
			if (inputLength == 0)
			{
				return new byte[0];
			}
			int num = LZ4Codec.MaximumOutputLength(inputLength);
			byte[] array = new byte[num];
			int num2 = LZ4Codec.Encode32HC(input, inputOffset, inputLength, array, 0, num);
			if (num2 < 0)
			{
				throw new ArgumentException("Provided data seems to be corrupted.");
			}
			if (num2 != num)
			{
				byte[] array2 = new byte[num2];
				Buffer.BlockCopy(array, 0, array2, 0, num2);
				array = array2;
			}
			return array;
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000E760C File Offset: 0x000E580C
		private static int LZ4_compressHC_64(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			return LZ4Codec.LZ4_compressHCCtx_64(LZ4Codec.LZ4HC_Create(input, inputOffset, inputLength, output, outputOffset, outputLength));
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000E7620 File Offset: 0x000E5820
		public static int Encode64HC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
		{
			if (inputLength == 0)
			{
				return 0;
			}
			LZ4Codec.CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
			int num = LZ4Codec.LZ4_compressHC_64(input, inputOffset, inputLength, output, outputOffset, outputLength);
			if (num > 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000E7658 File Offset: 0x000E5858
		public static byte[] Encode64HC(byte[] input, int inputOffset, int inputLength)
		{
			if (inputLength == 0)
			{
				return new byte[0];
			}
			int num = LZ4Codec.MaximumOutputLength(inputLength);
			byte[] array = new byte[num];
			int num2 = LZ4Codec.Encode64HC(input, inputOffset, inputLength, array, 0, num);
			if (num2 < 0)
			{
				throw new ArgumentException("Provided data seems to be corrupted.");
			}
			if (num2 != num)
			{
				byte[] array2 = new byte[num2];
				Buffer.BlockCopy(array, 0, array2, 0, num2);
				array = array2;
			}
			return array;
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000E76B0 File Offset: 0x000E58B0
		private static int LZ4_compressCtx_safe32(int[] hash_table, byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_32;
			int num = src_0;
			int num2 = src_0 + src_len;
			int num3 = num2 - 12;
			int num4 = dst_0;
			int num5 = num4 + dst_maxlen;
			int num6 = num2 - 5;
			int num7 = num6 - 1;
			int num8 = num6 - 3;
			int num9 = num5 - 6;
			int num10 = num5 - 8;
			if (src_len >= 13)
			{
				hash_table[(int)(LZ4Codec.Peek4(src, src_0) * 2654435761U >> 20)] = src_0 - src_0;
				int i = src_0 + 1;
				uint num11 = LZ4Codec.Peek4(src, i) * 2654435761U >> 20;
				for (;;)
				{
					int num12 = 67;
					int num13 = i;
					int num16;
					do
					{
						uint num14 = num11;
						int num15 = num12++ >> 6;
						i = num13;
						num13 = i + num15;
						if (num13 > num3)
						{
							goto IL_35E;
						}
						num11 = LZ4Codec.Peek4(src, num13) * 2654435761U >> 20;
						num16 = src_0 + hash_table[(int)num14];
						hash_table[(int)num14] = i - src_0;
					}
					while (num16 < i - 65535 || !LZ4Codec.Equal4(src, num16, i));
					while (i > num && num16 > src_0 && src[i - 1] == src[num16 - 1])
					{
						i--;
						num16--;
					}
					int j = i - num;
					int num17 = num4++;
					if (num4 + j + (j >> 8) > num10)
					{
						break;
					}
					if (j < 15)
					{
						dst[num17] = (byte)(j << 4);
						goto IL_192;
					}
					int num18 = j - 15;
					dst[num17] = 240;
					if (num18 <= 254)
					{
						dst[num4++] = (byte)num18;
						goto IL_192;
					}
					do
					{
						dst[num4++] = byte.MaxValue;
						num18 -= 255;
					}
					while (num18 > 254);
					dst[num4++] = (byte)num18;
					LZ4Codec.BlockCopy(src, num, dst, num4, j);
					num4 += j;
					for (;;)
					{
						IL_1AD:
						LZ4Codec.Poke2(dst, num4, (ushort)(i - num16));
						num4 += 2;
						i += 4;
						num16 += 4;
						num = i;
						while (i < num8)
						{
							int num19 = (int)LZ4Codec.Xor4(src, num16, i);
							if (num19 == 0)
							{
								i += 4;
								num16 += 4;
							}
							else
							{
								i += debruijn_TABLE_[(int)((uint)((num19 & -num19) * 125613361) >> 27)];
								IL_231:
								j = i - num;
								if (num4 + (j >> 8) > num9)
								{
									return 0;
								}
								if (j >= 15)
								{
									int num20 = num17;
									dst[num20] += 15;
									for (j -= 15; j > 509; j -= 510)
									{
										dst[num4++] = byte.MaxValue;
										dst[num4++] = byte.MaxValue;
									}
									if (j > 254)
									{
										j -= 255;
										dst[num4++] = byte.MaxValue;
									}
									dst[num4++] = (byte)j;
								}
								else
								{
									int num21 = num17;
									dst[num21] += (byte)j;
								}
								if (i > num3)
								{
									goto Block_21;
								}
								hash_table[(int)(LZ4Codec.Peek4(src, i - 2) * 2654435761U >> 20)] = i - 2 - src_0;
								uint num14 = LZ4Codec.Peek4(src, i) * 2654435761U >> 20;
								num16 = src_0 + hash_table[(int)num14];
								hash_table[(int)num14] = i - src_0;
								if (num16 > i - 65536 && LZ4Codec.Equal4(src, num16, i))
								{
									num17 = num4++;
									dst[num17] = 0;
									goto IL_1AD;
								}
								goto IL_340;
							}
						}
						if (i < num7 && LZ4Codec.Equal2(src, num16, i))
						{
							i += 2;
							num16 += 2;
						}
						if (i < num6 && src[num16] == src[i])
						{
							i++;
							goto IL_231;
						}
						goto IL_231;
					}
					IL_340:
					num = i++;
					num11 = LZ4Codec.Peek4(src, i) * 2654435761U >> 20;
					continue;
					IL_192:
					if (j > 0)
					{
						int num22 = num4 + j;
						LZ4Codec.WildCopy(src, num, dst, num4, num22);
						num4 = num22;
						goto IL_1AD;
					}
					goto IL_1AD;
				}
				return 0;
				Block_21:
				num = i;
			}
			IL_35E:
			int k = num2 - num;
			if (num4 + k + 1 + (k + 255 - 15) / 255 > num5)
			{
				return 0;
			}
			if (k >= 15)
			{
				dst[num4++] = 240;
				for (k -= 15; k > 254; k -= 255)
				{
					dst[num4++] = byte.MaxValue;
				}
				dst[num4++] = (byte)k;
			}
			else
			{
				dst[num4++] = (byte)(k << 4);
			}
			LZ4Codec.BlockCopy(src, num, dst, num4, num2 - num);
			num4 += num2 - num;
			return num4 - dst_0;
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000E7ABC File Offset: 0x000E5CBC
		private static int LZ4_compress64kCtx_safe32(ushort[] hash_table, byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_32;
			int num = src_0;
			int num2 = src_0 + src_len;
			int num3 = num2 - 12;
			int num4 = dst_0;
			int num5 = num4 + dst_maxlen;
			int num6 = num2 - 5;
			int num7 = num6 - 1;
			int num8 = num6 - 3;
			int num9 = num5 - 6;
			int num10 = num5 - 8;
			if (src_len >= 13)
			{
				int i = src_0 + 1;
				uint num11 = LZ4Codec.Peek4(src, i) * 2654435761U >> 19;
				for (;;)
				{
					int num12 = 67;
					int num13 = i;
					int num16;
					do
					{
						uint num14 = num11;
						int num15 = num12++ >> 6;
						i = num13;
						num13 = i + num15;
						if (num13 > num3)
						{
							goto IL_330;
						}
						num11 = LZ4Codec.Peek4(src, num13) * 2654435761U >> 19;
						num16 = src_0 + (int)hash_table[(int)num14];
						hash_table[(int)num14] = (ushort)(i - src_0);
					}
					while (!LZ4Codec.Equal4(src, num16, i));
					while (i > num && num16 > src_0 && src[i - 1] == src[num16 - 1])
					{
						i--;
						num16--;
					}
					int num17 = i - num;
					int num18 = num4++;
					if (num4 + num17 + (num17 >> 8) > num10)
					{
						break;
					}
					if (num17 < 15)
					{
						dst[num18] = (byte)(num17 << 4);
						goto IL_172;
					}
					int j = num17 - 15;
					dst[num18] = 240;
					if (j <= 254)
					{
						dst[num4++] = (byte)j;
						goto IL_172;
					}
					do
					{
						dst[num4++] = byte.MaxValue;
						j -= 255;
					}
					while (j > 254);
					dst[num4++] = (byte)j;
					LZ4Codec.BlockCopy(src, num, dst, num4, num17);
					num4 += num17;
					for (;;)
					{
						IL_18C:
						LZ4Codec.Poke2(dst, num4, (ushort)(i - num16));
						num4 += 2;
						i += 4;
						num16 += 4;
						num = i;
						while (i < num8)
						{
							int num19 = (int)LZ4Codec.Xor4(src, num16, i);
							if (num19 == 0)
							{
								i += 4;
								num16 += 4;
							}
							else
							{
								i += debruijn_TABLE_[(int)((uint)((num19 & -num19) * 125613361) >> 27)];
								IL_20F:
								j = i - num;
								if (num4 + (j >> 8) > num9)
								{
									return 0;
								}
								if (j >= 15)
								{
									int num20 = num18;
									dst[num20] += 15;
									for (j -= 15; j > 509; j -= 510)
									{
										dst[num4++] = byte.MaxValue;
										dst[num4++] = byte.MaxValue;
									}
									if (j > 254)
									{
										j -= 255;
										dst[num4++] = byte.MaxValue;
									}
									dst[num4++] = (byte)j;
								}
								else
								{
									int num21 = num18;
									dst[num21] += (byte)j;
								}
								if (i > num3)
								{
									goto Block_20;
								}
								hash_table[(int)(LZ4Codec.Peek4(src, i - 2) * 2654435761U >> 19)] = (ushort)(i - 2 - src_0);
								uint num14 = LZ4Codec.Peek4(src, i) * 2654435761U >> 19;
								num16 = src_0 + (int)hash_table[(int)num14];
								hash_table[(int)num14] = (ushort)(i - src_0);
								if (LZ4Codec.Equal4(src, num16, i))
								{
									num18 = num4++;
									dst[num18] = 0;
									goto IL_18C;
								}
								goto IL_313;
							}
						}
						if (i < num7 && LZ4Codec.Equal2(src, num16, i))
						{
							i += 2;
							num16 += 2;
						}
						if (i < num6 && src[num16] == src[i])
						{
							i++;
							goto IL_20F;
						}
						goto IL_20F;
					}
					IL_313:
					num = i++;
					num11 = LZ4Codec.Peek4(src, i) * 2654435761U >> 19;
					continue;
					IL_172:
					if (num17 > 0)
					{
						int num22 = num4 + num17;
						LZ4Codec.WildCopy(src, num, dst, num4, num22);
						num4 = num22;
						goto IL_18C;
					}
					goto IL_18C;
				}
				return 0;
				Block_20:
				num = i;
			}
			IL_330:
			int k = num2 - num;
			if (num4 + k + 1 + (k - 15 + 255) / 255 > num5)
			{
				return 0;
			}
			if (k >= 15)
			{
				dst[num4++] = 240;
				for (k -= 15; k > 254; k -= 255)
				{
					dst[num4++] = byte.MaxValue;
				}
				dst[num4++] = (byte)k;
			}
			else
			{
				dst[num4++] = (byte)(k << 4);
			}
			LZ4Codec.BlockCopy(src, num, dst, num4, num2 - num);
			num4 += num2 - num;
			return num4 - dst_0;
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000E7E94 File Offset: 0x000E6094
		private static int LZ4_uncompress_safe32(byte[] src, byte[] dst, int src_0, int dst_0, int dst_len)
		{
			int[] decoder_TABLE_ = LZ4Codec.DECODER_TABLE_32;
			int num = src_0;
			int i = dst_0;
			int num2 = i + dst_len;
			int num3 = num2 - 5;
			int num4 = num2 - 8;
			int num5 = num2 - 8;
			int num6;
			int num8;
			for (;;)
			{
				byte b = src[num++];
				if ((num6 = b >> 4) == 15)
				{
					int num7;
					while ((num7 = (int)src[num++]) == 255)
					{
						num6 += 255;
					}
					num6 += num7;
				}
				num8 = i + num6;
				if (num8 > num4)
				{
					break;
				}
				if (i < num8)
				{
					int num9 = LZ4Codec.WildCopy(src, num, dst, i, num8);
					num += num9;
					i += num9;
				}
				num -= i - num8;
				i = num8;
				int num10 = num8 - (int)LZ4Codec.Peek2(src, num);
				num += 2;
				if (num10 < dst_0)
				{
					goto IL_1CE;
				}
				if ((num6 = (int)(b & 15)) == 15)
				{
					while (src[num] == 255)
					{
						num++;
						num6 += 255;
					}
					num6 += (int)src[num++];
				}
				if (i - num10 < 4)
				{
					dst[i] = dst[num10];
					dst[i + 1] = dst[num10 + 1];
					dst[i + 2] = dst[num10 + 2];
					dst[i + 3] = dst[num10 + 3];
					i += 4;
					num10 += 4;
					num10 -= decoder_TABLE_[i - num10];
					LZ4Codec.Copy4(dst, num10, i);
					i = i;
					num10 = num10;
				}
				else
				{
					LZ4Codec.Copy4(dst, num10, i);
					i += 4;
					num10 += 4;
				}
				num8 = i + num6;
				if (num8 > num5)
				{
					if (num8 > num3)
					{
						goto IL_1CE;
					}
					if (i < num4)
					{
						int num9 = LZ4Codec.SecureCopy(dst, num10, i, num4);
						num10 += num9;
						i += num9;
					}
					while (i < num8)
					{
						dst[i++] = dst[num10++];
					}
					i = num8;
				}
				else
				{
					if (i < num8)
					{
						LZ4Codec.SecureCopy(dst, num10, i, num8);
					}
					i = num8;
				}
			}
			if (num8 == num2)
			{
				LZ4Codec.BlockCopy(src, num, dst, i, num6);
				num += num6;
				return num - src_0;
			}
			IL_1CE:
			return -(num - src_0);
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000E8074 File Offset: 0x000E6274
		private static int LZ4_uncompress_unknownOutputSize_safe32(byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			int[] decoder_TABLE_ = LZ4Codec.DECODER_TABLE_32;
			int i = src_0;
			int num = i + src_len;
			int j = dst_0;
			int num2 = j + dst_maxlen;
			int num3 = num - 8;
			int num4 = num - 6;
			int num5 = num2 - 8;
			int num6 = num2 - 8;
			int num7 = num2 - 5;
			int num8 = num2 - 12;
			if (i != num)
			{
				int num9;
				int num11;
				for (;;)
				{
					byte b = src[i++];
					if ((num9 = b >> 4) == 15)
					{
						int num10 = 255;
						while (i < num && num10 == 255)
						{
							num9 += (num10 = (int)src[i++]);
						}
					}
					num11 = j + num9;
					if (num11 > num8 || i + num9 > num3)
					{
						break;
					}
					if (j < num11)
					{
						int num12 = LZ4Codec.WildCopy(src, i, dst, j, num11);
						i += num12;
						j += num12;
					}
					i -= j - num11;
					j = num11;
					int num13 = num11 - (int)LZ4Codec.Peek2(src, i);
					i += 2;
					if (num13 < dst_0)
					{
						goto IL_213;
					}
					if ((num9 = (int)(b & 15)) == 15)
					{
						while (i < num4)
						{
							int num14 = (int)src[i++];
							num9 += num14;
							if (num14 != 255)
							{
								break;
							}
						}
					}
					if (j - num13 < 4)
					{
						dst[j] = dst[num13];
						dst[j + 1] = dst[num13 + 1];
						dst[j + 2] = dst[num13 + 2];
						dst[j + 3] = dst[num13 + 3];
						j += 4;
						num13 += 4;
						num13 -= decoder_TABLE_[j - num13];
						LZ4Codec.Copy4(dst, num13, j);
						j = j;
						num13 = num13;
					}
					else
					{
						LZ4Codec.Copy4(dst, num13, j);
						j += 4;
						num13 += 4;
					}
					num11 = j + num9;
					if (num11 > num6)
					{
						if (num11 > num7)
						{
							goto IL_213;
						}
						if (j < num5)
						{
							int num12 = LZ4Codec.SecureCopy(dst, num13, j, num5);
							num13 += num12;
							j += num12;
						}
						while (j < num11)
						{
							dst[j++] = dst[num13++];
						}
						j = num11;
					}
					else
					{
						if (j < num11)
						{
							LZ4Codec.SecureCopy(dst, num13, j, num11);
						}
						j = num11;
					}
				}
				if (num11 <= num2 && i + num9 == num)
				{
					LZ4Codec.BlockCopy(src, i, dst, j, num9);
					j += num9;
					return j - dst_0;
				}
			}
			IL_213:
			return -(i - src_0);
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000E8298 File Offset: 0x000E6498
		private static void LZ4HC_Insert_32(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p)
		{
			ushort[] chainTable = ctx.chainTable;
			int[] hashTable = ctx.hashTable;
			int i = ctx.nextToUpdate;
			byte[] src = ctx.src;
			int src_base = ctx.src_base;
			while (i < src_p)
			{
				int num = i;
				int num2 = num - (hashTable[(int)(LZ4Codec.Peek4(src, num) * 2654435761U >> 17)] + src_base);
				if (num2 > 65535)
				{
					num2 = 65535;
				}
				chainTable[num & 65535] = (ushort)num2;
				hashTable[(int)(LZ4Codec.Peek4(src, num) * 2654435761U >> 17)] = num - src_base;
				i++;
			}
			ctx.nextToUpdate = i;
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000E8330 File Offset: 0x000E6530
		private static int LZ4HC_CommonLength_32(LZ4Codec.LZ4HC_Data_Structure ctx, int p1, int p2)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_32;
			byte[] src = ctx.src;
			int src_LASTLITERALS = ctx.src_LASTLITERALS;
			int i = p1;
			while (i < src_LASTLITERALS - 3)
			{
				int num = (int)LZ4Codec.Xor4(src, p2, i);
				if (num != 0)
				{
					i += debruijn_TABLE_[(int)((uint)((num & -num) * 125613361) >> 27)];
					return i - p1;
				}
				i += 4;
				p2 += 4;
			}
			if (i < src_LASTLITERALS - 1 && LZ4Codec.Equal2(src, p2, i))
			{
				i += 2;
				p2 += 2;
			}
			if (i < src_LASTLITERALS && src[p2] == src[i])
			{
				i++;
			}
			return i - p1;
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000E83B8 File Offset: 0x000E65B8
		private static int LZ4HC_InsertAndFindBestMatch_32(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p, ref int src_match)
		{
			ushort[] chainTable = ctx.chainTable;
			int[] hashTable = ctx.hashTable;
			byte[] src = ctx.src;
			int src_base = ctx.src_base;
			int num = 256;
			int num2 = 0;
			int num3 = 0;
			ushort num4 = 0;
			LZ4Codec.LZ4HC_Insert_32(ctx, src_p);
			int num5 = hashTable[(int)(LZ4Codec.Peek4(src, src_p) * 2654435761U >> 17)] + src_base;
			if (num5 >= src_p - 4)
			{
				if (LZ4Codec.Equal4(src, num5, src_p))
				{
					num4 = (ushort)(src_p - num5);
					num3 = (num2 = LZ4Codec.LZ4HC_CommonLength_32(ctx, src_p + 4, num5 + 4) + 4);
					src_match = num5;
				}
				num5 -= (int)chainTable[num5 & 65535];
			}
			while (num5 >= src_p - 65535 && num != 0)
			{
				num--;
				if (src[num5 + num3] == src[src_p + num3] && LZ4Codec.Equal4(src, num5, src_p))
				{
					int num6 = LZ4Codec.LZ4HC_CommonLength_32(ctx, src_p + 4, num5 + 4) + 4;
					if (num6 > num3)
					{
						num3 = num6;
						src_match = num5;
					}
				}
				num5 -= (int)chainTable[num5 & 65535];
			}
			if (num2 != 0)
			{
				int i = src_p;
				int num7 = src_p + num2 - 3;
				while (i < num7 - (int)num4)
				{
					chainTable[i & 65535] = num4;
					i++;
				}
				do
				{
					chainTable[i & 65535] = num4;
					hashTable[(int)(LZ4Codec.Peek4(src, i) * 2654435761U >> 17)] = i - src_base;
					i++;
				}
				while (i < num7);
				ctx.nextToUpdate = num7;
			}
			return num3;
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000E8518 File Offset: 0x000E6718
		private static int LZ4HC_InsertAndGetWiderMatch_32(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p, int startLimit, int longest, ref int matchpos, ref int startpos)
		{
			ushort[] chainTable = ctx.chainTable;
			int[] hashTable = ctx.hashTable;
			byte[] src = ctx.src;
			int src_base = ctx.src_base;
			int src_LASTLITERALS = ctx.src_LASTLITERALS;
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_32;
			int num = 256;
			int num2 = src_p - startLimit;
			LZ4Codec.LZ4HC_Insert_32(ctx, src_p);
			int num3 = hashTable[(int)(LZ4Codec.Peek4(src, src_p) * 2654435761U >> 17)] + src_base;
			while (num3 >= src_p - 65535 && num != 0)
			{
				num--;
				if (src[startLimit + longest] == src[num3 - num2 + longest] && LZ4Codec.Equal4(src, num3, src_p))
				{
					int num4 = num3 + 4;
					int i = src_p + 4;
					int num5 = src_p;
					while (i < src_LASTLITERALS - 3)
					{
						int num6 = (int)LZ4Codec.Xor4(src, num4, i);
						if (num6 == 0)
						{
							i += 4;
							num4 += 4;
						}
						else
						{
							i += debruijn_TABLE_[(int)((uint)((num6 & -num6) * 125613361) >> 27)];
							IL_FF:
							num4 = num3;
							while (num5 > startLimit && num4 > src_base && src[num5 - 1] == src[num4 - 1])
							{
								num5--;
								num4--;
							}
							if (i - num5 > longest)
							{
								longest = i - num5;
								matchpos = num4;
								startpos = num5;
								goto IL_142;
							}
							goto IL_142;
						}
					}
					if (i < src_LASTLITERALS - 1 && LZ4Codec.Equal2(src, num4, i))
					{
						i += 2;
						num4 += 2;
					}
					if (i < src_LASTLITERALS && src[num4] == src[i])
					{
						i++;
						goto IL_FF;
					}
					goto IL_FF;
				}
				IL_142:
				num3 -= (int)chainTable[num3 & 65535];
			}
			return longest;
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x000E868C File Offset: 0x000E688C
		private static int LZ4_encodeSequence_32(LZ4Codec.LZ4HC_Data_Structure ctx, ref int src_p, ref int dst_p, ref int src_anchor, int matchLength, int src_ref, int dst_end)
		{
			byte[] src = ctx.src;
			byte[] dst = ctx.dst;
			int num = src_p - src_anchor;
			int num2 = dst_p;
			dst_p = num2 + 1;
			int num3 = num2;
			if (dst_p + num + 8 + (num >> 8) > dst_end)
			{
				return 1;
			}
			int i;
			if (num >= 15)
			{
				dst[num3] = 240;
				for (i = num - 15; i > 254; i -= 255)
				{
					byte[] array = dst;
					num2 = dst_p;
					dst_p = num2 + 1;
					array[num2] = 255;
				}
				byte[] array2 = dst;
				num2 = dst_p;
				dst_p = num2 + 1;
				array2[num2] = (byte)i;
			}
			else
			{
				dst[num3] = (byte)(num << 4);
			}
			if (num > 0)
			{
				int num4 = dst_p + num;
				src_anchor += LZ4Codec.WildCopy(src, src_anchor, dst, dst_p, num4);
				dst_p = num4;
			}
			LZ4Codec.Poke2(dst, dst_p, (ushort)(src_p - src_ref));
			dst_p += 2;
			i = matchLength - 4;
			if (dst_p + 6 + (num >> 8) > dst_end)
			{
				return 1;
			}
			if (i >= 15)
			{
				byte[] array3 = dst;
				int num5 = num3;
				array3[num5] += 15;
				for (i -= 15; i > 509; i -= 510)
				{
					byte[] array4 = dst;
					num2 = dst_p;
					dst_p = num2 + 1;
					array4[num2] = 255;
					byte[] array5 = dst;
					num2 = dst_p;
					dst_p = num2 + 1;
					array5[num2] = 255;
				}
				if (i > 254)
				{
					i -= 255;
					byte[] array6 = dst;
					num2 = dst_p;
					dst_p = num2 + 1;
					array6[num2] = 255;
				}
				byte[] array7 = dst;
				num2 = dst_p;
				dst_p = num2 + 1;
				array7[num2] = (byte)i;
			}
			else
			{
				byte[] array8 = dst;
				int num6 = num3;
				array8[num6] += (byte)i;
			}
			src_p += matchLength;
			src_anchor = src_p;
			return 0;
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000E8808 File Offset: 0x000E6A08
		private static int LZ4_compressHCCtx_32(LZ4Codec.LZ4HC_Data_Structure ctx)
		{
			byte[] src = ctx.src;
			byte[] dst = ctx.dst;
			int src_base = ctx.src_base;
			int src_end = ctx.src_end;
			int dst_base = ctx.dst_base;
			int dst_len = ctx.dst_len;
			int dst_end = ctx.dst_end;
			int i = src_base;
			int num = i;
			int num2 = src_end - 12;
			int num3 = dst_base;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			for (i++; i < num2; i++)
			{
				int num9 = LZ4Codec.LZ4HC_InsertAndFindBestMatch_32(ctx, i, ref num4);
				if (num9 != 0)
				{
					int num10 = i;
					int num11 = num4;
					int num12 = num9;
					int num13;
					for (;;)
					{
						num13 = ((i + num9 < num2) ? LZ4Codec.LZ4HC_InsertAndGetWiderMatch_32(ctx, i + num9 - 2, i + 1, num9, ref num6, ref num5) : num9);
						if (num13 == num9)
						{
							break;
						}
						if (num10 < i && num5 < i + num12)
						{
							i = num10;
							num4 = num11;
							num9 = num12;
						}
						if (num5 - i < 3)
						{
							num9 = num13;
							i = num5;
							num4 = num6;
						}
						else
						{
							int num16;
							for (;;)
							{
								if (num5 - i < 18)
								{
									int num14 = num9;
									if (num14 > 18)
									{
										num14 = 18;
									}
									if (i + num14 > num5 + num13 - 4)
									{
										num14 = num5 - i + num13 - 4;
									}
									int num15 = num14 - (num5 - i);
									if (num15 > 0)
									{
										num5 += num15;
										num6 += num15;
										num13 -= num15;
									}
								}
								num16 = ((num5 + num13 < num2) ? LZ4Codec.LZ4HC_InsertAndGetWiderMatch_32(ctx, num5 + num13 - 3, num5, num13, ref num8, ref num7) : num13);
								if (num16 == num13)
								{
									goto Block_13;
								}
								if (num7 < i + num9 + 3)
								{
									if (num7 >= i + num9)
									{
										break;
									}
									num5 = num7;
									num6 = num8;
									num13 = num16;
								}
								else
								{
									if (num5 < i + num9)
									{
										if (num5 - i < 15)
										{
											if (num9 > 18)
											{
												num9 = 18;
											}
											if (i + num9 > num5 + num13 - 4)
											{
												num9 = num5 - i + num13 - 4;
											}
											int num17 = num9 - (num5 - i);
											if (num17 > 0)
											{
												num5 += num17;
												num6 += num17;
												num13 -= num17;
											}
										}
										else
										{
											num9 = num5 - i;
										}
									}
									if (LZ4Codec.LZ4_encodeSequence_32(ctx, ref i, ref num3, ref num, num9, num4, dst_end) != 0)
									{
										return 0;
									}
									i = num5;
									num4 = num6;
									num9 = num13;
									num5 = num7;
									num6 = num8;
									num13 = num16;
								}
							}
							if (num5 < i + num9)
							{
								int num18 = i + num9 - num5;
								num5 += num18;
								num6 += num18;
								num13 -= num18;
								if (num13 < 4)
								{
									num5 = num7;
									num6 = num8;
									num13 = num16;
								}
							}
							if (LZ4Codec.LZ4_encodeSequence_32(ctx, ref i, ref num3, ref num, num9, num4, dst_end) != 0)
							{
								return 0;
							}
							i = num7;
							num4 = num8;
							num9 = num16;
							num10 = num5;
							num11 = num6;
							num12 = num13;
						}
					}
					if (LZ4Codec.LZ4_encodeSequence_32(ctx, ref i, ref num3, ref num, num9, num4, dst_end) != 0)
					{
						return 0;
					}
					continue;
					Block_13:
					if (num5 < i + num9)
					{
						num9 = num5 - i;
					}
					if (LZ4Codec.LZ4_encodeSequence_32(ctx, ref i, ref num3, ref num, num9, num4, dst_end) != 0)
					{
						return 0;
					}
					i = num5;
					if (LZ4Codec.LZ4_encodeSequence_32(ctx, ref i, ref num3, ref num, num13, num6, dst_end) != 0)
					{
						return 0;
					}
					continue;
				}
			}
			int j = src_end - num;
			if ((long)(num3 - dst_base + j + 1 + (j + 255 - 15) / 255) > (long)((ulong)dst_len))
			{
				return 0;
			}
			if (j >= 15)
			{
				dst[num3++] = 240;
				for (j -= 15; j > 254; j -= 255)
				{
					dst[num3++] = byte.MaxValue;
				}
				dst[num3++] = (byte)j;
			}
			else
			{
				dst[num3++] = (byte)(j << 4);
			}
			LZ4Codec.BlockCopy(src, num, dst, num3, src_end - num);
			num3 += src_end - num;
			return num3 - dst_base;
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000E8BA4 File Offset: 0x000E6DA4
		private static int LZ4_compressCtx_safe64(int[] hash_table, byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_64;
			int num = src_0;
			int num2 = src_0 + src_len;
			int num3 = num2 - 12;
			int num4 = dst_0;
			int num5 = num4 + dst_maxlen;
			int num6 = num2 - 5;
			int num7 = num6 - 1;
			int num8 = num6 - 3;
			int num9 = num6 - 7;
			int num10 = num5 - 6;
			int num11 = num5 - 8;
			if (src_len >= 13)
			{
				hash_table[(int)(LZ4Codec.Peek4(src, src_0) * 2654435761U >> 20)] = src_0 - src_0;
				int i = src_0 + 1;
				uint num12 = LZ4Codec.Peek4(src, i) * 2654435761U >> 20;
				for (;;)
				{
					int num13 = 67;
					int num14 = i;
					int num17;
					do
					{
						uint num15 = num12;
						int num16 = num13++ >> 6;
						i = num14;
						num14 = i + num16;
						if (num14 > num3)
						{
							goto IL_383;
						}
						num12 = LZ4Codec.Peek4(src, num14) * 2654435761U >> 20;
						num17 = src_0 + hash_table[(int)num15];
						hash_table[(int)num15] = i - src_0;
					}
					while (num17 < i - 65535 || !LZ4Codec.Equal4(src, num17, i));
					while (i > num && num17 > src_0 && src[i - 1] == src[num17 - 1])
					{
						i--;
						num17--;
					}
					int j = i - num;
					int num18 = num4++;
					if (num4 + j + (j >> 8) > num11)
					{
						break;
					}
					if (j < 15)
					{
						dst[num18] = (byte)(j << 4);
						goto IL_198;
					}
					int num19 = j - 15;
					dst[num18] = 240;
					if (num19 <= 254)
					{
						dst[num4++] = (byte)num19;
						goto IL_198;
					}
					do
					{
						dst[num4++] = byte.MaxValue;
						num19 -= 255;
					}
					while (num19 > 254);
					dst[num4++] = (byte)num19;
					LZ4Codec.BlockCopy(src, num, dst, num4, j);
					num4 += j;
					for (;;)
					{
						IL_1B3:
						LZ4Codec.Poke2(dst, num4, (ushort)(i - num17));
						num4 += 2;
						i += 4;
						num17 += 4;
						num = i;
						while (i < num9)
						{
							long num20 = (long)LZ4Codec.Xor8(src, num17, i);
							if (num20 == 0L)
							{
								i += 8;
								num17 += 8;
							}
							else
							{
								i += debruijn_TABLE_[(int)(checked((IntPtr)((ulong)(unchecked((num20 & -num20) * 151050438428048703L)) >> 58)))];
								IL_256:
								j = i - num;
								if (num4 + (j >> 8) > num10)
								{
									return 0;
								}
								if (j >= 15)
								{
									int num21 = num18;
									dst[num21] += 15;
									for (j -= 15; j > 509; j -= 510)
									{
										dst[num4++] = byte.MaxValue;
										dst[num4++] = byte.MaxValue;
									}
									if (j > 254)
									{
										j -= 255;
										dst[num4++] = byte.MaxValue;
									}
									dst[num4++] = (byte)j;
								}
								else
								{
									int num22 = num18;
									dst[num22] += (byte)j;
								}
								if (i > num3)
								{
									goto Block_23;
								}
								hash_table[(int)(LZ4Codec.Peek4(src, i - 2) * 2654435761U >> 20)] = i - 2 - src_0;
								uint num15 = LZ4Codec.Peek4(src, i) * 2654435761U >> 20;
								num17 = src_0 + hash_table[(int)num15];
								hash_table[(int)num15] = i - src_0;
								if (num17 > i - 65536 && LZ4Codec.Equal4(src, num17, i))
								{
									num18 = num4++;
									dst[num18] = 0;
									goto IL_1B3;
								}
								goto IL_365;
							}
						}
						if (i < num8 && LZ4Codec.Equal4(src, num17, i))
						{
							i += 4;
							num17 += 4;
						}
						if (i < num7 && LZ4Codec.Equal2(src, num17, i))
						{
							i += 2;
							num17 += 2;
						}
						if (i < num6 && src[num17] == src[i])
						{
							i++;
							goto IL_256;
						}
						goto IL_256;
					}
					IL_365:
					num = i++;
					num12 = LZ4Codec.Peek4(src, i) * 2654435761U >> 20;
					continue;
					IL_198:
					if (j > 0)
					{
						int num23 = num4 + j;
						LZ4Codec.WildCopy(src, num, dst, num4, num23);
						num4 = num23;
						goto IL_1B3;
					}
					goto IL_1B3;
				}
				return 0;
				Block_23:
				num = i;
			}
			IL_383:
			int k = num2 - num;
			if (num4 + k + 1 + (k + 255 - 15) / 255 > num5)
			{
				return 0;
			}
			if (k >= 15)
			{
				dst[num4++] = 240;
				for (k -= 15; k > 254; k -= 255)
				{
					dst[num4++] = byte.MaxValue;
				}
				dst[num4++] = (byte)k;
			}
			else
			{
				dst[num4++] = (byte)(k << 4);
			}
			LZ4Codec.BlockCopy(src, num, dst, num4, num2 - num);
			num4 += num2 - num;
			return num4 - dst_0;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000E8FD4 File Offset: 0x000E71D4
		private static int LZ4_compress64kCtx_safe64(ushort[] hash_table, byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_64;
			int num = src_0;
			int num2 = src_0 + src_len;
			int num3 = num2 - 12;
			int num4 = dst_0;
			int num5 = num4 + dst_maxlen;
			int num6 = num2 - 5;
			int num7 = num6 - 1;
			int num8 = num6 - 3;
			int num9 = num6 - 7;
			int num10 = num5 - 6;
			int num11 = num5 - 8;
			if (src_len >= 13)
			{
				int i = src_0 + 1;
				uint num12 = LZ4Codec.Peek4(src, i) * 2654435761U >> 19;
				for (;;)
				{
					int num13 = 67;
					int num14 = i;
					int num17;
					do
					{
						uint num15 = num12;
						int num16 = num13++ >> 6;
						i = num14;
						num14 = i + num16;
						if (num14 > num3)
						{
							goto IL_355;
						}
						num12 = LZ4Codec.Peek4(src, num14) * 2654435761U >> 19;
						num17 = src_0 + (int)hash_table[(int)num15];
						hash_table[(int)num15] = (ushort)(i - src_0);
					}
					while (!LZ4Codec.Equal4(src, num17, i));
					while (i > num && num17 > src_0 && src[i - 1] == src[num17 - 1])
					{
						i--;
						num17--;
					}
					int num18 = i - num;
					int num19 = num4++;
					if (num4 + num18 + (num18 >> 8) > num11)
					{
						break;
					}
					if (num18 < 15)
					{
						dst[num19] = (byte)(num18 << 4);
						goto IL_178;
					}
					int j = num18 - 15;
					dst[num19] = 240;
					if (j <= 254)
					{
						dst[num4++] = (byte)j;
						goto IL_178;
					}
					do
					{
						dst[num4++] = byte.MaxValue;
						j -= 255;
					}
					while (j > 254);
					dst[num4++] = (byte)j;
					LZ4Codec.BlockCopy(src, num, dst, num4, num18);
					num4 += num18;
					for (;;)
					{
						IL_192:
						LZ4Codec.Poke2(dst, num4, (ushort)(i - num17));
						num4 += 2;
						i += 4;
						num17 += 4;
						num = i;
						while (i < num9)
						{
							long num20 = (long)LZ4Codec.Xor8(src, num17, i);
							if (num20 == 0L)
							{
								i += 8;
								num17 += 8;
							}
							else
							{
								i += debruijn_TABLE_[(int)(checked((IntPtr)((ulong)(unchecked((num20 & -num20) * 151050438428048703L)) >> 58)))];
								IL_234:
								j = i - num;
								if (num4 + (j >> 8) > num10)
								{
									return 0;
								}
								if (j >= 15)
								{
									int num21 = num19;
									dst[num21] += 15;
									for (j -= 15; j > 509; j -= 510)
									{
										dst[num4++] = byte.MaxValue;
										dst[num4++] = byte.MaxValue;
									}
									if (j > 254)
									{
										j -= 255;
										dst[num4++] = byte.MaxValue;
									}
									dst[num4++] = (byte)j;
								}
								else
								{
									int num22 = num19;
									dst[num22] += (byte)j;
								}
								if (i > num3)
								{
									goto Block_22;
								}
								hash_table[(int)(LZ4Codec.Peek4(src, i - 2) * 2654435761U >> 19)] = (ushort)(i - 2 - src_0);
								uint num15 = LZ4Codec.Peek4(src, i) * 2654435761U >> 19;
								num17 = src_0 + (int)hash_table[(int)num15];
								hash_table[(int)num15] = (ushort)(i - src_0);
								if (LZ4Codec.Equal4(src, num17, i))
								{
									num19 = num4++;
									dst[num19] = 0;
									goto IL_192;
								}
								goto IL_338;
							}
						}
						if (i < num8 && LZ4Codec.Equal4(src, num17, i))
						{
							i += 4;
							num17 += 4;
						}
						if (i < num7 && LZ4Codec.Equal2(src, num17, i))
						{
							i += 2;
							num17 += 2;
						}
						if (i < num6 && src[num17] == src[i])
						{
							i++;
							goto IL_234;
						}
						goto IL_234;
					}
					IL_338:
					num = i++;
					num12 = LZ4Codec.Peek4(src, i) * 2654435761U >> 19;
					continue;
					IL_178:
					if (num18 > 0)
					{
						int num23 = num4 + num18;
						LZ4Codec.WildCopy(src, num, dst, num4, num23);
						num4 = num23;
						goto IL_192;
					}
					goto IL_192;
				}
				return 0;
				Block_22:
				num = i;
			}
			IL_355:
			int k = num2 - num;
			if (num4 + k + 1 + (k - 15 + 255) / 255 > num5)
			{
				return 0;
			}
			if (k >= 15)
			{
				dst[num4++] = 240;
				for (k -= 15; k > 254; k -= 255)
				{
					dst[num4++] = byte.MaxValue;
				}
				dst[num4++] = (byte)k;
			}
			else
			{
				dst[num4++] = (byte)(k << 4);
			}
			LZ4Codec.BlockCopy(src, num, dst, num4, num2 - num);
			num4 += num2 - num;
			return num4 - dst_0;
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000E93D4 File Offset: 0x000E75D4
		private static int LZ4_uncompress_safe64(byte[] src, byte[] dst, int src_0, int dst_0, int dst_len)
		{
			int[] decoder_TABLE_ = LZ4Codec.DECODER_TABLE_32;
			int[] decoder_TABLE_2 = LZ4Codec.DECODER_TABLE_64;
			int num = src_0;
			int i = dst_0;
			int num2 = i + dst_len;
			int num3 = num2 - 5;
			int num4 = num2 - 8;
			int num5 = num2 - 8 - 4;
			int num7;
			int num9;
			for (;;)
			{
				uint num6 = (uint)src[num++];
				if ((num7 = (int)((byte)(num6 >> 4))) == 15)
				{
					int num8;
					while ((num8 = (int)src[num++]) == 255)
					{
						num7 += 255;
					}
					num7 += num8;
				}
				num9 = i + num7;
				if (num9 > num4)
				{
					break;
				}
				if (i < num9)
				{
					int num10 = LZ4Codec.WildCopy(src, num, dst, i, num9);
					num += num10;
					i += num10;
				}
				num -= i - num9;
				i = num9;
				int num11 = num9 - (int)LZ4Codec.Peek2(src, num);
				num += 2;
				if (num11 < dst_0)
				{
					goto IL_200;
				}
				if ((num7 = (int)((byte)(num6 & 15U))) == 15)
				{
					while (src[num] == 255)
					{
						num++;
						num7 += 255;
					}
					num7 += (int)src[num++];
				}
				if (i - num11 < 8)
				{
					int num12 = decoder_TABLE_2[i - num11];
					dst[i] = dst[num11];
					dst[i + 1] = dst[num11 + 1];
					dst[i + 2] = dst[num11 + 2];
					dst[i + 3] = dst[num11 + 3];
					i += 4;
					num11 += 4;
					num11 -= decoder_TABLE_[i - num11];
					LZ4Codec.Copy4(dst, num11, i);
					i += 4;
					num11 -= num12;
				}
				else
				{
					LZ4Codec.Copy8(dst, num11, i);
					i += 8;
					num11 += 8;
				}
				num9 = i + num7 - 4;
				if (num9 > num5)
				{
					if (num9 > num3)
					{
						goto IL_200;
					}
					if (i < num4)
					{
						int num10 = LZ4Codec.SecureCopy(dst, num11, i, num4);
						num11 += num10;
						i += num10;
					}
					while (i < num9)
					{
						dst[i++] = dst[num11++];
					}
					i = num9;
				}
				else
				{
					if (i < num9)
					{
						LZ4Codec.SecureCopy(dst, num11, i, num9);
					}
					i = num9;
				}
			}
			if (num9 == num2)
			{
				LZ4Codec.BlockCopy(src, num, dst, i, num7);
				num += num7;
				return num - src_0;
			}
			IL_200:
			return -(num - src_0);
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000E95E8 File Offset: 0x000E77E8
		private static int LZ4_uncompress_unknownOutputSize_safe64(byte[] src, byte[] dst, int src_0, int dst_0, int src_len, int dst_maxlen)
		{
			int[] decoder_TABLE_ = LZ4Codec.DECODER_TABLE_32;
			int[] decoder_TABLE_2 = LZ4Codec.DECODER_TABLE_64;
			int i = src_0;
			int num = i + src_len;
			int j = dst_0;
			int num2 = j + dst_maxlen;
			int num3 = num - 8;
			int num4 = num - 6;
			int num5 = num2 - 8;
			int num6 = num2 - 12;
			int num7 = num2 - 5;
			int num8 = num2 - 12;
			if (i != num)
			{
				int num9;
				int num11;
				for (;;)
				{
					byte b = src[i++];
					if ((num9 = b >> 4) == 15)
					{
						int num10 = 255;
						while (i < num && num10 == 255)
						{
							num9 += (num10 = (int)src[i++]);
						}
					}
					num11 = j + num9;
					if (num11 > num8 || i + num9 > num3)
					{
						break;
					}
					if (j < num11)
					{
						int num12 = LZ4Codec.WildCopy(src, i, dst, j, num11);
						i += num12;
						j += num12;
					}
					i -= j - num11;
					j = num11;
					int num13 = num11 - (int)LZ4Codec.Peek2(src, i);
					i += 2;
					if (num13 < dst_0)
					{
						goto IL_230;
					}
					if ((num9 = (int)(b & 15)) == 15)
					{
						while (i < num4)
						{
							int num14 = (int)src[i++];
							num9 += num14;
							if (num14 != 255)
							{
								break;
							}
						}
					}
					if (j - num13 < 8)
					{
						int num15 = decoder_TABLE_2[j - num13];
						dst[j] = dst[num13];
						dst[j + 1] = dst[num13 + 1];
						dst[j + 2] = dst[num13 + 2];
						dst[j + 3] = dst[num13 + 3];
						j += 4;
						num13 += 4;
						num13 -= decoder_TABLE_[j - num13];
						LZ4Codec.Copy4(dst, num13, j);
						j += 4;
						num13 -= num15;
					}
					else
					{
						LZ4Codec.Copy8(dst, num13, j);
						j += 8;
						num13 += 8;
					}
					num11 = j + num9 - 4;
					if (num11 > num6)
					{
						if (num11 > num7)
						{
							goto IL_230;
						}
						if (j < num5)
						{
							int num12 = LZ4Codec.SecureCopy(dst, num13, j, num5);
							num13 += num12;
							j += num12;
						}
						while (j < num11)
						{
							dst[j++] = dst[num13++];
						}
						j = num11;
					}
					else
					{
						if (j < num11)
						{
							LZ4Codec.SecureCopy(dst, num13, j, num11);
						}
						j = num11;
					}
				}
				if (num11 <= num2 && i + num9 == num)
				{
					LZ4Codec.BlockCopy(src, i, dst, j, num9);
					j += num9;
					return j - dst_0;
				}
			}
			IL_230:
			return -(i - src_0);
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000E982C File Offset: 0x000E7A2C
		private static void LZ4HC_Insert_64(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p)
		{
			ushort[] chainTable = ctx.chainTable;
			int[] hashTable = ctx.hashTable;
			byte[] src = ctx.src;
			int src_base = ctx.src_base;
			int i;
			for (i = ctx.nextToUpdate; i < src_p; i++)
			{
				int num = i;
				int num2 = num - (hashTable[(int)(LZ4Codec.Peek4(src, num) * 2654435761U >> 17)] + src_base);
				if (num2 > 65535)
				{
					num2 = 65535;
				}
				chainTable[num & 65535] = (ushort)num2;
				hashTable[(int)(LZ4Codec.Peek4(src, num) * 2654435761U >> 17)] = num - src_base;
			}
			ctx.nextToUpdate = i;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000E98C4 File Offset: 0x000E7AC4
		private static int LZ4HC_CommonLength_64(LZ4Codec.LZ4HC_Data_Structure ctx, int p1, int p2)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_64;
			byte[] src = ctx.src;
			int src_LASTLITERALS = ctx.src_LASTLITERALS;
			int i = p1;
			while (i < src_LASTLITERALS - 7)
			{
				long num = (long)LZ4Codec.Xor8(src, p2, i);
				if (num != 0L)
				{
					i += debruijn_TABLE_[(int)(checked((IntPtr)((ulong)(unchecked((num & -num) * 151050438428048703L)) >> 58)))];
					return i - p1;
				}
				i += 8;
				p2 += 8;
			}
			if (i < src_LASTLITERALS - 3 && LZ4Codec.Equal4(src, p2, i))
			{
				i += 4;
				p2 += 4;
			}
			if (i < src_LASTLITERALS - 1 && LZ4Codec.Equal2(src, p2, i))
			{
				i += 2;
				p2 += 2;
			}
			if (i < src_LASTLITERALS && src[p2] == src[i])
			{
				i++;
			}
			return i - p1;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000E996C File Offset: 0x000E7B6C
		private static int LZ4HC_InsertAndFindBestMatch_64(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p, ref int matchpos)
		{
			ushort[] chainTable = ctx.chainTable;
			int[] hashTable = ctx.hashTable;
			byte[] src = ctx.src;
			int src_base = ctx.src_base;
			int num = 256;
			int num2 = 0;
			int num3 = 0;
			ushort num4 = 0;
			LZ4Codec.LZ4HC_Insert_64(ctx, src_p);
			int num5 = hashTable[(int)(LZ4Codec.Peek4(src, src_p) * 2654435761U >> 17)] + src_base;
			if (num5 >= src_p - 4)
			{
				if (LZ4Codec.Equal4(src, num5, src_p))
				{
					num4 = (ushort)(src_p - num5);
					num3 = (num2 = LZ4Codec.LZ4HC_CommonLength_64(ctx, src_p + 4, num5 + 4) + 4);
					matchpos = num5;
				}
				num5 -= (int)chainTable[num5 & 65535];
			}
			while (num5 >= src_p - 65535 && num != 0)
			{
				num--;
				if (src[num5 + num3] == src[src_p + num3] && LZ4Codec.Equal4(src, num5, src_p))
				{
					int num6 = LZ4Codec.LZ4HC_CommonLength_64(ctx, src_p + 4, num5 + 4) + 4;
					if (num6 > num3)
					{
						num3 = num6;
						matchpos = num5;
					}
				}
				num5 -= (int)chainTable[num5 & 65535];
			}
			if (num2 != 0)
			{
				int i = src_p;
				int num7 = src_p + num2 - 3;
				while (i < num7 - (int)num4)
				{
					chainTable[i & 65535] = num4;
					i++;
				}
				do
				{
					chainTable[i & 65535] = num4;
					hashTable[(int)(LZ4Codec.Peek4(src, i) * 2654435761U >> 17)] = i - src_base;
					i++;
				}
				while (i < num7);
				ctx.nextToUpdate = num7;
			}
			return num3;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000E9ACC File Offset: 0x000E7CCC
		private static int LZ4HC_InsertAndGetWiderMatch_64(LZ4Codec.LZ4HC_Data_Structure ctx, int src_p, int startLimit, int longest, ref int matchpos, ref int startpos)
		{
			int[] debruijn_TABLE_ = LZ4Codec.DEBRUIJN_TABLE_64;
			ushort[] chainTable = ctx.chainTable;
			int[] hashTable = ctx.hashTable;
			byte[] src = ctx.src;
			int src_base = ctx.src_base;
			int src_LASTLITERALS = ctx.src_LASTLITERALS;
			int num = 256;
			int num2 = src_p - startLimit;
			LZ4Codec.LZ4HC_Insert_64(ctx, src_p);
			int num3 = hashTable[(int)(LZ4Codec.Peek4(src, src_p) * 2654435761U >> 17)] + src_base;
			while (num3 >= src_p - 65535 && num != 0)
			{
				num--;
				if (src[startLimit + longest] == src[num3 - num2 + longest] && LZ4Codec.Equal4(src, num3, src_p))
				{
					int num4 = num3 + 4;
					int i = src_p + 4;
					int num5 = src_p;
					while (i < src_LASTLITERALS - 7)
					{
						long num6 = (long)LZ4Codec.Xor8(src, num4, i);
						if (num6 == 0L)
						{
							i += 8;
							num4 += 8;
						}
						else
						{
							i += debruijn_TABLE_[(int)(checked((IntPtr)((ulong)(unchecked((num6 & -num6) * 151050438428048703L)) >> 58)))];
							IL_126:
							num4 = num3;
							while (num5 > startLimit && num4 > src_base && src[num5 - 1] == src[num4 - 1])
							{
								num5--;
								num4--;
							}
							if (i - num5 > longest)
							{
								longest = i - num5;
								matchpos = num4;
								startpos = num5;
								goto IL_169;
							}
							goto IL_169;
						}
					}
					if (i < src_LASTLITERALS - 3 && LZ4Codec.Equal4(src, num4, i))
					{
						i += 4;
						num4 += 4;
					}
					if (i < src_LASTLITERALS - 1 && LZ4Codec.Equal2(src, num4, i))
					{
						i += 2;
						num4 += 2;
					}
					if (i < src_LASTLITERALS && src[num4] == src[i])
					{
						i++;
						goto IL_126;
					}
					goto IL_126;
				}
				IL_169:
				num3 -= (int)chainTable[num3 & 65535];
			}
			return longest;
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000E9C64 File Offset: 0x000E7E64
		private static int LZ4_encodeSequence_64(LZ4Codec.LZ4HC_Data_Structure ctx, ref int src_p, ref int dst_p, ref int src_anchor, int matchLength, int src_ref)
		{
			byte[] src = ctx.src;
			byte[] dst = ctx.dst;
			int dst_end = ctx.dst_end;
			int num = src_p - src_anchor;
			int num2 = dst_p;
			dst_p = num2 + 1;
			int num3 = num2;
			if (dst_p + num + 8 + (num >> 8) > dst_end)
			{
				return 1;
			}
			int i;
			if (num >= 15)
			{
				dst[num3] = 240;
				for (i = num - 15; i > 254; i -= 255)
				{
					byte[] array = dst;
					num2 = dst_p;
					dst_p = num2 + 1;
					array[num2] = 255;
				}
				byte[] array2 = dst;
				num2 = dst_p;
				dst_p = num2 + 1;
				array2[num2] = (byte)i;
			}
			else
			{
				dst[num3] = (byte)(num << 4);
			}
			if (num > 0)
			{
				int num4 = dst_p + num;
				src_anchor += LZ4Codec.WildCopy(src, src_anchor, dst, dst_p, num4);
				dst_p = num4;
			}
			LZ4Codec.Poke2(dst, dst_p, (ushort)(src_p - src_ref));
			dst_p += 2;
			i = matchLength - 4;
			if (dst_p + 6 + (num >> 8) > dst_end)
			{
				return 1;
			}
			if (i >= 15)
			{
				byte[] array3 = dst;
				int num5 = num3;
				array3[num5] += 15;
				for (i -= 15; i > 509; i -= 510)
				{
					byte[] array4 = dst;
					num2 = dst_p;
					dst_p = num2 + 1;
					array4[num2] = 255;
					byte[] array5 = dst;
					num2 = dst_p;
					dst_p = num2 + 1;
					array5[num2] = 255;
				}
				if (i > 254)
				{
					i -= 255;
					byte[] array6 = dst;
					num2 = dst_p;
					dst_p = num2 + 1;
					array6[num2] = 255;
				}
				byte[] array7 = dst;
				num2 = dst_p;
				dst_p = num2 + 1;
				array7[num2] = (byte)i;
			}
			else
			{
				byte[] array8 = dst;
				int num6 = num3;
				array8[num6] += (byte)i;
			}
			src_p += matchLength;
			src_anchor = src_p;
			return 0;
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x000E9DF0 File Offset: 0x000E7FF0
		private static int LZ4_compressHCCtx_64(LZ4Codec.LZ4HC_Data_Structure ctx)
		{
			byte[] src = ctx.src;
			int i = ctx.src_base;
			int src_end = ctx.src_end;
			int dst_base = ctx.dst_base;
			int num = i;
			int num2 = src_end - 12;
			byte[] dst = ctx.dst;
			int dst_len = ctx.dst_len;
			int num3 = ctx.dst_base;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			for (i++; i < num2; i++)
			{
				int num9 = LZ4Codec.LZ4HC_InsertAndFindBestMatch_64(ctx, i, ref num4);
				if (num9 != 0)
				{
					int num10 = i;
					int num11 = num4;
					int num12 = num9;
					int num13;
					for (;;)
					{
						num13 = ((i + num9 < num2) ? LZ4Codec.LZ4HC_InsertAndGetWiderMatch_64(ctx, i + num9 - 2, i + 1, num9, ref num6, ref num5) : num9);
						if (num13 == num9)
						{
							break;
						}
						if (num10 < i && num5 < i + num12)
						{
							i = num10;
							num4 = num11;
							num9 = num12;
						}
						if (num5 - i < 3)
						{
							num9 = num13;
							i = num5;
							num4 = num6;
						}
						else
						{
							int num16;
							for (;;)
							{
								if (num5 - i < 18)
								{
									int num14 = num9;
									if (num14 > 18)
									{
										num14 = 18;
									}
									if (i + num14 > num5 + num13 - 4)
									{
										num14 = num5 - i + num13 - 4;
									}
									int num15 = num14 - (num5 - i);
									if (num15 > 0)
									{
										num5 += num15;
										num6 += num15;
										num13 -= num15;
									}
								}
								num16 = ((num5 + num13 < num2) ? LZ4Codec.LZ4HC_InsertAndGetWiderMatch_64(ctx, num5 + num13 - 3, num5, num13, ref num8, ref num7) : num13);
								if (num16 == num13)
								{
									goto Block_13;
								}
								if (num7 < i + num9 + 3)
								{
									if (num7 >= i + num9)
									{
										break;
									}
									num5 = num7;
									num6 = num8;
									num13 = num16;
								}
								else
								{
									if (num5 < i + num9)
									{
										if (num5 - i < 15)
										{
											if (num9 > 18)
											{
												num9 = 18;
											}
											if (i + num9 > num5 + num13 - 4)
											{
												num9 = num5 - i + num13 - 4;
											}
											int num17 = num9 - (num5 - i);
											if (num17 > 0)
											{
												num5 += num17;
												num6 += num17;
												num13 -= num17;
											}
										}
										else
										{
											num9 = num5 - i;
										}
									}
									if (LZ4Codec.LZ4_encodeSequence_64(ctx, ref i, ref num3, ref num, num9, num4) != 0)
									{
										return 0;
									}
									i = num5;
									num4 = num6;
									num9 = num13;
									num5 = num7;
									num6 = num8;
									num13 = num16;
								}
							}
							if (num5 < i + num9)
							{
								int num18 = i + num9 - num5;
								num5 += num18;
								num6 += num18;
								num13 -= num18;
								if (num13 < 4)
								{
									num5 = num7;
									num6 = num8;
									num13 = num16;
								}
							}
							if (LZ4Codec.LZ4_encodeSequence_64(ctx, ref i, ref num3, ref num, num9, num4) != 0)
							{
								return 0;
							}
							i = num7;
							num4 = num8;
							num9 = num16;
							num10 = num5;
							num11 = num6;
							num12 = num13;
						}
					}
					if (LZ4Codec.LZ4_encodeSequence_64(ctx, ref i, ref num3, ref num, num9, num4) != 0)
					{
						return 0;
					}
					continue;
					Block_13:
					if (num5 < i + num9)
					{
						num9 = num5 - i;
					}
					if (LZ4Codec.LZ4_encodeSequence_64(ctx, ref i, ref num3, ref num, num9, num4) != 0)
					{
						return 0;
					}
					i = num5;
					if (LZ4Codec.LZ4_encodeSequence_64(ctx, ref i, ref num3, ref num, num13, num6) != 0)
					{
						return 0;
					}
					continue;
				}
			}
			int j = src_end - num;
			if ((long)(num3 - dst_base + j + 1 + (j + 255 - 15) / 255) > (long)((ulong)dst_len))
			{
				return 0;
			}
			if (j >= 15)
			{
				dst[num3++] = 240;
				for (j -= 15; j > 254; j -= 255)
				{
					dst[num3++] = byte.MaxValue;
				}
				dst[num3++] = (byte)j;
			}
			else
			{
				dst[num3++] = (byte)(j << 4);
			}
			LZ4Codec.BlockCopy(src, num, dst, num3, src_end - num);
			num3 += src_end - num;
			return num3 - dst_base;
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000EA15E File Offset: 0x000E835E
		public static int MaximumOutputLength(int inputLength)
		{
			return inputLength + inputLength / 255 + 16;
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000EA16C File Offset: 0x000E836C
		internal static void CheckArguments(byte[] input, int inputOffset, ref int inputLength, byte[] output, int outputOffset, ref int outputLength)
		{
			if (inputLength < 0)
			{
				inputLength = input.Length - inputOffset;
			}
			if (inputLength == 0)
			{
				outputLength = 0;
				return;
			}
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (inputOffset < 0 || inputOffset + inputLength > input.Length)
			{
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");
			}
			if (outputLength < 0)
			{
				outputLength = output.Length - outputOffset;
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (outputOffset < 0 || outputOffset + outputLength > output.Length)
			{
				throw new ArgumentException("outputOffset and outputLength are invalid for given output");
			}
		}

		// Token: 0x04002F51 RID: 12113
		private const int MEMORY_USAGE = 14;

		// Token: 0x04002F52 RID: 12114
		private const int NOTCOMPRESSIBLE_DETECTIONLEVEL = 6;

		// Token: 0x04002F53 RID: 12115
		private const int BLOCK_COPY_LIMIT = 16;

		// Token: 0x04002F54 RID: 12116
		private const int MINMATCH = 4;

		// Token: 0x04002F55 RID: 12117
		private const int SKIPSTRENGTH = 6;

		// Token: 0x04002F56 RID: 12118
		private const int COPYLENGTH = 8;

		// Token: 0x04002F57 RID: 12119
		private const int LASTLITERALS = 5;

		// Token: 0x04002F58 RID: 12120
		private const int MFLIMIT = 12;

		// Token: 0x04002F59 RID: 12121
		private const int MINLENGTH = 13;

		// Token: 0x04002F5A RID: 12122
		private const int MAXD_LOG = 16;

		// Token: 0x04002F5B RID: 12123
		private const int MAXD = 65536;

		// Token: 0x04002F5C RID: 12124
		private const int MAXD_MASK = 65535;

		// Token: 0x04002F5D RID: 12125
		private const int MAX_DISTANCE = 65535;

		// Token: 0x04002F5E RID: 12126
		private const int ML_BITS = 4;

		// Token: 0x04002F5F RID: 12127
		private const int ML_MASK = 15;

		// Token: 0x04002F60 RID: 12128
		private const int RUN_BITS = 4;

		// Token: 0x04002F61 RID: 12129
		private const int RUN_MASK = 15;

		// Token: 0x04002F62 RID: 12130
		private const int STEPSIZE_64 = 8;

		// Token: 0x04002F63 RID: 12131
		private const int STEPSIZE_32 = 4;

		// Token: 0x04002F64 RID: 12132
		private const int LZ4_64KLIMIT = 65547;

		// Token: 0x04002F65 RID: 12133
		private const int HASH_LOG = 12;

		// Token: 0x04002F66 RID: 12134
		private const int HASH_TABLESIZE = 4096;

		// Token: 0x04002F67 RID: 12135
		private const int HASH_ADJUST = 20;

		// Token: 0x04002F68 RID: 12136
		private const int HASH64K_LOG = 13;

		// Token: 0x04002F69 RID: 12137
		private const int HASH64K_TABLESIZE = 8192;

		// Token: 0x04002F6A RID: 12138
		private const int HASH64K_ADJUST = 19;

		// Token: 0x04002F6B RID: 12139
		private const int HASHHC_LOG = 15;

		// Token: 0x04002F6C RID: 12140
		private const int HASHHC_TABLESIZE = 32768;

		// Token: 0x04002F6D RID: 12141
		private const int HASHHC_ADJUST = 17;

		// Token: 0x04002F6E RID: 12142
		private static readonly int[] DECODER_TABLE_32 = new int[]
		{
			0,
			3,
			2,
			3,
			0,
			0,
			0,
			0
		};

		// Token: 0x04002F6F RID: 12143
		private static readonly int[] DECODER_TABLE_64 = new int[]
		{
			0,
			0,
			0,
			-1,
			0,
			1,
			2,
			3
		};

		// Token: 0x04002F70 RID: 12144
		private static readonly int[] DEBRUIJN_TABLE_32 = new int[]
		{
			0,
			0,
			3,
			0,
			3,
			1,
			3,
			0,
			3,
			2,
			2,
			1,
			3,
			2,
			0,
			1,
			3,
			3,
			1,
			2,
			2,
			2,
			2,
			0,
			3,
			1,
			2,
			0,
			1,
			0,
			1,
			1
		};

		// Token: 0x04002F71 RID: 12145
		private static readonly int[] DEBRUIJN_TABLE_64 = new int[]
		{
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			2,
			0,
			3,
			1,
			3,
			1,
			4,
			2,
			7,
			0,
			2,
			3,
			6,
			1,
			5,
			3,
			5,
			1,
			3,
			4,
			4,
			2,
			5,
			6,
			7,
			7,
			0,
			1,
			2,
			3,
			3,
			4,
			6,
			2,
			6,
			5,
			5,
			3,
			4,
			5,
			6,
			7,
			1,
			2,
			4,
			6,
			4,
			4,
			5,
			7,
			2,
			6,
			5,
			7,
			6,
			7,
			7
		};

		// Token: 0x04002F72 RID: 12146
		private const int MAX_NB_ATTEMPTS = 256;

		// Token: 0x04002F73 RID: 12147
		private const int OPTIMAL_ML = 18;

		// Token: 0x020012D7 RID: 4823
		private class LZ4HC_Data_Structure
		{
			// Token: 0x04009722 RID: 38690
			public byte[] src;

			// Token: 0x04009723 RID: 38691
			public int src_base;

			// Token: 0x04009724 RID: 38692
			public int src_end;

			// Token: 0x04009725 RID: 38693
			public int src_LASTLITERALS;

			// Token: 0x04009726 RID: 38694
			public byte[] dst;

			// Token: 0x04009727 RID: 38695
			public int dst_base;

			// Token: 0x04009728 RID: 38696
			public int dst_len;

			// Token: 0x04009729 RID: 38697
			public int dst_end;

			// Token: 0x0400972A RID: 38698
			public int[] hashTable;

			// Token: 0x0400972B RID: 38699
			public ushort[] chainTable;

			// Token: 0x0400972C RID: 38700
			public int nextToUpdate;
		}
	}
}
