using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.NPC
{
	// Token: 0x02000BFE RID: 3070
	public class NKCUINPCManagerKimHaNa : NKCUINPCSpine
	{
		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x06008E47 RID: 36423 RVA: 0x0030723A File Offset: 0x0030543A
		protected override string LUA_ASSET_NAME
		{
			get
			{
				return "LUA_NPC_MANAGER_KIMHANA_TEMPLET";
			}
		}

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x06008E48 RID: 36424 RVA: 0x00307241 File Offset: 0x00305441
		protected override NPC_TYPE NPCType
		{
			get
			{
				return NPC_TYPE.MANAGER_KIMHANA;
			}
		}

		// Token: 0x06008E49 RID: 36425 RVA: 0x00307244 File Offset: 0x00305444
		public override void Init(bool bUseIdleAnimation = true)
		{
			if (base.m_dicNPCTemplet == null || base.m_dicNPCTemplet.Count == 0)
			{
				base.LoadFromLua();
			}
			this.m_bUseIdleAni = (bUseIdleAnimation && base.m_dicNPCTemplet != null && base.m_dicNPCTemplet.ContainsKey(NPC_ACTION_TYPE.IDLE));
			base.Init(true);
		}

		// Token: 0x06008E4A RID: 36426 RVA: 0x00307294 File Offset: 0x00305494
		public static void PlayVoice(NPC_TYPE npcType, List<NPC_ACTION_TYPE> lstNpcActionType, bool bStopCurrentSound)
		{
			NKCNPCTemplet npctemplet = NKCUINPCBase.GetNPCTemplet(npcType, lstNpcActionType);
			if (npctemplet != null)
			{
				NKCUINPCBase.PlayVoice(npcType, npctemplet, bStopCurrentSound, false, false);
			}
		}

		// Token: 0x06008E4B RID: 36427 RVA: 0x003072B8 File Offset: 0x003054B8
		public static void PlayVoice(NPC_TYPE npcType, NPC_ACTION_TYPE npcActionType, bool bStopCurrentSound)
		{
			NKCNPCTemplet npctemplet = NKCUINPCBase.GetNPCTemplet(npcType, npcActionType);
			if (npctemplet != null)
			{
				NKCUINPCBase.PlayVoice(npcType, npctemplet, bStopCurrentSound, false, false);
			}
		}

		// Token: 0x06008E4C RID: 36428 RVA: 0x003072DB File Offset: 0x003054DB
		public override void PlayAni(eEmotion emotion)
		{
		}

		// Token: 0x06008E4D RID: 36429 RVA: 0x003072E0 File Offset: 0x003054E0
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

		// Token: 0x06008E4E RID: 36430 RVA: 0x0030733E File Offset: 0x0030553E
		public void ShowText(string text = "")
		{
		}
	}
}
