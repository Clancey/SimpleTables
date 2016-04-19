using System;
using System.Collections;
using System.Collections.Generic;
using SimpleTables.Cells;

namespace SimpleTables
{
	/// <summary>
	/// Sections contain individual Element instances that are rendered by MonoDroid.Dialog
	/// </summary>
	/// <remarks>
	/// Sections are used to group elements in the screen and they are the
	/// only valid direct child of the RootElement.    Sections can contain
	/// any of the standard elements, including new RootElements.
	/// 
	/// RootElements embedded in a section are used to navigate to a new
	/// deeper level.
	/// 
	/// You can assign a header and a footer either as strings (Header and Footer)
	/// properties, or as Views to be shown (HeaderView and FooterView).   Internally
	/// this uses the same storage, so you can only show one or the other.
	/// </remarks>
    public class Section : IEnumerable<Cell>
	{
		public string Caption;

		public List<Cell> Elements = new List<Cell>();

		List<string> ElementTypes = new List<string>();

		public int StartIndex {get;set;}

		/// <summary>
		///  Constructs a Section without header or footers.
		/// </summary>
        public Section()
            : this("")
		{
		}

		/// <summary>
		///  Constructs a Section with the specified header
		/// </summary>
		/// <param name="caption">
		/// The header to display
		/// </param>
		public Section(string caption)
		{
			Caption = caption;
		}

	

	
		/// <summary>
		///    The section header, as a string
		/// </summary>
		public string Header
		{
			get { return Caption; }
			set { Caption = value; }
		}

		public int Count
		{
			get { return Elements.Count; }
		}

		public Cell this[int idx]
		{
			get { return Elements[idx]; }
		}

		/// <summary>
		/// Adds a new child Element to the Section
		/// </summary>
		/// <param name="element">
		/// An element to add to the section.
		/// </param>
		public void Add(Cell element)
		{
			if (element == null)
				return;

			var elementType = element.GetType().FullName;

			if (!ElementTypes.Contains(elementType))
				ElementTypes.Add(elementType);

			Elements.Add(element);
		}

		/// <summary>
		///    Add version that can be used with LINQ
		/// </summary>
		/// <param name="elements">
		/// An enumerable list that can be produced by something like:
		///    from x in ... select (Element) new MyElement (...)
		/// </param>
		public int Add(IEnumerable<Cell> elements)
		{
			int count = 0;
			foreach (Cell e in elements)
			{
				Add(e);
				count++;
			}
			return count;
		}



		/// <summary>
		/// Inserts a series of elements into the Section using the specified animation
		/// </summary>
		/// <param name="idx">
		/// The index where the elements are inserted
		/// </param>
		/// <param name="anim">
		/// The animation to use
		/// </param>
		/// <param name="newElements">
		/// A series of elements.
		/// </param>
		public void Insert(int idx, params Cell[] newElements)
		{
			if (newElements == null)
				return;

			int pos = idx;
			foreach (Cell e in newElements)
			{
				Elements.Insert(pos++, e);
			}
		
		}

		public int Insert(int idx, IEnumerable<Cell> newElements)
		{
			if (newElements == null)
				return 0;

			int pos = idx;
			int count = 0;
			foreach (Cell e in newElements)
			{
				Elements.Insert(pos++, e);
				count++;
			}

			return count;
		}

	

		public void Remove(Cell e)
		{
			if (e == null)
				return;
			for (int i = Elements.Count; i > 0;)
			{
				i--;
				if (Elements[i] == e)
				{
					RemoveRange(i, 1);
					return;
				}
			}
		}

		public void Remove(int idx)
		{
			RemoveRange(idx, 1);
		}

		/// <summary>
		/// Removes a range of elements from the Section
		/// </summary>
		/// <param name="start">
		/// Starting position
		/// </param>
		/// <param name="count">
		/// Number of elements to remove from the section
		/// </param>
		public void RemoveRange(int start, int count)
		{
			if (start < 0 || start >= Elements.Count)
				return;
			if (count == 0)
				return;

			if (start + count > Elements.Count)
				count = Elements.Count - start;

			Elements.RemoveRange(start, count);

		}

		public void Clear()
		{
			foreach (Cell e in Elements)
				e.Dispose();
			Elements = new List<Cell>();

		}



        /// Enumerator to get all the elements in the Section.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}"/>
        /// </returns>
		public IEnumerator<Cell> GetEnumerator()
        {
            foreach (Cell e in Elements)
                yield return e;
        }

        /// Enumerator to get all the elements in the Section.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}"/>
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
			foreach (Cell e in Elements)
                yield return e;
        }
	}
}