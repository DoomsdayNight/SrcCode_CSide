using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006A7 RID: 1703
	public class NKCMessage
	{
		// Token: 0x06003824 RID: 14372 RVA: 0x00122361 File Offset: 0x00120561
		public static void Init()
		{
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x00122364 File Offset: 0x00120564
		public static void Update()
		{
			for (LinkedListNode<NKCMessageData> linkedListNode = NKCMessage.m_linklistNKMMessageData.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				NKCMessageData value = linkedListNode.Value;
				if (value != null)
				{
					if (value.m_fLatency <= 0f)
					{
						NKCScenManager.GetScenManager().MsgProc(value);
						LinkedListNode<NKCMessageData> next = linkedListNode.Next;
						NKCMessage.m_linklistNKMMessageData.Remove(linkedListNode);
						linkedListNode = next;
						continue;
					}
					value.m_fLatency -= Time.deltaTime;
				}
			}
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x001223D4 File Offset: 0x001205D4
		public static void SendMessage(NKC_EVENT_MESSAGE eNKC_EVENT_MESSAGE, int msgID2 = 0, object param1 = null, object param2 = null, object param3 = null, bool bDirect = false, float fLatency = 0f)
		{
			NKCMessageData nkcmessageData = new NKCMessageData();
			nkcmessageData.m_NKC_EVENT_MESSAGE = eNKC_EVENT_MESSAGE;
			nkcmessageData.m_MsgID2 = msgID2;
			nkcmessageData.m_Param1 = param1;
			nkcmessageData.m_Param2 = param2;
			nkcmessageData.m_Param3 = param3;
			nkcmessageData.m_fLatency = fLatency;
			if (!bDirect)
			{
				NKCMessage.m_linklistNKMMessageData.AddLast(nkcmessageData);
				return;
			}
			NKCScenManager.GetScenManager().MsgProc(nkcmessageData);
		}

		// Token: 0x040034A8 RID: 13480
		private static LinkedList<NKCMessageData> m_linklistNKMMessageData = new LinkedList<NKCMessageData>();
	}
}
