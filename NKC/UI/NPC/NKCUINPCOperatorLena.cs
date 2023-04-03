using System;

namespace NKC.UI.NPC
{
	// Token: 0x02000C00 RID: 3072
	public class NKCUINPCOperatorLena : NKCUINPCSpine
	{
		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x06008E5A RID: 36442 RVA: 0x00307439 File Offset: 0x00305639
		protected override string LUA_ASSET_NAME
		{
			get
			{
				return "LUA_NPC_OPERATOR_LENA_TEMPLET";
			}
		}

		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x06008E5B RID: 36443 RVA: 0x00307440 File Offset: 0x00305640
		protected override NPC_TYPE NPCType
		{
			get
			{
				return NPC_TYPE.OPERATOR_LENA;
			}
		}

		// Token: 0x06008E5C RID: 36444 RVA: 0x00307444 File Offset: 0x00305644
		public override void Init(bool bUseIdleAnimation = true)
		{
			if (base.m_dicNPCTemplet == null || base.m_dicNPCTemplet.Count == 0)
			{
				base.LoadFromLua();
			}
			this.m_bUseIdleAni = (bUseIdleAnimation && base.m_dicNPCTemplet != null && base.m_dicNPCTemplet.ContainsKey(NPC_ACTION_TYPE.IDLE));
			base.Init(true);
		}

		// Token: 0x06008E5D RID: 36445 RVA: 0x00307493 File Offset: 0x00305693
		public static void PlayVoice(VOICE_TYPE type)
		{
			if (type == VOICE_TYPE.VT_LOBBY_CONNECT)
			{
				NKCUINPCOperatorLena.PlayVoice(NPC_TYPE.OPERATOR_LENA, NPC_ACTION_TYPE.LOBBY_CONNECT, true);
				return;
			}
			if (type != VOICE_TYPE.VT_LOBBY_RETURN)
			{
				return;
			}
			NKCUINPCOperatorLena.PlayVoice(NPC_TYPE.OPERATOR_LENA, NPC_ACTION_TYPE.LOBBY_RETURN, true);
		}

		// Token: 0x06008E5E RID: 36446 RVA: 0x003074B4 File Offset: 0x003056B4
		public static void PlayVoice(NPC_TYPE npcType, NPC_ACTION_TYPE npcActionType, bool bStopCurrentSound)
		{
			NKCNPCTemplet npctemplet = NKCUINPCBase.GetNPCTemplet(npcType, npcActionType);
			if (npctemplet != null)
			{
				NKCUINPCBase.PlayVoice(npcType, npctemplet, bStopCurrentSound, false, false);
			}
		}

		// Token: 0x06008E5F RID: 36447 RVA: 0x003074D7 File Offset: 0x003056D7
		public override void PlayAni(eEmotion emotion)
		{
		}

		// Token: 0x06008E60 RID: 36448 RVA: 0x003074D9 File Offset: 0x003056D9
		public void ShowText(string text = "")
		{
		}

		// Token: 0x06008E61 RID: 36449 RVA: 0x003074DB File Offset: 0x003056DB
		public override void DragEvent()
		{
		}
	}
}
