using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolController<T> : MonoBehaviour
{
	static protected List<PoolController<T>> m_list;

	static protected Comp Spawn<Comp>( string p_path, string p_parentName ) where Comp : Component
	{
		GameObject go = Instantiate( Resources.Load( p_path ) ) as GameObject;
		
		GameObject parent = GameObject.Find( p_parentName );
		if( parent == null ){
			parent = new GameObject();
			parent.name = p_parentName;

			DontDestroyOnLoad( parent );
		}
		go.transform.parent = parent.transform;
		
		return go.GetComponent<Comp>();
	}

	static public void UnspawnAll()
	{
		if( m_list != null ) {
			foreach( PoolController<T> poolController in m_list ) {
				Destroy( poolController.gameObject );
			}
		}
	}

	static public void DeactivateAll()
	{
		if( m_list != null ) {
			foreach( PoolController<T> poolController in m_list ) {
				//poolController.Unpause();
				//poolController.Deactivate();
			}
		}
	}

	protected virtual void Awake()
	{
		if( m_list == null ) {
			m_list = new List<PoolController<T>>();
		}

		m_list.Add( this );
	}
	
	protected virtual void OnDestroy()
	{
		m_list.Remove( this );

		if( m_list.Count == 0 ) {
			m_list = null;
		}
	}
}
