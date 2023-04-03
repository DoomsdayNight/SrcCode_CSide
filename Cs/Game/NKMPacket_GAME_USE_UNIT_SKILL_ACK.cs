using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F4B RID: 3915
	[PacketId(ClientPacketId.kNKMPacket_GAME_USE_UNIT_SKILL_ACK)]
	public sealed class NKMPacket_GAME_USE_UNIT_SKILL_ACK : ISerializable
	{
		// Token: 0x06009976 RID: 39286 RVA: 0x003300C4 File Offset: 0x0032E2C4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.gameUnitUID);
			stream.PutOrGet(ref this.skillStateID);
		}

		// Token: 0x04008C77 RID: 35959
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C78 RID: 35960
		public short gameUnitUID;

		// Token: 0x04008C79 RID: 35961
		public sbyte skillStateID;
	}
}
