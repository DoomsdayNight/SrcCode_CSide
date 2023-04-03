using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F49 RID: 3913
	[PacketId(ClientPacketId.kNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK)]
	public sealed class NKMPacket_GAME_AUTO_SKILL_CHANGE_ACK : ISerializable
	{
		// Token: 0x06009972 RID: 39282 RVA: 0x0033008C File Offset: 0x0032E28C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<NKM_GAME_AUTO_SKILL_TYPE>(ref this.gameAutoSkillType);
		}

		// Token: 0x04008C74 RID: 35956
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C75 RID: 35957
		public NKM_GAME_AUTO_SKILL_TYPE gameAutoSkillType;
	}
}
