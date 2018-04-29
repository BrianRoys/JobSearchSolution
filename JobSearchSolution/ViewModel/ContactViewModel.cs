using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JobSearchSolution.ViewModel
{
	public class ContactViewModel
	{
		public Contact Contact { get; set; }
		public IEnumerable<SelectListItem> AllEvents { get; set; }
		public IEnumerable<SelectListItem> AllOpps { get; set; }

		private List<int> _selectedEvents;
		private List<int> _selectedOpps;

		public List<int> SelectedEvents
		{
			get
			{
				if (_selectedEvents == null)
				{
					_selectedEvents = Contact.Event.Select(e => e.Id).ToList();
				}
				return _selectedEvents;
			}
			set
			{
				_selectedEvents = value;
			}
		}

		public List<int> SelectedOpps
		{
			get
			{
				if (_selectedOpps == null)
				{
					_selectedOpps = Contact.Opp.Select(e => e.Id).ToList();
				}
				return _selectedOpps;
			}
			set
			{
				_selectedOpps = value;
			}
		}
	}
}