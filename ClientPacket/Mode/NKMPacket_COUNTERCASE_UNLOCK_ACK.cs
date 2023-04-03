using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E33 RID: 3635
	[PacketId(ClientPacketId.kNKMPacket_COUNTERCASE_UNLOCK_ACK)]
	public sealed class NKMPacket_COUNTERCASE_UNLOCK_ACK : ISerializable
	{
		// Token: 0x06009756 RID: 38742 RVA: 0x0032CD54 File Offset: 0x0032AF54
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.dungeonID);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x04008977 RID: 35191
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008978 RID: 35192
		public int dungeonID;

		// Token: 0x04008979 RID: 35193
		public NKMItemMiscData costItemData;
	}
}
