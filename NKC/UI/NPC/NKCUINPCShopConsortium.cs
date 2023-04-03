using System;

namespace NKC.UI.NPC
{
	// Token: 0x02000C03 RID: 3075
	public class NKCUINPCShopConsortium : NKCUINPCSpine
	{
		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x06008E74 RID: 36468 RVA: 0x00307715 File Offset: 0x00305915
		protected override string LUA_ASSET_NAME
		{
			get
			{
				return "LUA_NPC_STORE_SIGMA_TEMPLET";
			}
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x06008E75 RID: 36469 RVA: 0x0030771C File Offset: 0x0030591C
		protected override NPC_TYPE NPCType
		{
			get
			{
				return NPC_TYPE.STORE_SIGMA;
			}
		}

		// Token: 0x06008E76 RID: 36470 RVA: 0x00307720 File Offset: 0x00305920
		public override void Init(bool bUseIdleAnimation = true)
		{
			if (base.m_dicNPCTemplet == null || base.m_dicNPCTemplet.Count == 0)
			{
				base.LoadFromLua();
			}
			this.m_bUseIdleAni = (bUseIdleAnimation && base.m_dicNPCTemplet != null && base.m_dicNPCTemplet.ContainsKey(NPC_ACTION_TYPE.IDLE));
			base.Init(true);
		}

		// Token: 0x06008E77 RID: 36471 RVA: 0x00307770 File Offset: 0x00305970
		public static void PlayVoice(NPC_TYPE npcType, NPC_ACTION_TYPE npcActionType, bool bStopCurrentSound)
		{
			NKCNPCTemplet npctemplet = NKCUINPCBase.GetNPCTemplet(npcType, npcActionType);
			if (npctemplet != null)
			{
				NKCUINPCBase.PlayVoice(npcType, npctemplet, bStopCurrentSound, false, false);
			}
		}

		// Token: 0x06008E78 RID: 36472 RVA: 0x00307793 File Offset: 0x00305993
		public override void PlayAni(eEmotion emotion)
		{
		}

		// Token: 0x06008E79 RID: 36473 RVA: 0x00307795 File Offset: 0x00305995
		public void ShowText(string text = "")
		{
		}

		// Token: 0x06008E7A RID: 36474 RVA: 0x00307797 File Offset: 0x00305997
		public override void DragEvent()
		{
		}
	}
}
