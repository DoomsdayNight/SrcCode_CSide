using System;
using NKM.Templet;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.NPC
{
	// Token: 0x02000C01 RID: 3073
	public class NKCUINPCProfessorOlivia : NKCUINPCSpine
	{
		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x06008E63 RID: 36451 RVA: 0x003074E5 File Offset: 0x003056E5
		protected override string LUA_ASSET_NAME
		{
			get
			{
				return "LUA_NPC_PROFESSOR_OLIVIA_TEMPLET";
			}
		}

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x06008E64 RID: 36452 RVA: 0x003074EC File Offset: 0x003056EC
		protected override NPC_TYPE NPCType
		{
			get
			{
				return NPC_TYPE.PROFESSOR_OLIVIA;
			}
		}

		// Token: 0x06008E65 RID: 36453 RVA: 0x003074F0 File Offset: 0x003056F0
		public override void Init(bool bUseIdleAnimation = true)
		{
			if (base.m_dicNPCTemplet == null || base.m_dicNPCTemplet.Count == 0)
			{
				base.LoadFromLua();
			}
			this.m_bUseIdleAni = (bUseIdleAnimation && base.m_dicNPCTemplet != null && base.m_dicNPCTemplet.ContainsKey(NPC_ACTION_TYPE.IDLE));
			base.Init(true);
		}

		// Token: 0x06008E66 RID: 36454 RVA: 0x00307540 File Offset: 0x00305740
		public void PlayVoice(NKM_UNIT_STYLE_TYPE unitStyleType, NKCUILab.LAB_DETAIL_STATE labState)
		{
			switch (unitStyleType)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
				switch (labState)
				{
				case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE:
					this.PlayAni(NPC_ACTION_TYPE.ENHANCE_COUNTER, false);
					return;
				case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK:
					this.PlayAni(NPC_ACTION_TYPE.LIMIT_BREAK_COUNTER, false);
					return;
				case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN:
					this.PlayAni(NPC_ACTION_TYPE.TRAINING_COUNTER, false);
					return;
				default:
					return;
				}
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
				switch (labState)
				{
				case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE:
					this.PlayAni(NPC_ACTION_TYPE.ENHANCE_SOLDIER, false);
					return;
				case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK:
					this.PlayAni(NPC_ACTION_TYPE.LIMIT_BREAK_SOLDIER, false);
					return;
				case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN:
					this.PlayAni(NPC_ACTION_TYPE.TRAINING_SOLDIER, false);
					return;
				default:
					return;
				}
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
				switch (labState)
				{
				case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE:
					this.PlayAni(NPC_ACTION_TYPE.ENHANCE_MECHANIC, false);
					return;
				case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK:
					this.PlayAni(NPC_ACTION_TYPE.LIMIT_BREAK_MECHANIC, false);
					return;
				case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN:
					this.PlayAni(NPC_ACTION_TYPE.TRAINING_MECHANIC, false);
					return;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06008E67 RID: 36455 RVA: 0x003075FC File Offset: 0x003057FC
		public static void PlayVoice(NPC_TYPE npcType, NPC_ACTION_TYPE npcActionType, bool bStopCurrentSound)
		{
			NKCNPCTemplet npctemplet = NKCUINPCBase.GetNPCTemplet(npcType, npcActionType);
			if (npctemplet != null)
			{
				NKCUINPCBase.PlayVoice(npcType, npctemplet, bStopCurrentSound, false, false);
			}
		}

		// Token: 0x06008E68 RID: 36456 RVA: 0x0030761F File Offset: 0x0030581F
		public override void PlayAni(eEmotion emotion)
		{
		}

		// Token: 0x06008E69 RID: 36457 RVA: 0x00307621 File Offset: 0x00305821
		public void ShowText(string text = "")
		{
		}

		// Token: 0x06008E6A RID: 36458 RVA: 0x00307624 File Offset: 0x00305824
		public override void DragEvent()
		{
			EventTrigger eventTrigger = base.gameObject.GetComponent<EventTrigger>();
			if (eventTrigger == null)
			{
				eventTrigger = base.gameObject.AddComponent<EventTrigger>();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.Drag;
			entry.callback.AddListener(new UnityAction<BaseEventData>(NKCSystemEvent.UI_SCEN_BG_DRAG));
			eventTrigger.triggers.Add(entry);
		}
	}
}
