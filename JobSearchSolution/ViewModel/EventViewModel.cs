using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JobSearchSolution.ViewModel
{
	public class EventViewModel
	{
		public Event Event { get; set; }
		public IEnumerable<SelectListItem> AllContacts { get; set; }
		public IEnumerable<SelectListItem> AllOpps { get; set; }
		public IEnumerable<SelectListItem> AllEventTypes { get; set; }

		private List<int> _selectedContacts;
		private List<int> _selectedOpps;
		private int _selectedEventType;

		public List<int> SelectedContacts
		{
			get
			{
				if (_selectedContacts == null)
				{
					_selectedContacts = Event.Contact.Select(e => e.Id).ToList();
				}
				return _selectedContacts;
			}
			set
			{
				_selectedContacts = value;
			}
		}

		public List<int> SelectedOpps
		{
			get
			{
				if (_selectedOpps == null)
				{
					_selectedOpps = Event. Opp.Select(e => e.Id).ToList();
				}
				return _selectedOpps;
			}
			set
			{
				_selectedOpps = value;
			}
		}

		public int SelectedEventType
		{
			get
			{
				return _selectedEventType;
			}
			set
			{
				_selectedEventType = value;
			}
		}
	}
}