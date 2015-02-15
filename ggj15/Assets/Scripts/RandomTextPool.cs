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
		"She was...\nAll I ever hoped for.",
		"I thought\nit would be easy.",
		"I wish she was still the one.",
		"She'd have made\nthis day brighter.",
		"We could've picked up\nthe pieces together.",
		"I can't help this.\nReturning just to spite myself.",
		"It's hard to breathe...",
		"It... can't be her.\nIt never could be.",
		"I cherished everything.",
		"I wonder what she's doing...",
		"How do I find her?",
		"Where is she?",
		"I can't stop thinking of her.",
		"How do people last through this?"
	};

	private static string[] m_midMessages = {
		"Why do I keep\ncoming back?",
		"Enough, please. Enough...",
		"Running through my mind.\nWalking in circles.",
		"Where am I going...?\nWhat do I do now?",
		"Should I keep my heart in this...?",
		"What did we do wrong?\nWas it me?",
		"There must be an end to this.",
		"I want things to be different...",
		"It must be me.",
		"It's too hard to hold on.",
		"Are these dreams or memories?\nNightmares?",
		"You lose what you cling to.",
		"I want to get out of here.",
		"I thought holding on makes us strong\nbut...",
		"I wonder what she's doing,\nbut I don't want to know."
	};

	private static string[] m_lateMessages = {
		"But Elsa kept telling me I should...",
		"Should I stay\nor should I go?",
		"I'm just getting worse.\nHow do I get out of this?",
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
		"Something... Something feels different.",
		"That was... new.",
		"I think this is right.",
		"Maybe... this is what I need.",
		"I... feel better.",
		"That was... a surprising feeling."
	};

	private static string[] m_bestMessages = {
		"One day at a time, I think.",
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
			//Debug.Log( "BEST" );
			list = m_bestMessages;
			queue = m_bestQueue;
		} else if ( m_missedCount > 0 ) {
			//Debug.Log( "GOOD" );
			list = m_goodMessages;
			queue = m_goodQueue;
		} else if ( panelIndex < 10 ) {
			//Debug.Log( "EARLY" );
			list = m_earlyMessages;
			queue = m_earlyQueue;
		} else if ( panelIndex < 25 ) {
			//Debug.Log( "MID" );
			list = m_midMessages;
			queue = m_midQueue;
		} else {
			//Debug.Log( "LATE" );
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
