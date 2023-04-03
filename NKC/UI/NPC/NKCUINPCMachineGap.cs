using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.NPC
{
	// Token: 0x02000BFD RID: 3069
	public class NKCUINPCMachineGap : NKCUINPCSpine
	{
		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06008E3F RID: 36415 RVA: 0x0030714A File Offset: 0x0030534A
		protected override string LUA_ASSET_NAME
		{
			get
			{
				return "LUA_NPC_MACHINE_GAP_TEMPLET";
			}
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06008E40 RID: 36416 RVA: 0x00307151 File Offset: 0x00305351
		protected override NPC_TYPE NPCType
		{
			get
			{
				return NPC_TYPE.MACHINE_GAP;
			}
		}

		// Token: 0x06008E41 RID: 36417 RVA: 0x00307154 File Offset: 0x00305354
		public override void Init(bool bUseIdleAnimation = true)
		{
			if (base.m_dicNPCTemplet == null || base.m_dicNPCTemplet.Count == 0)
			{
				base.LoadFromLua();
			}
			this.m_bUseIdleAni = (bUseIdleAnimation && base.m_dicNPCTemplet != null && base.m_dicNPCTemplet.ContainsKey(NPC_ACTION_TYPE.IDLE));
			base.Init(true);
		}

		// Token: 0x06008E42 RID: 36418 RVA: 0x003071A4 File Offset: 0x003053A4
		public static void PlayVoice(NPC_TYPE npcType, NPC_ACTION_TYPE npcActionType, bool bStopCurrentSound = true)
		{
			NKCNPCTemplet npctemplet = NKCUINPCBase.GetNPCTemplet(npcType, npcActionType);
			if (npctemplet != null)
			{
				NKCUINPCBase.PlayVoice(npcType, npctemplet, bStopCurrentSound, false, false);
			}
		}

		// Token: 0x06008E43 RID: 36419 RVA: 0x003071C7 File Offset: 0x003053C7
		public override void PlayAni(eEmotion emotion)
		{
			base.PlayAni(emotion);
		}

		// Token: 0x06008E44 RID: 36420 RVA: 0x003071D0 File Offset: 0x003053D0
		public void ShowText(string text = "")
		{
		}

		// Token: 0x06008E45 RID: 36421 RVA: 0x003071D4 File Offset: 0x003053D4
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
