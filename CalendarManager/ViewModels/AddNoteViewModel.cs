using CalendarManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarManager.ViewModels
{
    class AddNoteViewModel : BaseViewModel
    {
        private DateTime selectedDate;
        private Note note;

        public AddNoteViewModel(DateTime date)
        {
            this.SelectedDate = date;
            note = new Note();
            note.Date = SelectedDate;
        }

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        public Note Note
        {
            get { return note; }
            set
            {
                note = value;
                OnPropertyChanged("SelectedDate");
            }
        }
        
    }
}
