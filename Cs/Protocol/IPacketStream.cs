using System;
using System.Collections;
using System.Collections.Generic;

namespace Cs.Protocol
{
	// Token: 0x020010BE RID: 4286
	public interface IPacketStream
	{
		// Token: 0x06009CC4 RID: 40132
		void PutOrGet(ref bool data);

		// Token: 0x06009CC5 RID: 40133
		void PutOrGet(ref sbyte data);

		// Token: 0x06009CC6 RID: 40134
		void PutOrGet(ref byte data);

		// Token: 0x06009CC7 RID: 40135
		void PutOrGet(ref short data);

		// Token: 0x06009CC8 RID: 40136
		void PutOrGet(ref ushort data);

		// Token: 0x06009CC9 RID: 40137
		void PutOrGet(ref int data);

		// Token: 0x06009CCA RID: 40138
		void PutOrGet(ref uint data);

		// Token: 0x06009CCB RID: 40139
		void PutOrGet(ref long data);

		// Token: 0x06009CCC RID: 40140
		void PutOrGet(ref ulong data);

		// Token: 0x06009CCD RID: 40141
		void PutOrGet(ref float data);

		// Token: 0x06009CCE RID: 40142
		void AsHalf(ref float data);

		// Token: 0x06009CCF RID: 40143
		void PutOrGet(ref double data);

		// Token: 0x06009CD0 RID: 40144
		void PutOrGet(ref string data);

		// Token: 0x06009CD1 RID: 40145
		void PutOrGet(ref bool[] data);

		// Token: 0x06009CD2 RID: 40146
		void PutOrGet(ref int[] data);

		// Token: 0x06009CD3 RID: 40147
		void PutOrGet(ref long[] data);

		// Token: 0x06009CD4 RID: 40148
		void PutOrGet<T>(ref T[] data) where T : ISerializable;

		// Token: 0x06009CD5 RID: 40149
		void PutOrGet(ref byte[] data);

		// Token: 0x06009CD6 RID: 40150
		void PutOrGet(ref BitArray data);

		// Token: 0x06009CD7 RID: 40151
		void PutOrGet(ref DateTime data);

		// Token: 0x06009CD8 RID: 40152
		void PutOrGet(ref TimeSpan data);

		// Token: 0x06009CD9 RID: 40153
		void PutOrGet<T>(ref T data) where T : ISerializable;

		// Token: 0x06009CDA RID: 40154
		void PutOrGetEnum<T>(ref T data) where T : Enum;

		// Token: 0x06009CDB RID: 40155
		void PutOrGetEnum<T>(ref List<T> data) where T : Enum;

		// Token: 0x06009CDC RID: 40156
		void PutOrGet(ref List<bool> data);

		// Token: 0x06009CDD RID: 40157
		void PutOrGet(ref List<byte> data);

		// Token: 0x06009CDE RID: 40158
		void PutOrGet(ref List<short> data);

		// Token: 0x06009CDF RID: 40159
		void PutOrGet(ref List<int> data);

		// Token: 0x06009CE0 RID: 40160
		void PutOrGet(ref List<float> data);

		// Token: 0x06009CE1 RID: 40161
		void PutOrGet(ref List<long> data);

		// Token: 0x06009CE2 RID: 40162
		void PutOrGet(ref List<string> data);

		// Token: 0x06009CE3 RID: 40163
		void PutOrGet<T>(ref List<T> data) where T : ISerializable;

		// Token: 0x06009CE4 RID: 40164
		void PutOrGet(ref HashSet<short> data);

		// Token: 0x06009CE5 RID: 40165
		void PutOrGet(ref HashSet<int> data);

		// Token: 0x06009CE6 RID: 40166
		void PutOrGet(ref HashSet<string> data);

		// Token: 0x06009CE7 RID: 40167
		void PutOrGet(ref HashSet<long> data);

		// Token: 0x06009CE8 RID: 40168
		void PutOrGet<T>(ref HashSet<T> data) where T : ISerializable;

		// Token: 0x06009CE9 RID: 40169
		void PutOrGetEnum<T>(ref HashSet<T> data) where T : Enum;

		// Token: 0x06009CEA RID: 40170
		void PutOrGet(ref Dictionary<int, int> data);

		// Token: 0x06009CEB RID: 40171
		void PutOrGet(ref Dictionary<int, float> data);

		// Token: 0x06009CEC RID: 40172
		void PutOrGet(ref Dictionary<int, long> data);

		// Token: 0x06009CED RID: 40173
		void PutOrGet(ref Dictionary<long, int> data);

		// Token: 0x06009CEE RID: 40174
		void PutOrGet(ref Dictionary<byte, byte> data);

		// Token: 0x06009CEF RID: 40175
		void PutOrGet(ref Dictionary<byte, long> data);

		// Token: 0x06009CF0 RID: 40176
		void PutOrGet<T>(ref Dictionary<byte, T> data) where T : ISerializable;

		// Token: 0x06009CF1 RID: 40177
		void PutOrGet<T>(ref Dictionary<short, T> data) where T : ISerializable;

		// Token: 0x06009CF2 RID: 40178
		void PutOrGet<T>(ref Dictionary<int, T> data) where T : ISerializable;

		// Token: 0x06009CF3 RID: 40179
		void PutOrGet<T>(ref Dictionary<long, T> data) where T : ISerializable;

		// Token: 0x06009CF4 RID: 40180
		void PutOrGet<T>(ref Dictionary<string, T> data) where T : ISerializable;
	}
}
