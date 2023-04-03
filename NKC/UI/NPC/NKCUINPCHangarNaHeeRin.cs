using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.NPC
{
	// Token: 0x02000BFC RID: 3068
	public class NKCUINPCHangarNaHeeRin : NKCUINPCSpine
	{
		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x06008E37 RID: 36407 RVA: 0x0030705A File Offset: 0x0030525A
		protected override string LUA_ASSET_NAME
		{
			get
			{
				return "LUA_NPC_HANGAR_NAHEERIN_TEMPLET";
			}
		}

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x06008E38 RID: 36408 RVA: 0x00307061 File Offset: 0x00305261
		protected override NPC_TYPE NPCType
		{
			get
			{
				return NPC_TYPE.HANGAR_NAHEERIN;
			}
		}

		// Token: 0x06008E39 RID: 36409 RVA: 0x00307064 File Offset: 0x00305264
		public override void Init(bool bUseIdleAnimation = true)
		{
			if (base.m_dicNPCTemplet == null || base.m_dicNPCTemplet.Count == 0)
			{
				base.LoadFromLua();
			}
			this.m_bUseIdleAni = (bUseIdleAnimation && base.m_dicNPCTemplet != null && base.m_dicNPCTemplet.ContainsKey(NPC_ACTION_TYPE.IDLE));
			base.Init(true);
		}

		// Token: 0x06008E3A RID: 36410 RVA: 0x003070B4 File Offset: 0x003052B4
		public static void PlayVoice(NPC_TYPE npcType, NPC_ACTION_TYPE npcActionType, bool bStopCurrentSound = true)
		{
			NKCNPCTemplet npctemplet = NKCUINPCBase.GetNPCTemplet(npcType, npcActionType);
			if (npctemplet != null)
			{
				NKCUINPCBase.PlayVoice(npcType, npctemplet, bStopCurrentSound, false, false);
			}
		}

		// Token: 0x06008E3B RID: 36411 RVA: 0x003070D7 File Offset: 0x003052D7
		public override void PlayAni(eEmotion emotion)
		{
			base.PlayAni(emotion);
		}

		// Token: 0x06008E3C RID: 36412 RVA: 0x003070E0 File Offset: 0x003052E0
		public void ShowText(string text = "")
		{
		}

		// Token: 0x06008E3D RID: 36413 RVA: 0x003070E4 File Offset: 0x003052E4
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
