using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.NPC
{
	// Token: 0x02000BF4 RID: 3060
	public class NKCUINPCAssistantLeeYoonJung : NKCUINPCSpine
	{
		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x06008E0C RID: 36364 RVA: 0x0030675C File Offset: 0x0030495C
		protected override string LUA_ASSET_NAME
		{
			get
			{
				return "LUA_NPC_ASSISTANT_LEE_TEMPLET";
			}
		}

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x06008E0D RID: 36365 RVA: 0x00306763 File Offset: 0x00304963
		protected override NPC_TYPE NPCType
		{
			get
			{
				return NPC_TYPE.ASSISTANT_LEEYOONJUNG;
			}
		}

		// Token: 0x06008E0E RID: 36366 RVA: 0x00306768 File Offset: 0x00304968
		public override void Init(bool bUseIdleAnimation = true)
		{
			if (base.m_dicNPCTemplet == null || base.m_dicNPCTemplet.Count == 0)
			{
				base.LoadFromLua();
			}
			this.m_bUseIdleAni = (bUseIdleAnimation && base.m_dicNPCTemplet != null && base.m_dicNPCTemplet.ContainsKey(NPC_ACTION_TYPE.IDLE));
			base.Init(true);
		}

		// Token: 0x06008E0F RID: 36367 RVA: 0x003067B8 File Offset: 0x003049B8
		public static void PlayVoice(NPC_TYPE npcType, NPC_ACTION_TYPE npcActionType, bool bStopCurrentSound)
		{
			NKCNPCTemplet npctemplet = NKCUINPCBase.GetNPCTemplet(npcType, npcActionType);
			if (npctemplet != null)
			{
				NKCUINPCBase.PlayVoice(npcType, npctemplet, bStopCurrentSound, false, false);
			}
		}

		// Token: 0x06008E10 RID: 36368 RVA: 0x003067DB File Offset: 0x003049DB
		public override void PlayAni(eEmotion emotion)
		{
		}

		// Token: 0x06008E11 RID: 36369 RVA: 0x003067DD File Offset: 0x003049DD
		public void ShowText(string text = "")
		{
		}

		// Token: 0x06008E12 RID: 36370 RVA: 0x003067E0 File Offset: 0x003049E0
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
