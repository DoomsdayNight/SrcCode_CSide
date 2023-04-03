using System;
using NKC.Templet;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A58 RID: 2648
	public class NKCPopupFilterThemeSlot : MonoBehaviour
	{
		// Token: 0x06007431 RID: 29745 RVA: 0x0026A69B File Offset: 0x0026889B
		public void Init(NKCUIComToggleGroup toggleGroup)
		{
			if (this.m_Toggle != null)
			{
				this.m_Toggle.SetToggleGroup(toggleGroup);
			}
			NKCUtil.SetToggleValueChangedDelegate(this.m_Toggle, new UnityAction<bool>(this.OnToggle));
		}

		// Token: 0x06007432 RID: 29746 RVA: 0x0026A6D0 File Offset: 0x002688D0
		public void SetData(NKCThemeGroupTemplet templet, NKCPopupFilterThemeSlot.OnClick onClick)
		{
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("ab_inven_icon_fnc_theme", templet.GroupIconName));
			NKCUtil.SetImageSprite(this.m_ImgIcon, orLoadAssetResource, false);
			NKCUtil.SetLabelText(this.m_lbName, NKCStringTable.GetString(templet.GroupStringKey, false));
			this.m_ID = templet.Key;
			this.dOnClick = onClick;
		}

		// Token: 0x06007433 RID: 29747 RVA: 0x0026A72A File Offset: 0x0026892A
		public void SetSelected(bool value)
		{
			this.m_Toggle.Select(value, true, false);
		}

		// Token: 0x06007434 RID: 29748 RVA: 0x0026A73B File Offset: 0x0026893B
		private void OnToggle(bool value)
		{
			if (value)
			{
				NKCPopupFilterThemeSlot.OnClick onClick = this.dOnClick;
				if (onClick == null)
				{
					return;
				}
				onClick(this.m_ID);
			}
		}

		// Token: 0x04006096 RID: 24726
		public Image m_ImgIcon;

		// Token: 0x04006097 RID: 24727
		public Text m_lbName;

		// Token: 0x04006098 RID: 24728
		public NKCUIComToggle m_Toggle;

		// Token: 0x04006099 RID: 24729
		private NKCPopupFilterThemeSlot.OnClick dOnClick;

		// Token: 0x0400609A RID: 24730
		private int m_ID;

		// Token: 0x020017AD RID: 6061
		// (Invoke) Token: 0x0600B3F5 RID: 46069
		public delegate void OnClick(int id);
	}
}
