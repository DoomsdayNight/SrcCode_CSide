using System;

namespace NKC.UI.NPC
{
	// Token: 0x02000C02 RID: 3074
	public class NKCUINPCShop : NKCUINPCSpine
	{
		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x06008E6C RID: 36460 RVA: 0x0030768A File Offset: 0x0030588A
		protected override string LUA_ASSET_NAME
		{
			get
			{
				return "LUA_NPC_STORE_SERINA_TEMPLET";
			}
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x06008E6D RID: 36461 RVA: 0x00307691 File Offset: 0x00305891
		protected override NPC_TYPE NPCType
		{
			get
			{
				return NPC_TYPE.STORE_SERINA;
			}
		}

		// Token: 0x06008E6E RID: 36462 RVA: 0x00307694 File Offset: 0x00305894
		public override void Init(bool bUseIdleAnimation = true)
		{
			if (base.m_dicNPCTemplet == null || base.m_dicNPCTemplet.Count == 0)
			{
				base.LoadFromLua();
			}
			this.m_bUseIdleAni = (bUseIdleAnimation && base.m_dicNPCTemplet != null && base.m_dicNPCTemplet.ContainsKey(NPC_ACTION_TYPE.IDLE));
			base.Init(true);
		}

		// Token: 0x06008E6F RID: 36463 RVA: 0x003076E4 File Offset: 0x003058E4
		public static void PlayVoice(NPC_TYPE npcType, NPC_ACTION_TYPE npcActionType, bool bStopCurrentSound)
		{
			NKCNPCTemplet npctemplet = NKCUINPCBase.GetNPCTemplet(npcType, npcActionType);
			if (npctemplet != null)
			{
				NKCUINPCBase.PlayVoice(npcType, npctemplet, bStopCurrentSound, false, false);
			}
		}

		// Token: 0x06008E70 RID: 36464 RVA: 0x00307707 File Offset: 0x00305907
		public override void PlayAni(eEmotion emotion)
		{
		}

		// Token: 0x06008E71 RID: 36465 RVA: 0x00307709 File Offset: 0x00305909
		public void ShowText(string text = "")
		{
		}

		// Token: 0x06008E72 RID: 36466 RVA: 0x0030770B File Offset: 0x0030590B
		public override void DragEvent()
		{
		}
	}
}
