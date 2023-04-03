using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.NPC
{
	// Token: 0x02000BFB RID: 3067
	public class NKCUINPCFactoryAnastasia : NKCUINPCSpine
	{
		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x06008E2F RID: 36399 RVA: 0x00306F6F File Offset: 0x0030516F
		protected override string LUA_ASSET_NAME
		{
			get
			{
				return "LUA_NPC_FACTORY_ANASTASIA_TEMPLET";
			}
		}

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x06008E30 RID: 36400 RVA: 0x00306F76 File Offset: 0x00305176
		protected override NPC_TYPE NPCType
		{
			get
			{
				return NPC_TYPE.FACTORY_ANASTASIA;
			}
		}

		// Token: 0x06008E31 RID: 36401 RVA: 0x00306F7C File Offset: 0x0030517C
		public override void Init(bool bUseIdleAnimation = true)
		{
			if (base.m_dicNPCTemplet == null || base.m_dicNPCTemplet.Count == 0)
			{
				base.LoadFromLua();
			}
			this.m_bUseIdleAni = (bUseIdleAnimation && base.m_dicNPCTemplet != null && base.m_dicNPCTemplet.ContainsKey(NPC_ACTION_TYPE.IDLE));
			base.Init(true);
		}

		// Token: 0x06008E32 RID: 36402 RVA: 0x00306FCC File Offset: 0x003051CC
		public static void PlayVoice(NPC_TYPE npcType, NPC_ACTION_TYPE npcActionType, bool bStopCurrentSound)
		{
			NKCNPCTemplet npctemplet = NKCUINPCBase.GetNPCTemplet(npcType, npcActionType);
			if (npctemplet != null)
			{
				NKCUINPCBase.PlayVoice(npcType, npctemplet, bStopCurrentSound, false, false);
			}
		}

		// Token: 0x06008E33 RID: 36403 RVA: 0x00306FEF File Offset: 0x003051EF
		public override void PlayAni(eEmotion emotion)
		{
		}

		// Token: 0x06008E34 RID: 36404 RVA: 0x00306FF1 File Offset: 0x003051F1
		public void ShowText(string text = "")
		{
		}

		// Token: 0x06008E35 RID: 36405 RVA: 0x00306FF4 File Offset: 0x003051F4
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
