using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JobSearchSolution.ViewModel
{
	public class OppViewModel
	{
		public Opp Opp { get; set; }
		public IEnumerable<SelectListItem> AllEvents { get; set; }
		public IEnumerable<SelectListItem> AllContacts { get; set; }
		public IEnumerable<SelectListItem> AllStatuses { get; set; }

		private List<int> _selectedEvents;
		private List<int> _selectedContacts;
		private int _selectedStatus;

		public List<int> SelectedEvents
		{
			get
			{
				if (_selectedEvents == null)
				{
					_selectedEvents = Opp.Event.Select(e => e.Id).ToList();
				}
				return _selectedEvents;
			}
			set
			{
				_selectedEvents = value;
			}
		}

		public List<int> SelectedContacts
		{
			get
			{
				if (_selectedContacts == null)
				{
					_selectedContacts = Opp.Contact.Select(e => e.Id).ToList();
				}
				return _selectedContacts;
			}
			set
			{
				_selectedContacts = value;
			}
		}

		public int SelectedStatus
		{
			get
			{
				return _selectedStatus;
			}
			set
			{
				_selectedStatus = value;
			}
		}
	}
}
