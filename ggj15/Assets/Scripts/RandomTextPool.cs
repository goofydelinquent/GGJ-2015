using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class RandomTextPool {

	private static Queue<int> m_earlyQueue = new Queue<int>();
	private static Queue<int> m_midQueue = new Queue<int>();
	private static Queue<int> m_lateQueue = new Queue<int>();
	private static Queue<int> m_goodQueue = new Queue<int>();
	private static Queue<int> m_bestQueue = new Queue<int>();
		
	private static string[] m_earlyMessages = {
		"She was.",
		"I thought\nit would be easy.",
		"I wish she was still one.",
		"She would've made\nthis day brighter.",
		"We could've picked up\nthe broken pieces together.",
		"Follow your heart.",
		"No more tears.",
		"It can't be her.\nIt never is.",
		"I used to cherish\neverything.",
		"How can I find her?",
		"But where is she?"
	};

	private static string[] m_midMessages = {
		"Why do I keep on\ncoming back?",
		"Enough.",
		"If only...\nI won't be here anymore.",
		"Running through my mind.\nWalking in circles.",
		"Where am I going?",
		"I could keep my heart\nin this or...",
		"I know I could do better.",
		"There must be an end to this.",
		"Maybe this time\nit would be different.",
		"It must be me.",
		"It's too hard to hold on.",
		"Are these memories\nor nightmares?",
		"I need to at least try.",
		"You can only lose\nwhat you cling to.",
		"I can accept change.",
		"Some think holding on\nmakes us strong."
	};

	private static string[] m_lateMessages = {
		"Elsa said...",
		"Should I stay\nor should I go?",
		"It really must've\nbeen me.",
		"Do I really need\nto recall?",
		"I don't need\nto keep hurting myself.",
		"Just imagine\ntime running out.",
		"Too much thinking.",
		"Why do I even bother?",
		"Sometimes the hardest part\nis learning to start over."
	};

	private static string[] m_goodMessages = {
		"Finally...",
		"That was new.",
		"I think this is right.",
		"This is what I need.",
		"This could be the end of everything."
	};

	private static string[] m_bestMessages = {
		"Almost there.",
		"There's no looking back now."
	};

	public static void Reset () {
		m_earlyQueue.Clear();
		m_midQueue.Clear();
		m_lateQueue.Clear();

		m_goodQueue.Clear();
		m_bestQueue.Clear();
	}

	public static string GetRandomText() {

		int panelIndex = PanelManager.Instance.CurrentPanelIndex;

		string[] list = null;
		Queue<int> queue = null;

		if ( panelIndex < 10 ) {
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
