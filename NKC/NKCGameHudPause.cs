using System;
using NKC.UI.Option;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC
{
	// Token: 0x0200062A RID: 1578
	public class NKCGameHudPause : MonoBehaviour
	{
		// Token: 0x060030B8 RID: 12472 RVA: 0x000F183C File Offset: 0x000EFA3C
		private void Start()
		{
			this.m_csbtnOption.PointerClick.RemoveAllListeners();
			this.m_csbtnOption.PointerClick.AddListener(new UnityAction(this.OnClickOption));
			this.m_csbtnContinue.PointerClick.RemoveAllListeners();
			this.m_csbtnContinue.PointerClick.AddListener(new UnityAction(this.OnClickContinue));
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.BeginDrag;
			entry.callback.AddListener(new UnityAction<BaseEventData>(this.UI_GAME_CAMERA_DRAG_BEGIN));
			this.m_etBG.triggers.Add(entry);
			entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.Drag;
			entry.callback.AddListener(new UnityAction<BaseEventData>(this.UI_GAME_CAMERA_DRAG));
			this.m_etBG.triggers.Add(entry);
			entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.EndDrag;
			entry.callback.AddListener(new UnityAction<BaseEventData>(this.UI_GAME_CAMERA_DRAG_END));
			this.m_etBG.triggers.Add(entry);
			entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerDown;
			entry.callback.AddListener(new UnityAction<BaseEventData>(this.UI_GAME_CAMERA_TOUCH_DOWN));
			this.m_etBG.triggers.Add(entry);
			entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerUp;
			entry.callback.AddListener(new UnityAction<BaseEventData>(this.UI_GAME_CAMERA_TOUCH_UP));
			this.m_etBG.triggers.Add(entry);
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000F19AC File Offset: 0x000EFBAC
		public bool IsOpen()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000F19B9 File Offset: 0x000EFBB9
		public void Open(NKCGameHudPause.dOnClickContinue _dOnClickContinue = null)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetOnClickContinue(_dOnClickContinue);
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000F19CE File Offset: 0x000EFBCE
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000F19DC File Offset: 0x000EFBDC
		private void SetOnClickContinue(NKCGameHudPause.dOnClickContinue _dOnClickContinue)
		{
			this.m_dOnClickContinue = _dOnClickContinue;
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000F19E5 File Offset: 0x000EFBE5
		private void OnClickOption()
		{
			if (NKCReplayMgr.IsPlayingReplay())
			{
				NKCUIGameOption.Instance.Open(NKC_GAME_OPTION_MENU_TYPE.REPLAY, null);
				return;
			}
			NKCUIGameOption.Instance.Open(NKC_GAME_OPTION_MENU_TYPE.DUNGEON, null);
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000F1A07 File Offset: 0x000EFC07
		public void OnClickContinue()
		{
			if (this.m_dOnClickContinue != null)
			{
				this.m_dOnClickContinue();
			}
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000F1A1C File Offset: 0x000EFC1C
		public void UI_GAME_CAMERA_DRAG_BEGIN(BaseEventData cBaseEventData)
		{
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (NKCScenManager.GetScenManager() != null && NKCScenManager.GetScenManager().GetGameClient() != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_GAME_CAMERA_DRAG_BEGIN(pointerEventData.position);
			}
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000F1A60 File Offset: 0x000EFC60
		public void UI_GAME_CAMERA_DRAG(BaseEventData cBaseEventData)
		{
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (NKCScenManager.GetScenManager() != null && NKCScenManager.GetScenManager().GetGameClient() != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_GAME_CAMERA_DRAG(pointerEventData.delta, pointerEventData.position);
			}
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000F1AA8 File Offset: 0x000EFCA8
		public void UI_GAME_CAMERA_DRAG_END(BaseEventData cBaseEventData)
		{
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (NKCScenManager.GetScenManager() != null && NKCScenManager.GetScenManager().GetGameClient() != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_GAME_CAMERA_DRAG_END(pointerEventData.delta, pointerEventData.position);
			}
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000F1AF0 File Offset: 0x000EFCF0
		public void UI_GAME_CAMERA_TOUCH_DOWN(BaseEventData cBaseEventData)
		{
			if (NKCScenManager.GetScenManager() != null && NKCScenManager.GetScenManager().GetGameClient() != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_GAME_CAMERA_TOUCH_DOWN();
			}
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000F1B1A File Offset: 0x000EFD1A
		public void UI_GAME_CAMERA_TOUCH_UP(BaseEventData cBaseEventData)
		{
			if (NKCScenManager.GetScenManager() != null && NKCScenManager.GetScenManager().GetGameClient() != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_GAME_CAMERA_TOUCH_UP();
			}
		}

		// Token: 0x04003034 RID: 12340
		public EventTrigger m_etBG;

		// Token: 0x04003035 RID: 12341
		public NKCUIComStateButton m_csbtnOption;

		// Token: 0x04003036 RID: 12342
		public NKCUIComStateButton m_csbtnContinue;

		// Token: 0x04003037 RID: 12343
		private NKCGameHudPause.dOnClickContinue m_dOnClickContinue;

		// Token: 0x020012E1 RID: 4833
		// (Invoke) Token: 0x0600A4A1 RID: 42145
		public delegate void dOnClickContinue();
	}
}
