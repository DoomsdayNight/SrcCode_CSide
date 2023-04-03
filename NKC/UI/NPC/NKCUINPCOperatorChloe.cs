using System;
using NKM;
using NKM.Templet;

namespace NKC.UI.NPC
{
	// Token: 0x02000BFF RID: 3071
	public class NKCUINPCOperatorChloe : NKCUINPCSpine
	{
		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x06008E50 RID: 36432 RVA: 0x00307348 File Offset: 0x00305548
		private static string VOICE_ASSET_BUNDLE_NAME
		{
			get
			{
				return "AB_NPC_VOICE_OPERATOR_CHLOE";
			}
		}

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x06008E51 RID: 36433 RVA: 0x0030734F File Offset: 0x0030554F
		protected override string LUA_ASSET_NAME
		{
			get
			{
				return "LUA_NPC_OPERATOR_CHLOE_TEMPLET";
			}
		}

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06008E52 RID: 36434 RVA: 0x00307356 File Offset: 0x00305556
		protected override NPC_TYPE NPCType
		{
			get
			{
				return NPC_TYPE.OPERATOR_CHLOE;
			}
		}

		// Token: 0x06008E53 RID: 36435 RVA: 0x0030735C File Offset: 0x0030555C
		public override void Init(bool bUseIdleAnimation = true)
		{
			if (base.m_dicNPCTemplet == null || base.m_dicNPCTemplet.Count == 0)
			{
				base.LoadFromLua();
			}
			this.m_bUseIdleAni = (bUseIdleAnimation && base.m_dicNPCTemplet != null && base.m_dicNPCTemplet.ContainsKey(NPC_ACTION_TYPE.IDLE));
			base.Init(true);
		}

		// Token: 0x06008E54 RID: 36436 RVA: 0x003073AC File Offset: 0x003055AC
		public static void PlayVoice(NPC_TYPE npcType, NPC_ACTION_TYPE npcActionType, bool bStopCurrentSound)
		{
			NKCNPCTemplet npctemplet = NKCUINPCBase.GetNPCTemplet(npcType, npcActionType);
			if (npctemplet != null)
			{
				NKCUINPCBase.PlayVoice(npcType, npctemplet, bStopCurrentSound, false, false);
			}
		}

		// Token: 0x06008E55 RID: 36437 RVA: 0x003073CF File Offset: 0x003055CF
		public override void PlayAni(eEmotion emotion)
		{
		}

		// Token: 0x06008E56 RID: 36438 RVA: 0x003073D1 File Offset: 0x003055D1
		public void ShowText(string text = "")
		{
		}

		// Token: 0x06008E57 RID: 36439 RVA: 0x003073D4 File Offset: 0x003055D4
		public static NPC_ACTION_TYPE GetNPCActionType(NKMDiveSlot diveSlot)
		{
			if (diveSlot == null)
			{
				return NPC_ACTION_TYPE.NONE;
			}
			switch (diveSlot.SectorType)
			{
			case NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_BOSS:
			case NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_BOSS_HARD:
				return NPC_ACTION_TYPE.SELECT_CORE;
			case NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_POINCARE:
			case NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_POINCARE_HARD:
				return NPC_ACTION_TYPE.SELECT_RED;
			case NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_REIMANN:
			case NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_REIMANN_HARD:
				return NPC_ACTION_TYPE.SELECT_PURPLE;
			case NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_GAUNTLET:
			case NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_GAUNTLET_HARD:
				return NPC_ACTION_TYPE.SELECT_YELLOW;
			case NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_EUCLID:
			case NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_EUCLID_HARD:
				return NPC_ACTION_TYPE.SELECT_WHITE;
			default:
				return NPC_ACTION_TYPE.NONE;
			}
		}

		// Token: 0x06008E58 RID: 36440 RVA: 0x0030742F File Offset: 0x0030562F
		public override void DragEvent()
		{
		}
	}
}
