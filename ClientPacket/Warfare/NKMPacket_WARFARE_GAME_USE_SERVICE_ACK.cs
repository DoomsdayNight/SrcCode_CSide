using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000CA4 RID: 3236
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_USE_SERVICE_ACK)]
	public sealed class NKMPacket_WARFARE_GAME_USE_SERVICE_ACK : ISerializable
	{
		// Token: 0x06009445 RID: 37957 RVA: 0x003286DC File Offset: 0x003268DC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.warfareGameUnitUID);
			stream.PutOrGetEnum<NKM_WARFARE_SERVICE_TYPE>(ref this.warfareServiceType);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
			stream.PutOrGet(ref this.hp);
			stream.PutOrGet(ref this.supply);
		}

		// Token: 0x04008594 RID: 34196
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008595 RID: 34197
		public int warfareGameUnitUID;

		// Token: 0x04008596 RID: 34198
		public NKM_WARFARE_SERVICE_TYPE warfareServiceType;

		// Token: 0x04008597 RID: 34199
		public NKMItemMiscData costItemData;

		// Token: 0x04008598 RID: 34200
		public float hp;

		// Token: 0x04008599 RID: 34201
		public byte supply;
	}
}
