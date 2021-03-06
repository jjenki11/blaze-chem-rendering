
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
/**
 * Namespace: System.Web.UI.WebControls
 * Class:     DataGridColumnCollection
 *
 * Author:  Gaurav Vaish
 * Maintainer: gvaish@iitk.ac.in
 * Contact: <my_scripts2001@yahoo.com>, <gvaish@iitk.ac.in>
 * Implementation: yes
 * Status:  ??%
 *
 * (C) Gaurav Vaish (2002)
 */

using System;
using System.ComponentModel;
using System.Collections;
using System.Web;
using System.Web.UI;

namespace System.Web.UI.WebControls
{
	public sealed class DataGridColumnCollection : ICollection, IEnumerable, IStateManager
	{
		private DataGrid  owner;
		private ArrayList columns;
		private bool      trackViewState;

		public DataGridColumnCollection(DataGrid owner, ArrayList columns)
		{
			this.owner   = owner;
			this.columns = columns;
		}

		[Browsable (false)]
		public int Count
		{
			get
			{
				return columns.Count;
			}
		}

		[Browsable (false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		[Browsable (false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		[Browsable (false)]
		public DataGridColumn this[int index]
		{
			get
			{
				return (DataGridColumn)(columns[index]);
			}
		}

		[Browsable (false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		public void Add(DataGridColumn column)
		{
			AddAt(-1, column);
		}

		public void AddAt(int index, DataGridColumn column)
		{
			if(index == -1)
			{
				columns.Add(column);
			} else
			{
				columns.Insert(index, column);
			}

			column.SetOwner (owner);
			if(trackViewState)
			{
				((IStateManager)column).TrackViewState();
			}
			OnColumnsChanged();
		}

		internal void OnColumnsChanged()
		{
			if(owner != null)
			{
				owner.OnColumnsChanged();
			}
		}

		public void Clear()
		{
			columns.Clear();
			OnColumnsChanged();
		}

		public void CopyTo(Array array, int index)
		{
			foreach(DataGridColumn current in this)
			{
				array.SetValue(current, index++);
			}
		}

		public IEnumerator GetEnumerator()
		{
			return columns.GetEnumerator();
		}

		public int IndexOf(DataGridColumn column)
		{
			if(column != null)
			{
				return columns.IndexOf(column);
			}
			return -1;
		}

		public void Remove(DataGridColumn column)
		{
			if(column != null)
			{
				RemoveAt(IndexOf(column));
			}
		}

		public void RemoveAt(int index)
		{
			if(index >= 0 && index < columns.Count)
			{
				columns.RemoveAt(index);
				OnColumnsChanged();
				return;
			}
			//This exception is not documented, but thrown
			throw new ArgumentOutOfRangeException("string");
		}

		object IStateManager.SaveViewState()
		{
			if (columns.Count == 0)
				return null;

			ArrayList retVal = new ArrayList (columns.Count);
			bool found = false;
			foreach (IStateManager current in columns) {
				object o = current.SaveViewState ();
				retVal.Add (o);
				if (o != null)
					found = true;
			}

			if (!found)
				return null;
			
			return retVal;
		}

		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState == null || !(savedState is ArrayList))
				return;

			ArrayList list = (ArrayList) savedState;
			int end = list.Count;
			if (end != columns.Count)
				return;

			for (int i = 0; i < end; i++) {
				IStateManager col = (IStateManager) columns [i];
				col.LoadViewState (list [i]);
			}
		}

		void IStateManager.TrackViewState()
		{
			trackViewState = true;
			foreach (IStateManager col in columns)
				col.TrackViewState ();
		}

		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return trackViewState;
			}
		}
	}
}
