using System;
using System.Text;
using Unity.Profiling;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class NKCMemoryStats : MonoBehaviour
{
	// Token: 0x060000C4 RID: 196 RVA: 0x00003C08 File Offset: 0x00001E08
	public static long TotalUsedMemory()
	{
		if (NKCMemoryStats.instance == null)
		{
			return 0L;
		}
		return NKCMemoryStats.instance.GetTotalUsedMemory();
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00003C24 File Offset: 0x00001E24
	private void Awake()
	{
		if (NKCMemoryStats.instance == null)
		{
			NKCMemoryStats.instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00003C50 File Offset: 0x00001E50
	public long GetTotalUsedMemory()
	{
		if (this.totalUsedMemoryRecorder.Valid)
		{
			return this.totalUsedMemoryRecorder.LastValue;
		}
		return 0L;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00003C70 File Offset: 0x00001E70
	private void OnEnable()
	{
		this.totalReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Reserved Memory", 1, ProfilerRecorderOptions.Default);
		this.gcReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory", 1, ProfilerRecorderOptions.Default);
		this.systemUsedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory", 1, ProfilerRecorderOptions.Default);
		this.totalUsedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Used Memory", 1, ProfilerRecorderOptions.Default);
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00003CDD File Offset: 0x00001EDD
	private void OnDisable()
	{
		this.totalReservedMemoryRecorder.Dispose();
		this.gcReservedMemoryRecorder.Dispose();
		this.systemUsedMemoryRecorder.Dispose();
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00003D00 File Offset: 0x00001F00
	private void Update()
	{
		StringBuilder stringBuilder = new StringBuilder(500);
		if (this.totalReservedMemoryRecorder.Valid)
		{
			stringBuilder.Append("Total Reserved Memory: ");
			stringBuilder.AppendLine(string.Format("{0:#,0}", this.totalReservedMemoryRecorder.LastValue));
		}
		if (this.gcReservedMemoryRecorder.Valid)
		{
			stringBuilder.Append("GC Reserved Memory: ");
			stringBuilder.AppendLine(string.Format("{0:#,0}", this.gcReservedMemoryRecorder.LastValue));
		}
		if (this.systemUsedMemoryRecorder.Valid)
		{
			stringBuilder.Append("System Used Memory: ");
			stringBuilder.AppendLine(string.Format("{0:#,0}", this.systemUsedMemoryRecorder.LastValue));
		}
		if (this.totalUsedMemoryRecorder.Valid)
		{
			stringBuilder.Append("Total Used Memory: ");
			stringBuilder.AppendLine(string.Format("{0:#,0}", this.totalUsedMemoryRecorder.LastValue));
		}
		this.statsText = stringBuilder.ToString();
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00003E0C File Offset: 0x0000200C
	private void OnGUI()
	{
		int num = GUI.skin.textArea.fontSize;
		GUI.skin.textArea.fontSize = this.fontSize;
		GUI.TextArea(this.rect, this.statsText);
		GUI.skin.textArea.fontSize = num;
	}

	// Token: 0x04000053 RID: 83
	public int fontSize = 50;

	// Token: 0x04000054 RID: 84
	public Rect rect = new Rect(10f, 10f, 1000f, 250f);

	// Token: 0x04000055 RID: 85
	private static NKCMemoryStats instance;

	// Token: 0x04000056 RID: 86
	private string statsText;

	// Token: 0x04000057 RID: 87
	private ProfilerRecorder totalReservedMemoryRecorder;

	// Token: 0x04000058 RID: 88
	private ProfilerRecorder gcReservedMemoryRecorder;

	// Token: 0x04000059 RID: 89
	private ProfilerRecorder systemUsedMemoryRecorder;

	// Token: 0x0400005A RID: 90
	private ProfilerRecorder totalUsedMemoryRecorder;
}
