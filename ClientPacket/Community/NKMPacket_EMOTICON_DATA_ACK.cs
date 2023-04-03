using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001006 RID: 4102
	[PacketId(ClientPacketId.kNKMPacket_EMOTICON_DATA_ACK)]
	public sealed class NKMPacket_EMOTICON_DATA_ACK : ISerializable
	{
		// Token: 0x06009ADC RID: 39644 RVA: 0x00331F6E File Offset: 0x0033016E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<EmoticonPresetData>(ref this.presetData);
			stream.PutOrGet(ref this.collections);
		}

		// Token: 0x04008E3A RID: 36410
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E3B RID: 36411
		public EmoticonPresetData presetData = new EmoticonPresetData();

		// Token: 0x04008E3C RID: 36412
		public HashSet<int> collections = new HashSet<int>();
	}
}
