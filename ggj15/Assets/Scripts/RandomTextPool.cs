using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class RandomTextPool {

	private static Queue<int> m_previousIndices = new Queue<int>();
	public static readonly int MAX_QUEUE_SIZE = 12;

	private static string[] m_messages = {
		"She was.",
		"If only it were that easy.",
		"I wish she was still one.",
		"She would've made this day brighter.",
		"We could've picked up the broken pieces together.",
		"Follow your heart.",
		"How can I find her?",
		"I'm trying, Elsa.",
		"But where is she?",
		"No more tears.",
		"It can't be her. It never is.",
		"It's too hard to hold on.",
		"If only... I won't be here anymore.",
		"Running through my mind. Walking in circles.",
		"Enough.",
		"Why do I keep on coming back?",
		"You could do better.",
		"Where am I going?",
		"I could keep my heart in this or..."
	};

	public static void Reset () {
		m_previousIndices.Clear();
	}

	public static string GetRandomText() {

		while ( true ) {
			int index = Random.Range( 0, m_messages.Length - 1 );
			if ( m_previousIndices.Contains( index ) ) { continue; }

			m_previousIndices.Enqueue( index );
			if ( m_previousIndices.Count > MAX_QUEUE_SIZE ) {
				m_previousIndices.Dequeue();
			}

			return m_messages[ index ];

		}

	}
}
