using CalendarManager.Core;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CalendarManager.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        string filepath = "notes.json";
        private DateTime selectedDate = DateTime.Today;
        private Note selectedNote;
        public ObservableCollection<Note> notes = new ObservableCollection<Note>();

        public ObservableCollection<Note> Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                OnPropertyChanged("Notes");
            }
        }

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                Notes = GetNotes(selectedDate);
                OnPropertyChanged("SelectedDate");
            }
        }

        public ObservableCollection<Note> GetNotes(DateTime date)
        {
            List<Note> items = readJson();
            ObservableCollection<Note> notes = new ObservableCollection<Note>();
            foreach (Note note in items)
            {
                if(note.Date == date)
                {
                    notes.Add(note);
                }
            }
            return notes;
        }


        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Note note = new Note { Id = 5, Date = selectedDate, Title = "В универ", Description = "Сдать заявление, подойти к Пономарьову." };
                      writeJson(note);
                      notes.Add(note);
                  }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {

                    }
                    )
            }
        }

        public List<Note> readJson()
        {
            List<Note> items = new List<Note>();
            using (StreamReader r = new StreamReader(filepath))
            {
                string jsonRead = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Note>>(jsonRead);
            }
            return items;
        }

        public void writeJson(Note note)
        {
            List<Note> notes = readJson();
            notes.Add(note);
            string jsonWrite = JsonConvert.SerializeObject(notes);
            File.WriteAllText(filepath, jsonWrite);
        }

    }
}
