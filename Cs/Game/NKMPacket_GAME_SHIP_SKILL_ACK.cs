using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F40 RID: 3904
	[PacketId(ClientPacketId.kNKMPacket_GAME_SHIP_SKILL_ACK)]
	public sealed class NKMPacket_GAME_SHIP_SKILL_ACK : ISerializable
	{
		// Token: 0x06009960 RID: 39264 RVA: 0x0032FF77 File Offset: 0x0032E177
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.gameUnitUID);
			stream.PutOrGet(ref this.shipSkillID);
			stream.PutOrGet(ref this.skillPosX);
		}

		// Token: 0x04008C65 RID: 35941
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C66 RID: 35942
		public short gameUnitUID;

		// Token: 0x04008C67 RID: 35943
		public int shipSkillID;

		// Token: 0x04008C68 RID: 35944
		public float skillPosX;
	}
}
