using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F3E RID: 3902
	[PacketId(ClientPacketId.kNKMPacket_GAME_RESPAWN_ACK)]
	public sealed class NKMPacket_GAME_RESPAWN_ACK : ISerializable
	{
		// Token: 0x0600995C RID: 39260 RVA: 0x0032FF1B File Offset: 0x0032E11B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.assistUnit);
		}

		// Token: 0x04008C5F RID: 35935
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C60 RID: 35936
		public long unitUID;

		// Token: 0x04008C61 RID: 35937
		public bool assistUnit;
	}
}
