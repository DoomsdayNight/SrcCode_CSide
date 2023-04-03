using System;
using NKM;
using NKM.Event;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.Event
{
	// Token: 0x02000BE5 RID: 3045
	public class NKCUIEventSubUISimple : NKCUIEventSubUIBase
	{
		// Token: 0x06008D49 RID: 36169 RVA: 0x00300F90 File Offset: 0x002FF190
		public override void Init()
		{
			base.Init();
			EventTrigger component = base.GetComponent<EventTrigger>();
			if (component != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnTouch));
				component.triggers.Clear();
				component.triggers.Add(entry);
			}
		}

		// Token: 0x06008D4A RID: 36170 RVA: 0x00300FEE File Offset: 0x002FF1EE
		public override void Open(NKMEventTabTemplet tabTemplet)
		{
			this.m_tabTemplet = tabTemplet;
			if (this.m_tabTemplet != null)
			{
				this.m_ShortcutType = this.m_tabTemplet.m_ShortCutType;
				this.m_ShortcutParam = this.m_tabTemplet.m_ShortCut;
				base.SetDateLimit();
			}
		}

		// Token: 0x06008D4B RID: 36171 RVA: 0x00301027 File Offset: 0x002FF227
		public override void Refresh()
		{
		}

		// Token: 0x06008D4C RID: 36172 RVA: 0x00301029 File Offset: 0x002FF229
		private void OnTouch(BaseEventData baseEventData)
		{
			if (this.m_ShortcutType == NKM_SHORTCUT_TYPE.SHORTCUT_NONE)
			{
				return;
			}
			if (!base.CheckEventTime(true))
			{
				return;
			}
			NKCContentManager.MoveToShortCut(this.m_ShortcutType, this.m_ShortcutParam, false);
		}

		// Token: 0x04007A24 RID: 31268
		public NKM_SHORTCUT_TYPE m_ShortcutType;

		// Token: 0x04007A25 RID: 31269
		public string m_ShortcutParam;
	}
}
