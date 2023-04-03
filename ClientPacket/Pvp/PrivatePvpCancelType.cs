using System;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D93 RID: 3475
	public enum PrivatePvpCancelType
	{
		// Token: 0x04008827 RID: 34855
		None,
		// Token: 0x04008828 RID: 34856
		HostCancelInvitation,
		// Token: 0x04008829 RID: 34857
		OtherPlayerCancelGame,
		// Token: 0x0400882A RID: 34858
		OtherPlayerLogout,
		// Token: 0x0400882B RID: 34859
		MyInvitationRejected,
		// Token: 0x0400882C RID: 34860
		IRejectInvitation,
		// Token: 0x0400882D RID: 34861
		InvitationTimeout,
		// Token: 0x0400882E RID: 34862
		HostWasGone
	}
}
