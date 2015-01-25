using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class RandomTextPool {

	private static Queue<int> m_earlyQueue = new Queue<int>();
	private static Queue<int> m_midQueue = new Queue<int>();
	private static Queue<int> m_lateQueue = new Queue<int>();
	private static Queue<int> m_goodQueue = new Queue<int>();
	private static Queue<int> m_bestQueue = new Queue<int>();

	private static int m_triggeredCount = 0;
	private static int m_missedCount = 0;

	private static string[] m_earlyMessages = {
		"She was...",
		"I thought\nit would be easy.",
		"I wish she was still the one.",
		"She would've made\nthis day brighter.",
		"We could've picked up\nthe pieces together.",
		"Follow your heart.",
		"Please. No more tears.",
		"It can't be her.\nIt never is.",
		"I cherished everything.",
		"I wonder what she's doing.",
		"How can I find her?",
		"Where is she?",
		"It feels like a text from her.",
		"I can't stop thinking of her.",
		"How do people last through this?"
	};

	private static string[] m_midMessages = {
		"Why do I keep on\ncoming back?",
		"Enough.",
		"If only...\nI won't be here anymore.",
		"Running through my mind.\nWalking in circles.",
		"Where am I going? ...\nWhat do I do?",
		"Should I keep my heart in this...?",
		"I know I could do better.",
		"There must be an end to this.",
		"Maybe this time\nit would be different.",
		"It must be me.",
		"It's too hard to hold on.",
		"Are these dreams or memories? Nightmares?",
		"At least I could try to...",
		"You lose what you cling to.",
		"I want to get out of here.",
		"I thought holding on makes us strong but...",
		"I wonder what she's doing,\nbut I don't want to know."
	};

	private static string[] m_lateMessages = {
		"Elsa kept telling me...",
		"Should I stay\nor should I go?",
		"I'm just getting worse.",
		"Do I really need\nto do this to myself?",
		"I don't need\nto keep hurting myself.",
		"I feel like I'm going nowhere.",
		"Too much thinking.",
		"Why do I even bother?",
		"The hardest part is learning to start over.",
		"I don't want to keep doing this to myself.",
		"I need to get away."
	};

	private static string[] m_goodMessages = {
		"Finally...",
		"That was... new.",
		"I think this is right.",
		"This is what I need.",
		"I... feel better.",
		"That was... a surprising feeling."
	};

	private static string[] m_bestMessages = {
		"Almost there.",
		"There's no looking back now.",
		"Just keep going.",
		"This could be it.",
		"Pain dulls with time.",
		"Keep thinking: new memories."
	};

	public static void Reset () {
		m_earlyQueue.Clear();
		m_midQueue.Clear();
		m_lateQueue.Clear();

		m_goodQueue.Clear();
		m_bestQueue.Clear();

		m_triggeredCount = 0;
		m_missedCount = 0;
	}

	public static void AddMissedTrigger()
	{
		m_missedCount++;
	}

	public static void AddTriggeredMemory()
	{
		m_triggeredCount++;
		m_missedCount = 0;
	}

	public static string GetRandomText() {

		int panelIndex = PanelManager.Instance.CurrentPanelIndex;

		string[] list = null;
		Queue<int> queue = null;

		if ( m_missedCount > 4 ) {
			list = m_bestMessages;
			queue = m_bestQueue;
		} else if ( m_missedCount > 0 ) {
			list = m_goodMessages;
			queue = m_goodQueue;
		} else if ( panelIndex < 10 ) {
			list = m_earlyMessages;
			queue = m_earlyQueue;
		} else if ( panelIndex < 25 ) {
			list = m_midMessages;
			queue = m_midQueue;
		} else {
			list = m_lateMessages;
			queue = m_lateQueue;
		}


		while ( true ) {

			int index = Random.Range( 0, list.Length );
			if ( queue.Contains( index ) ) { continue; }

			queue.Enqueue( index );

			if ( queue.Count >= Mathf.FloorToInt( list.Length * 0.5f ) ) {
				queue.Dequeue();
			}
			return list[ index ];

		}

	}
}
