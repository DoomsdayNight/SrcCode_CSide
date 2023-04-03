using System;
using NKC.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007FE RID: 2046
	public class NKCUIPopupVoiceSlot : MonoBehaviour
	{
		// Token: 0x06005108 RID: 20744 RVA: 0x001894D8 File Offset: 0x001876D8
		public static NKCUIPopupVoiceSlot newInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_VOICE_LIST_SLOT", false, null);
			if (nkcassetInstanceData.m_Instant == null)
			{
				Debug.LogError("NKCUIPopupVoiceSlot Prefab null!");
				return null;
			}
			NKCUIPopupVoiceSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIPopupVoiceSlot>();
			component.m_instance = nkcassetInstanceData;
			component.transform.SetParent(parent);
			component.transform.localPosition = Vector3.zero;
			component.transform.localScale = Vector3.one;
			component.Init();
			return component;
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x00189555 File Offset: 0x00187755
		private void Init()
		{
			this.m_toggle.OnValueChanged.RemoveAllListeners();
			this.m_toggle.OnValueChanged.AddListener(new UnityAction<bool>(this.ChangeToggle));
			this.m_toggle.Select(false, true, false);
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x00189594 File Offset: 0x00187794
		public void SetUI(int index, NKCCollectionVoiceTemplet templet, bool bSkin, NKCUIPopupVoiceSlot.OnChangeToggle onChangeToggle)
		{
			this.m_index = index;
			NKCUtil.SetLabelText(this.m_txtType_On, NKCUtilString.GetStringVoiceCategory(templet.m_bVoiceCondLifetime));
			NKCUtil.SetLabelText(this.m_txtType_Off, NKCUtilString.GetStringVoiceCategory(templet.m_bVoiceCondLifetime));
			NKCUtil.SetLabelText(this.m_txtName_On, templet.ButtonName);
			NKCUtil.SetLabelText(this.m_txtName_Off, templet.ButtonName);
			NKCUtil.SetGameobjectActive(this.m_objSkin_On, bSkin);
			NKCUtil.SetGameobjectActive(this.m_objSkin_Off, bSkin);
			this.dOnChangeToggle = onChangeToggle;
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x00189616 File Offset: 0x00187816
		public void Clear()
		{
			if (this.m_instance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_instance);
			}
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x0018962B File Offset: 0x0018782B
		public void SetToggle(int curIndex)
		{
			this.m_toggle.Select(curIndex == this.m_index, true, false);
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x00189644 File Offset: 0x00187844
		private void ChangeToggle(bool bSet)
		{
			NKCUIPopupVoiceSlot.OnChangeToggle onChangeToggle = this.dOnChangeToggle;
			if (onChangeToggle == null)
			{
				return;
			}
			onChangeToggle(bSet, this.m_index);
		}

		// Token: 0x04004170 RID: 16752
		public NKCUIComToggle m_toggle;

		// Token: 0x04004171 RID: 16753
		[Header("ON")]
		public Text m_txtType_On;

		// Token: 0x04004172 RID: 16754
		public Text m_txtName_On;

		// Token: 0x04004173 RID: 16755
		public GameObject m_objSkin_On;

		// Token: 0x04004174 RID: 16756
		[Header("OFF")]
		public Text m_txtType_Off;

		// Token: 0x04004175 RID: 16757
		public Text m_txtName_Off;

		// Token: 0x04004176 RID: 16758
		public GameObject m_objSkin_Off;

		// Token: 0x04004177 RID: 16759
		private NKCAssetInstanceData m_instance;

		// Token: 0x04004178 RID: 16760
		private int m_index;

		// Token: 0x04004179 RID: 16761
		private NKCUIPopupVoiceSlot.OnChangeToggle dOnChangeToggle;

		// Token: 0x020014BC RID: 5308
		// (Invoke) Token: 0x0600A9C3 RID: 43459
		public delegate void OnChangeToggle(bool bValue, int index);
	}
}
