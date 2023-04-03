using System;
using System.Collections.Generic;
using NKM;
using NKM.Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200079A RID: 1946
	public class NKCUILobbyMenuEvent : MonoBehaviour
	{
		// Token: 0x06004C5C RID: 19548 RVA: 0x0016DCE4 File Offset: 0x0016BEE4
		public void SetData(NKMEventTabTemplet eventTabTemplet, NKCUILobbyMenuEvent.OnButton onButton)
		{
			if (eventTabTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_btnEvent.PointerClick.RemoveAllListeners();
			this.m_btnEvent.PointerClick.AddListener(new UnityAction(this.OnClick));
			this.m_dOnButton = onButton;
			this.m_NKMEventTabTemplet = eventTabTemplet;
			this.SetTypeObject(eventTabTemplet);
			NKCUtil.SetLabelText(this.m_lbEventTitle, eventTabTemplet.GetTitle());
			NKCUtil.SetImageSprite(this.m_imgBG, NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("ab_ui_nkm_ui_event_texture", eventTabTemplet.m_EventTabImage)), false);
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x0016DD74 File Offset: 0x0016BF74
		private void SetTypeObject(NKMEventTabTemplet tabTemplet)
		{
			foreach (NKCUILobbyMenuEvent.EventTypeObject eventTypeObject in this.m_lstTypeObject)
			{
				if (string.Equals(eventTypeObject.type, tabTemplet.m_LobbyBannerType, StringComparison.InvariantCultureIgnoreCase))
				{
					string @string = NKCStringTable.GetString(tabTemplet.m_LobbyBannerText, false);
					if (string.IsNullOrEmpty(@string))
					{
						NKCUtil.SetGameobjectActive(eventTypeObject.obj, false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(eventTypeObject.obj, true);
						NKCUtil.SetLabelText(eventTypeObject.text, @string);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(eventTypeObject.obj, false);
				}
			}
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x0016DE20 File Offset: 0x0016C020
		private void OnClick()
		{
			NKCUILobbyMenuEvent.OnButton dOnButton = this.m_dOnButton;
			if (dOnButton == null)
			{
				return;
			}
			dOnButton(this.m_NKMEventTabTemplet);
		}

		// Token: 0x04003C18 RID: 15384
		public NKCUIComStateButton m_btnEvent;

		// Token: 0x04003C19 RID: 15385
		public Image m_imgBG;

		// Token: 0x04003C1A RID: 15386
		public Text m_lbEventTitle;

		// Token: 0x04003C1B RID: 15387
		public List<NKCUILobbyMenuEvent.EventTypeObject> m_lstTypeObject;

		// Token: 0x04003C1C RID: 15388
		private NKCUILobbyMenuEvent.OnButton m_dOnButton;

		// Token: 0x04003C1D RID: 15389
		private NKMEventTabTemplet m_NKMEventTabTemplet;

		// Token: 0x0200145C RID: 5212
		[Serializable]
		public struct EventTypeObject
		{
			// Token: 0x04009E26 RID: 40486
			public string type;

			// Token: 0x04009E27 RID: 40487
			public GameObject obj;

			// Token: 0x04009E28 RID: 40488
			public Text text;
		}

		// Token: 0x0200145D RID: 5213
		// (Invoke) Token: 0x0600A87E RID: 43134
		public delegate void OnButton(NKMEventTabTemplet tabTemplet);
	}
}
