using System;
using System.ComponentModel;
using NKC;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200000E RID: 14
public class PatchToolView : MonoBehaviour
{
	// Token: 0x06000087 RID: 135 RVA: 0x000030F4 File Offset: 0x000012F4
	private void Awake()
	{
		this._vm.PropertyChanged += this.OnChangedProperty;
		this._vm.Init();
		this.SetEvent(this.RunButton, delegate
		{
			this._vm.RefreshServerAddress(this.ServerInputField.text, this.ProtocolInputField.text);
			PatchToolVM vm = this._vm;
			base.StartCoroutine((vm != null) ? vm.Run() : null);
		});
		this.SetEvent(this.RunExtraButton, delegate
		{
			this._vm.RefreshServerAddress(this.ServerInputField.text, this.ProtocolInputField.text);
			PatchToolVM vm = this._vm;
			base.StartCoroutine((vm != null) ? vm.RunExtraAsset() : null);
		});
		this.SetEvent(this.CleanUpButton, delegate
		{
			PatchToolVM vm = this._vm;
			if (vm == null)
			{
				return;
			}
			vm.CleanUp();
		});
		this.SetEvent(this.SaveLogButton, delegate
		{
			PatchToolVM vm = this._vm;
			if (vm == null)
			{
				return;
			}
			vm.SaveLog();
		});
		this.SetEvent(this.SaveTagButton, delegate
		{
			PatchToolVM vm = this._vm;
			if (vm == null)
			{
				return;
			}
			vm.SaveOpenTag();
		});
	}

	// Token: 0x06000088 RID: 136 RVA: 0x0000319B File Offset: 0x0000139B
	private void Update()
	{
		PatchToolVM vm = this._vm;
		if (vm != null)
		{
			vm.Update();
		}
		this.SetLogScrollViewSize();
	}

	// Token: 0x06000089 RID: 137 RVA: 0x000031B4 File Offset: 0x000013B4
	public void SetLogScrollViewSize()
	{
		int lineCount = this.LogText.cachedTextGenerator.lineCount;
		int num = (this.LogText.fontSize + 2) * lineCount;
		this.LogText.rectTransform.SetHeight((float)num);
	}

	// Token: 0x0600008A RID: 138 RVA: 0x000031F4 File Offset: 0x000013F4
	public void OnChangedProperty(object sender, PropertyChangedEventArgs e)
	{
		string propertyName = e.PropertyName;
		if (propertyName != null)
		{
			if (propertyName == "ConfigServerAddress")
			{
				this.ServerInputField.text = this._vm.ConfigServerAddress;
				return;
			}
			if (propertyName == "ProtocolVersion")
			{
				this.ProtocolInputField.text = this._vm.ProtocolVersion;
				return;
			}
			if (propertyName == "Status")
			{
				this.StatusText.text = this._vm.Status;
				return;
			}
			if (propertyName == "Log")
			{
				this.LogText.text = this._vm.Log;
				return;
			}
			if (!(propertyName == "Solution"))
			{
				return;
			}
			this.SolutionText.text = this._vm.Solution;
		}
	}

	// Token: 0x0600008B RID: 139 RVA: 0x000032C4 File Offset: 0x000014C4
	private void SetEvent(Button button, UnityAction action)
	{
		if (button == null)
		{
			Debug.Log("Not Found Button");
			return;
		}
		Button.ButtonClickedEvent buttonClickedEvent = new Button.ButtonClickedEvent();
		buttonClickedEvent.AddListener(action);
		button.onClick = buttonClickedEvent;
	}

	// Token: 0x0400002E RID: 46
	private readonly PatchToolVM _vm = new PatchToolVM();

	// Token: 0x0400002F RID: 47
	public Button RunButton;

	// Token: 0x04000030 RID: 48
	public Button RunExtraButton;

	// Token: 0x04000031 RID: 49
	public Button CleanUpButton;

	// Token: 0x04000032 RID: 50
	public Button SaveLogButton;

	// Token: 0x04000033 RID: 51
	public Button SaveTagButton;

	// Token: 0x04000034 RID: 52
	public Text StatusText;

	// Token: 0x04000035 RID: 53
	public Text LogText;

	// Token: 0x04000036 RID: 54
	public Text SolutionText;

	// Token: 0x04000037 RID: 55
	public InputField ServerInputField;

	// Token: 0x04000038 RID: 56
	public InputField ProtocolInputField;
}
