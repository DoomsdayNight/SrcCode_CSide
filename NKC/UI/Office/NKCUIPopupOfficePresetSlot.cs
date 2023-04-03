using System;
using ClientPacket.Office;
using NKC.Office;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AFE RID: 2814
	public class NKCUIPopupOfficePresetSlot : MonoBehaviour
	{
		// Token: 0x06007F46 RID: 32582 RVA: 0x002AAF34 File Offset: 0x002A9134
		public void Init()
		{
			if (this.m_ifPresetName != null)
			{
				this.m_ifPresetName.onEndEdit.RemoveAllListeners();
				this.m_ifPresetName.onEndEdit.AddListener(new UnityAction<string>(this.OnTextInputDoneEdit));
				this.m_ifPresetName.onValueChanged.RemoveAllListeners();
				this.m_ifPresetName.onValueChanged.AddListener(new UnityAction<string>(this.OnInputNameChanged));
				this.m_ifPresetName.onValidateInput = new InputField.OnValidateInput(NKCFilterManager.FilterEmojiInput);
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnRename, new UnityAction(this.OnBtnRename));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClear, new UnityAction(this.OnBtnClear));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnLoad, new UnityAction(this.OnBtnLoad));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSave, new UnityAction(this.OnBtnSave));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnAdd, new UnityAction(this.OnBtnAdd));
			NKCUtil.SetGameobjectActive(this.m_objUnlockFx, false);
		}

		// Token: 0x06007F47 RID: 32583 RVA: 0x002AB040 File Offset: 0x002A9240
		public void SetData(int currentRoomID, NKMOfficePreset preset, NKCUIPopupOfficePresetList.OnAction onAction)
		{
			NKCUtil.SetGameobjectActive(this.m_objNormal, true);
			NKCUtil.SetGameobjectActive(this.m_csbtnAdd, false);
			NKCUtil.SetGameobjectActive(this.m_objUnlockFx, false);
			this.m_PresetID = preset.presetId;
			if (this.m_ifPresetName != null)
			{
				this.m_ifPresetName.text = preset.name;
			}
			bool value = NKCOfficeManager.IsEmpryPreset(preset);
			if (this.m_csbtnLoad != null)
			{
				this.m_csbtnLoad.SetLock(value, false);
			}
			if (this.m_csbtnClear != null)
			{
				this.m_csbtnClear.SetLock(value, false);
			}
			if (this.m_csbtnSave != null)
			{
				bool value2 = NKCOfficeManager.IsEmpryRoom(NKCScenManager.CurrentUserData().OfficeData.GetOfficeRoom(currentRoomID));
				this.m_csbtnSave.SetLock(value2, false);
			}
			NKCUtil.SetLabelText(this.m_lbDefaultName, NKCOfficeManager.GetDefaultPresetName(this.m_PresetID));
			this.dOnAction = onAction;
		}

		// Token: 0x06007F48 RID: 32584 RVA: 0x002AB127 File Offset: 0x002A9327
		public void SetLoopScroll(LoopScrollRect sr)
		{
			if (this.m_comDragScrollInputField == null)
			{
				return;
			}
			this.m_comDragScrollInputField.ScrollRect = sr;
		}

		// Token: 0x06007F49 RID: 32585 RVA: 0x002AB144 File Offset: 0x002A9344
		public void SetPlus(NKCUIPopupOfficePresetList.OnAction onAction)
		{
			NKCUtil.SetGameobjectActive(this.m_objNormal, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnAdd, true);
			NKCUtil.SetGameobjectActive(this.m_objUnlockFx, false);
			this.dOnAction = onAction;
		}

		// Token: 0x06007F4A RID: 32586 RVA: 0x002AB171 File Offset: 0x002A9371
		public void PlayUnlockEffect()
		{
			NKCUtil.SetGameobjectActive(this.m_objUnlockFx, true);
		}

		// Token: 0x06007F4B RID: 32587 RVA: 0x002AB180 File Offset: 0x002A9380
		private void OnTextInputDoneEdit(string str)
		{
			NKMOfficePreset preset = NKCScenManager.CurrentUserData().OfficeData.GetPreset(this.m_PresetID);
			if (preset != null && preset.name != str)
			{
				NKCUIPopupOfficePresetList.OnAction onAction = this.dOnAction;
				if (onAction != null)
				{
					onAction(NKCUIPopupOfficePresetList.ActionType.Rename, this.m_PresetID, str);
				}
			}
			if (this.m_comDragScrollInputField != null)
			{
				this.m_comDragScrollInputField.ActiveInput = false;
			}
			if (this.m_ifPresetName != null)
			{
				this.m_ifPresetName.enabled = false;
			}
		}

		// Token: 0x06007F4C RID: 32588 RVA: 0x002AB204 File Offset: 0x002A9404
		private void OnInputNameChanged(string _string)
		{
			this.m_ifPresetName.text = NKCFilterManager.CheckBadChat(this.m_ifPresetName.text);
			if (this.m_ifPresetName.text.Length >= NKMCommonConst.Office.PresetConst.MaxNameLength)
			{
				this.m_ifPresetName.text = this.m_ifPresetName.text.Substring(0, NKMCommonConst.Office.PresetConst.MaxNameLength);
			}
		}

		// Token: 0x06007F4D RID: 32589 RVA: 0x002AB278 File Offset: 0x002A9478
		private void OnBtnRename()
		{
			if (!this.m_ifPresetName.isFocused)
			{
				this.m_ifPresetName.enabled = true;
				this.m_ifPresetName.Select();
				this.m_ifPresetName.ActivateInputField();
				if (this.m_comDragScrollInputField != null)
				{
					this.m_comDragScrollInputField.ActiveInput = true;
				}
			}
		}

		// Token: 0x06007F4E RID: 32590 RVA: 0x002AB2CE File Offset: 0x002A94CE
		private void OnBtnLoad()
		{
			NKCUIPopupOfficePresetList.OnAction onAction = this.dOnAction;
			if (onAction == null)
			{
				return;
			}
			onAction(NKCUIPopupOfficePresetList.ActionType.Load, this.m_PresetID, null);
		}

		// Token: 0x06007F4F RID: 32591 RVA: 0x002AB2E8 File Offset: 0x002A94E8
		private void OnBtnSave()
		{
			NKCUIPopupOfficePresetList.OnAction onAction = this.dOnAction;
			if (onAction == null)
			{
				return;
			}
			onAction(NKCUIPopupOfficePresetList.ActionType.Save, this.m_PresetID, null);
		}

		// Token: 0x06007F50 RID: 32592 RVA: 0x002AB302 File Offset: 0x002A9502
		private void OnBtnClear()
		{
			NKCUIPopupOfficePresetList.OnAction onAction = this.dOnAction;
			if (onAction == null)
			{
				return;
			}
			onAction(NKCUIPopupOfficePresetList.ActionType.Clear, this.m_PresetID, null);
		}

		// Token: 0x06007F51 RID: 32593 RVA: 0x002AB31C File Offset: 0x002A951C
		private void OnBtnAdd()
		{
			NKCUIPopupOfficePresetList.OnAction onAction = this.dOnAction;
			if (onAction == null)
			{
				return;
			}
			onAction(NKCUIPopupOfficePresetList.ActionType.Add, 1, null);
		}

		// Token: 0x04006BD0 RID: 27600
		public GameObject m_objNormal;

		// Token: 0x04006BD1 RID: 27601
		public InputField m_ifPresetName;

		// Token: 0x04006BD2 RID: 27602
		public NKCUIComDragScrollInputField m_comDragScrollInputField;

		// Token: 0x04006BD3 RID: 27603
		public NKCUIComStateButton m_csbtnRename;

		// Token: 0x04006BD4 RID: 27604
		public NKCUIComStateButton m_csbtnClear;

		// Token: 0x04006BD5 RID: 27605
		public NKCUIComStateButton m_csbtnLoad;

		// Token: 0x04006BD6 RID: 27606
		public NKCUIComStateButton m_csbtnSave;

		// Token: 0x04006BD7 RID: 27607
		public NKCUIComStateButton m_csbtnAdd;

		// Token: 0x04006BD8 RID: 27608
		public GameObject m_objUnlockFx;

		// Token: 0x04006BD9 RID: 27609
		public Text m_lbDefaultName;

		// Token: 0x04006BDA RID: 27610
		private int m_PresetID;

		// Token: 0x04006BDB RID: 27611
		private NKCUIPopupOfficePresetList.OnAction dOnAction;
	}
}
